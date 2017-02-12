namespace ACKTools
{
    partial class HistoryDebugger
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
            this.GameHistory = new System.Windows.Forms.CheckedListBox();
            this.SimpleClassifyField = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.HearthpwnRichTextBox = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.SImple = new System.Windows.Forms.TabPage();
            this.DeckTrackerRichBox = new System.Windows.Forms.RichTextBox();
            this.Detailed = new System.Windows.Forms.TabPage();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.VariablesRichTextBox = new System.Windows.Forms.RichTextBox();
            this.ClassificationTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.wrongClassificationPath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.matchHistoryPath2 = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.SImple.SuspendLayout();
            this.Detailed.SuspendLayout();
            this.SuspendLayout();
            // 
            // GameHistory
            // 
            this.GameHistory.FormattingEnabled = true;
            this.GameHistory.Location = new System.Drawing.Point(12, 28);
            this.GameHistory.Name = "GameHistory";
            this.GameHistory.Size = new System.Drawing.Size(192, 409);
            this.GameHistory.TabIndex = 32;
            this.GameHistory.SelectedIndexChanged += new System.EventHandler(this.GameHistory_SelectedIndexChanged);
            // 
            // SimpleClassifyField
            // 
            this.SimpleClassifyField.Location = new System.Drawing.Point(4, 31);
            this.SimpleClassifyField.Name = "SimpleClassifyField";
            this.SimpleClassifyField.ReadOnly = true;
            this.SimpleClassifyField.Size = new System.Drawing.Size(178, 20);
            this.SimpleClassifyField.TabIndex = 34;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button3.Location = new System.Drawing.Point(190, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(125, 23);
            this.button3.TabIndex = 23;
            this.button3.Text = "Correct Classification";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button4.Location = new System.Drawing.Point(190, 29);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(125, 23);
            this.button4.TabIndex = 6;
            this.button4.Text = "Incorrect Classification";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 34;
            this.label1.Text = "Classified As";
            // 
            // HearthpwnRichTextBox
            // 
            this.HearthpwnRichTextBox.Location = new System.Drawing.Point(6, 95);
            this.HearthpwnRichTextBox.Name = "HearthpwnRichTextBox";
            this.HearthpwnRichTextBox.Size = new System.Drawing.Size(176, 301);
            this.HearthpwnRichTextBox.TabIndex = 36;
            this.HearthpwnRichTextBox.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 37;
            this.label2.Text = "DeckList";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.SImple);
            this.tabControl1.Controls.Add(this.Detailed);
            this.tabControl1.Location = new System.Drawing.Point(210, 9);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(329, 428);
            this.tabControl1.TabIndex = 39;
            // 
            // SImple
            // 
            this.SImple.Controls.Add(this.DeckTrackerRichBox);
            this.SImple.Controls.Add(this.label1);
            this.SImple.Controls.Add(this.SimpleClassifyField);
            this.SImple.Controls.Add(this.HearthpwnRichTextBox);
            this.SImple.Controls.Add(this.label2);
            this.SImple.Controls.Add(this.button3);
            this.SImple.Controls.Add(this.button4);
            this.SImple.Location = new System.Drawing.Point(4, 22);
            this.SImple.Name = "SImple";
            this.SImple.Padding = new System.Windows.Forms.Padding(3);
            this.SImple.Size = new System.Drawing.Size(321, 402);
            this.SImple.TabIndex = 0;
            this.SImple.Text = "Simple";
            this.SImple.UseVisualStyleBackColor = true;
            // 
            // DeckTrackerRichBox
            // 
            this.DeckTrackerRichBox.Location = new System.Drawing.Point(188, 95);
            this.DeckTrackerRichBox.Name = "DeckTrackerRichBox";
            this.DeckTrackerRichBox.Size = new System.Drawing.Size(127, 301);
            this.DeckTrackerRichBox.TabIndex = 38;
            this.DeckTrackerRichBox.Text = "";
            // 
            // Detailed
            // 
            this.Detailed.Controls.Add(this.button6);
            this.Detailed.Controls.Add(this.button5);
            this.Detailed.Controls.Add(this.label4);
            this.Detailed.Controls.Add(this.VariablesRichTextBox);
            this.Detailed.Controls.Add(this.ClassificationTextBox);
            this.Detailed.Controls.Add(this.label3);
            this.Detailed.Location = new System.Drawing.Point(4, 22);
            this.Detailed.Name = "Detailed";
            this.Detailed.Padding = new System.Windows.Forms.Padding(3);
            this.Detailed.Size = new System.Drawing.Size(321, 402);
            this.Detailed.TabIndex = 1;
            this.Detailed.Text = "Detailed";
            this.Detailed.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button6.Location = new System.Drawing.Point(190, 29);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(125, 23);
            this.button6.TabIndex = 40;
            this.button6.Text = "Incorrect Classification";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button5.Location = new System.Drawing.Point(190, 3);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(125, 23);
            this.button5.TabIndex = 39;
            this.button5.Text = "Correct Classification";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 34;
            this.label4.Text = "Variables";
            // 
            // VariablesRichTextBox
            // 
            this.VariablesRichTextBox.Location = new System.Drawing.Point(6, 74);
            this.VariablesRichTextBox.Name = "VariablesRichTextBox";
            this.VariablesRichTextBox.ReadOnly = true;
            this.VariablesRichTextBox.Size = new System.Drawing.Size(309, 322);
            this.VariablesRichTextBox.TabIndex = 34;
            this.VariablesRichTextBox.Text = "";
            this.VariablesRichTextBox.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // ClassificationTextBox
            // 
            this.ClassificationTextBox.Location = new System.Drawing.Point(6, 31);
            this.ClassificationTextBox.Name = "ClassificationTextBox";
            this.ClassificationTextBox.ReadOnly = true;
            this.ClassificationTextBox.Size = new System.Drawing.Size(178, 20);
            this.ClassificationTextBox.TabIndex = 34;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 35;
            this.label3.Text = "Classified As";
            // 
            // wrongClassificationPath
            // 
            this.wrongClassificationPath.Location = new System.Drawing.Point(12, 458);
            this.wrongClassificationPath.Name = "wrongClassificationPath";
            this.wrongClassificationPath.ReadOnly = true;
            this.wrongClassificationPath.Size = new System.Drawing.Size(517, 20);
            this.wrongClassificationPath.TabIndex = 40;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 444);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 13);
            this.label5.TabIndex = 41;
            this.label5.Text = "Reviewed Decks";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 481);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 13);
            this.label6.TabIndex = 42;
            this.label6.Text = "MatchHistory Path";
            // 
            // matchHistoryPath2
            // 
            this.matchHistoryPath2.Location = new System.Drawing.Point(12, 497);
            this.matchHistoryPath2.Name = "matchHistoryPath2";
            this.matchHistoryPath2.ReadOnly = true;
            this.matchHistoryPath2.Size = new System.Drawing.Size(517, 20);
            this.matchHistoryPath2.TabIndex = 43;
            // 
            // HistoryDebugger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 528);
            this.Controls.Add(this.matchHistoryPath2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.wrongClassificationPath);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.GameHistory);
            this.Name = "HistoryDebugger";
            this.Text = "HistoryDebugger";
            this.Load += new System.EventHandler(this.HistoryDebugger_Load);
            this.tabControl1.ResumeLayout(false);
            this.SImple.ResumeLayout(false);
            this.SImple.PerformLayout();
            this.Detailed.ResumeLayout(false);
            this.Detailed.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox GameHistory;
        private System.Windows.Forms.TextBox SimpleClassifyField;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox HearthpwnRichTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage SImple;
        private System.Windows.Forms.TabPage Detailed;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ClassificationTextBox;
        private System.Windows.Forms.RichTextBox VariablesRichTextBox;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox DeckTrackerRichBox;
        private System.Windows.Forms.TextBox wrongClassificationPath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox matchHistoryPath2;
    }
}