namespace GUI
{
    partial class CrabForm
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
            choose_button = new Button();
            choose_box = new ListBox();
            discard2_button = new Button();
            discard1_button = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // choose_button
            // 
            choose_button.Location = new Point(657, 520);
            choose_button.Margin = new Padding(3, 4, 3, 4);
            choose_button.Name = "choose_button";
            choose_button.Size = new Size(368, 132);
            choose_button.TabIndex = 13;
            choose_button.Text = "Выбрать";
            choose_button.UseVisualStyleBackColor = true;
            choose_button.Visible = false;
            choose_button.Click += choose_button_Click;
            // 
            // choose_box
            // 
            choose_box.Font = new Font("Segoe UI", 9.900001F, FontStyle.Regular, GraphicsUnit.Point, 204);
            choose_box.FormattingEnabled = true;
            choose_box.ItemHeight = 45;
            choose_box.Location = new Point(95, 228);
            choose_box.Margin = new Padding(3, 4, 3, 4);
            choose_box.Name = "choose_box";
            choose_box.Size = new Size(512, 679);
            choose_box.TabIndex = 12;
            choose_box.Visible = false;
            // 
            // discard2_button
            // 
            discard2_button.Location = new Point(95, 577);
            discard2_button.Name = "discard2_button";
            discard2_button.Size = new Size(930, 330);
            discard2_button.TabIndex = 14;
            discard2_button.Text = "Сброс 2";
            discard2_button.UseVisualStyleBackColor = true;
            discard2_button.Click += discard2_button_Click;
            // 
            // discard1_button
            // 
            discard1_button.Location = new Point(95, 228);
            discard1_button.Name = "discard1_button";
            discard1_button.Size = new Size(930, 330);
            discard1_button.TabIndex = 15;
            discard1_button.Text = "Сброс 1";
            discard1_button.UseVisualStyleBackColor = true;
            discard1_button.Click += discard1_button_Click;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 11.1F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.Location = new Point(95, 69);
            label1.Name = "label1";
            label1.Size = new Size(930, 134);
            label1.TabIndex = 16;
            label1.Text = "Выберите сброс, из которого хотите взять карту";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // CrabForm
            // 
            AutoScaleDimensions = new SizeF(17F, 41F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1145, 992);
            Controls.Add(label1);
            Controls.Add(discard1_button);
            Controls.Add(discard2_button);
            Controls.Add(choose_button);
            Controls.Add(choose_box);
            Name = "CrabForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form3";
            ResumeLayout(false);
        }

        #endregion

        private Button choose_button;
        private ListBox choose_box;
        private Button discard2_button;
        private Button discard1_button;
        private Label label1;
    }
}