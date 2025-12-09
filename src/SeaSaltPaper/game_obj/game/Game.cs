namespace GameLogic;

public class Game
{
    public Game()
    {
        Deck = new Deck();
        Discard1 = new Discard();
        Discard2 = new Discard();
        Players = new List<Player>();
        ActivePlayer = 0;
    }
    public Game(BaseDeck deck, BaseDeck discard1, BaseDeck discard2, List<Player> players, int active_player = 0)
    {
        Deck = deck;
        Discard1 = discard1;
        Discard2 = discard2;
        Players = players;
        ActivePlayer = active_player;
    }
    public BaseDeck Deck { get; }
    public BaseDeck Discard1 { get;  }
    public BaseDeck Discard2 { get; }
    public List<Player> Players { get; }
    public int ActivePlayer { get; set; }

    public override bool Equals(object obj)
    {
        if (obj is Game other)
        {
            //Console.WriteLine(this.Deck.Equals(other.deck));
            //Console.WriteLine(this.Discard1.Equals(other.Discard1));
            //foreach (Player p in this.players)
              //  Console.WriteLine(p.Name);
            //foreach (Player p in other.players) Console.WriteLine(p.Name);
            //Console.WriteLine(this.Players.SequenceEqual(other.Players));
            bool res = this.Deck.Equals(other.Deck) && this.Discard1.Equals(other.Discard1)
             && this.Discard2.Equals(other.Discard2) && this.Players.SequenceEqual(other.Players)
             && this.ActivePlayer == other.ActivePlayer;
            //Console.WriteLine("decks and players = ", res);
            //Console.WriteLine("players cards = ", res);
            return res;
        }   

        return false;
    }

}
