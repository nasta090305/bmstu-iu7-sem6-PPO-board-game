namespace GUI
{
    partial class BoatForm
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
            discard2_button = new Button();
            discard1_button = new Button();
            deck_button = new Button();
            label1 = new Label();
            card2_button = new Button();
            card1_button = new Button();
            discard1_put_button = new Button();
            discard2_put_button = new Button();
            SuspendLayout();
            // 
            // discard2_button
            // 
            discard2_button.Location = new Point(535, 611);
            discard2_button.Margin = new Padding(3, 4, 3, 4);
            discard2_button.Name = "discard2_button";
            discard2_button.Size = new Size(495, 300);
            discard2_button.TabIndex = 5;
            discard2_button.Text = "Сброс";
            discard2_button.UseVisualStyleBackColor = true;
            discard2_button.Click += discard2_button_Click;
            // 
            // discard1_button
            // 
            discard1_button.Location = new Point(535, 236);
            discard1_button.Margin = new Padding(3, 4, 3, 4);
            discard1_button.Name = "discard1_button";
            discard1_button.Size = new Size(495, 300);
            discard1_button.TabIndex = 4;
            discard1_button.Text = "Сброс";
            discard1_button.UseVisualStyleBackColor = true;
            discard1_button.Click += discard1_button_Click;
            // 
            // deck_button
            // 
            deck_button.Location = new Point(100, 236);
            deck_button.Margin = new Padding(3, 4, 3, 4);
            deck_button.Name = "deck_button";
            deck_button.Size = new Size(397, 679);
            deck_button.TabIndex = 3;
            deck_button.Text = "Колода";
            deck_button.UseVisualStyleBackColor = true;
            deck_button.Click += deck_button_Click;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 11.1F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.Location = new Point(100, 47);
            label1.Name = "label1";
            label1.Size = new Size(930, 155);
            label1.TabIndex = 6;
            label1.Text = " Выберите откуда взять карты";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // card2_button
            // 
            card2_button.Location = new Point(590, 235);
            card2_button.Name = "card2_button";
            card2_button.Size = new Size(440, 680);
            card2_button.TabIndex = 25;
            card2_button.Text = "card2";
            card2_button.UseVisualStyleBackColor = true;
            card2_button.Visible = false;
            card2_button.Click += card2_button_Click;
            // 
            // card1_button
            // 
            card1_button.Location = new Point(100, 231);
            card1_button.Name = "card1_button";
            card1_button.Size = new Size(440, 680);
            card1_button.TabIndex = 24;
            card1_button.Text = "card1";
            card1_button.UseVisualStyleBackColor = true;
            card1_button.Visible = false;
            card1_button.Click += card1_button_Click;
            // 
            // discard1_put_button
            // 
            discard1_put_button.Location = new Point(100, 236);
            discard1_put_button.Name = "discard1_put_button";
            discard1_put_button.Size = new Size(930, 330);
            discard1_put_button.TabIndex = 23;
            discard1_put_button.Text = "Сброс 1";
            discard1_put_button.UseVisualStyleBackColor = true;
            discard1_put_button.Visible = false;
            discard1_put_button.Click += discard1_put_button_Click;
            // 
            // discard2_put_button
            // 
            discard2_put_button.Location = new Point(100, 581);
            discard2_put_button.Name = "discard2_put_button";
            discard2_put_button.Size = new Size(930, 330);
            discard2_put_button.TabIndex = 22;
            discard2_put_button.Text = "Сброс 2";
            discard2_put_button.UseVisualStyleBackColor = true;
            discard2_put_button.Visible = false;
            discard2_put_button.Click += discard2_put_button_Click;
            // 
            // BoatForm
            // 
            AutoScaleDimensions = new SizeF(17F, 41F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1145, 992);
            Controls.Add(card2_button);
            Controls.Add(card1_button);
            Controls.Add(discard1_put_button);
            Controls.Add(discard2_put_button);
            Controls.Add(label1);
            Controls.Add(discard2_button);
            Controls.Add(discard1_button);
            Controls.Add(deck_button);
            Name = "BoatForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "BoatForm";
            ResumeLayout(false);
        }

        #endregion

        private Button discard2_button;
        private Button discard1_button;
        private Button deck_button;
        private Label label1;
        private Button card2_button;
        private Button card1_button;
        private Button discard1_put_button;
        private Button discard2_put_button;
    }
}