using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace GameLogic;

public enum CardType
{
    None,
    Fish,
    Crab,
    Boat,
    Shark,
    Swimmer,
    Mermaid,
    Shell,
    Octopus,
    Sailor,
    Penguin,
    FishMult,
    BoatMult,
    SailorMult,
    PenguinMult
}

public enum CardColor
{
    None,
    Blue,
    Teal,
    Black,
    Yellow,
    Green,
    White,
    Purple,
    Gray,
    Peach,
    Pink,
    Orange
}

public class MultiplierCard: BaseCard
{
    int coef;
    public CardType Type { get; private set; }
    public CardColor Color { get; private set; }

    public MultiplierCard(CardType type, CardColor color, int coef)
    {
        this.Type = type;
        this.Color = color;
        this.coef = coef;
    }

    public bool IsPlayable(BaseCard card) => false;

    public void Play(CardsManager manager, params int[] args) => throw new LogicException("Карту невозможно разыграть.");

    public int GetScore(params int[] args) => coef * args[0];

    public override bool Equals(object obj)
    {
        if (obj is BaseCard other)
            return Type == other.Type && Color == other.Color;
        return false;
    }
}

public class CollectorCard : BaseCard
{
    List<int> coefs;
    public CardType Type { get; private set; }
    public CardColor Color { get; private set; }
    public CollectorCard(CardType type, CardColor color, List<int> coefs)
    {
        this.Type = type;
        this.Color = color;
        this.coefs = coefs;
    }

    public bool IsPlayable(BaseCard card) => false;

    public void Play(CardsManager manager, params int[] args) => throw new LogicException("Карту невозможно разыграть.");
    
    public int GetScore(params int[] args) => coefs[args[0] - 1];

    public override bool Equals(object obj)
    {
        if (obj is BaseCard other)
            return Type == other.Type && Color == other.Color;
        return false;
    }
}

public class MermaidCard : BaseCard
{
    public CardType Type { get; private set; }
    public CardColor Color { get; private set; }

    public MermaidCard(CardType type = CardType.Mermaid)
    {
        Type = type;
        Color = CardColor.None;
    }

    public bool IsPlayable(BaseCard card) => false;

    public void Play(CardsManager manager, params int[] args) => throw new LogicException("Карту невозможно разыграть.");

    public int GetScore(params int[] args) => 0;

    public override bool Equals(object obj)
    {
        if (obj is BaseCard other)
            return Type == other.Type && Color == other.Color;
        return false;
    }
}

public class FishCard : BaseCard
{
    public CardType Type { get; private set; }
    public CardColor Color { get; private set; }

    public FishCard(CardColor color)
    {
        this.Type = CardType.Fish;
        this.Color = color;
    }

    public bool IsPlayable(BaseCard card) => card.Type == this.Type;

    public void Play(CardsManager manager, params int[] args)
    {
        BaseCard card = manager.RemoveFromDeck(0);
        manager.AddToPlayer(manager.Game.ActivePlayer, card);
    }

    public int GetScore(params int[] args) => 0;

    public override bool Equals(object obj)
    {
        if (obj is BaseCard other)
            return Type == other.Type && Color == other.Color;
        return false;
    }
}

public class CrabCard : BaseCard
{
    public CardType Type { get; private set; }
    public CardColor Color { get; private set; }

    public CrabCard(CardColor color)
    {
        this.Type = CardType.Crab;
        this.Color = color;
    }

    public bool IsPlayable(BaseCard card) => card.Type == this.Type;

    public void Play(CardsManager manager, params int[] args)
    {
        BaseCard card = manager.RemoveFromDeck((DeckType) args[0], args[1]);
        manager.AddToPlayer(manager.Game.ActivePlayer, card);
    }

    public int GetScore(params int[] args) => 0;

    public override bool Equals(object obj)
    {
        if (obj is BaseCard other)
            return Type == other.Type && Color == other.Color;
        return false;
    }
}

public class BoatCard : BaseCard
{
    public CardType Type { get; private set; }
    public CardColor Color { get; private set; }

    public BoatCard(CardColor color)
    {
        this.Type = CardType.Boat;
        this.Color = color;
    }

    public bool IsPlayable(BaseCard card) => card.Type == this.Type;

    public void Play(CardsManager manager, params int[] args)
    {
        BaseCard card;
        DeckType deck_id = (DeckType) args[0];
        switch (deck_id)
        {
            case DeckType.Deck:
                int card_id = args[1];
                DeckType discard = (DeckType)args[2];
                if (card_id == 0)
                {
                    card = manager.RemoveFromDeck(deck_id);
                    manager.AddToPlayer(manager.Game.ActivePlayer, card);
                    card = manager.RemoveFromDeck(deck_id);
                    manager.AddToDeck(discard, card);
                }
                else if (card_id == 1)
                {
                    card = manager.RemoveFromDeck(deck_id);
                    manager.AddToDeck(discard, card);
                    card = manager.RemoveFromDeck(deck_id);
                    manager.AddToPlayer(manager.Game.ActivePlayer, card);
                }
                else
                    throw new LogicException("Во время хода нельзя взять карту, которая не является одной из двух верхних.");
                break;
            case DeckType.Discard1:
                card = manager.RemoveFromDeck(deck_id);
                manager.AddToPlayer(manager.Game.ActivePlayer, card);
                break;
            case DeckType.Discard2:
                card = manager.RemoveFromDeck(deck_id);
                manager.AddToPlayer(manager.Game.ActivePlayer, card);
                break;
            default:
                throw new LogicException("Некорректная колода.");
        }
    }

    public int GetScore(params int[] args) => 0;

    public override bool Equals(object obj)
    {
        if (obj is BaseCard other)
            return Type == other.Type && Color == other.Color;
        return false;
    }
}

public class SharkCard : BaseCard
{
    public CardType Type { get; private set; }
    public CardColor Color { get; private set; }

    public SharkCard(CardColor color)
    {
        this.Type = CardType.Shark;
        this.Color = color;
    }
    public bool IsPlayable(BaseCard card) => card.Type == CardType.Swimmer;

    public void Play(CardsManager manager, params int[] args)
    {
        if (args[0] == manager.Game.ActivePlayer)
            throw new LogicException("Нельзя забрать карту у себя");
        BaseCard card = manager.RemoveRandomCardFromPlayer(args[0]);
        manager.AddToPlayer(manager.Game.ActivePlayer, card);
    }

    public int GetScore(params int[] args) => 0;

    public override bool Equals(object obj)
    {
        if (obj is BaseCard other)
            return Type == other.Type && Color == other.Color;
        return false;
    }
}

public class SwimmerCard : BaseCard
{
    public CardType Type { get; private set; }
    public CardColor Color { get; private set; }

    public SwimmerCard(CardColor color)
    {
        this.Type = CardType.Swimmer;
        this.Color = color;
    }

    public bool IsPlayable(BaseCard card) => card.Type == CardType.Shark;

    public void Play(CardsManager manager, params int[] args)
    {
        if (args[0] == manager.Game.ActivePlayer)
            throw new LogicException("Нельзя забрать карту у себя");
        BaseCard card = manager.RemoveRandomCardFromPlayer(args[0]);
        manager.AddToPlayer(manager.Game.ActivePlayer, card);
    }

    public int GetScore(params int[] args) => 0;

    public override bool Equals(object obj)
    {
        if (obj is BaseCard other)
            return Type == other.Type && Color == other.Color;
        return false;
    }
}
