namespace GUI;

partial class PlayForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        deck_button = new Button();
        discard1_button = new Button();
        discard2_button = new Button();
        label1 = new Label();
        label2 = new Label();
        label3 = new Label();
        label4 = new Label();
        label5 = new Label();
        show_hand_button = new Button();
        play_cards_button = new Button();
        info_label = new Label();
        finish_turn_button = new Button();
        finish_game_button = new Button();
        SuspendLayout();
        // 
        // deck_button
        // 
        deck_button.Location = new Point(359, 349);
        deck_button.Margin = new Padding(3, 4, 3, 4);
        deck_button.Name = "deck_button";
        deck_button.Size = new Size(300, 448);
        deck_button.TabIndex = 0;
        deck_button.Text = "Колода";
        deck_button.UseVisualStyleBackColor = true;
        deck_button.Click += deck_button_Click;
        // 
        // discard1_button
        // 
        discard1_button.Location = new Point(685, 349);
        discard1_button.Margin = new Padding(3, 4, 3, 4);
        discard1_button.Name = "discard1_button";
        discard1_button.Size = new Size(495, 213);
        discard1_button.TabIndex = 1;
        discard1_button.Text = "Сброс";
        discard1_button.UseVisualStyleBackColor = true;
        discard1_button.Click += discard1_button_Click;
        // 
        // discard2_button
        // 
        discard2_button.Location = new Point(685, 597);
        discard2_button.Margin = new Padding(3, 4, 3, 4);
        discard2_button.Name = "discard2_button";
        discard2_button.Size = new Size(495, 200);
        discard2_button.TabIndex = 2;
        discard2_button.Text = "Сброс";
        discard2_button.UseVisualStyleBackColor = true;
        discard2_button.Click += discard2_button_Click;
        // 
        // label1
        // 
        label1.Location = new Point(171, 50);
        label1.Name = "label1";
        label1.Size = new Size(1575, 257);
        label1.TabIndex = 3;
        label1.TextAlign = ContentAlignment.TopCenter;
        // 
        // label2
        // 
        label2.Location = new Point(1539, 489);
        label2.Name = "label2";
        label2.Size = new Size(290, 988);
        label2.TabIndex = 4;
        // 
        // label3
        // 
        label3.Location = new Point(30, 511);
        label3.Name = "label3";
        label3.Size = new Size(290, 918);
        label3.TabIndex = 5;
        // 
        // label4
        // 
        label4.Location = new Point(188, 1450);
        label4.Name = "label4";
        label4.Size = new Size(1536, 327);
        label4.TabIndex = 7;
        label4.TextAlign = ContentAlignment.TopCenter;
        // 
        // label5
        // 
        label5.AutoSize = true;
        label5.Font = new Font("Microsoft Sans Serif", 9.900001F, FontStyle.Regular, GraphicsUnit.Point, 204);
        label5.Location = new Point(837, 1368);
        label5.Name = "label5";
        label5.Size = new Size(252, 39);
        label5.TabIndex = 8;
        label5.Text = "Сейчас ходит: ";
        // 
        // show_hand_button
        // 
        show_hand_button.Location = new Point(188, 1278);
        show_hand_button.Margin = new Padding(3, 4, 3, 4);
        show_hand_button.Name = "show_hand_button";
        show_hand_button.Size = new Size(336, 142);
        show_hand_button.TabIndex = 9;
        show_hand_button.Text = "Посмотреть карты на руке";
        show_hand_button.UseVisualStyleBackColor = true;
        show_hand_button.Click += show_hand_button_Click;
        // 
        // play_cards_button
        // 
        play_cards_button.Location = new Point(1389, 1278);
        play_cards_button.Margin = new Padding(3, 4, 3, 4);
        play_cards_button.Name = "play_cards_button";
        play_cards_button.Size = new Size(336, 142);
        play_cards_button.TabIndex = 10;
        play_cards_button.Text = "Сыграть карты";
        play_cards_button.UseVisualStyleBackColor = true;
        play_cards_button.Click += play_cards_button_Click;
        // 
        // info_label
        // 
        info_label.Font = new Font("Segoe UI", 11.1F, FontStyle.Regular, GraphicsUnit.Point, 204);
        info_label.Location = new Point(359, 854);
        info_label.Name = "info_label";
        info_label.Size = new Size(1132, 356);
        info_label.TabIndex = 12;
        info_label.Text = "Сделайте ход, нажав на любую из колод выше, или завершите игру";
        info_label.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // finish_turn_button
        // 
        finish_turn_button.Enabled = false;
        finish_turn_button.Location = new Point(1201, 349);
        finish_turn_button.Margin = new Padding(3, 4, 3, 4);
        finish_turn_button.Name = "finish_turn_button";
        finish_turn_button.Size = new Size(300, 213);
        finish_turn_button.TabIndex = 13;
        finish_turn_button.Text = "Завершить ход";
        finish_turn_button.UseVisualStyleBackColor = true;
        finish_turn_button.Click += finish_turn_button_Click;
        // 
        // finish_game_button
        // 
        finish_game_button.Enabled = false;
        finish_game_button.Location = new Point(1201, 597);
        finish_game_button.Margin = new Padding(3, 4, 3, 4);
        finish_game_button.Name = "finish_game_button";
        finish_game_button.Size = new Size(300, 200);
        finish_game_button.TabIndex = 14;
        finish_game_button.Text = "Завершить игру";
        finish_game_button.UseVisualStyleBackColor = true;
        finish_game_button.Click += finish_game_button_Click;
        // 
        // PlayForm
        // 
        AutoScaleDimensions = new SizeF(17F, 41F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1896, 1788);
        Controls.Add(finish_game_button);
        Controls.Add(finish_turn_button);
        Controls.Add(info_label);
        Controls.Add(play_cards_button);
        Controls.Add(show_hand_button);
        Controls.Add(label5);
        Controls.Add(label4);
        Controls.Add(label3);
        Controls.Add(label2);
        Controls.Add(label1);
        Controls.Add(discard2_button);
        Controls.Add(discard1_button);
        Controls.Add(deck_button);
        Margin = new Padding(3, 4, 3, 4);
        Name = "PlayForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Form2";
        ResumeLayout(false);
        PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button deck_button;
    private System.Windows.Forms.Button discard1_button;
    private System.Windows.Forms.Button discard2_button;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Button show_hand_button;
    private System.Windows.Forms.Button play_cards_button;
    private System.Windows.Forms.Label info_label;
    private System.Windows.Forms.Button finish_turn_button;
    private Button finish_game_button;
}