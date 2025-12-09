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

public partial class StartForm : Form
{
    BaseGameManager gameManager;
    public StartForm(BaseGameManager gameManager)
    {
        this.gameManager = gameManager;
        InitializeComponent();
    }

    private void start_button_Click(object sender, EventArgs e)
    {
        List<string> names = new List<string>();
        foreach (var name in names_textbox.Text.Split(' '))
            names.Add(name);
        if (names.Count < 2 || names.Count > 4)
        {
            MessageBox.Show("Некорректное число игроков");
            return;
        }
        gameManager.StartGame(names);
        PlayForm form2 = new PlayForm(gameManager);
        form2.FormClosed += (s, args) => Show();
        form2.Show();
        Hide();
    }

    private void continue_button_Click(object sender, EventArgs e)
    {
        gameManager.ContinueGame();
        PlayForm form2 = new PlayForm(gameManager);
        form2.FormClosed += (s, args) => Show();
        form2.Show();
        Hide();
    }

    private void exit_button_Click(object sender, EventArgs e)
    {
        Close();
    }
}
