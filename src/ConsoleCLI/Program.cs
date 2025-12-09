using GameLogic;
using DB;
using ConsoleCLI;
using System.Runtime.CompilerServices;
using System.Linq.Expressions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("D:\\sem6\\ppo\\lab4\\ppo\\src\\SeaSaltPaper\\ConsoleCLI\\config.json", optional: false, reloadOnChange: true)
            .Build();

var connectionString = config.GetConnectionString("DefaultConnection");

var loggerFactory = LoggerFactory.Create(builder =>
{
    var serilogLogger = new LoggerConfiguration()
        .ReadFrom.Configuration(config)
        .WriteTo.File("D:\\sem6\\ppo\\lab4\\ppo\\src\\SeaSaltPaper\\Logs\\app.log", rollingInterval: RollingInterval.Day)
        .CreateLogger();

    builder.ClearProviders();
    builder.AddSerilog(serilogLogger, dispose: true);
});

var logger = loggerFactory.CreateLogger("AppLogger");

GameManager gameManager = new GameManager(new Game(), new ScoreCalculator(), new GameRepository(connectionString), logger);
ConsoleDataCollector collector = new ConsoleDataCollector();
bool turn_was_made = false;

Console.WriteLine("Консольная версия игры Море-Соль-Бумага.");

while (true)
{
    while (true)
    {
        try
        {
            Console.WriteLine();
            Console.WriteLine("1. Начать новую игру");
            Console.WriteLine("2. Продолжить игру");
            Console.WriteLine("3. Завершить выполнение");
            Console.WriteLine();
            Console.WriteLine("Введите номер команды: ");

            int command = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            if (command == 1)
            {
                Console.WriteLine("Введите имена игроков через пробел: ");
                List<string> names = new List<string>();
                foreach (var name in Console.ReadLine().Split(' '))
                    names.Add(name);
                gameManager.StartGame(names);
                Console.WriteLine("Игра начата");
                break;
            }
            else if (command == 2)
            {
                gameManager.ContinueGame();
                Console.WriteLine("Игра загружена");
                break;
            }
            else if (command == 3)
            {
                Console.WriteLine("Завершение");
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Некорректная команда");
            }
        }
        catch (Exception e) 
        {
            logger.LogError(e.Message);
            Console.WriteLine("Ошибка. {0}", e.Message); 
        }
    }
    while (true)
    {
        try
        {
            GameDTO game = gameManager.GetInfo();

            Console.WriteLine();
            Console.WriteLine("Ходит {0}.\nВ колоде {1} карт.\nВерхняя карта первого сброса: {2}, {3}. Верхняя карта второго сброса: {4}, {5}.",
                game.Players[game.ActivePlayer].Name, game.DeckCounts[(int)DeckType.Deck], game.TopDiscard1.Type, game.TopDiscard1.Color, game.TopDiscard2.Type, game.TopDiscard2.Color);
            Console.WriteLine();
            Console.WriteLine("1. Посмотреть карты на руке");
            Console.WriteLine("2. Посмотреть полную информацию об игре");
            Console.WriteLine("3. Сыграть карты");
            Console.WriteLine("4. Сделать ход (взять карты)");
            Console.WriteLine("5. Завершить ход");
            Console.WriteLine("6. Завершить раунд");
            Console.WriteLine("7. Завершить выполнение");
            Console.WriteLine();
            Console.WriteLine("Введите номер команды: ");
            int command = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            if (command == 1)
            {
                Console.WriteLine("Карты на руке:");
                foreach (var card in game.ActiveHand)
                    Console.WriteLine("{0}, {1}.", card.Type, card.Color);
                Console.WriteLine();
            }
            else if (command == 2)
            {
                Console.WriteLine("В первом сбросе {0} карт", game.DeckCounts[(int)DeckType.Discard1]);
                Console.WriteLine("Во втором сбросе {0} карт", game.DeckCounts[(int)DeckType.Discard2]);
                foreach (var player in game.Players)
                {
                    Console.WriteLine("Сыгранные карты у {0}:", player.Name);
                    foreach (var card in player.CardsPlayed)
                        Console.WriteLine("{0}, {1}.", card.Type, card.Color);
                    Console.WriteLine("Карт на руке у {0}: {1}", player.Name, player.HandCount);
                    Console.WriteLine();
                }
            }
            else if (command == 3)
            {
                Console.WriteLine("Карты на руке:");
                for (int i = 0; i < game.ActiveHand.Count; i++)
                    Console.WriteLine("{0}. {1}, {2}.", i + 1, game.ActiveHand[i].Type, game.ActiveHand[i].Color);
                Console.WriteLine();
                Console.WriteLine("Введите через пробел номера двух карт, которые вы хотите сыграть: ");
                string[] parts = Console.ReadLine().Split(' ');
                int card1 = int.Parse(parts[0]) - 1;
                int card2 = int.Parse(parts[1]) - 1;
                if (card1 >= game.ActiveHand.Count || card2 >= game.ActiveHand.Count || card1 < 0 || card2 < 0)
                {
                    Console.WriteLine("Некорректные номера карт");
                    continue;
                }
                if (!gameManager.CheckPlayable(card1, card2))
                    Console.WriteLine("Эти карты нельзя сыграть вместе.");
                else if (game.ActiveHand[card1].Type == CardType.Fish)
                {
                    gameManager.PlayCards(card1, card2);
                    Console.WriteLine("Карты сыграны.");
                }
                else if (game.ActiveHand[card1].Type == CardType.Boat)
                {
                    gameManager.PlayCards(card1, card2, collector.GetMakeTurnData(gameManager));
                    Console.WriteLine("Карты сыграны.");
                }
                else if (game.ActiveHand[card1].Type == CardType.Crab)
                {
                    gameManager.PlayCards(card1, card2, collector.GetCrabData(gameManager));
                    Console.WriteLine("Карты сыграны.");
                }
                else if (game.ActiveHand[card1].Type == CardType.Shark || game.ActiveHand[card1].Type == CardType.Swimmer)
                {
                    gameManager.PlayCards(card1, card2, collector.GetSwimmerSharkData(game));
                    Console.WriteLine("Карты сыграны.");
                }
                else
                    Console.WriteLine("Невозможно сыграть карты.");
            }
            else if (command == 4)
            {
                if (turn_was_made)
                {
                    Console.WriteLine("Этим игроком ход был уже сделан");
                    continue;
                }
                int[] data = collector.GetMakeTurnData(gameManager);
                if (data.Count() == 1)
                    gameManager.MakeTurn((DeckType)data[0]);
                else if (data.Count() == 3)
                    gameManager.MakeTurn((DeckType)data[0], data[1], (DeckType)data[2]);
                else
                    throw new Exception("Ошибка ввода данных");
                Console.WriteLine("Ход сделан");
                turn_was_made = true;
            }
            else if (command == 5)
            {
                if (turn_was_made)
                {
                    gameManager.FinishTurn();
                    turn_was_made = false;
                }
                else Console.WriteLine("Вы еще не выполнили действие хода. Ход завершить нельзя");
            }
            else if (command == 6)
            {
                if (turn_was_made)
                {
                    Console.WriteLine("Нельзя завершить игру, если вы уже сделали действие хода");
                    continue;
                }
                GameResultDTO res = gameManager.FinishGame();
                Console.WriteLine("Победители:");
                foreach (var winner in res.Winners)
                    Console.WriteLine(res.Players[winner].Name);
                Console.WriteLine();
                Console.WriteLine("Баллы:");
                for (int i = 0; i < res.Players.Count; i++)
                    Console.WriteLine("{0}: {1}", res.Players[i].Name, res.Score[i]);
                break;
            }
            else if (command == 7)
            {
                Console.WriteLine("Завершение");
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Некорректная команда");
            }
        }
        catch (Exception e) 
        {
            logger.LogError(e.Message);
            Console.WriteLine("Ошибка. {0}", e.Message);
        }
    }
}