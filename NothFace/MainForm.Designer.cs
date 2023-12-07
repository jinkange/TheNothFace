namespace NothFace
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private WindowUtile WindowUtile = new WindowUtile();

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Start = new Button();
            Stop = new Button();
            passwordBox = new TextBox();
            passwordText = new Label();
            countText = new Label();
            countBox = new TextBox();
            macroIdBox = new TextBox();
            macroIdText = new Label();
            sizeText = new Label();
            sizeBox = new TextBox();
            SuspendLayout();
            // 
            // Start
            // 
            Start.Location = new Point(12, 156);
            Start.Name = "Start";
            Start.Size = new Size(211, 23);
            Start.TabIndex = 0;
            Start.Text = "Start";
            Start.UseVisualStyleBackColor = true;
            Start.Click += Start_Click;
            // 
            // Stop
            // 
            Stop.Location = new Point(12, 185);
            Stop.Name = "Stop";
            Stop.Size = new Size(210, 23);
            Stop.TabIndex = 1;
            Stop.Text = "Stop";
            Stop.UseVisualStyleBackColor = true;
            Stop.Click += Stop_Click;
            // 
            // passwordBox
            // 
            passwordBox.Location = new Point(122, 36);
            passwordBox.Name = "passwordBox";
            passwordBox.PlaceholderText = "6자리숫자";
            passwordBox.Size = new Size(100, 23);
            passwordBox.TabIndex = 2;
            passwordBox.TextChanged += textBox1_TextChanged;
            // 
            // passwordText
            // 
            passwordText.AutoSize = true;
            passwordText.Location = new Point(12, 39);
            passwordText.Name = "passwordText";
            passwordText.Size = new Size(55, 15);
            passwordText.TabIndex = 3;
            passwordText.Text = "비밀번호";
            passwordText.Click += password_Click;
            // 
            // countText
            // 
            countText.AutoSize = true;
            countText.Location = new Point(11, 68);
            countText.Name = "countText";
            countText.Size = new Size(55, 15);
            countText.TabIndex = 4;
            countText.Text = "구매수량";
            countText.Click += count_Click;
            // 
            // countBox
            // 
            countBox.Location = new Point(122, 65);
            countBox.Name = "countBox";
            countBox.Size = new Size(100, 23);
            countBox.TabIndex = 5;
            countBox.TextChanged += textBox2_TextChanged;
            // 
            // macroIdBox
            // 
            macroIdBox.Location = new Point(122, 6);
            macroIdBox.MaxLength = 1;
            macroIdBox.Name = "macroIdBox";
            macroIdBox.PlaceholderText = "1~3";
            macroIdBox.Size = new Size(100, 23);
            macroIdBox.TabIndex = 6;
            macroIdBox.TextChanged += macroId_TextChanged;
            // 
            // macroIdText
            // 
            macroIdText.AutoSize = true;
            macroIdText.Location = new Point(12, 9);
            macroIdText.Name = "macroIdText";
            macroIdText.Size = new Size(67, 15);
            macroIdText.TabIndex = 7;
            macroIdText.Text = "매크로번호";
            macroIdText.Click += id_Click;
            // 
            // sizeText
            // 
            sizeText.AutoSize = true;
            sizeText.Location = new Point(12, 97);
            sizeText.Name = "sizeText";
            sizeText.Size = new Size(43, 15);
            sizeText.TabIndex = 8;
            sizeText.Text = "사이즈";
            // 
            // sizeBox
            // 
            sizeBox.Location = new Point(122, 94);
            sizeBox.Name = "sizeBox";
            sizeBox.PlaceholderText = "xs,s,m,l,xl,xxl";
            sizeBox.Size = new Size(100, 23);
            sizeBox.TabIndex = 9;
            sizeBox.TextChanged += textBox1_TextChanged_1;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(234, 220);
            Controls.Add(sizeBox);
            Controls.Add(sizeText);
            Controls.Add(macroIdText);
            Controls.Add(macroIdBox);
            Controls.Add(countBox);
            Controls.Add(countText);
            Controls.Add(passwordText);
            Controls.Add(passwordBox);
            Controls.Add(Stop);
            Controls.Add(Start);
            Name = "MainForm";
            Text = "TheNothFace";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Start;
        private Button Stop;
        private TextBox passwordBox;
        private Label passwordText;
        private Label countText;
        private TextBox countBox;
        private TextBox macroIdBox;
        private Label macroIdText;
        private Label sizeText;
        private TextBox sizeBox;
    }
}