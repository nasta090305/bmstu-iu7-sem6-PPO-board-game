namespace GameLogic;

public class Player
{
    public Player(string name, List<BaseCard> cards_hand, List<BaseCard> cards_played)
    {
        Name = name;
        Cards_hand = cards_hand;
        Cards_played = cards_played;
    }
    public Player(string name) : this(name, new List<BaseCard>(), new List<BaseCard>()) { }
    public string Name { get; set; }
    public List<BaseCard> Cards_hand { get; }
    public List<BaseCard> Cards_played { get; }

    public override bool Equals(object? obj)
    {
        if (obj is Player other)
            return this.Name == other.Name &&
                this.Cards_hand.OrderBy(card => card.Type).ThenBy(card => card.Color).SequenceEqual(other.Cards_hand.OrderBy(card => card.Type).ThenBy(card => card.Color)) &&
                this.Cards_played.OrderBy(card => card.Type).ThenBy(card => card.Color).SequenceEqual(other.Cards_played.OrderBy(card => card.Type).ThenBy(card => card.Color));
        return false;
    }

}
