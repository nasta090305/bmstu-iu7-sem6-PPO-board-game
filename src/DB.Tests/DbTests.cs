using GameLogic;
using DB;

[TestClass]
public class DbTests
{
    [TestMethod]
    public void StartGameTest()
    {
        List<string> playerNames = new List<string> {"First", "Second"};
        GameManager gameManager_saver = new GameManager(new Game(), new ScoreCalculator(), new GameRepository("Host=localhost;Port=5432;Database=SeaSaltPaper;Username=postgres;Password=Postgres"));
        gameManager_saver.StartGame(playerNames);
        GameManager gameManager_loader = new GameManager(new Game(), new ScoreCalculator(), new GameRepository("Host=localhost;Port=5432;Database=SeaSaltPaper;Username=postgres;Password=Postgres"));
        gameManager_loader.ContinueGame();
        Assert.IsTrue(gameManager_saver.cmp_game_managers(gameManager_loader));
    }

    [TestMethod]
    public void MakeTurnTest()
    {
        List<string> playerNames = new List<string> { "First", "Second" };
        GameManager gameManager_saver = new GameManager(new Game(), new ScoreCalculator(), new GameRepository("Host=localhost;Port=5432;Database=SeaSaltPaper;Username=postgres;Password=Postgres"));
        gameManager_saver.StartGame(playerNames);
        gameManager_saver.MakeTurn(DeckType.Deck, 0);
        GameManager gameManager_loader = new GameManager(new Game(), new ScoreCalculator(), new GameRepository("Host=localhost;Port=5432;Database=SeaSaltPaper;Username=postgres;Password=Postgres"));
        gameManager_loader.ContinueGame();
        Assert.IsTrue(gameManager_saver.cmp_game_managers(gameManager_loader));
    }

    [TestMethod]
    public void FinishTurnTest()
    {
        List<string> playerNames = new List<string> { "First", "Second" };
        GameManager gameManager_saver = new GameManager(new Game(), new ScoreCalculator(), new GameRepository("Host=localhost;Port=5432;Database=SeaSaltPaper;Username=postgres;Password=Postgres"));
        gameManager_saver.StartGame(playerNames);
        gameManager_saver.MakeTurn(DeckType.Deck, 0);
        gameManager_saver.FinishTurn();
        GameManager gameManager_loader = new GameManager(new Game(), new ScoreCalculator(), new GameRepository("Host=localhost;Port=5432;Database=SeaSaltPaper;Username=postgres;Password=Postgres"));
        gameManager_loader.ContinueGame();
        Assert.IsTrue(gameManager_saver.cmp_game_managers(gameManager_loader));
    }

    [TestMethod]
    public void PlayFishTest()
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
        GameManager gameManager_saver = new GameManager(game, scoreCalculator, new GameRepository("Host=localhost;Port=5432;Database=SeaSaltPaper;Username=postgres;Password=Postgres"));
        gameManager_saver.PlayCards(0, 1);
        GameManager gameManager_loader = new GameManager(new Game(), new ScoreCalculator(), new GameRepository("Host=localhost;Port=5432;Database=SeaSaltPaper;Username=postgres;Password=Postgres"));
        gameManager_loader.ContinueGame();
        Assert.IsTrue(gameManager_saver.cmp_game_managers(gameManager_loader));
    }

    [TestMethod]
    public void PlayCrabTest()
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
        GameManager gameManager_saver = new GameManager(game, scoreCalculator, new GameRepository("Host=localhost;Port=5432;Database=SeaSaltPaper;Username=postgres;Password=Postgres"));
        gameManager_saver.PlayCards(0, 1, 1);
        GameManager gameManager_loader = new GameManager(new Game(), new ScoreCalculator(), new GameRepository("Host=localhost;Port=5432;Database=SeaSaltPaper;Username=postgres;Password=Postgres"));
        gameManager_loader.ContinueGame();
        Assert.IsTrue(gameManager_saver.cmp_game_managers(gameManager_loader));
    }

    [TestMethod]
    public void PlayBoatTest()
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
        GameManager gameManager_saver = new GameManager(game, scoreCalculator, new GameRepository("Host=localhost;Port=5432;Database=SeaSaltPaper;Username=postgres;Password=Postgres"));
        gameManager_saver.PlayCards(0, 1, 1);
        GameManager gameManager_loader = new GameManager(new Game(), new ScoreCalculator(), new GameRepository("Host=localhost;Port=5432;Database=SeaSaltPaper;Username=postgres;Password=Postgres"));
        gameManager_loader.ContinueGame();
        Assert.IsTrue(gameManager_saver.cmp_game_managers(gameManager_loader));
    }

    [TestMethod]
    public void PlaySwimmerSharkTest()
    {
        List<Player> players = new List<Player>();
        List<string> playerNames = new List<string> { "AAA", "BBB", "CCC" };
        foreach (string playerName in playerNames) { players.Add(new Player(playerName)); }
        players[0].Cards_hand.Add(new SwimmerCard(CardColor.Peach));
        players[0].Cards_hand.Add(new SharkCard(CardColor.Orange));
        players[1].Cards_hand.Add(new BoatCard(CardColor.Blue));
        Game game = new Game(new Deck(), new Discard(), new Discard(), players, 0);
        BaseScoreCalculator scoreCalculator = new ScoreCalculator();
        GameManager gameManager_saver = new GameManager(game, scoreCalculator, new GameRepository("Host=localhost;Port=5432;Database=SeaSaltPaper;Username=postgres;Password=Postgres"));
        gameManager_saver.PlayCards(0, 1, 1);
        GameManager gameManager_loader = new GameManager(new Game(), new ScoreCalculator(), new GameRepository("Host=localhost;Port=5432;Database=SeaSaltPaper;Username=postgres;Password=Postgres"));
        gameManager_loader.ContinueGame();
        Assert.IsTrue(gameManager_saver.cmp_game_managers(gameManager_loader));
    }

    [TestMethod]
    public void MakeCoupleTurnsTest()
    {
        List<string> playerNames = new List<string> { "First", "Second" };
        GameManager gameManager_saver = new GameManager(new Game(), new ScoreCalculator(), new GameRepository("Host=localhost;Port=5432;Database=SeaSaltPaper;Username=postgres;Password=Postgres"));
        gameManager_saver.StartGame(playerNames);
        gameManager_saver.MakeTurn(DeckType.Deck, 0);
        gameManager_saver.FinishTurn();
        gameManager_saver.MakeTurn(DeckType.Deck, 1);
        GameManager gameManager_loader = new GameManager(new Game(), new ScoreCalculator(), new GameRepository("Host=localhost;Port=5432;Database=SeaSaltPaper;Username=postgres;Password=Postgres"));
        gameManager_loader.ContinueGame();
        Assert.IsTrue(gameManager_saver.cmp_game_managers(gameManager_loader));
    }
}