using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLogic;

namespace ConsoleCLI;

public class ConsoleDataCollector
{
    public int[] GetMakeTurnData(GameManager gameManager)
    {
        Console.WriteLine("Выберите откуда взять карту: ");
        Console.WriteLine("1. Колода");
        Console.WriteLine("2. Первый сброс");
        Console.WriteLine("3. Второй сброс");
        Console.WriteLine("Введите номер колоды: ");
        int deck_id = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine();
        if (deck_id == 1)
        {
            List<CardDTO> cards = gameManager.ShowUpper2Deck();
            Console.WriteLine("Полученные карты:");
            Console.WriteLine("1. {0}, {1}.", cards[0].Type, cards[0].Color);
            Console.WriteLine("2. {0}, {1}.", cards[1].Type, cards[1].Color);
            Console.WriteLine("Введите номер карты, которую вы хотите оставить: ");
            int card_id = Convert.ToInt32(Console.ReadLine());
            while (card_id > 2 || card_id < 1)
            {
                Console.WriteLine("Некорректный номер карты");
                Console.WriteLine("Введите номер карты, которую вы хотите оставить: ");
                card_id = Convert.ToInt32(Console.ReadLine());
            }
            Console.WriteLine("Введите номер сброса, куда хотите положить оставшуюся карту: ");
            int discard_id = Convert.ToInt32(Console.ReadLine());
            while (discard_id > 2 || discard_id < 1)
            {
                Console.WriteLine("Некорректный номер сброса");
                Console.WriteLine("Введите номер сброса, куда хотите положить оставшуюся карту: ");
                discard_id = Convert.ToInt32(Console.ReadLine());
            }
            return new int[] { deck_id - 1, card_id - 1, discard_id };
        }
        else if (deck_id == 2 || deck_id == 3)
            return new int[] { deck_id - 1 };
        else
            throw new ArgumentException("Некорректный номер колоды.");
    }

    public int[] GetCrabData(GameManager gameManager)
    {
        Console.WriteLine("Введите номер сброса, откуда вы хотите взять карту: ");
        int discard_id = Convert.ToInt32(Console.ReadLine());
        while (discard_id > 2 || discard_id < 1)
        {
            Console.WriteLine("Некорректный номер сброса");
            Console.WriteLine("Введите номер сброса, откуда вы хотите взять карту: ");
            discard_id = Convert.ToInt32(Console.ReadLine());
        }
        List<CardDTO> cards = new List<CardDTO>();
        if (discard_id == 1)
            cards = gameManager.ShowDiscard1();
        else
            cards = gameManager.ShowDiscard2();
        if (cards.Count == 0)
            throw new ArgumentException("Сброс пуст");
        Console.WriteLine("Содержимое сброса:");
        for (int i = 0; i < cards.Count; i++)
            Console.WriteLine("{0}. {1}, {2}.", i + 1, cards[i].Type, cards[i].Color);
        Console.WriteLine("Введите номер карты, которую вы хотите взять: ");
        int card_id = Convert.ToInt32(Console.ReadLine());
        while (card_id < 1 || card_id > cards.Count)
        {
            Console.WriteLine("Некорректный номер карты");
            Console.WriteLine("Введите номер карты, которую вы хотите взять: ");
            card_id = Convert.ToInt32(Console.ReadLine());
        }
        return new[] { discard_id, card_id - 1 };
    }

    public int[] GetSwimmerSharkData(GameDTO game)
    {
        for (int i = 0; i < game.Players.Count; i++)
            Console.WriteLine("{0}. {1}", i + 1, game.Players[i].Name);
        Console.WriteLine("Введите номер игрока, у которого хотите забрать карту: ");
        int player_id = Convert.ToInt32(Console.ReadLine());
        while (player_id < 1 || player_id > game.Players.Count)
        {
            Console.WriteLine("Некорректный номер карты");
            Console.WriteLine("Введите номер игрока, у которого хотите забрать карту: ");
            player_id = Convert.ToInt32(Console.ReadLine());
        }
        return new int[] { player_id - 1 };
    }
}
