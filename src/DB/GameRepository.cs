using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLogic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DB;

public class GameRepository : BaseGameRepository
{
    private PostgresDbContext _dbContext { get; }

    public GameRepository(string connectionString)
    {
        DbContextOptionsBuilder<PostgresDbContext> options_builder = new DbContextOptionsBuilder<PostgresDbContext>();
        options_builder.UseNpgsql(connectionString);
        _dbContext = new PostgresDbContext(options_builder.Options);
    }

    public Game Load()
    {
        List<Player> players = new List<Player>();
        int activePlayer = -1;
        foreach (DbPlayer dbPlayer in _dbContext.Players.OrderBy(p => p.Order))
        {
            players.Add(dbPlayer.ToPlayerObject());
            if (dbPlayer.IsActive)
            {
                if (activePlayer == -1) { activePlayer = dbPlayer.Order - 1; }
                else { throw new DbException("Несколько игроков помечены как активные в базе данных"); }
            }
        }
        BaseDeck deck = new Deck();
        foreach (DbCard dbCard in _dbContext.Deck.OrderBy(c => c.Id))
        {
            deck.Add(dbCard.ToCardObject());
        }
        BaseDeck discard1 = new Discard();
        foreach (DbCard dbCard in _dbContext.Discard1.OrderBy(c => c.Id))
        {
            discard1.Add(dbCard.ToCardObject());
        }
        BaseDeck discard2 = new Discard();
        foreach (DbCard dbCard in _dbContext.Discard2.OrderBy(c => c.Id))
        {
            discard2.Add(dbCard.ToCardObject());
        }
        foreach (DbPlayersCard dbPlayersCard in _dbContext.PlayersCards)
        {
            if (dbPlayersCard.IsPlayed)
                players[dbPlayersCard.DbPlayerId - 1].Cards_played.Add(dbPlayersCard.ToCardObject());
            else
                players[dbPlayersCard.DbPlayerId - 1].Cards_hand.Add(dbPlayersCard.ToCardObject());
        }
        return new Game(deck, discard1, discard2, players, activePlayer);
    }
    public void Save(Game game)
    {
        _dbContext.Deck.RemoveRange(_dbContext.Deck);
        _dbContext.Discard1.RemoveRange(_dbContext.Discard1);
        _dbContext.Discard2.RemoveRange(_dbContext.Discard2);
        _dbContext.PlayersCards.RemoveRange(_dbContext.PlayersCards);
        _dbContext.Players.RemoveRange(_dbContext.Players);
        _dbContext.SaveChanges();

        for (int i = 0; i < game.Players.Count; i++)
        {
            _dbContext.Players.Add(new DbPlayer(i + 1, game.Players[i].Name, false));
            foreach (var card in game.Players[i].Cards_hand)
                _dbContext.PlayersCards.Add(new DbPlayersCard(card.Type.ToString(), card.Color.ToString(), i + 1, false));
            foreach (var card in game.Players[i].Cards_played)
                _dbContext.PlayersCards.Add(new DbPlayersCard(card.Type.ToString(), card.Color.ToString(), i + 1, true));
        }
        _dbContext.SaveChanges();

        _dbContext.Players.Where(p => p.Order - 1 == game.ActivePlayer).First().IsActive = true;

        List<BaseCard> cards = game.Deck.ShowUpperN(game.Deck.Count);
        cards.Reverse();
        foreach (var card in cards)
            _dbContext.Deck.Add(new DbDeckCard(card.Type.ToString(), card.Color.ToString()));
        foreach (var card in game.Discard1.Show())
            _dbContext.Discard1.Add(new DbDiscard1Card(card.Type.ToString(), card.Color.ToString()));
        foreach (var card in game.Discard2.Show())
            _dbContext.Discard2.Add(new DbDiscard2Card(card.Type.ToString(), card.Color.ToString()));
        _dbContext.SaveChanges();
    }
}

