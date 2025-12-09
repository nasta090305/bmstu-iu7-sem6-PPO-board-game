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

public partial class PlayCardsForm : Form
{
    BaseGameManager gameManager;
    public PlayCardsForm(BaseGameManager gameManager)
    {
        this.gameManager = gameManager;
        InitializeComponent();
        foreach (var card in gameManager.GetInfo().ActiveHand)
            choose_box.Items.Add($"{card.Type} {card.Color}");
    }

    private void choose_button_Click(object sender, EventArgs e)
    {
        int[] indices = choose_box.SelectedIndices.Cast<int>().ToArray();
        if (indices.Length != 2)
        { 
            MessageBox.Show("Выбрано некорректное число карт");
            return; 
        }
        GameDTO game = gameManager.GetInfo();
        if (!gameManager.CheckPlayable(indices[0], indices[1]))
        {
            MessageBox.Show("Выбранные карты невозможно сыграть вместе");
            return;
        }
        else if (game.ActiveHand[indices[0]].Type == CardType.Fish)
        {
            gameManager.PlayCards(indices[0], indices[1]);
            MessageBox.Show("Карты сыграны");
            Close();
        }
        else if (game.ActiveHand[indices[0]].Type == CardType.Crab)
        {
            CrabForm crabForm = new CrabForm(gameManager, indices[0], indices[1]);
            crabForm.ShowDialog();
            Close();
        }
        else if (game.ActiveHand[indices[0]].Type == CardType.Boat)
        {
            BoatForm boatForm = new BoatForm(gameManager, indices[0], indices[1]);
            boatForm.ShowDialog(); 
            Close();
        }
        else if (game.ActiveHand[indices[0]].Type == CardType.Swimmer ||
            game.ActiveHand[indices[0]].Type == CardType.Shark)
        {
            SwimmerSharkForm swimmerSharkForm = new SwimmerSharkForm(gameManager, indices[0], indices[1]);
            swimmerSharkForm.ShowDialog();
            Close();
        }
        else
        {
            MessageBox.Show("Эти карты невозможно сыграть");
            Close();
        }
    }
}
