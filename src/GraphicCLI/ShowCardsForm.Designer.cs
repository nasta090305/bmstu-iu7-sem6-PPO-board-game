namespace GUI
{
    partial class ShowCardsForm
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
            cards_box = new ListBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // cards_box
            // 
            cards_box.Font = new Font("Segoe UI", 9.900001F, FontStyle.Regular, GraphicsUnit.Point, 204);
            cards_box.FormattingEnabled = true;
            cards_box.ItemHeight = 45;
            cards_box.Location = new Point(141, 268);
            cards_box.Margin = new Padding(3, 4, 3, 4);
            cards_box.Name = "cards_box";
            cards_box.Size = new Size(512, 724);
            cards_box.TabIndex = 13;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 11.1F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.Location = new Point(141, 78);
            label1.Name = "label1";
            label1.Size = new Size(512, 186);
            label1.TabIndex = 14;
            label1.Text = "Карты на руке игрока";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // ShowCardsForm
            // 
            AutoScaleDimensions = new SizeF(17F, 41F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 1078);
            Controls.Add(label1);
            Controls.Add(cards_box);
            Name = "ShowCardsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ShowCardsForm";
            ResumeLayout(false);
        }

        #endregion

        private ListBox cards_box;
        private Label label1;
    }
}