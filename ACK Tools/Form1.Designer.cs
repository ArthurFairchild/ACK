namespace ACKTools
{
    partial class Form1
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
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.VariablesRichTextBox = new System.Windows.Forms.RichTextBox();
            this.MulliganCoreRichTextBox = new System.Windows.Forms.RichTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label8 = new System.Windows.Forms.Label();
            this.ReportRichTextBox = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SaveToFileBox = new System.Windows.Forms.CheckBox();
            this.SaveVariablesBox = new System.Windows.Forms.CheckBox();
            this.ClassificationTextBox = new System.Windows.Forms.TextBox();
            this.RecomendedName = new System.Windows.Forms.TextBox();
            this.NotesAboutTheDeck = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.HearthpwnRichTextBox = new System.Windows.Forms.RichTextBox();
            this.DeckTrackerRichBox = new System.Windows.Forms.RichTextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.GameHistory = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.History;
            this.folderBrowserDialog1.SelectedPath = "C:\\Users\\artur\\Desktop\\sb-v40.8\\Logs\\ACKTracker";
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(647, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(195, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "File Save Location (Default, Permanent)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(647, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Variables";
            // 
            // textBox2
            // 
            this.textBox2.AccessibleDescription = "";
            this.textBox2.Location = new System.Drawing.Point(646, 28);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(284, 20);
            this.textBox2.TabIndex = 25;
            this.textBox2.Text = "Location of Rich Reports";
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(647, 262);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "Mulligan Core";
            // 
            // VariablesRichTextBox
            // 
            this.VariablesRichTextBox.Location = new System.Drawing.Point(646, 73);
            this.VariablesRichTextBox.Name = "VariablesRichTextBox";
            this.VariablesRichTextBox.ReadOnly = true;
            this.VariablesRichTextBox.Size = new System.Drawing.Size(284, 175);
            this.VariablesRichTextBox.TabIndex = 5;
            this.VariablesRichTextBox.Text = "";
            // 
            // MulliganCoreRichTextBox
            // 
            this.MulliganCoreRichTextBox.Location = new System.Drawing.Point(646, 278);
            this.MulliganCoreRichTextBox.Name = "MulliganCoreRichTextBox";
            this.MulliganCoreRichTextBox.ReadOnly = true;
            this.MulliganCoreRichTextBox.Size = new System.Drawing.Size(281, 207);
            this.MulliganCoreRichTextBox.TabIndex = 21;
            this.MulliganCoreRichTextBox.Text = "";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(546, 262);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(91, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "(Discord Channel)";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(458, 262);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(85, 13);
            this.linkLabel1.TabIndex = 18;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "#Identifier-Dump";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(396, 262);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Post this to";
            // 
            // ReportRichTextBox
            // 
            this.ReportRichTextBox.Location = new System.Drawing.Point(399, 278);
            this.ReportRichTextBox.Name = "ReportRichTextBox";
            this.ReportRichTextBox.Size = new System.Drawing.Size(227, 207);
            this.ReportRichTextBox.TabIndex = 7;
            this.ReportRichTextBox.Text = "";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(399, 120);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Comment";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(399, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Recomended Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(399, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Classified As";
            // 
            // SaveToFileBox
            // 
            this.SaveToFileBox.AutoSize = true;
            this.SaveToFileBox.Checked = true;
            this.SaveToFileBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SaveToFileBox.Location = new System.Drawing.Point(544, 12);
            this.SaveToFileBox.Name = "SaveToFileBox";
            this.SaveToFileBox.Size = new System.Drawing.Size(82, 17);
            this.SaveToFileBox.TabIndex = 24;
            this.SaveToFileBox.Text = "Save to File";
            this.SaveToFileBox.UseVisualStyleBackColor = true;
            this.SaveToFileBox.CheckedChanged += new System.EventHandler(this.SaveBox_CheckedChanged);
            // 
            // SaveVariablesBox
            // 
            this.SaveVariablesBox.AutoSize = true;
            this.SaveVariablesBox.Location = new System.Drawing.Point(399, 12);
            this.SaveVariablesBox.Name = "SaveVariablesBox";
            this.SaveVariablesBox.Size = new System.Drawing.Size(140, 17);
            this.SaveVariablesBox.TabIndex = 10;
            this.SaveVariablesBox.Text = "User Variables in Report";
            this.SaveVariablesBox.UseVisualStyleBackColor = true;
            this.SaveVariablesBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // ClassificationTextBox
            // 
            this.ClassificationTextBox.Location = new System.Drawing.Point(399, 53);
            this.ClassificationTextBox.Name = "ClassificationTextBox";
            this.ClassificationTextBox.ReadOnly = true;
            this.ClassificationTextBox.Size = new System.Drawing.Size(144, 20);
            this.ClassificationTextBox.TabIndex = 4;
            this.ClassificationTextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // RecomendedName
            // 
            this.RecomendedName.Location = new System.Drawing.Point(399, 96);
            this.RecomendedName.Name = "RecomendedName";
            this.RecomendedName.Size = new System.Drawing.Size(144, 20);
            this.RecomendedName.TabIndex = 13;
            // 
            // NotesAboutTheDeck
            // 
            this.NotesAboutTheDeck.Location = new System.Drawing.Point(399, 136);
            this.NotesAboutTheDeck.Name = "NotesAboutTheDeck";
            this.NotesAboutTheDeck.Size = new System.Drawing.Size(227, 112);
            this.NotesAboutTheDeck.TabIndex = 15;
            this.NotesAboutTheDeck.Text = "";
            this.NotesAboutTheDeck.TextChanged += new System.EventHandler(this.NotesAboutTheDeck_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(201, 334);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "ID String from Deck Tracker";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(201, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Deck (From Hearthpwn)";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button2.Location = new System.Drawing.Point(201, 9);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(192, 29);
            this.button2.TabIndex = 27;
            this.button2.Text = "Load History";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.Location = new System.Drawing.Point(201, 39);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(192, 30);
            this.button1.TabIndex = 3;
            this.button1.Text = "Classify (Manual)";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // HearthpwnRichTextBox
            // 
            this.HearthpwnRichTextBox.Location = new System.Drawing.Point(204, 93);
            this.HearthpwnRichTextBox.Name = "HearthpwnRichTextBox";
            this.HearthpwnRichTextBox.Size = new System.Drawing.Size(192, 238);
            this.HearthpwnRichTextBox.TabIndex = 2;
            this.HearthpwnRichTextBox.Text = "";
            // 
            // DeckTrackerRichBox
            // 
            this.DeckTrackerRichBox.Location = new System.Drawing.Point(201, 350);
            this.DeckTrackerRichBox.Name = "DeckTrackerRichBox";
            this.DeckTrackerRichBox.Size = new System.Drawing.Size(192, 89);
            this.DeckTrackerRichBox.TabIndex = 0;
            this.DeckTrackerRichBox.Text = "";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(201, 445);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(192, 40);
            this.button3.TabIndex = 20;
            this.button3.Text = "Clear all fields";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.button5.Location = new System.Drawing.Point(3, 445);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(192, 40);
            this.button5.TabIndex = 32;
            this.button5.Text = "Generate List of Wrong Classifications";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(0, 13);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(195, 13);
            this.label11.TabIndex = 33;
            this.label11.Text = "Marked items are used in Wrong Report";
            // 
            // GameHistory
            // 
            this.GameHistory.FormattingEnabled = true;
            this.GameHistory.Location = new System.Drawing.Point(3, 30);
            this.GameHistory.Name = "GameHistory";
            this.GameHistory.Size = new System.Drawing.Size(192, 409);
            this.GameHistory.TabIndex = 31;
            this.GameHistory.SelectedIndexChanged += new System.EventHandler(this.GameHistory_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AccessibleDescription = "For ";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(938, 491);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.GameHistory);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.SaveToFileBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.MulliganCoreRichTextBox);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.NotesAboutTheDeck);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.RecomendedName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.SaveVariablesBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ReportRichTextBox);
            this.Controls.Add(this.VariablesRichTextBox);
            this.Controls.Add(this.ClassificationTextBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.HearthpwnRichTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DeckTrackerRichBox);
            this.Name = "Form1";
            this.Text = "ACK - Debugger 2.12";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.RichTextBox VariablesRichTextBox;
        private System.Windows.Forms.RichTextBox MulliganCoreRichTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RichTextBox ReportRichTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox SaveToFileBox;
        private System.Windows.Forms.CheckBox SaveVariablesBox;
        private System.Windows.Forms.TextBox ClassificationTextBox;
        private System.Windows.Forms.TextBox RecomendedName;
        private System.Windows.Forms.RichTextBox NotesAboutTheDeck;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox HearthpwnRichTextBox;
        private System.Windows.Forms.RichTextBox DeckTrackerRichBox;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckedListBox GameHistory;
    }
}

