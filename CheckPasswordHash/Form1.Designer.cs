namespace CheckPasswordHash
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
            this.openFile_Btn = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.password_TB = new System.Windows.Forms.TextBox();
            this.checkHash_Btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // openFile_Btn
            // 
            this.openFile_Btn.Location = new System.Drawing.Point(12, 12);
            this.openFile_Btn.Name = "openFile_Btn";
            this.openFile_Btn.Size = new System.Drawing.Size(75, 46);
            this.openFile_Btn.TabIndex = 0;
            this.openFile_Btn.Text = "Open Hash File";
            this.openFile_Btn.UseVisualStyleBackColor = true;
            this.openFile_Btn.Click += new System.EventHandler(this.openFile_Btn_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // password_TB
            // 
            this.password_TB.Location = new System.Drawing.Point(12, 74);
            this.password_TB.Name = "password_TB";
            this.password_TB.PasswordChar = '*';
            this.password_TB.Size = new System.Drawing.Size(260, 20);
            this.password_TB.TabIndex = 1;
            // 
            // checkHash_Btn
            // 
            this.checkHash_Btn.Location = new System.Drawing.Point(12, 100);
            this.checkHash_Btn.Name = "checkHash_Btn";
            this.checkHash_Btn.Size = new System.Drawing.Size(75, 23);
            this.checkHash_Btn.TabIndex = 2;
            this.checkHash_Btn.Text = "Check Hash";
            this.checkHash_Btn.UseVisualStyleBackColor = true;
            this.checkHash_Btn.Click += new System.EventHandler(this.checkHash_Btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 147);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Lines Read = ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(91, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkHash_Btn);
            this.Controls.Add(this.password_TB);
            this.Controls.Add(this.openFile_Btn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button openFile_Btn;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox password_TB;
        private System.Windows.Forms.Button checkHash_Btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

