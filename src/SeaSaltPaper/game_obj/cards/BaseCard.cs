namespace GameLogic;

public interface BaseCard
{
    public CardType Type { get; }
    public CardColor Color { get; }
    public int GetScore(params int[] args);
    public bool IsPlayable(BaseCard card);
    public void Play(CardsManager cards_manager, params int[] args);
    public bool Equals(object obj);
}

