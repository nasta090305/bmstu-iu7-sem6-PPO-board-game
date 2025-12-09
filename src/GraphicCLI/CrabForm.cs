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

namespace GUI
{
    public partial class CrabForm : Form
    {
        BaseGameManager gameManager;
        int card1_id, card2_id;
        int discard_id;

        public CrabForm(BaseGameManager gameManager, int card1_id, int card2_id)
        {
            this.gameManager = gameManager;
            this.card1_id = card1_id;
            this.card2_id = card2_id;
            InitializeComponent();
            GameDTO game = gameManager.GetInfo();
            discard1_button.Text = $"Сброс. {game.DeckCounts[(int)DeckType.Discard1]} карт\n{game.TopDiscard1.Type} {game.TopDiscard1.Color}";
            discard2_button.Text = $"Сброс. {game.DeckCounts[(int)DeckType.Discard2]} карт\n{game.TopDiscard2.Type} {game.TopDiscard2.Color}";
        }

        private void discard1_button_Click(object sender, EventArgs e)
        {
            discard_id = 1;
            discard1_button.Visible = false;
            discard2_button.Visible = false;
            choose_box.Visible = true;
            foreach (var card in gameManager.ShowDiscard1())
                choose_box.Items.Add($"{card.Type} {card.Color}");
            if (choose_box.Items.Count == 0)
            {
                MessageBox.Show("Колода пуста");
                return;
            }
            choose_button.Visible = true;
        }

        private void choose_button_Click(object sender, EventArgs e)
        {
            gameManager.PlayCards(card1_id, card2_id, discard_id, choose_box.SelectedIndex);
            MessageBox.Show("Карты сыграны");
            Close();
        }

        private void discard2_button_Click(object sender, EventArgs e)
        {
            discard_id = 2;
            discard1_button.Visible = false;
            discard2_button.Visible = false;
            choose_box.Visible = true;
            foreach (var card in gameManager.ShowDiscard2())
                choose_box.Items.Add($"{card.Type} {card.Color}");
            if (choose_box.Items.Count == 0)
            {
                MessageBox.Show("Колода пуста");
                return;
            }
            choose_button.Visible = true;
        }
    }
}
