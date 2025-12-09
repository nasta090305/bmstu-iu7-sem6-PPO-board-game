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

public partial class SwimmerSharkForm : Form
{
    BaseGameManager gameManager;
    int card1_id, card2_id;
    public SwimmerSharkForm(BaseGameManager gameManager, int card1_id, int card2_id)
    {
        this.gameManager = gameManager;
        this.card1_id = card1_id;
        this.card2_id = card2_id;
        InitializeComponent();
        GameDTO game = gameManager.GetInfo();
        for (int i = 0; i < game.Players.Count; i++)
        {
            if (i != game.ActivePlayer)
                choose_box.Items.Add($"Игрок {game.Players[i].Name}. {game.Players[i].HandCount} карт");
        }
    }

    private void choose_button_Click(object sender, EventArgs e)
    {
        try
        {
            gameManager.PlayCards(card1_id, card2_id, choose_box.SelectedIndex);
            MessageBox.Show("Карты сыграны");
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}
