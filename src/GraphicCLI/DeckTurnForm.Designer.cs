namespace GUI
{
    partial class DeckTurnForm
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
            label1 = new Label();
            discard1_button = new Button();
            discard2_button = new Button();
            card1_button = new Button();
            card2_button = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 11.1F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.Location = new Point(107, 77);
            label1.Name = "label1";
            label1.Size = new Size(930, 134);
            label1.TabIndex = 19;
            label1.Text = "Выберите карту, которую хотите взять";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // discard1_button
            // 
            discard1_button.Location = new Point(107, 236);
            discard1_button.Name = "discard1_button";
            discard1_button.Size = new Size(930, 330);
            discard1_button.TabIndex = 18;
            discard1_button.Text = "Сброс 1";
            discard1_button.UseVisualStyleBackColor = true;
            discard1_button.Visible = false;
            discard1_button.Click += discard1_button_Click;
            // 
            // discard2_button
            // 
            discard2_button.Location = new Point(107, 585);
            discard2_button.Name = "discard2_button";
            discard2_button.Size = new Size(930, 330);
            discard2_button.TabIndex = 17;
            discard2_button.Text = "Сброс 2";
            discard2_button.UseVisualStyleBackColor = true;
            discard2_button.Visible = false;
            discard2_button.Click += discard2_button_Click;
            // 
            // card1_button
            // 
            card1_button.Location = new Point(107, 236);
            card1_button.Name = "card1_button";
            card1_button.Size = new Size(440, 680);
            card1_button.TabIndex = 20;
            card1_button.Text = "card1";
            card1_button.UseVisualStyleBackColor = true;
            card1_button.Click += card1_button_Click;
            // 
            // card2_button
            // 
            card2_button.Location = new Point(597, 236);
            card2_button.Name = "card2_button";
            card2_button.Size = new Size(440, 680);
            card2_button.TabIndex = 21;
            card2_button.Text = "card2";
            card2_button.UseVisualStyleBackColor = true;
            card2_button.Click += card2_button_Click;
            // 
            // DeckTurnForm
            // 
            AutoScaleDimensions = new SizeF(17F, 41F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1145, 992);
            Controls.Add(card2_button);
            Controls.Add(card1_button);
            Controls.Add(label1);
            Controls.Add(discard1_button);
            Controls.Add(discard2_button);
            Name = "DeckTurnForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DeckTurnForm";
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Button discard1_button;
        private Button discard2_button;
        private Button card1_button;
        private Button card2_button;
    }
}