using System.Reflection;
using System.Runtime.ExceptionServices;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using static System.Formats.Asn1.AsnWriter;

namespace GameLogic;

public class GameManager : BaseGameManager
{
    CardsManager cards_manager;
    DataManager data_manager;
    BaseScoreCalculator score_calculator;
    ILogger logger;
    Game Game { get; set; }
    
    public GameManager(Game Game, BaseScoreCalculator scoreCalculator, BaseGameRepository repository, ILogger logger)
    {
        data_manager = new DataManager(repository, logger);
        this.Game = Game;
        cards_manager = new CardsManager(Game, logger);
        score_calculator = scoreCalculator;
        this.logger = logger;
        logger.LogDebug("Создан GameManager");
    }

    public GameManager(Game Game, BaseScoreCalculator scoreCalculator, BaseGameRepository repository)
    {
        this.Game = Game;
        score_calculator = scoreCalculator;
        this.logger = NullLogger.Instance;
        cards_manager = new CardsManager(Game, logger);
        data_manager = new DataManager(repository, logger);
        logger.LogDebug("Создан GameManager с заглушкой-логгером");
    }

    public void StartGame(List<string> players_names)
    {
        logger.LogInformation("Старт новой игры для {Count} игроков", players_names.Count);
        List<Player> players = new List<Player>();
        Deck deck = new Deck();
        Discard discard1 = new Discard();
        Discard discard2 = new Discard();
        for (int i = 0; i < players_names.Count; i++)
        {
            players.Add(new Player(players_names[i]));
            logger.LogDebug("Создан игрок {Name}", players_names[i]);
        }
        deck.Fill();
        deck.Shuffle();
        logger.LogDebug("Колода заполнена и перемешана");
        Game = new Game(deck, discard1, discard2, players, 0);
        cards_manager.Game = Game;
        data_manager.Save(Game);
        logger.LogInformation("Игра успешно инициализирована");
    }

    public void ContinueGame()
    {
        logger.LogInformation("Загрузка сохраненной игры");
        Game = data_manager.Load();
        cards_manager.Game = Game;
    }

    public GameDTO GetInfo()
    {
        logger.LogDebug("Получение информации об игре");
        return new GameDTO(Game);
    }

    public List<CardDTO> ShowDiscard1()
    {
        logger.LogDebug("Получение информации о содержании первого сброса");
        List<CardDTO> result = new List<CardDTO>();
        foreach (BaseCard card in Game.Discard1.Show())
            result.Add(new CardDTO(card));
        return result;
    }

    public List<CardDTO> ShowDiscard2()
    {
        logger.LogDebug("Получение информации о содержании второго сброса");
        List<CardDTO> result = new List<CardDTO>();
        foreach (BaseCard card in Game.Discard2.Show())
            result.Add(new CardDTO(card));
        return result;
    }

    public List<CardDTO> ShowUpper2Deck()
    {
        logger.LogDebug("Получение информации о двух верхних картах колоды");
        List<CardDTO> result = new List<CardDTO>();
        foreach (BaseCard card in Game.Deck.ShowUpperN(2))
            result.Add(new CardDTO(card));
        return result;
    }

    public void MakeTurn(DeckType deck_id, int card_id = 0, DeckType discard = 0)
    {
        logger.LogInformation("Игрок {Player} делает ход: берет карту из {Deck} (card_id={CardId}, discard={Discard})",
            Game.Players[Game.ActivePlayer].Name, deck_id.ToString(), card_id, discard);
        BaseCard card;
        switch (deck_id)
        {
            case DeckType.Deck:
                if (card_id == 0)
                {
                    card = cards_manager.RemoveFromDeck(deck_id);
                    cards_manager.AddToPlayer(Game.ActivePlayer, card);
                    card = cards_manager.RemoveFromDeck(deck_id);
                    cards_manager.AddToDeck(discard, card);
                }
                else if (card_id == 1)
                {
                    card = cards_manager.RemoveFromDeck(deck_id);
                    cards_manager.AddToDeck(discard, card);
                    card = cards_manager.RemoveFromDeck(deck_id);
                    cards_manager.AddToPlayer(Game.ActivePlayer, card);
                }
                else
                    throw new LogicException("Во время хода нельзя взять карту, которая не является одной из двух верхних.");
                break;
            case DeckType.Discard1:
                card = cards_manager.RemoveFromDeck(deck_id);
                cards_manager.AddToPlayer(Game.ActivePlayer, card);
                break;
            case DeckType.Discard2:
                card = cards_manager.RemoveFromDeck(deck_id);
                cards_manager.AddToPlayer(Game.ActivePlayer, card);
                break;
            default:
                throw new LogicException("Некорректная колода.");
        }
        data_manager.Save(Game);
        logger.LogInformation("Действие хода игрока {Player} завершено", Game.Players[Game.ActivePlayer].Name);
    }

    public bool CheckPlayable(int card1_id, int card2_id)
    {
        BaseCard card1 = Game.Players[Game.ActivePlayer].Cards_hand[card1_id];
        BaseCard card2 = Game.Players[Game.ActivePlayer].Cards_hand[card2_id];
        logger.LogDebug("Проверка, можно ли сыграть карты {card1_type}, {card1_color} и {card2_type}, {card2_color}", 
            card1.Type, card1.Color, card2.Type, card2.Color);
        if (card1_id == card2_id) return false;
        if (card1.IsPlayable(card2)) { return true; }
        return false;
    }

    public void PlayCards(int card1_id, int card2_id, params int[] args)
    {
        if (card1_id > Game.Players[Game.ActivePlayer].Cards_hand.Count() - 1 || card1_id < 0
            || card2_id > Game.Players[Game.ActivePlayer].Cards_hand.Count() - 1 || card2_id < 0)
        { throw new LogicException("Некорректный номер карты."); }
        BaseCard card1 = Game.Players[Game.ActivePlayer].Cards_hand[card1_id];
        BaseCard card2 = Game.Players[Game.ActivePlayer].Cards_hand[card2_id];
        logger.LogInformation("Игрок {Name} играет карты {card1_type}, {card1_color} и {card2_type}, {card2_color}",
            Game.Players[Game.ActivePlayer].Name, card1.Type, card1.Color, card2.Type, card2.Color);
        (card1_id, card2_id) = (Math.Max(card1_id, card2_id), Math.Min(card1_id, card2_id));
        card1.Play(cards_manager, args);
        cards_manager.PlayCard(card1_id);
        cards_manager.PlayCard(card2_id);
        data_manager.Save(Game);
        logger.LogInformation("Розыгрыш карт игроком {Name} успешно закончен", Game.Players[Game.ActivePlayer].Name);
    }

    public bool CheckEndGame()
    {
        logger.LogDebug("Проверка, не закончилась ли игра");
        foreach (Player player in Game.Players)
        {
            int mermaids = 0;
            foreach (var card in player.Cards_hand)
            {
                if (card.GetType() == typeof(MermaidCard))
                    mermaids++;
            }
            if (mermaids == 4) { return true; }
        }
        if (Game.Deck.Count < 2)
            { return true; }
        return false;
    }

    public GameResultDTO? FinishTurn()
    {
        logger.LogInformation("Игрок {Player} завершает ход и передает его следующему игроку", Game.Players[Game.ActivePlayer].Name);
        Game.ActivePlayer = (Game.ActivePlayer + 1) % Game.Players.Count;
        if (CheckEndGame()) { return FinishGame(); }
        data_manager.Save(Game);
        return null;
    }

    public GameResultDTO FinishGame()
    {
        logger.LogInformation("Игра завершается, начинается определение результатов партии");
        GameResultDTO GameResultDTO = new GameResultDTO();
        foreach (Player player in Game.Players)
        {
            int score = score_calculator.GetScore(player);
            GameResultDTO.Score.Add(score); 
            GameResultDTO.Players.Add(new PlayerDTO(player));
            logger.LogDebug("Игрок {Player} получил {Score} очков", player.Name, score);
        }
        if (Game.Deck.Count < 2)
        { 
            GameResultDTO.EndgameReason = EndgameReasons.EmptyDeck;
            logger.LogInformation("Игра окончена: колода пуста");
            return GameResultDTO;
        }
        for (int i = 0; i < Game.Players.Count; i++)
        {
            int mermaids = 0;
            foreach (var card in Game.Players[i].Cards_hand)
            {
                if (card.GetType() == typeof(MermaidCard))
                    mermaids++;
            }
            if (mermaids == 4)
            {
                GameResultDTO.EndgameReason = EndgameReasons.Mermaids;
                GameResultDTO.Winners.Add(i);
                logger.LogInformation("Игра окончена: у игрока {Player} 4 русалки", Game.Players[i].Name);
                return GameResultDTO;
            }
        }
        GameResultDTO.EndgameReason = EndgameReasons.Regular;
        int max_score = GameResultDTO.Score.Max();
        for (int i = 0; i < GameResultDTO.Score.Count; i++)
        {
            if (GameResultDTO.Score[i] == max_score)
                GameResultDTO.Winners.Add(i);
        }
        logger.LogInformation("Игра окончена: по желанию игрока {Player}", Game.Players[Game.ActivePlayer].Name);
        return GameResultDTO;
    }

    public bool cmp_games(object obj)
    {
        if (obj is Game other)
            return Game.Equals(other);
        return false;
    }

    public bool cmp_game_managers(object obj)
    {
        if (obj is GameManager other)
            return Game.Equals(other.Game);
        return false;
    }
}
