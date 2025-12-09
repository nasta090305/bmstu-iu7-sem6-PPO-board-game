using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using GameLogic;

[TestClass]
public class LogicTests
{
    private class MockGameRepo : BaseGameRepository
    {
        public MockGameRepo() { }

        public Game Load() { return new Game(); }

        public void Save(Game game) {}
    }

    [TestMethod]
    public void StartGame()
    {
        List<Player> players = new List<Player>();
        List<string> playerNames = new List<string> { "AAA", "BBB", "CCC" };
        foreach (string playerName in playerNames) { players.Add(new Player(playerName)); }
        Deck deck = new Deck();
        deck.Fill();
        Discard discard1 = new Discard();
        Discard discard2 = new Discard();
        Game exp_game = new Game(deck, discard1, discard2, players, 0);

        GameManager gameManager = new GameManager(new Game(), new ScoreCalculator(), new MockGameRepo());
        gameManager.StartGame(playerNames);
        Assert.IsTrue(gameManager.cmp_games(exp_game));
    }

    [TestMethod]
    public void MakeTurnDeck()
    {
        List<Player> players = new List<Player>();
        List<string> playerNames = new List<string> { "AAA", "BBB", "CCC" };
        foreach (string playerName in playerNames) { players.Add(new Player(playerName)); }
        Deck deck = new Deck();
        deck.Fill();
        Game game = new Game(deck, new Discard(), new Discard(), players, 0);
        BaseScoreCalculator scoreCalculator = new ScoreCalculator();
        GameManager gameManager = new GameManager(game, scoreCalculator, new MockGameRepo());
        gameManager.MakeTurn(0, 1);

        List<Player> exp_players = new List<Player>();
        foreach (string playerName in playerNames) { exp_players.Add(new Player(playerName)); }
        Deck exp_deck = new Deck();
        exp_deck.Fill();
        BaseCard card = exp_deck.Remove(exp_deck.Count - 2);
        exp_players[0].Cards_hand.Add(card);
        Game exp_game = new Game(exp_deck, new Discard(), new Discard(), exp_players, 0);
        Assert.IsTrue(gameManager.cmp_games(exp_game));
    }

    [TestMethod]
    public void MakeTurnDiscard()
    {
        List<Player> players = new List<Player>();
        List<string> playerNames = new List<string> { "AAA", "BBB", "CCC" };
        foreach (string playerName in playerNames) { players.Add(new Player(playerName)); }
        Discard discard1 = new Discard();
        discard1.Add(new SwimmerCard(CardColor.White));
        Game game = new Game(new Deck(), discard1, new Discard(), players, 0);
        BaseScoreCalculator scoreCalculator = new ScoreCalculator();
        GameManager gameManager = new GameManager(game, scoreCalculator, new MockGameRepo());
        gameManager.MakeTurn(DeckType.Discard1);

        List<Player> exp_players = new List<Player>();
        foreach (string playerName in playerNames) { exp_players.Add(new Player(playerName)); }
        exp_players[0].Cards_hand.Add(new SwimmerCard(CardColor.White));
        Game exp_game = new Game(new Deck(), new Discard(), new Discard(), exp_players, 0);
        Assert.IsTrue(gameManager.cmp_games(exp_game));
    }

    [TestMethod]
    public void CheckPlayableYes()
    {
        List<Player> players = new List<Player>();
        List<string> playerNames = new List<string> { "AAA", "BBB", "CCC" };
        foreach (string playerName in playerNames) { players.Add(new Player(playerName)); }
        players[0].Cards_hand.Add(new BoatCard(CardColor.Peach));
        players[0].Cards_hand.Add(new BoatCard(CardColor.Orange));
        players[0].Cards_hand.Add(new CollectorCard(CardType.Sailor, CardColor.Orange, [0, 5]));
        Game game = new Game(new Deck(), new Discard(), new Discard(), players, 0);
        BaseScoreCalculator scoreCalculator = new ScoreCalculator();
        GameManager gameManager = new GameManager(game, scoreCalculator, new MockGameRepo());
        Assert.IsTrue(gameManager.CheckPlayable(0, 1));
    }

    [TestMethod]
    public void CheckPlayableNo()
    {
        List<Player> players = new List<Player>();
        List<string> playerNames = new List<string> { "AAA", "BBB", "CCC" };
        foreach (string playerName in playerNames) { players.Add(new Player(playerName)); }
        players[0].Cards_hand.Add(new BoatCard(CardColor.Peach));
        players[0].Cards_hand.Add(new BoatCard(CardColor.Orange));
        players[0].Cards_hand.Add(new CollectorCard(CardType.Sailor, CardColor.Orange, [0, 5]));
        Game game = new Game(new Deck(), new Discard(), new Discard(), players, 0);
        BaseScoreCalculator scoreCalculator = new ScoreCalculator();
        GameManager gameManager = new GameManager(game, scoreCalculator, new MockGameRepo());
        Assert.IsFalse(gameManager.CheckPlayable(1, 2));
    }

    [TestMethod]
    public void PlayCardsCrab()
    {
        List<Player> players = new List<Player>();
        List<string> playerNames = new List<string> { "AAA", "BBB", "CCC" };
        foreach (string playerName in playerNames) { players.Add(new Player(playerName)); }
        players[0].Cards_hand.Add(new CrabCard(CardColor.Peach));
        players[0].Cards_hand.Add(new CrabCard(CardColor.Orange));
        BaseDeck discard1 = new Discard();
        discard1.Add(new BoatCard(CardColor.Gray));
        discard1.Add(new FishCard(CardColor.Green));
        discard1.Add(new SwimmerCard(CardColor.White));
        Game game = new Game(new Deck(), discard1, new Discard(), players, 0);
        BaseScoreCalculator scoreCalculator = new ScoreCalculator();
        GameManager gameManager = new GameManager(game, scoreCalculator, new MockGameRepo());
        gameManager.PlayCards(0, 1, 1, 1);

        List<Player> exp_players = new List<Player>();
        foreach (string playerName in playerNames) { exp_players.Add(new Player(playerName)); }
        exp_players[0].Cards_hand.Add(new FishCard(CardColor.Green));
        exp_players[0].Cards_played.Add(new CrabCard(CardColor.Peach));
        exp_players[0].Cards_played.Add(new CrabCard(CardColor.Orange));
        BaseDeck exp_discard1 = new Discard();
        exp_discard1.Add(new BoatCard(CardColor.Gray));
        exp_discard1.Add(new SwimmerCard(CardColor.White));
        Game exp_game = new Game(new Deck(), exp_discard1, new Discard(), exp_players, 0);

        Assert.IsTrue(gameManager.cmp_games(exp_game));
    }

    [TestMethod]
    public void PlayCardsFish()
    {
        List<Player> players = new List<Player>();
        List<string> playerNames = new List<string> { "AAA", "BBB", "CCC" };
        foreach (string playerName in playerNames) { players.Add(new Player(playerName)); }
        players[0].Cards_hand.Add(new FishCard(CardColor.Peach));
        players[0].Cards_hand.Add(new FishCard(CardColor.Orange));
        Deck deck = new Deck();
        deck.Fill();
        Game game = new Game(deck, new Discard(), new Discard(), players, 0);
        BaseScoreCalculator scoreCalculator = new ScoreCalculator();
        GameManager gameManager = new GameManager(game, scoreCalculator, new MockGameRepo());
        gameManager.PlayCards(0, 1);

        List<Player> exp_players = new List<Player>();
        foreach (string playerName in playerNames) { exp_players.Add(new Player(playerName)); }
        Deck exp_deck = new Deck();
        exp_deck.Fill();
        BaseCard card = exp_deck.RemoveTop();
        exp_players[0].Cards_hand.Add(card);
        exp_players[0].Cards_played.Add(new FishCard(CardColor.Peach));
        exp_players[0].Cards_played.Add(new FishCard(CardColor.Orange));
        Game exp_game = new Game(exp_deck, new Discard(), new Discard(), exp_players, 0);

        Assert.IsTrue(gameManager.cmp_games(exp_game));
    }

    [TestMethod]
    public void PlayCardsBoat()
    {
        List<Player> players = new List<Player>();
        List<string> playerNames = new List<string> { "AAA", "BBB", "CCC" };
        foreach (string playerName in playerNames) { players.Add(new Player(playerName)); }
        players[0].Cards_hand.Add(new BoatCard(CardColor.Peach));
        players[0].Cards_hand.Add(new BoatCard(CardColor.Orange));
        BaseDeck discard1 = new Discard();
        discard1.Add(new BoatCard(CardColor.Gray));
        discard1.Add(new FishCard(CardColor.Green));
        discard1.Add(new SwimmerCard(CardColor.White));
        Game game = new Game(new Deck(), discard1, new Discard(), players, 0);
        BaseScoreCalculator scoreCalculator = new ScoreCalculator();
        GameManager gameManager = new GameManager(game, scoreCalculator, new MockGameRepo());
        gameManager.PlayCards(0, 1, 1);

        List<Player> exp_players = new List<Player>();
        foreach (string playerName in playerNames) { exp_players.Add(new Player(playerName)); }
        exp_players[0].Cards_hand.Add(new SwimmerCard(CardColor.White));
        exp_players[0].Cards_played.Add(new BoatCard(CardColor.Peach));
        exp_players[0].Cards_played.Add(new BoatCard(CardColor.Orange));
        BaseDeck exp_discard1 = new Discard();
        exp_discard1.Add(new BoatCard(CardColor.Gray));
        exp_discard1.Add(new FishCard(CardColor.Green));
        Game exp_game = new Game(new Deck(), exp_discard1, new Discard(), exp_players, 0);

        Assert.IsTrue(gameManager.cmp_games(exp_game));
    }

    [TestMethod]
    public void PlayCardsSwimmerShark()
    {
        List<Player> players = new List<Player>();
        List<string> playerNames = new List<string> { "AAA", "BBB", "CCC" };
        foreach (string playerName in playerNames) { players.Add(new Player(playerName)); }
        players[0].Cards_hand.Add(new SwimmerCard(CardColor.Peach));
        players[0].Cards_hand.Add(new SharkCard(CardColor.Orange));
        players[1].Cards_hand.Add(new BoatCard(CardColor.Blue));
        Game game = new Game(new Deck(), new Discard(), new Discard(), players, 0);
        BaseScoreCalculator scoreCalculator = new ScoreCalculator();
        GameManager gameManager = new GameManager(game, scoreCalculator, new MockGameRepo());
        gameManager.PlayCards(0, 1, 1);

        List<Player> exp_players = new List<Player>();
        foreach (string playerName in playerNames) { exp_players.Add(new Player(playerName)); }
        exp_players[0].Cards_played.Add(new SwimmerCard(CardColor.Peach));
        exp_players[0].Cards_played.Add(new SharkCard(CardColor.Orange));
        exp_players[0].Cards_hand.Add(new BoatCard(CardColor.Blue));
        Game exp_game = new Game(new Deck(), new Discard(), new Discard(), exp_players, 0);

        Assert.IsTrue(gameManager.cmp_games(exp_game));
    }

    [TestMethod]
    public void FinishGameRegular()
    {
        List<Player> players = new List<Player>();
        List<string> playerNames = new List<string> { "AAA", "BBB", "CCC" };
        foreach (string playerName in playerNames) { players.Add(new Player(playerName)); }
        players[0].Cards_hand.Add(new SwimmerCard(CardColor.Peach));
        players[0].Cards_hand.Add(new SharkCard(CardColor.Orange));
        players[0].Cards_hand.Add(new BoatCard(CardColor.Peach));
        players[0].Cards_hand.Add(new FishCard(CardColor.Orange));
        players[0].Cards_played.Add(new CrabCard(CardColor.Yellow));
        players[0].Cards_played.Add(new CrabCard(CardColor.Orange));
        players[0].Cards_hand.Add(new CollectorCard(CardType.Octopus, CardColor.Yellow, [0, 3, 6, 9, 12]));
        players[0].Cards_hand.Add(new CollectorCard(CardType.Octopus, CardColor.Purple, [0, 3, 6, 9, 12]));
        players[0].Cards_hand.Add(new CollectorCard(CardType.Octopus, CardColor.Gray, [0, 3, 6, 9, 12]));
        players[0].Cards_hand.Add(new CollectorCard(CardType.Shell, CardColor.Yellow, [0, 2, 4, 6, 8, 10]));
        players[0].Cards_hand.Add(new MermaidCard());

        players[1].Cards_hand.Add(new BoatCard(CardColor.Blue));
        players[1].Cards_hand.Add(new CollectorCard(CardType.Shell, CardColor.Pink, [0, 2, 4, 6, 8, 10]));
        players[1].Cards_hand.Add(new CollectorCard(CardType.Shell, CardColor.Yellow, [0, 2, 4, 6, 8, 10]));
        players[1].Cards_hand.Add(new CollectorCard(CardType.Shell, CardColor.Blue, [0, 2, 4, 6, 8, 10]));
        players[1].Cards_played.Add(new BoatCard(CardColor.Black));
        players[1].Cards_played.Add(new BoatCard(CardColor.Blue));
        players[1].Cards_played.Add(new FishCard(CardColor.Black));
        players[1].Cards_played.Add(new FishCard(CardColor.Blue));
        players[1].Cards_hand.Add(new MultiplierCard(CardType.BoatMult, CardColor.Blue, 1));
        players[1].Cards_hand.Add(new MultiplierCard(CardType.SailorMult, CardColor.Orange, 3));
        players[1].Cards_hand.Add(new CollectorCard(CardType.Penguin, CardColor.Pink, [1, 3, 5]));

        players[2].Cards_hand.Add(new BoatCard(CardColor.Black));
        players[2].Cards_hand.Add(new SwimmerCard(CardColor.Peach));
        players[2].Cards_hand.Add(new CollectorCard(CardType.Penguin, CardColor.Peach, [1, 3, 5]));
        players[2].Cards_hand.Add(new CollectorCard(CardType.Penguin, CardColor.Purple, [1, 3, 5]));
        players[2].Cards_hand.Add(new MultiplierCard(CardType.PenguinMult, CardColor.Black, 2));
        players[2].Cards_played.Add(new SwimmerCard(CardColor.Peach));
        players[2].Cards_played.Add(new SharkCard(CardColor.Orange));
        players[2].Cards_hand.Add(new CollectorCard(CardType.Sailor, CardColor.Blue, [0, 5]));
        players[2].Cards_hand.Add(new MermaidCard());
        players[2].Cards_hand.Add(new MermaidCard());
        Deck deck = new Deck();
        deck.Fill();
        Game game = new Game(deck, new Discard(), new Discard(), players, 0);
        BaseScoreCalculator scoreCalculator = new ScoreCalculator();
        GameManager gameManager = new GameManager(game, scoreCalculator, new MockGameRepo());
        GameResultDTO gameResultDTO = gameManager.FinishGame();

        GameResultDTO expGameResultDTO = new GameResultDTO();
        expGameResultDTO.EndgameReason = EndgameReasons.Regular;
        foreach (Player player in players)
            expGameResultDTO.Players.Add(new PlayerDTO(player));
        expGameResultDTO.Score.Add(11);
        expGameResultDTO.Score.Add(10);
        expGameResultDTO.Score.Add(13);
        expGameResultDTO.Winners.Add(2);

        Assert.IsTrue(gameResultDTO.Equals(expGameResultDTO));
    }

    [TestMethod]
    public void FinishGameMermaids()
    {
        List<Player> players = new List<Player>();
        List<string> playerNames = new List<string> { "AAA", "BBB", "CCC" };
        foreach (string playerName in playerNames) { players.Add(new Player(playerName)); }
        players[0].Cards_hand.Add(new SwimmerCard(CardColor.Peach));
        players[0].Cards_hand.Add(new SharkCard(CardColor.Orange));
        players[0].Cards_hand.Add(new BoatCard(CardColor.Peach));
        players[0].Cards_hand.Add(new FishCard(CardColor.Orange));
        players[0].Cards_played.Add(new CrabCard(CardColor.Yellow));
        players[0].Cards_played.Add(new CrabCard(CardColor.Orange));
        players[0].Cards_hand.Add(new CollectorCard(CardType.Octopus, CardColor.Yellow, [0, 3, 6, 9, 12]));
        players[0].Cards_hand.Add(new CollectorCard(CardType.Octopus, CardColor.Purple, [0, 3, 6, 9, 12]));
        players[0].Cards_hand.Add(new CollectorCard(CardType.Octopus, CardColor.Gray, [0, 3, 6, 9, 12]));
        players[0].Cards_hand.Add(new CollectorCard(CardType.Shell, CardColor.Yellow, [0, 2, 4, 6, 8, 10]));

        players[1].Cards_hand.Add(new BoatCard(CardColor.Blue));
        players[1].Cards_hand.Add(new CollectorCard(CardType.Shell, CardColor.Pink, [0, 2, 4, 6, 8, 10]));
        players[1].Cards_hand.Add(new CollectorCard(CardType.Shell, CardColor.Yellow, [0, 2, 4, 6, 8, 10]));
        players[1].Cards_hand.Add(new CollectorCard(CardType.Shell, CardColor.Blue, [0, 2, 4, 6, 8, 10]));
        players[1].Cards_played.Add(new BoatCard(CardColor.Black));
        players[1].Cards_played.Add(new BoatCard(CardColor.Blue));
        players[1].Cards_played.Add(new FishCard(CardColor.Black));
        players[1].Cards_played.Add(new FishCard(CardColor.Blue));
        players[1].Cards_hand.Add(new MultiplierCard(CardType.BoatMult, CardColor.Blue, 1));
        players[1].Cards_hand.Add(new MultiplierCard(CardType.SailorMult, CardColor.Orange, 3));
        players[1].Cards_hand.Add(new CollectorCard(CardType.Penguin, CardColor.Pink, [1, 3, 5]));

        players[2].Cards_hand.Add(new BoatCard(CardColor.Black));
        players[2].Cards_hand.Add(new SwimmerCard(CardColor.Peach));
        players[2].Cards_played.Add(new SwimmerCard(CardColor.Peach));
        players[2].Cards_played.Add(new SharkCard(CardColor.Orange));
        players[2].Cards_hand.Add(new CollectorCard(CardType.Sailor, CardColor.Blue, [0, 5]));
        players[2].Cards_hand.Add(new MermaidCard());
        players[2].Cards_hand.Add(new MermaidCard());
        players[2].Cards_hand.Add(new MermaidCard());
        players[2].Cards_hand.Add(new MermaidCard());
        Deck deck = new Deck();
        deck.Fill();
        Game game = new Game(deck, new Discard(), new Discard(), players, 0);
        BaseScoreCalculator scoreCalculator = new ScoreCalculator();
        GameManager gameManager = new GameManager(game, scoreCalculator, new MockGameRepo());
        GameResultDTO gameResultDTO = gameManager.FinishGame();

        GameResultDTO expGameResultDTO = new GameResultDTO();
        expGameResultDTO.EndgameReason = EndgameReasons.Mermaids;
        foreach (Player player in players)
            expGameResultDTO.Players.Add(new PlayerDTO(player));
        expGameResultDTO.Score.Add(8);
        expGameResultDTO.Score.Add(10);
        expGameResultDTO.Score.Add(6);
        expGameResultDTO.Winners.Add(2);

        Assert.IsTrue(gameResultDTO.Equals(expGameResultDTO));
    }

    [TestMethod]
    public void FinishGameEmptyDeck()
    {
        List<Player> players = new List<Player>();
        List<string> playerNames = new List<string> { "AAA", "BBB", "CCC" };
        foreach (string playerName in playerNames) { players.Add(new Player(playerName)); }
        players[0].Cards_hand.Add(new BoatCard(CardColor.Peach));
        players[0].Cards_hand.Add(new BoatCard(CardColor.Orange));
        players[0].Cards_played.Add(new CrabCard(CardColor.Yellow));
        players[0].Cards_played.Add(new CrabCard(CardColor.Orange));
        players[0].Cards_hand.Add(new CollectorCard(CardType.Octopus, CardColor.Yellow, [0, 3, 6, 9, 12]));
        players[0].Cards_hand.Add(new CollectorCard(CardType.Octopus, CardColor.Purple, [0, 3, 6, 9, 12]));
        players[0].Cards_hand.Add(new CollectorCard(CardType.Shell, CardColor.Gray, [0, 2, 4, 6, 8, 10]));
        players[0].Cards_hand.Add(new CollectorCard(CardType.Shell, CardColor.Yellow, [0, 2, 4, 6, 8, 10]));
        players[0].Cards_hand.Add(new MermaidCard());

        players[1].Cards_hand.Add(new BoatCard(CardColor.Blue));
        players[1].Cards_played.Add(new BoatCard(CardColor.Black));
        players[1].Cards_played.Add(new BoatCard(CardColor.Blue));
        players[1].Cards_played.Add(new FishCard(CardColor.Black));
        players[1].Cards_played.Add(new FishCard(CardColor.Blue));
        players[1].Cards_hand.Add(new MultiplierCard(CardType.SailorMult, CardColor.Orange, 3));
        players[1].Cards_hand.Add(new CollectorCard(CardType.Sailor, CardColor.Blue, [0, 5]));
        players[1].Cards_hand.Add(new CollectorCard(CardType.Sailor, CardColor.Pink, [0, 5]));

        players[2].Cards_hand.Add(new MultiplierCard(CardType.FishMult, CardColor.Blue, 1));
        players[2].Cards_hand.Add(new BoatCard(CardColor.Black));
        players[2].Cards_hand.Add(new SwimmerCard(CardColor.Peach));
        players[2].Cards_played.Add(new FishCard(CardColor.Blue));
        players[2].Cards_played.Add(new FishCard(CardColor.Teal));
        players[2].Cards_played.Add(new FishCard(CardColor.Black));
        players[2].Cards_played.Add(new FishCard(CardColor.Teal));
        players[2].Cards_hand.Add(new CollectorCard(CardType.Penguin, CardColor.Blue, [1, 3, 5]));
        players[2].Cards_hand.Add(new CollectorCard(CardType.Penguin, CardColor.Pink, [1, 3, 5]));
        BaseDeck deck = new Deck();
        while (deck.Count > 1)
            deck.RemoveTop();
        Game game = new Game(deck, new Discard(), new Discard(), players, 0);
        BaseScoreCalculator scoreCalculator = new ScoreCalculator();
        GameManager gameManager = new GameManager(game, scoreCalculator, new MockGameRepo());
        GameResultDTO gameResultDTO = gameManager.FinishGame();

        GameResultDTO expGameResultDTO = new GameResultDTO();
        expGameResultDTO.EndgameReason = EndgameReasons.EmptyDeck;
        foreach (Player player in players)
            expGameResultDTO.Players.Add(new PlayerDTO(player));
        expGameResultDTO.Score.Add(10);
        expGameResultDTO.Score.Add(13);
        expGameResultDTO.Score.Add(9);

        Assert.IsTrue(gameResultDTO.Equals(expGameResultDTO));
    }

    //static private BaseCard CreateCardByDTO(CardDTO cardDTO)
    //{
    //    BaseCard card = null;
    //    switch (cardDTO.Type)
    //    {
    //        case CardType.Fish:
    //            card = new FishCard(cardDTO.Color);
    //            break;
    //        case CardType.Crab:
    //            card = new CrabCard(cardDTO.Color);
    //            break;
    //        case CardType.Boat:
    //            card = new BoatCard(cardDTO.Color);
    //            break;
    //        case CardType.Mermaid:
    //            card = new MermaidCard();
    //            break;
    //        case CardType.Shark:
    //            card = new SharkCard(cardDTO.Color);
    //            break;
    //        case CardType.Swimmer:
    //            card = new SwimmerCard(cardDTO.Color);
    //            break;
    //        case CardType.Shell:
    //            card = new CollectorCard(CardType.Shell, cardDTO.Color, [0, 2, 4, 6, 8, 10]);
    //            break;
    //        case CardType.Octopus:
    //            card = new CollectorCard(CardType.Octopus, cardDTO.Color, [0, 3, 6, 9, 12]);
    //            break;
    //        case CardType.Sailor:
    //            card = new CollectorCard(CardType.Sailor, cardDTO.Color, [0, 5]);
    //            break;
    //        case CardType.Penguin:
    //            card = new CollectorCard(CardType.Penguin, cardDTO.Color, [1, 3, 5]);
    //            break;
    //        case CardType.FishMult:
    //            card = new MultiplierCard(CardType.FishMult, cardDTO.Color, 1);
    //            break;
    //        case CardType.BoatMult:
    //            card = new MultiplierCard(CardType.BoatMult, cardDTO.Color, 1);
    //            break;
    //        case CardType.SailorMult:
    //            card = new MultiplierCard(CardType.SailorMult, cardDTO.Color, 3);
    //            break;
    //        case CardType.PenguinMult:
    //            card = new MultiplierCard(CardType.PenguinMult, cardDTO.Color, 2);
    //            break;
    //    }
    //    return card;
    //}

}
