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

public partial class BoatForm : Form
{
    BaseGameManager gameManager;
    int card1_id, card2_id;
    int card_id;

    public BoatForm(BaseGameManager gameManager, int card1_id, int card2_id)
    {
        this.gameManager = gameManager;
        this.card1_id = card1_id;
        this.card2_id = card2_id;
        InitializeComponent();
        GameDTO game = gameManager.GetInfo();
        deck_button.Text = $"Колода\n{game.DeckCounts[(int)DeckType.Deck]} карт";
        discard1_button.Text = $"Сброс. {game.DeckCounts[(int)DeckType.Discard1]} карт\n{game.TopDiscard1.Type} {game.TopDiscard1.Color}";
        discard2_button.Text = $"Сброс. {game.DeckCounts[(int)DeckType.Discard2]} карт\n{game.TopDiscard2.Type} {game.TopDiscard2.Color}";
    }

    private void deck_button_Click(object sender, EventArgs e)
    {
        discard1_button.Visible = false;
        discard2_button.Visible = false;
        deck_button.Visible = false;
        card1_button.Visible = true;
        card2_button.Visible = true;
        label1.Text = "Выберите карту, которую хотите взять";
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
        discard1_put_button.Text = $"Сброс. {game.DeckCounts[(int)DeckType.Discard1]} карт\n{game.TopDiscard1.Type} {game.TopDiscard1.Color}";
        discard2_put_button.Text = $"Сброс. {game.DeckCounts[(int)DeckType.Discard2]} карт\n{game.TopDiscard2.Type} {game.TopDiscard2.Color}";
        discard1_put_button.Visible = true;
        discard2_put_button.Visible = true;
    }

    private void card2_button_Click(object sender, EventArgs e)
    {
        card_id = 1;
        label1.Text = "Выберите сброс, в который хотите положить оставшуюся карту";
        card1_button.Visible = false;
        card2_button.Visible = false;
        GameDTO game = gameManager.GetInfo();
        discard1_put_button.Text = $"Сброс. {game.DeckCounts[(int)DeckType.Discard1]} карт\n{game.TopDiscard1.Type} {game.TopDiscard1.Color}";
        discard2_put_button.Text = $"Сброс. {game.DeckCounts[(int)DeckType.Discard2]} карт\n{game.TopDiscard2.Type} {game.TopDiscard2.Color}";
        discard1_put_button.Visible = true;
        discard2_put_button.Visible = true;
    }

    private void discard1_button_Click(object sender, EventArgs e)
    {
        try
        {
            gameManager.PlayCards(card1_id, card2_id, (int)DeckType.Discard1);
            MessageBox.Show("Карты сыграны");
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void discard2_button_Click(object sender, EventArgs e)
    {
        try
        {
            gameManager.PlayCards(card1_id, card2_id, (int)DeckType.Discard2);
            MessageBox.Show("Карты сыграны");
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void discard1_put_button_Click(object sender, EventArgs e)
    {
        gameManager.PlayCards(card1_id, card2_id, (int)DeckType.Deck, card_id, (int)DeckType.Discard1);
        MessageBox.Show("Карты сыграны");
        Close();
    }

    private void discard2_put_button_Click(object sender, EventArgs e)
    {
        gameManager.PlayCards(card1_id, card2_id, (int)DeckType.Deck, card_id, (int)DeckType.Discard2);
        MessageBox.Show("Карты сыграны");
        Close();
    }
}
