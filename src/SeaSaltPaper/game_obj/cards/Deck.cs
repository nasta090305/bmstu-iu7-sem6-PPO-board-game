using System.Linq;

namespace GameLogic;

public class Deck: BaseDeck
{
    private static Random rng = new();

    public override List<BaseCard> Show() => new List<BaseCard>();

    public override void Shuffle()
    {
        int n = cards.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            (cards[i], cards[j]) = (cards[j], cards[i]); 
        }
    }

    public Deck()
    { cards = new List<BaseCard>(); }

    public void Fill()
    {
        cards = new List<BaseCard>();
        cards.Add(new FishCard(CardColor.Blue));
        cards.Add(new FishCard(CardColor.Yellow));
        cards.Add(new FishCard(CardColor.Black));
        cards.Add(new FishCard(CardColor.Black));
        cards.Add(new FishCard(CardColor.Blue));
        cards.Add(new FishCard(CardColor.Green));
        cards.Add(new FishCard(CardColor.Teal));

        cards.Add(new BoatCard(CardColor.Teal));
        cards.Add(new BoatCard(CardColor.Blue));
        cards.Add(new BoatCard(CardColor.Blue));
        cards.Add(new BoatCard(CardColor.Teal));
        cards.Add(new BoatCard(CardColor.Yellow));
        cards.Add(new BoatCard(CardColor.Black));
        cards.Add(new BoatCard(CardColor.Yellow));
        cards.Add(new BoatCard(CardColor.Black));

        cards.Add(new CrabCard(CardColor.Teal));
        cards.Add(new CrabCard(CardColor.Black));
        cards.Add(new CrabCard(CardColor.Blue));
        cards.Add(new CrabCard(CardColor.Yellow));
        cards.Add(new CrabCard(CardColor.Yellow));
        cards.Add(new CrabCard(CardColor.Green));
        cards.Add(new CrabCard(CardColor.Teal));
        cards.Add(new CrabCard(CardColor.Gray));
        cards.Add(new CrabCard(CardColor.Blue));

        cards.Add(new SwimmerCard(CardColor.Black));
        cards.Add(new SwimmerCard(CardColor.Yellow));
        cards.Add(new SwimmerCard(CardColor.Peach));
        cards.Add(new SwimmerCard(CardColor.Teal));
        cards.Add(new SwimmerCard(CardColor.Blue));

        cards.Add(new SharkCard(CardColor.Purple));
        cards.Add(new SharkCard(CardColor.Black));
        cards.Add(new SharkCard(CardColor.Blue));
        cards.Add(new SharkCard(CardColor.Green));
        cards.Add(new SharkCard(CardColor.Teal));

        cards.Add(new CollectorCard(CardType.Shell, CardColor.Gray, [0, 2, 4, 6, 8, 10]));
        cards.Add(new CollectorCard(CardType.Shell, CardColor.Yellow, [0, 2, 4, 6, 8, 10]));
        cards.Add(new CollectorCard(CardType.Shell, CardColor.Green, [0, 2, 4, 6, 8, 10]));
        cards.Add(new CollectorCard(CardType.Shell, CardColor.Blue, [0, 2, 4, 6, 8, 10]));
        cards.Add(new CollectorCard(CardType.Shell, CardColor.Teal, [0, 2, 4, 6, 8, 10]));
        cards.Add(new CollectorCard(CardType.Shell, CardColor.Black, [0, 2, 4, 6, 8, 10]));

        cards.Add(new CollectorCard(CardType.Octopus, CardColor.Gray, [0, 3, 6, 9, 12]));
        cards.Add(new CollectorCard(CardType.Octopus, CardColor.Green, [0, 3, 6, 9, 12]));
        cards.Add(new CollectorCard(CardType.Octopus, CardColor.Yellow, [0, 3, 6, 9, 12]));
        cards.Add(new CollectorCard(CardType.Octopus, CardColor.Purple, [0, 3, 6, 9, 12]));
        cards.Add(new CollectorCard(CardType.Octopus, CardColor.Teal, [0, 3, 6, 9, 12]));

        cards.Add(new CollectorCard(CardType.Penguin, CardColor.Purple, [1, 3, 5]));
        cards.Add(new CollectorCard(CardType.Penguin, CardColor.Peach, [1, 3, 5]));
        cards.Add(new CollectorCard(CardType.Penguin, CardColor.Pink, [1, 3, 5]));

        cards.Add(new CollectorCard(CardType.Sailor, CardColor.Pink, [0, 5]));
        cards.Add(new CollectorCard(CardType.Sailor, CardColor.Orange, [0, 5]));

        cards.Add(new MultiplierCard(CardType.SailorMult, CardColor.Peach, 3));
        cards.Add(new MultiplierCard(CardType.FishMult, CardColor.Gray, 1));
        cards.Add(new MultiplierCard(CardType.PenguinMult, CardColor.Green, 2));
        cards.Add(new MultiplierCard(CardType.BoatMult, CardColor.Purple, 1));

        for (int i = 0; i < 4; i++)
            cards.Add(new MermaidCard());
    }

    public override bool Equals(object obj)
    {
        if (obj is Deck other)
        {
            return this.cards.OrderBy(card => card.Type).ThenBy(card => card.Color).SequenceEqual(other.cards.OrderBy(card => card.Type).ThenBy(card => card.Color));
        }
        return false;
    }
}
