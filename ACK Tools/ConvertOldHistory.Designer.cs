namespace ACKTools
{
    partial class ConvertOldHistory
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
            this.matchHistoryPath = new System.Windows.Forms.TextBox();
            this.matchHistoryLbl = new System.Windows.Forms.Label();
            this.deckPerformancePath = new System.Windows.Forms.TextBox();
            this.deckPerformanceHistoryLbl = new System.Windows.Forms.Label();
            this.matchHistoryPthBtn = new System.Windows.Forms.Button();
            this.DeckPerformanceHistoryBtn = new System.Windows.Forms.Button();
            this.mergeBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.numGamesTxtBox = new System.Windows.Forms.ComboBox();
            this.deckPerformanceHistoryCount = new System.Windows.Forms.TextBox();
            this.matchHistoryCount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // matchHistoryPath
            // 
            this.matchHistoryPath.Location = new System.Drawing.Point(12, 22);
            this.matchHistoryPath.Name = "matchHistoryPath";
            this.matchHistoryPath.ReadOnly = true;
            this.matchHistoryPath.Size = new System.Drawing.Size(282, 20);
            this.matchHistoryPath.TabIndex = 0;
            // 
            // matchHistoryLbl
            // 
            this.matchHistoryLbl.AutoSize = true;
            this.matchHistoryLbl.Location = new System.Drawing.Point(12, 6);
            this.matchHistoryLbl.Name = "matchHistoryLbl";
            this.matchHistoryLbl.Size = new System.Drawing.Size(83, 13);
            this.matchHistoryLbl.TabIndex = 1;
            this.matchHistoryLbl.Text = "MatchHistory.txt";
            // 
            // deckPerformancePath
            // 
            this.deckPerformancePath.Location = new System.Drawing.Point(12, 61);
            this.deckPerformancePath.Name = "deckPerformancePath";
            this.deckPerformancePath.ReadOnly = true;
            this.deckPerformancePath.Size = new System.Drawing.Size(282, 20);
            this.deckPerformancePath.TabIndex = 2;
            // 
            // deckPerformanceHistoryLbl
            // 
            this.deckPerformanceHistoryLbl.AutoSize = true;
            this.deckPerformanceHistoryLbl.Location = new System.Drawing.Point(12, 45);
            this.deckPerformanceHistoryLbl.Name = "deckPerformanceHistoryLbl";
            this.deckPerformanceHistoryLbl.Size = new System.Drawing.Size(139, 13);
            this.deckPerformanceHistoryLbl.TabIndex = 3;
            this.deckPerformanceHistoryLbl.Text = "DeckPerofrmanceHistory.txt";
            // 
            // matchHistoryPthBtn
            // 
            this.matchHistoryPthBtn.Location = new System.Drawing.Point(300, 19);
            this.matchHistoryPthBtn.Name = "matchHistoryPthBtn";
            this.matchHistoryPthBtn.Size = new System.Drawing.Size(75, 23);
            this.matchHistoryPthBtn.TabIndex = 4;
            this.matchHistoryPthBtn.Text = "Browse";
            this.matchHistoryPthBtn.UseVisualStyleBackColor = true;
            this.matchHistoryPthBtn.Click += new System.EventHandler(this.MatchHistoryPthBtn_Click);
            // 
            // DeckPerformanceHistoryBtn
            // 
            this.DeckPerformanceHistoryBtn.Location = new System.Drawing.Point(300, 58);
            this.DeckPerformanceHistoryBtn.Name = "DeckPerformanceHistoryBtn";
            this.DeckPerformanceHistoryBtn.Size = new System.Drawing.Size(75, 23);
            this.DeckPerformanceHistoryBtn.TabIndex = 5;
            this.DeckPerformanceHistoryBtn.Text = "Browse";
            this.DeckPerformanceHistoryBtn.UseVisualStyleBackColor = true;
            this.DeckPerformanceHistoryBtn.Click += new System.EventHandler(this.DeckPerformanceHistoryBtn_Click);
            // 
            // mergeBtn
            // 
            this.mergeBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.mergeBtn.Location = new System.Drawing.Point(300, 89);
            this.mergeBtn.Name = "mergeBtn";
            this.mergeBtn.Size = new System.Drawing.Size(75, 35);
            this.mergeBtn.TabIndex = 6;
            this.mergeBtn.Text = "Merge";
            this.mergeBtn.UseVisualStyleBackColor = false;
            this.mergeBtn.Click += new System.EventHandler(this.MergeBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(201, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Games to Merge";
            // 
            // numGamesTxtBox
            // 
            this.numGamesTxtBox.FormattingEnabled = true;
            this.numGamesTxtBox.Location = new System.Drawing.Point(199, 97);
            this.numGamesTxtBox.Name = "numGamesTxtBox";
            this.numGamesTxtBox.Size = new System.Drawing.Size(95, 21);
            this.numGamesTxtBox.TabIndex = 10;
            // 
            // deckPerformanceHistoryCount
            // 
            this.deckPerformanceHistoryCount.Location = new System.Drawing.Point(101, 97);
            this.deckPerformanceHistoryCount.Name = "deckPerformanceHistoryCount";
            this.deckPerformanceHistoryCount.ReadOnly = true;
            this.deckPerformanceHistoryCount.Size = new System.Drawing.Size(92, 20);
            this.deckPerformanceHistoryCount.TabIndex = 11;
            // 
            // matchHistoryCount
            // 
            this.matchHistoryCount.Location = new System.Drawing.Point(12, 97);
            this.matchHistoryCount.Name = "matchHistoryCount";
            this.matchHistoryCount.ReadOnly = true;
            this.matchHistoryCount.Size = new System.Drawing.Size(83, 20);
            this.matchHistoryCount.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "MH Count";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(109, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "DPH Count";
            // 
            // ConvertOldHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 131);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.matchHistoryCount);
            this.Controls.Add(this.deckPerformanceHistoryCount);
            this.Controls.Add(this.numGamesTxtBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mergeBtn);
            this.Controls.Add(this.DeckPerformanceHistoryBtn);
            this.Controls.Add(this.matchHistoryPthBtn);
            this.Controls.Add(this.deckPerformanceHistoryLbl);
            this.Controls.Add(this.deckPerformancePath);
            this.Controls.Add(this.matchHistoryLbl);
            this.Controls.Add(this.matchHistoryPath);
            this.Name = "ConvertOldHistory";
            this.Text = "ConvertOldHistory";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox matchHistoryPath;
        private System.Windows.Forms.Label matchHistoryLbl;
        private System.Windows.Forms.TextBox deckPerformancePath;
        private System.Windows.Forms.Label deckPerformanceHistoryLbl;
        private System.Windows.Forms.Button matchHistoryPthBtn;
        private System.Windows.Forms.Button DeckPerformanceHistoryBtn;
        private System.Windows.Forms.Button mergeBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox numGamesTxtBox;
        private System.Windows.Forms.TextBox deckPerformanceHistoryCount;
        private System.Windows.Forms.TextBox matchHistoryCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}