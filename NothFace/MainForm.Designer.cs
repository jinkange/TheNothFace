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
            Start1 = new Button();
            passwordBox1 = new TextBox();
            passwordText = new Label();
            sizeText = new Label();
            sizeBox1 = new TextBox();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            label1 = new Label();
            sizeBox2 = new TextBox();
            passwordBox2 = new TextBox();
            label2 = new Label();
            groupBox3 = new GroupBox();
            label3 = new Label();
            sizeBox3 = new TextBox();
            passwordBox3 = new TextBox();
            label4 = new Label();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // Start1
            // 
            Start1.Location = new Point(12, 105);
            Start1.Name = "Start1";
            Start1.Size = new Size(549, 23);
            Start1.TabIndex = 0;
            Start1.Text = "Start";
            Start1.UseVisualStyleBackColor = true;
            Start1.Click += Start1_Click;
            // 
            // passwordBox1
            // 
            passwordBox1.Location = new Point(63, 20);
            passwordBox1.Name = "passwordBox1";
            passwordBox1.PlaceholderText = "6자리숫자";
            passwordBox1.Size = new Size(100, 23);
            passwordBox1.TabIndex = 2;
            passwordBox1.TextChanged += textBox1_TextChanged;
            // 
            // passwordText
            // 
            passwordText.AutoSize = true;
            passwordText.Location = new Point(6, 23);
            passwordText.Name = "passwordText";
            passwordText.Size = new Size(51, 15);
            passwordText.TabIndex = 3;
            passwordText.Text = "NPayPw";
            // 
            // sizeText
            // 
            sizeText.AutoSize = true;
            sizeText.Location = new Point(14, 53);
            sizeText.Name = "sizeText";
            sizeText.Size = new Size(43, 15);
            sizeText.TabIndex = 8;
            sizeText.Text = "사이즈";
            // 
            // sizeBox1
            // 
            sizeBox1.Location = new Point(63, 50);
            sizeBox1.Name = "sizeBox1";
            sizeBox1.PlaceholderText = "xs,s,m,l,xl,xxl";
            sizeBox1.Size = new Size(100, 23);
            sizeBox1.TabIndex = 9;
            sizeBox1.TextChanged += textBox1_TextChanged_1;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(passwordText);
            groupBox1.Controls.Add(sizeBox1);
            groupBox1.Controls.Add(passwordBox1);
            groupBox1.Controls.Add(sizeText);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(179, 87);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            groupBox1.Text = "1번 매크로";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(sizeBox2);
            groupBox2.Controls.Add(passwordBox2);
            groupBox2.Controls.Add(label2);
            groupBox2.Location = new Point(197, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(177, 87);
            groupBox2.TabIndex = 11;
            groupBox2.TabStop = false;
            groupBox2.Text = "2번 매크로";
            groupBox2.Enter += groupBox2_Enter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 23);
            label1.Name = "label1";
            label1.Size = new Size(51, 15);
            label1.TabIndex = 3;
            label1.Text = "NPayPw";
            label1.Click += label1_Click;
            // 
            // sizeBox2
            // 
            sizeBox2.Location = new Point(63, 50);
            sizeBox2.Name = "sizeBox2";
            sizeBox2.PlaceholderText = "xs,s,m,l,xl,xxl";
            sizeBox2.Size = new Size(100, 23);
            sizeBox2.TabIndex = 9;
            // 
            // passwordBox2
            // 
            passwordBox2.Location = new Point(63, 20);
            passwordBox2.Name = "passwordBox2";
            passwordBox2.PlaceholderText = "6자리숫자";
            passwordBox2.Size = new Size(100, 23);
            passwordBox2.TabIndex = 2;
            passwordBox2.TextChanged += passwordBox2_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 53);
            label2.Name = "label2";
            label2.Size = new Size(43, 15);
            label2.TabIndex = 8;
            label2.Text = "사이즈";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(label3);
            groupBox3.Controls.Add(sizeBox3);
            groupBox3.Controls.Add(passwordBox3);
            groupBox3.Controls.Add(label4);
            groupBox3.Location = new Point(380, 12);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(181, 87);
            groupBox3.TabIndex = 12;
            groupBox3.TabStop = false;
            groupBox3.Text = "3번 매크로";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 23);
            label3.Name = "label3";
            label3.Size = new Size(51, 15);
            label3.TabIndex = 3;
            label3.Text = "NPayPw";
            // 
            // sizeBox3
            // 
            sizeBox3.Location = new Point(63, 50);
            sizeBox3.Name = "sizeBox3";
            sizeBox3.PlaceholderText = "xs,s,m,l,xl,xxl";
            sizeBox3.Size = new Size(100, 23);
            sizeBox3.TabIndex = 9;
            // 
            // passwordBox3
            // 
            passwordBox3.Location = new Point(63, 20);
            passwordBox3.Name = "passwordBox3";
            passwordBox3.PlaceholderText = "6자리숫자";
            passwordBox3.Size = new Size(100, 23);
            passwordBox3.TabIndex = 2;
            passwordBox3.TextChanged += passwordBox3_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(14, 53);
            label4.Name = "label4";
            label4.Size = new Size(43, 15);
            label4.TabIndex = 8;
            label4.Text = "사이즈";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(575, 138);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(Start1);
            Controls.Add(groupBox1);
            Name = "MainForm";
            Text = "TheNothFace";
            FormClosing += TNF_FormClosing;
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button Start1;
        private TextBox passwordBox1;
        private Label passwordText;
        private Label sizeText;
        private TextBox sizeBox1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private TextBox sizeBox2;
        private TextBox passwordBox2;
        private Label label2;
        private GroupBox groupBox3;
        private Label label3;
        private TextBox sizeBox3;
        private TextBox passwordBox3;
        private Label label4;
    }
}