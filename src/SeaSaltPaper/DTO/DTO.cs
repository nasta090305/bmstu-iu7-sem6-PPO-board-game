using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic;

public enum EndgameReasons
{
    Regular,
    Mermaids,
    EmptyDeck
}

public class GameDTO
{
    public List<PlayerDTO> Players { get; }
    public List<CardDTO> ActiveHand { get; }
    public int ActivePlayer { get; }
    public List<int> DeckCounts { get; }
    public CardDTO TopDiscard1 { get; }
    public CardDTO TopDiscard2 { get; }

    public GameDTO(List<PlayerDTO> players, List<CardDTO> activeHand, int activePlayer, List<int> deckCounts, CardDTO topDiscard1, CardDTO topDiscard2)
    {
        Players = players;
        ActiveHand = activeHand;
        ActivePlayer = activePlayer;
        DeckCounts = deckCounts;
        TopDiscard1 = topDiscard1;
        TopDiscard2 = topDiscard2;
    }

    public GameDTO(Game game)
    {
        Players = new List<PlayerDTO>();
        foreach (var player in game.Players)
            Players.Add(new PlayerDTO(player));
        ActiveHand = new List<CardDTO>();
        foreach (BaseCard card in game.Players[game.ActivePlayer].Cards_hand)
            ActiveHand.Add(new CardDTO(card));
        ActivePlayer = game.ActivePlayer;
        DeckCounts = new List<int>();
        DeckCounts.Add(game.Deck.Count);
        DeckCounts.Add(game.Discard1.Count);
        DeckCounts.Add(game.Discard2.Count);
        try
        {
            TopDiscard1 = new CardDTO(game.Discard1.ShowUpperN(1)[0]);
        }
        catch (LogicException ex)
        {
            TopDiscard1 = new CardDTO(CardType.None, CardColor.None);
        }
        try
        {
            TopDiscard2 = new CardDTO(game.Discard2.ShowUpperN(1)[0]);
        }
        catch (LogicException ex)
        {
            TopDiscard2 = new CardDTO(CardType.None, CardColor.None);
        }
    }
}


public class CardDTO
{
    public CardType Type { get; }
    public CardColor Color { get; }
    public CardDTO(CardType type, CardColor color)
    {
        Type = type;
        Color = color;
    }
    public CardDTO(BaseCard card)
    {
        Type = card.Type;
        Color = card.Color;
    }
    public override bool Equals(object obj)
    {
        if (obj is CardDTO other)
            return Type == other.Type && Color == other.Color;
        return false;
    }
}

public class PlayerDTO
{
    public string Name { get; }
    public List<CardDTO> CardsPlayed { get; }
    public int HandCount { get; }
    public PlayerDTO(string name, List<CardDTO> cards_played, int hand_count)
    {
        Name = name;
        CardsPlayed = cards_played;
        HandCount = hand_count;
    }
    public PlayerDTO(Player player) : this(player.Name, new List<CardDTO>(), player.Cards_hand.Count)
    {
        foreach (BaseCard card in player.Cards_played)
            CardsPlayed.Add(new CardDTO(card));
    }
    public override bool Equals(object? obj)
    {
        if (obj is PlayerDTO other)
            return this.Name == other.Name &&
                this.HandCount == other.HandCount &&
                this.CardsPlayed.OrderBy(card => card.Type).ThenBy(card => card.Color).SequenceEqual(other.CardsPlayed.OrderBy(card => card.Type).ThenBy(card => card.Color));
        return false;
    }
}

public class GameResultDTO
{
    public EndgameReasons EndgameReason { get; set; }
    public List<PlayerDTO> Players { get; }
    public List<int> Winners { get; }
    public List<int> Score { get; }
    public GameResultDTO()
    {
        Players = new List<PlayerDTO>();
        Winners = new List<int>();
        Score = new List<int>();
    }
    public GameResultDTO(EndgameReasons endgameReason, List<PlayerDTO> players, List<int> winners, List<int> score)
    {
        this.EndgameReason = endgameReason;
        this.Players = players;
        this.Winners = winners;
        this.Score = score;
    }
    public override bool Equals(object obj)
    {
        if (obj is GameResultDTO other)
        {
            return this.EndgameReason == other.EndgameReason &&
                this.Players.SequenceEqual(other.Players) &&
                this.Winners.SequenceEqual(other.Winners) &&
                this.Score.SequenceEqual(other.Score);
        }
        return false;
    }
}