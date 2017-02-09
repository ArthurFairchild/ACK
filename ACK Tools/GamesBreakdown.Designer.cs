namespace ACKTools
{
    partial class GamesBreakdown
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
            this.cardPerformanceGrid = new System.Windows.Forms.Panel();
            this.loadHistory = new System.Windows.Forms.Button();
            this.EnemyClassBox = new System.Windows.Forms.ComboBox();
            this.CoinBox = new System.Windows.Forms.ComboBox();
            this.enemyLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cardPerformanceGrid
            // 
            this.cardPerformanceGrid.Location = new System.Drawing.Point(12, 55);
            this.cardPerformanceGrid.Name = "cardPerformanceGrid";
            this.cardPerformanceGrid.Size = new System.Drawing.Size(597, 423);
            this.cardPerformanceGrid.TabIndex = 0;
            // 
            // loadHistory
            // 
            this.loadHistory.Location = new System.Drawing.Point(506, 28);
            this.loadHistory.Name = "loadHistory";
            this.loadHistory.Size = new System.Drawing.Size(103, 23);
            this.loadHistory.TabIndex = 1;
            this.loadHistory.Text = "Apply";
            this.loadHistory.UseVisualStyleBackColor = true;
            this.loadHistory.Click += new System.EventHandler(this.loadHistory_Click);
            // 
            // EnemyClassBox
            // 
            this.EnemyClassBox.FormattingEnabled = true;
            this.EnemyClassBox.Location = new System.Drawing.Point(12, 28);
            this.EnemyClassBox.Name = "EnemyClassBox";
            this.EnemyClassBox.Size = new System.Drawing.Size(145, 21);
            this.EnemyClassBox.TabIndex = 2;
            // 
            // CoinBox
            // 
            this.CoinBox.FormattingEnabled = true;
            this.CoinBox.Location = new System.Drawing.Point(163, 28);
            this.CoinBox.Name = "CoinBox";
            this.CoinBox.Size = new System.Drawing.Size(87, 21);
            this.CoinBox.TabIndex = 3;
            // 
            // enemyLabel
            // 
            this.enemyLabel.AutoSize = true;
            this.enemyLabel.Location = new System.Drawing.Point(12, 12);
            this.enemyLabel.Name = "enemyLabel";
            this.enemyLabel.Size = new System.Drawing.Size(67, 13);
            this.enemyLabel.TabIndex = 4;
            this.enemyLabel.Text = "Enemy Class";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(160, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Coin";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(259, 28);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(115, 21);
            this.comboBox1.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(256, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Number of Games";
            // 
            // GamesBreakdown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 573);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.enemyLabel);
            this.Controls.Add(this.CoinBox);
            this.Controls.Add(this.EnemyClassBox);
            this.Controls.Add(this.loadHistory);
            this.Controls.Add(this.cardPerformanceGrid);
            this.Name = "GamesBreakdown";
            this.Text = "GamesBreakdown";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel cardPerformanceGrid;
        private System.Windows.Forms.Button loadHistory;
        private System.Windows.Forms.ComboBox EnemyClassBox;
        private System.Windows.Forms.ComboBox CoinBox;
        private System.Windows.Forms.Label enemyLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
    }
}