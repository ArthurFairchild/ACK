namespace ACKTools
{
    partial class Form2
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
            this.dValues = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.Rush = new System.Windows.Forms.Button();
            this.Face = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dValues
            // 
            this.dValues.Location = new System.Drawing.Point(13, 12);
            this.dValues.Name = "dValues";
            this.dValues.Size = new System.Drawing.Size(85, 23);
            this.dValues.TabIndex = 0;
            this.dValues.Text = "Default";
            this.dValues.UseVisualStyleBackColor = true;
            this.dValues.Click += new System.EventHandler(this.Default_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(13, 51);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(277, 134);
            this.listBox1.TabIndex = 1;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // Rush
            // 
            this.Rush.Location = new System.Drawing.Point(104, 12);
            this.Rush.Name = "Rush";
            this.Rush.Size = new System.Drawing.Size(90, 23);
            this.Rush.TabIndex = 2;
            this.Rush.Text = "Rush";
            this.Rush.UseVisualStyleBackColor = true;
            this.Rush.Click += new System.EventHandler(this.Rush_Click);
            // 
            // Face
            // 
            this.Face.Location = new System.Drawing.Point(200, 12);
            this.Face.Name = "Face";
            this.Face.Size = new System.Drawing.Size(90, 23);
            this.Face.TabIndex = 3;
            this.Face.Text = "Face";
            this.Face.UseVisualStyleBackColor = true;
            this.Face.Click += new System.EventHandler(this.Face_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(297, 51);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(293, 134);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(296, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "My Deck (Hearthpwn Cockatrice)";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(13, 204);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(277, 55);
            this.richTextBox2.TabIndex = 6;
            this.richTextBox2.Text = "";
            // 
            // richTextBox3
            // 
            this.richTextBox3.Location = new System.Drawing.Point(299, 204);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.Size = new System.Drawing.Size(291, 55);
            this.richTextBox3.TabIndex = 7;
            this.richTextBox3.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 188);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Seed";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(299, 188);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Desired Result (seed)";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 481);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.richTextBox3);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.Face);
            this.Controls.Add(this.Rush);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.dValues);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button dValues;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button Rush;
        private System.Windows.Forms.Button Face;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.RichTextBox richTextBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}