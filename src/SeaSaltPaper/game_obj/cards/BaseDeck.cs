namespace GameLogic;

public enum DeckType
{
    Deck,
    Discard1,
    Discard2
}

public abstract class BaseDeck
{
    protected List<BaseCard> cards;
    public int Count { get { return cards.Count(); } }

    public BaseCard RemoveTop()
    {
        Console.WriteLine("Debug: {0}", cards.Count());
        if (cards.Count() == 0) { throw new LogicException("Колода пуста"); }
        BaseCard result = cards[cards.Count - 1];
        cards.RemoveAt(cards.Count - 1);
        return result;
    }
    public BaseCard Remove(int id)
    {
        if (cards.Count() == 0) { throw new LogicException("Колода пуста"); }
        BaseCard result = cards[id];
        cards.RemoveAt(id);
        return result;
    }
    public void Add(BaseCard card) => cards.Add(card);
    public abstract List<BaseCard> Show();
    public List<BaseCard> ShowUpperN(int n)
    {
        List<BaseCard> result = new List<BaseCard>();
        if (cards.Count < n)
            throw new LogicException("Недостаточно карт.");
        for (int i = cards.Count - 1; i >= cards.Count - n; i--)
            result.Add(cards[i]);
        return result;
    }
    public abstract void Shuffle();
    public abstract bool Equals(object obj);
}
