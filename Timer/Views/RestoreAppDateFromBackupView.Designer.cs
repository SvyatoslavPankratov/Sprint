namespace Sprint.Views
{
    partial class RestoreAppDateFromBackupView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RestoreAppDateFromBackupView));
            this.panel12 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.panel16 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.fileNames_lb = new System.Windows.Forms.ListBox();
            this.panel12.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel16.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel12.Controls.Add(this.panel1);
            this.panel12.Controls.Add(this.panel16);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel12.Location = new System.Drawing.Point(0, 364);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(440, 65);
            this.panel12.TabIndex = 9;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(10);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(178, 65);
            this.panel1.TabIndex = 4;
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Left;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(10, 10);
            this.button2.Name = "button2";
            this.button2.Padding = new System.Windows.Forms.Padding(10);
            this.button2.Size = new System.Drawing.Size(147, 45);
            this.button2.TabIndex = 2;
            this.button2.Text = "Восстановить";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel16
            // 
            this.panel16.Controls.Add(this.button1);
            this.panel16.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel16.Location = new System.Drawing.Point(290, 0);
            this.panel16.Margin = new System.Windows.Forms.Padding(10);
            this.panel16.Name = "panel16";
            this.panel16.Padding = new System.Windows.Forms.Padding(10);
            this.panel16.Size = new System.Drawing.Size(150, 65);
            this.panel16.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(22, 10);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(10);
            this.button1.Size = new System.Drawing.Size(118, 45);
            this.button1.TabIndex = 2;
            this.button1.Text = "Закрыть";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(416, 69);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Описание";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(395, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "Пожалуйста, выберите требуемую резервную копию для восстановления.\r\nПрограмма про" +
    "изведет попытку восстановить данные из резервной копии.\r\nПосле выполнения данной" +
    " операции, программа перезапустит себя.";
            // 
            // fileNames_lb
            // 
            this.fileNames_lb.FormattingEnabled = true;
            this.fileNames_lb.Location = new System.Drawing.Point(12, 87);
            this.fileNames_lb.Name = "fileNames_lb";
            this.fileNames_lb.Size = new System.Drawing.Size(416, 264);
            this.fileNames_lb.TabIndex = 11;
            // 
            // RestoreAppDateFromBackupView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 429);
            this.Controls.Add(this.fileNames_lb);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel12);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RestoreAppDateFromBackupView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Восстановление данных программы из резервной копии";
            this.panel12.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel16.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox fileNames_lb;
    }
}