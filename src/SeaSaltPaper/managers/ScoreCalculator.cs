using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics.Metrics;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace GameLogic;

public class ScoreCalculator: BaseScoreCalculator
{
    public int GetScore(Player player)
    {
        List<BaseCard> cards = new List<BaseCard>();
        cards.AddRange(player.Cards_hand);
        cards.AddRange(player.Cards_played);
        int score = 0;
        var types = new Dictionary<CardType, int>();
        var colors = new Dictionary<CardColor, int>();
        foreach (CardColor color in Enum.GetValues(typeof(CardColor)))
            colors[color] = cards.Count(c => c.Color == color);
        colors[CardColor.None] = 0;
        var sorted_colors = colors.OrderByDescending(pair => pair.Value);
        foreach (CardType type in Enum.GetValues(typeof(CardType)))
            types[type] = cards.Count(c => c.Type == type);
        var multipliers = cards.Where(c => c is MultiplierCard).ToList();
        score += types[CardType.Fish] / 2;
        score += types[CardType.Crab] / 2;
        score += types[CardType.Boat] / 2;
        score += Math.Min(types[CardType.Shark], types[CardType.Swimmer]);
        score += GetCollectionsScore(types, cards);
        score += GetMultiplierScore(types, multipliers);
        for (int i = 0; i < types[CardType.Mermaid]; i++)
        {
            score += sorted_colors.ElementAt(i).Value;
        }
        return score;
    }

    private int GetCollectionsScore(Dictionary<CardType, int> types, List<BaseCard> cards)
    {
        int score = 0;
        if (types[CardType.Penguin] != 0)
        {
            BaseCard card = cards.Where(c => c.Type == CardType.Penguin).First();
            score += card.GetScore(types[CardType.Penguin]);
        }
        if (types[CardType.Shell] != 0)
        {
            BaseCard card = cards.Where(c => c.Type == CardType.Shell).First();
            score += card.GetScore(types[CardType.Shell]);
        }
        if (types[CardType.Octopus] != 0)
        {
            BaseCard card = cards.Where(c => c.Type == CardType.Octopus).First();
            score += card.GetScore(types[CardType.Octopus]);
        }
        if (types[CardType.Sailor] != 0)
        {
            BaseCard card = cards.Where(c => c.Type == CardType.Sailor).First();
            score += card.GetScore(types[CardType.Sailor]);
        }
        return score;
    }

    private int GetMultiplierScore(Dictionary<CardType, int> types, List<BaseCard> multipliers)
    {
        int score = 0;
        foreach (var card in multipliers)
        {
            switch(card.Type)
            {
                case CardType.PenguinMult:
                    score += card.GetScore(types[CardType.Penguin]);
                    break;
                case CardType.SailorMult:
                    score += card.GetScore(types[CardType.Sailor]);
                    break;
                case CardType.FishMult:
                    score += card.GetScore(types[CardType.Fish]);
                    break;
                case CardType.BoatMult:
                    score += card.GetScore(types[CardType.Boat]);
                    break;
            }
        }
        return score;
    }

    /*//public int GetScore(Player player)
    //{
    //    List<BaseCard> cards = new List<BaseCard>();
    //    cards.AddRange(player.Cards_hand);
    //    cards.AddRange(player.Cards_played);
    //    int score = 0;
    //    int fishes = 0;
    //    int crabs = 0;
    //    int boats = 0;
    //    int mermaids = 0;
    //    int swimmers = 0;
    //    int sharks = 0;
    //    var collections = new Dictionary<CardType, int>();
    //    var colors = new Dictionary<CardColor, int>();
    //    List<BaseCard> multipliers = new List<BaseCard>();
    //    foreach (var card in cards)
    //    {
    //        if (colors.ContainsKey(card.Color))
    //            colors[card.Color]++;
    //        else
    //            colors[card.Color] = 1;
    //        switch (card)
    //        {
    //            case FishCard:
    //                fishes++;
    //                break;
    //            case CrabCard:
    //                crabs++;
    //                break;
    //            case BoatCard:
    //                boats++;
    //                break;
    //            case MermaidCard:
    //                mermaids++;
    //                break;
    //            case SwimmerCard:
    //                swimmers++;
    //                break;
    //            case SharkCard:
    //                sharks++;
    //                break;
    //            case MultiplierCard:
    //                multipliers.Add(card);
    //                break;
    //            case CollectorCard:
    //                if (collections.ContainsKey(card.Type))
    //                {
    //                    score += card.GetScore(collections[card.Type]);
    //                    collections[card.Type]++;
    //                }
    //                else
    //                {
    //                    collections[card.Type] = 1;
    //                    score += card.GetScore(0);
    //                }
    //                break;
    //            default:
    //                throw new LogicException("Невозможно подсчитать очки данной карты");
    //        }
    //    }
    //    score += fishes / 2;
    //    score += crabs / 2;
    //    score += boats / 2;
    //    score += Math.Min(sharks, swimmers);
    //    foreach (var card in multipliers)
    //    {
    //        switch (card.Type)
    //        {
    //            case CardType.FishMult:
    //                score += card.GetScore(fishes);
    //                break;
    //            case CardType.BoatMult:
    //                score += card.GetScore(boats);
    //                break;
    //            case CardType.SailorMult:
    //                if (collections.ContainsKey(CardType.Sailor))
    //                    score += card.GetScore(collections[CardType.Sailor]);
    //                break;
    //            case CardType.PenguinMult:
    //                if (collections.ContainsKey(CardType.Penguin))
    //                    score += card.GetScore(collections[CardType.Penguin]);
    //                break;
    //        }
    //    }
    //    var sorted_colors = colors.OrderByDescending(pair => pair.Value);
    //    for (int i = 0; i < mermaids; i++)
    //    {
    //        score += sorted_colors.ElementAt(i).Value;
    //    }
    //    return score;
    //}*/
}
