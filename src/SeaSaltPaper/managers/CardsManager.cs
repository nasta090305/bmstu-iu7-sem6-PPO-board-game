using System.Reflection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
namespace GameLogic;

public class CardsManager
{
    ILogger logger;
    public CardsManager(Game Game, ILogger logger)
    {
        this.Game = Game;
        this.logger = logger;
    }
    public Game Game { get;  set; }
    public BaseCard RemoveFromDeck(DeckType deck_id)
    {
        logger.LogDebug("Попытка взять верхнюю карту из {deck}", deck_id);
        BaseCard card = deck_id switch
        {
            DeckType.Deck => Game.Deck.RemoveTop(),
            DeckType.Discard1 => Game.Discard1.RemoveTop(),
            DeckType.Discard2 => Game.Discard2.RemoveTop(),
            _ => throw new LogicException("Некорректная колода.")
        };
        logger.LogInformation("Карта {card_type}, {card_color} успешно взята из {deck}", card.Type, card.Color, deck_id);
        return card;
    }

    public BaseCard RemoveFromDeck(DeckType deck_id, int card_id)
    {
        logger.LogDebug("Попытка взять карту {cardId} из {deck}", card_id, deck_id);
        BaseCard card = deck_id switch
        {
            DeckType.Deck => Game.Deck.Remove(card_id),
            DeckType.Discard1 => Game.Discard1.Remove(card_id),
            DeckType.Discard2 => Game.Discard2.Remove(card_id),
            _ => throw new LogicException("Некорректная колода.")
        };
        logger.LogInformation("Карта {card_type}, {card_color} успешно взята из {deck}", card.Type, card.Color, deck_id);
        return card;
    }

    public BaseCard RemoveCardFromPlayer(int player_id, int card_number)
    {
        logger.LogDebug("Попытка вытащить карту {cardNumber} с руки игрока {playerId}", card_number, Game.Players[player_id].Name);
        if (player_id + 1 > Game.Players.Count || player_id < 0)
        { throw new LogicException("Некорректный номер игрока."); }
        if (card_number + 1 > Game.Players[player_id].Cards_hand.Count || card_number < 0)
        { throw new LogicException("Некорректный номер карты."); }
        BaseCard result = Game.Players[player_id].Cards_hand[card_number];
        Game.Players[player_id].Cards_hand.Remove(result);
        logger.LogInformation("Карта {card_type}, {card_color} взята из руки игрока {playerId}", result.Type, result.Color, Game.Players[player_id].Name);
        return result;
    }

    public BaseCard RemoveRandomCardFromPlayer(int player_id)
    {
        logger.LogDebug("Попытка вытащить случайную карту с руки игрока {playerId}", Game.Players[player_id].Name);
        if (player_id + 1 > Game.Players.Count || player_id < 0)
        { throw new LogicException("Некорректный номер игрока."); }
        Random random = new Random();
        int card_id = random.Next(0, Game.Players[player_id].Cards_hand.Count);
        BaseCard result = Game.Players[player_id].Cards_hand[card_id];
        Game.Players[player_id].Cards_hand.Remove(result);
        logger.LogInformation("Карта {card_type}, {card_color} взята из руки игрока {playerId}", result.Type, result.Color, Game.Players[player_id].Name);
        return result;
    }

    public void AddToDeck(DeckType deck_id, BaseCard card)
    {
        logger.LogDebug("Добавление карты {card_type}, {card_color} в {deck}", card.Type, card.Color, deck_id);
        switch (deck_id)
        {
            case DeckType.Deck:
                Game.Deck.Add(card);
                break;
            case DeckType.Discard1:
                Game.Discard1.Add(card);
                break;
            case DeckType.Discard2:
                Game.Discard2.Add(card);
                break;
            default:
                throw new LogicException("Некорректная колода.");
        }
        logger.LogInformation("Карта {card_type}, {card_color} успешно добавлена в {deck}", card.Type, card.Color, deck_id);
    }

    public void AddToPlayer(int player_id, BaseCard card)
    {
        logger.LogDebug("Добавление карты {card_type}, {card_color} в руку игрока {player}", card.Type, card.Color, Game.Players[player_id].Name);
        if (player_id + 1 > Game.Players.Count || player_id < 0)
        { throw new LogicException("Некорректный номер игрока."); }
        Game.Players[player_id].Cards_hand.Add(card);
        logger.LogInformation("Карта {card_type}, {card_color} успешно добавлена в руку игрока {player}", card.Type, card.Color, Game.Players[player_id].Name);
    }

    public void PlayCard(int card_id)
    {
        logger.LogDebug("Игрок {Name} пытается переместить карту {cardId} из руки в сыгранные им карты на столе", Game.Players[Game.ActivePlayer].Name, card_id);
        if (card_id + 1 > Game.Players[Game.ActivePlayer].Cards_hand.Count || card_id < 0)
        { throw new LogicException("Некорректный номер карты."); }
        BaseCard played_card = Game.Players[Game.ActivePlayer].Cards_hand[card_id];
        Game.Players[Game.ActivePlayer].Cards_hand.Remove(played_card);
        Game.Players[Game.ActivePlayer].Cards_played.Add(played_card);
        logger.LogInformation("Карта {card_type}, {card_color} успешно перемещена из руки игрока {player} в сыгранные им карты на столе", played_card.Type, played_card.Color, Game.Players[Game.ActivePlayer].Name);
    }

    //public List<CardDTO> ShowUpperN(DeckType deck_id, int n)
    //{
    //    if (Game == null) { throw new LogicException("Игра не начата."); }
    //    switch (deck_id)
    //    {
    //        case DeckType.Deck:
    //            return Game.Deck.ShowUpperN(n);
    //        case DeckType.Discard1:
    //            return Game.Discard1.ShowUpperN(n);
    //        case DeckType.Discard2:
    //            return Game.Discard2.ShowUpperN(n);
    //        default:
    //            throw new LogicException("Некорректная колода.");
    //    }
    //}

    //public List<CardDTO> ShowDeck(DeckType deck_id)
    //{
    //    if (Game == null) { throw new LogicException("Игра не начата."); }
    //    switch (deck_id)
    //    {
    //        case DeckType.Deck:
    //            return Game.Deck.Show();
    //        case DeckType.Discard1:
    //            return Game.Discard1.Show();
    //        case DeckType.Discard2:
    //            return Game.Discard2.Show();
    //        default:
    //            throw new LogicException("Некорректная колода.");
    //    }
    //}
}