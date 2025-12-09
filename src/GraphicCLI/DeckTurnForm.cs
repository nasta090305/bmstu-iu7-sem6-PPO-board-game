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
    public partial class DeckTurnForm : Form
    {
        BaseGameManager gameManager;
        int card_id = -1;
        public DeckTurnForm(BaseGameManager gameManager)
        {
            this.gameManager = gameManager;
            InitializeComponent(); 
            List<CardDTO> cardDTOs = gameManager.ShowUpper2Deck();
            card1_button.Text = $"{cardDTOs[0].Type} {cardDTOs[0].Color}";
            card2_button.Text = $"{cardDTOs[1].Type} {cardDTOs[1].Color}";
        }
        private void card1_button_Click(object sender, EventArgs e)
        {
            card_id = 0;
            label1.Text = "Выберите сброс, в который хотите положить оставшуюся карту";
            card1_button.Visible = false;
            card2_button.Visible = false;
            GameDTO game = gameManager.GetInfo();
            discard1_button.Text = $"Сброс. {game.DeckCounts[(int)DeckType.Discard1]} карт\n{game.TopDiscard1.Type} {game.TopDiscard1.Color}";
            discard2_button.Text = $"Сброс. {game.DeckCounts[(int)DeckType.Discard2]} карт\n{game.TopDiscard2.Type} {game.TopDiscard2.Color}";
            discard1_button.Visible = true;
            discard2_button.Visible = true;
        }
        private void card2_button_Click(object sender, EventArgs e)
        {
            card_id = 1;
            label1.Text = "Выберите сброс, в который хотите положить оставшуюся карту";
            card1_button.Visible = false;
            card2_button.Visible = false;
            GameDTO game = gameManager.GetInfo();
            discard1_button.Text = $"Сброс. {game.DeckCounts[(int)DeckType.Discard1]} карт\n{game.TopDiscard1.Type} {game.TopDiscard1.Color}";
            discard2_button.Text = $"Сброс. {game.DeckCounts[(int)DeckType.Discard2]} карт\n{game.TopDiscard2.Type} {game.TopDiscard2.Color}";
            discard1_button.Visible = true;
            discard2_button.Visible = true;
        }
        private void discard1_button_Click(object sender, EventArgs e)
        {
            gameManager.MakeTurn(DeckType.Deck, card_id, DeckType.Discard1);
            Close();
        }
        private void discard2_button_Click(object sender, EventArgs e)
        {
            gameManager.MakeTurn(DeckType.Deck, card_id, DeckType.Discard2);
            Close();
        }
    }
}
