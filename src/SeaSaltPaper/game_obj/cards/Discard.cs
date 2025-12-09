namespace GameLogic;

public class Discard: BaseDeck
{
    public Discard()
    {
        cards = new List<BaseCard>();
    }

    public override List<BaseCard> Show() => cards;

    public override void Shuffle()
    { }

    public override bool Equals(object obj)
    {
        if (obj is Discard other)
        {
            return this.cards.SequenceEqual(other.cards);
        }
        return false;
    }
}
