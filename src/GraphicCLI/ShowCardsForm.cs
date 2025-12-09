using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameLogic;

namespace GUI;

public partial class ShowCardsForm : Form
{
    BaseGameManager gameManager;
    public ShowCardsForm(BaseGameManager gameManager)
    {
        this.gameManager = gameManager;
        InitializeComponent();
        label1.Text = $"Карты на руке игрока {gameManager.GetInfo().Players[gameManager.GetInfo().ActivePlayer].Name}";
        foreach (var card in gameManager.GetInfo().ActiveHand)
            cards_box.Items.Add($"{card.Type} {card.Color}");
    }

}
