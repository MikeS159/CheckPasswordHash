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
            this.addHash_Btn = new System.Windows.Forms.Button();
            this.hint_TB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.passwordRetype_TB = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.passwordAndHints_LB = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bugReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readMeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.iNeedHashFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFile_Btn
            // 
            this.openFile_Btn.Location = new System.Drawing.Point(12, 27);
            this.openFile_Btn.Name = "openFile_Btn";
            this.openFile_Btn.Size = new System.Drawing.Size(75, 46);
            this.openFile_Btn.TabIndex = 7;
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
            this.password_TB.Location = new System.Drawing.Point(79, 89);
            this.password_TB.Name = "password_TB";
            this.password_TB.PasswordChar = '*';
            this.password_TB.Size = new System.Drawing.Size(260, 20);
            this.password_TB.TabIndex = 1;
            // 
            // checkHash_Btn
            // 
            this.checkHash_Btn.Location = new System.Drawing.Point(12, 210);
            this.checkHash_Btn.Name = "checkHash_Btn";
            this.checkHash_Btn.Size = new System.Drawing.Size(91, 23);
            this.checkHash_Btn.TabIndex = 5;
            this.checkHash_Btn.Text = "Check Hashs";
            this.checkHash_Btn.UseVisualStyleBackColor = true;
            this.checkHash_Btn.Click += new System.EventHandler(this.checkHash_Btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 250);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Lines Read = ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(90, 250);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "0";
            // 
            // addHash_Btn
            // 
            this.addHash_Btn.Location = new System.Drawing.Point(12, 181);
            this.addHash_Btn.Name = "addHash_Btn";
            this.addHash_Btn.Size = new System.Drawing.Size(91, 23);
            this.addHash_Btn.TabIndex = 4;
            this.addHash_Btn.Text = "Add Hash";
            this.addHash_Btn.UseVisualStyleBackColor = true;
            this.addHash_Btn.Click += new System.EventHandler(this.addHash_Btn_Click);
            // 
            // hint_TB
            // 
            this.hint_TB.Location = new System.Drawing.Point(79, 144);
            this.hint_TB.MaxLength = 100;
            this.hint_TB.Name = "hint_TB";
            this.hint_TB.Size = new System.Drawing.Size(260, 20);
            this.hint_TB.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Password";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Hint";
            // 
            // passwordRetype_TB
            // 
            this.passwordRetype_TB.Location = new System.Drawing.Point(79, 115);
            this.passwordRetype_TB.Name = "passwordRetype_TB";
            this.passwordRetype_TB.PasswordChar = '*';
            this.passwordRetype_TB.Size = new System.Drawing.Size(260, 20);
            this.passwordRetype_TB.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Re-type";
            // 
            // passwordAndHints_LB
            // 
            this.passwordAndHints_LB.FormattingEnabled = true;
            this.passwordAndHints_LB.Location = new System.Drawing.Point(393, 27);
            this.passwordAndHints_LB.Name = "passwordAndHints_LB";
            this.passwordAndHints_LB.Size = new System.Drawing.Size(985, 355);
            this.passwordAndHints_LB.TabIndex = 12;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.iNeedHashFilesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1390, 24);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bugReportToolStripMenuItem,
            this.readMeToolStripMenuItem,
            this.aboutToolStripMenuItem1});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.aboutToolStripMenuItem.Text = "Help";
            // 
            // bugReportToolStripMenuItem
            // 
            this.bugReportToolStripMenuItem.Name = "bugReportToolStripMenuItem";
            this.bugReportToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.bugReportToolStripMenuItem.Text = "Bug Report";
            this.bugReportToolStripMenuItem.Click += new System.EventHandler(this.bugReportToolStripMenuItem_Click);
            // 
            // readMeToolStripMenuItem
            // 
            this.readMeToolStripMenuItem.Name = "readMeToolStripMenuItem";
            this.readMeToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.readMeToolStripMenuItem.Text = "Read Me";
            this.readMeToolStripMenuItem.Click += new System.EventHandler(this.readMeToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(133, 22);
            this.aboutToolStripMenuItem1.Text = "About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // iNeedHashFilesToolStripMenuItem
            // 
            this.iNeedHashFilesToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.iNeedHashFilesToolStripMenuItem.Name = "iNeedHashFilesToolStripMenuItem";
            this.iNeedHashFilesToolStripMenuItem.Size = new System.Drawing.Size(112, 20);
            this.iNeedHashFilesToolStripMenuItem.Text = "I Need Hash Files!";
            this.iNeedHashFilesToolStripMenuItem.Click += new System.EventHandler(this.iNeedHashFilesToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1390, 409);
            this.Controls.Add(this.passwordAndHints_LB);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.passwordRetype_TB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.hint_TB);
            this.Controls.Add(this.addHash_Btn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkHash_Btn);
            this.Controls.Add(this.password_TB);
            this.Controls.Add(this.openFile_Btn);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
        private System.Windows.Forms.Button addHash_Btn;
        private System.Windows.Forms.TextBox hint_TB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox passwordRetype_TB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox passwordAndHints_LB;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bugReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readMeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem iNeedHashFilesToolStripMenuItem;
    }
}

