using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GameLogic;

namespace DB;

public class DbCard
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    [Column("type")]
    public string Type { get; set; }
    [Column("color")]
    public string Color { get; set; }
    protected DbCard() { }
    public DbCard(string type, string color)
    {
        Type = type;
        Color = color;
    }

    public BaseCard ToCardObject()
    {
        BaseCard card = null;
        switch (Enum.Parse<CardType>(Type))
        {
            case CardType.Fish:
                card = new FishCard(Enum.Parse<CardColor>(Color));
                break;
            case CardType.Crab:
                card = new CrabCard(Enum.Parse<CardColor>(Color));
                break;
            case CardType.Boat:
                card = new BoatCard(Enum.Parse<CardColor>(Color));
                break;
            case CardType.Mermaid:
                card = new MermaidCard();
                break;
            case CardType.Shark:
                card = new SharkCard(Enum.Parse<CardColor>(Color));
                break;
            case CardType.Swimmer:
                card = new SwimmerCard(Enum.Parse<CardColor>(Color));
                break;
            case CardType.Shell:
                card = new CollectorCard(CardType.Shell, Enum.Parse<CardColor>(Color), [0, 2, 4, 6, 8, 10]);
                break;
            case CardType.Octopus:
                card = new CollectorCard(CardType.Octopus, Enum.Parse<CardColor>(Color), [0, 3, 6, 9, 12]);
                break;
            case CardType.Sailor:
                card = new CollectorCard(CardType.Sailor, Enum.Parse<CardColor>(Color), [0, 5]);
                break;
            case CardType.Penguin:
                card = new CollectorCard(CardType.Penguin, Enum.Parse<CardColor>(Color), [1, 3, 5]);
                break;
            case CardType.FishMult:
                card = new MultiplierCard(CardType.FishMult, Enum.Parse<CardColor>(Color), 1);
                break;
            case CardType.BoatMult:
                card = new MultiplierCard(CardType.BoatMult, Enum.Parse<CardColor>(Color), 1);
                break;
            case CardType.SailorMult:
                card = new MultiplierCard(CardType.SailorMult, Enum.Parse<CardColor>(Color), 3);
                break;
            case CardType.PenguinMult:
                card = new MultiplierCard(CardType.PenguinMult, Enum.Parse<CardColor>(Color), 2);
                break;
            default:
                throw new DbException("Некорректный тип карты в базе данных.");
        }
        return card;
    }
}

[Table("deck")]
public class DbDeckCard : DbCard 
{
    public DbDeckCard(string type, string color) : base(type, color)
    {
    }

    protected DbDeckCard()
    {
    }
}

[Table("discard1")]
public class DbDiscard1Card : DbCard
{
    public DbDiscard1Card(string type, string color) : base(type, color)
    {
    }

    protected DbDiscard1Card()
    {
    }
}

[Table("discard2")]
public class DbDiscard2Card : DbCard
{
    public DbDiscard2Card(string type, string color) : base(type, color)
    {
    }

    protected DbDiscard2Card()
    {
    }
}

[Table("players_cards")]
public class DbPlayersCard : DbCard
{
    [ForeignKey("DbPlayer")]
    [Column("player")]
    public int DbPlayerId { get; set; }
    [Column("is_played")]
    public bool IsPlayed { get; set; }

    public DbPlayersCard(string type, string color, int player, bool isPlayed) : base(type, color)
    {
        DbPlayerId = player;
        IsPlayed = isPlayed;
    }

    protected DbPlayersCard()
    {
    }
}