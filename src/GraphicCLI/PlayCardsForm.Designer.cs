namespace GUI
{
    partial class PlayCardsForm
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
            choose_button = new Button();
            choose_box = new ListBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 11.1F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.Location = new Point(107, 77);
            label1.Name = "label1";
            label1.Size = new Size(930, 134);
            label1.TabIndex = 19;
            label1.Text = "Выберите две карты, которые хотите сыграть";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // choose_button
            // 
            choose_button.Location = new Point(669, 528);
            choose_button.Margin = new Padding(3, 4, 3, 4);
            choose_button.Name = "choose_button";
            choose_button.Size = new Size(368, 132);
            choose_button.TabIndex = 18;
            choose_button.Text = "Выбрать";
            choose_button.UseVisualStyleBackColor = true;
            choose_button.Click += choose_button_Click;
            // 
            // choose_box
            // 
            choose_box.Font = new Font("Segoe UI", 9.900001F, FontStyle.Regular, GraphicsUnit.Point, 204);
            choose_box.FormattingEnabled = true;
            choose_box.ItemHeight = 45;
            choose_box.Location = new Point(107, 236);
            choose_box.Margin = new Padding(3, 4, 3, 4);
            choose_box.Name = "choose_box";
            choose_box.SelectionMode = SelectionMode.MultiSimple;
            choose_box.Size = new Size(512, 679);
            choose_box.TabIndex = 17;
            // 
            // PlayCardsForm
            // 
            AutoScaleDimensions = new SizeF(17F, 41F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1145, 992);
            Controls.Add(label1);
            Controls.Add(choose_button);
            Controls.Add(choose_box);
            Name = "PlayCardsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PlayCardsForm";
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Button choose_button;
        private ListBox choose_box;
    }
}