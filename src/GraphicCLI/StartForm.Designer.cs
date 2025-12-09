namespace GUI;

partial class StartForm
{
    /// <summary>
    /// Обязательная переменная конструктора.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Освободить все используемые ресурсы.
    /// </summary>
    /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Код, автоматически созданный конструктором форм Windows

    /// <summary>
    /// Требуемый метод для поддержки конструктора — не изменяйте 
    /// содержимое этого метода с помощью редактора кода.
    /// </summary>
    private void InitializeComponent()
    {
        start_button = new Button();
        continue_button = new Button();
        exit_button = new Button();
        names_textbox = new TextBox();
        label1 = new Label();
        label2 = new Label();
        SuspendLayout();
        // 
        // start_button
        // 
        start_button.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
        start_button.Location = new Point(668, 754);
        start_button.Margin = new Padding(3, 4, 3, 4);
        start_button.Name = "start_button";
        start_button.Size = new Size(595, 209);
        start_button.TabIndex = 0;
        start_button.Text = "Начать новую игру";
        start_button.UseVisualStyleBackColor = true;
        start_button.Click += start_button_Click;
        // 
        // continue_button
        // 
        continue_button.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
        continue_button.Location = new Point(668, 1015);
        continue_button.Margin = new Padding(3, 4, 3, 4);
        continue_button.Name = "continue_button";
        continue_button.Size = new Size(595, 209);
        continue_button.TabIndex = 1;
        continue_button.Text = "Продолжить игру";
        continue_button.UseVisualStyleBackColor = true;
        continue_button.Click += continue_button_Click;
        // 
        // exit_button
        // 
        exit_button.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
        exit_button.Location = new Point(668, 1280);
        exit_button.Margin = new Padding(3, 4, 3, 4);
        exit_button.Name = "exit_button";
        exit_button.Size = new Size(595, 209);
        exit_button.TabIndex = 2;
        exit_button.Text = "Выход";
        exit_button.UseVisualStyleBackColor = true;
        exit_button.Click += exit_button_Click;
        // 
        // names_textbox
        // 
        names_textbox.Font = new Font("Microsoft Sans Serif", 12F);
        names_textbox.Location = new Point(668, 629);
        names_textbox.Margin = new Padding(3, 4, 3, 4);
        names_textbox.Name = "names_textbox";
        names_textbox.Size = new Size(595, 53);
        names_textbox.TabIndex = 3;
        // 
        // label1
        // 
        label1.Font = new Font("Microsoft Sans Serif", 11.1F, FontStyle.Regular, GraphicsUnit.Point, 204);
        label1.Location = new Point(661, 420);
        label1.Name = "label1";
        label1.Size = new Size(595, 186);
        label1.TabIndex = 4;
        label1.Text = "Для начала новой игры введите имена от 2 до 4 игроков через пробел:";
        label1.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // label2
        // 
        label2.Font = new Font("Microsoft Sans Serif", 8.1F, FontStyle.Italic, GraphicsUnit.Point, 204);
        label2.Location = new Point(662, -12);
        label2.Name = "label2";
        label2.Size = new Size(595, 186);
        label2.TabIndex = 5;
        label2.Text = "Море-Соль-Бумага";
        label2.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // StartForm
        // 
        AutoScaleDimensions = new SizeF(17F, 41F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1896, 1788);
        Controls.Add(label2);
        Controls.Add(label1);
        Controls.Add(names_textbox);
        Controls.Add(exit_button);
        Controls.Add(continue_button);
        Controls.Add(start_button);
        Margin = new Padding(3, 4, 3, 4);
        Name = "StartForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Form1";
        ResumeLayout(false);
        PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button start_button;
    private System.Windows.Forms.Button continue_button;
    private System.Windows.Forms.Button exit_button;
    private System.Windows.Forms.TextBox names_textbox;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
}

