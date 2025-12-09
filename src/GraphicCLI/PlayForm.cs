using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameLogic;
using static System.Formats.Asn1.AsnWriter;

namespace GUI;

public partial class PlayForm : Form
{
    BaseGameManager gameManager;
    public PlayForm(BaseGameManager gameManager)
    {
        this.gameManager = gameManager;
        InitializeComponent();
        update_labels();
    }

    public void update_labels()
    {
        GameDTO game = gameManager.GetInfo();
        deck_button.Text = $"Колода\n{game.DeckCounts[(int)DeckType.Deck]} карт";
        discard1_button.Text = $"Сброс. {game.DeckCounts[(int)DeckType.Discard1]} карт\n{game.TopDiscard1.Type} {game.TopDiscard1.Color}";
        discard2_button.Text = $"Сброс. {game.DeckCounts[(int)DeckType.Discard2]} карт\n{game.TopDiscard2.Type} {game.TopDiscard2.Color}";
        string label = $"Игрок {game.Players[game.ActivePlayer].Name}.\n{game.ActiveHand.Count} карт на руке.\nCыгранные карты:  ";
        foreach (CardDTO card in game.Players[game.ActivePlayer].CardsPlayed)
            label += $"{card.Type} {card.Color}, ";
        label4.Text = label[..^2];
        int next_player = (game.ActivePlayer + 1) % game.Players.Count;
        if (game.Players.Count == 2)
        {
            label = $"Игрок {game.Players[next_player].Name}.\n{game.Players[next_player].HandCount} карт на руке.\nCыгранные карты:  ";
            foreach (CardDTO card in game.Players[next_player].CardsPlayed)
                label += $"{card.Type} {card.Color}, ";
            label1.Text = label[..^2];
            return;
        }
        if (game.Players.Count == 3 || game.Players.Count == 4)
        {
            label = $"Игрок {game.Players[next_player].Name}.\n{game.Players[next_player].HandCount} карт на руке.\nCыгранные карты:\n";
            foreach (CardDTO card in game.Players[next_player].CardsPlayed)
                label += $"{card.Type} {card.Color}\n";
            label3.Text = label[..^1];
        }
        next_player = (game.ActivePlayer + 2) % game.Players.Count;
        if (game.Players.Count == 3)
        {
            label = $"Игрок {game.Players[next_player].Name}.\n{game.Players[next_player].HandCount} карт на руке.\nCыгранные карты:\n";
            foreach (CardDTO card in game.Players[next_player].CardsPlayed)
                label += $"{card.Type} {card.Color}\n";
            label2.Text = label[..^1];
            return;
        }
        if (game.Players.Count == 4)
        {
            label = $"Игрок {game.Players[next_player].Name}.\n{game.Players[next_player].HandCount} карт на руке.\nCыгранные карты:  ";
            foreach (CardDTO card in game.Players[next_player].CardsPlayed)
                label += $"{card.Type} {card.Color}, ";
            label1.Text = label[..^2];
        }
        next_player = (game.ActivePlayer + 3) % game.Players.Count;
        label = $"Игрок {game.Players[next_player].Name}.\n{game.Players[next_player].HandCount} карт на руке.\nCыгранные карты:\n";
        foreach (CardDTO card in game.Players[next_player].CardsPlayed)
            label += $"{card.Type} {card.Color}\n";
        label2.Text = label[..^1];
        return;
    }

    private void discard1_button_Click(object sender, EventArgs e)
    {
        try
        {
            gameManager.MakeTurn(DeckType.Discard1);
            discard1_button.Enabled = false;
            discard2_button.Enabled = false;
            deck_button.Enabled = false;
            finish_turn_button.Enabled = true;
            finish_game_button.Enabled = false;
            info_label.Text = "Вы совершили действие хода. Теперь можете сыграть карты или завершить ход";
            update_labels();
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
            gameManager.MakeTurn(DeckType.Discard2);
            discard1_button.Enabled = false;
            discard2_button.Enabled = false;
            deck_button.Enabled = false;
            finish_turn_button.Enabled = true;
            finish_game_button.Enabled = false;
            info_label.Text = "Вы совершили действие хода. Теперь можете сыграть карты или завершить ход";
            update_labels();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void deck_button_Click(object sender, EventArgs e)
    {
        DeckTurnForm deckTurnForm = new DeckTurnForm(gameManager);
        deckTurnForm.ShowDialog();
        discard1_button.Enabled = false;
        discard2_button.Enabled = false;
        deck_button.Enabled = false;
        finish_turn_button.Enabled = true;
        finish_game_button.Enabled = false;
        info_label.Text = "Вы совершили действие хода. Теперь можете сыграть карты или завершить ход";
        update_labels();
    }

    private void show_results(GameResultDTO res)
    {
        string label = "Игра окончена.\nПобедители: ";
        foreach (var winner in res.Winners)
            label += $"{res.Players[winner].Name}, ";
        label = label[..^2] + "\n\n";
        label += "Баллы:\n";
        for (int i = 0; i < res.Players.Count; i++)
            label += $"{res.Players[i].Name}: {res.Score[i]}\n";
        info_label.Text = label;
    }

    private void finish_turn_button_Click(object sender, EventArgs e)
    {
        GameResultDTO? res = gameManager.FinishTurn();
        if (res != null)
        {
            show_results(res);
            finish_game_button.Enabled = false;
            finish_turn_button.Enabled = false;
            show_hand_button.Enabled = false;
            play_cards_button.Enabled = false;
        }
        else
        {
            info_label.Text = "Сделайте ход, нажав на любую из колод выше, или завершите игру";
            deck_button.Enabled = true;
            discard1_button.Enabled = true;
            discard2_button.Enabled = true;
            finish_game_button.Enabled = true;
            finish_turn_button.Enabled = false;
        }
        update_labels();
    }

    private void finish_game_button_Click(object sender, EventArgs e)
    {
        GameResultDTO res = gameManager.FinishGame();
        show_results(res);
        finish_game_button.Enabled = false;
        finish_turn_button.Enabled = false;
        show_hand_button.Enabled = false;
        play_cards_button.Enabled = false;
        discard1_button.Enabled = false;
        discard2_button.Enabled = false;
        deck_button.Enabled = false;
    }

    private void show_hand_button_Click(object sender, EventArgs e)
    {
        ShowCardsForm showCardsForm = new ShowCardsForm(gameManager);
        showCardsForm.Show();
    }

    private void play_cards_button_Click(object sender, EventArgs e)
    {
        PlayCardsForm playCardsForm = new PlayCardsForm(gameManager);
        playCardsForm.ShowDialog();
        update_labels();
    }
}
