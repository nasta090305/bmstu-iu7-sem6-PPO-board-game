using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLogic;

namespace DB;

[Table("players")]
public class DbPlayer
{
    [Key]
    [Column("order")]
    public int Order { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("is_active")]
    public bool IsActive { get; set; }
    public DbPlayer(int order,  string name, bool isActive)
    {
        Order = order;
        Name = name;
        IsActive = isActive;
    }
    public DbPlayer() { }
    public Player ToPlayerObject()
    {
        return new Player(Name);
    }
}
