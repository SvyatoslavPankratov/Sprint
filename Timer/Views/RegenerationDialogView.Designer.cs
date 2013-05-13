namespace Sprint.Views
{
    partial class RegenerationDialogView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegenerationDialogView));
            this.panel12 = new System.Windows.Forms.Panel();
            this.panel16 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.load_rb = new System.Windows.Forms.RadioButton();
            this.null_rb = new System.Windows.Forms.RadioButton();
            this.all_rb = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel12.SuspendLayout();
            this.panel16.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel12.Controls.Add(this.panel16);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel12.Location = new System.Drawing.Point(0, 212);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(462, 65);
            this.panel12.TabIndex = 9;
            // 
            // panel16
            // 
            this.panel16.Controls.Add(this.button1);
            this.panel16.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel16.Location = new System.Drawing.Point(295, 0);
            this.panel16.Margin = new System.Windows.Forms.Padding(10);
            this.panel16.Name = "panel16";
            this.panel16.Padding = new System.Windows.Forms.Padding(10);
            this.panel16.Size = new System.Drawing.Size(167, 65);
            this.panel16.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(11, 10);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(10);
            this.button1.Size = new System.Drawing.Size(146, 45);
            this.button1.TabIndex = 2;
            this.button1.Text = "Восстановить";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.load_rb);
            this.groupBox1.Controls.Add(this.null_rb);
            this.groupBox1.Controls.Add(this.all_rb);
            this.groupBox1.Location = new System.Drawing.Point(12, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(438, 149);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Варианты возобновления работы";
            // 
            // load_rb
            // 
            this.load_rb.Checked = true;
            this.load_rb.Location = new System.Drawing.Point(11, 101);
            this.load_rb.Name = "load_rb";
            this.load_rb.Size = new System.Drawing.Size(421, 38);
            this.load_rb.TabIndex = 2;
            this.load_rb.TabStop = true;
            this.load_rb.Text = "Участники еще не начали заезд, в связи с чем, необходимо только восстановить спис" +
    "ки участников и настройки приложения";
            this.load_rb.UseVisualStyleBackColor = true;
            // 
            // null_rb
            // 
            this.null_rb.Location = new System.Drawing.Point(11, 57);
            this.null_rb.Name = "null_rb";
            this.null_rb.Size = new System.Drawing.Size(421, 38);
            this.null_rb.TabIndex = 1;
            this.null_rb.Text = "Полный перезаезд только не удавшихся кругов у участников, которые были на треке, " +
    "когда произошел сбой";
            this.null_rb.UseVisualStyleBackColor = true;
            // 
            // all_rb
            // 
            this.all_rb.Location = new System.Drawing.Point(11, 13);
            this.all_rb.Name = "all_rb";
            this.all_rb.Size = new System.Drawing.Size(421, 38);
            this.all_rb.TabIndex = 0;
            this.all_rb.Text = "Полный перезаезд всех кругов у участников, которые были на треке, когда произошел" +
    " сбой";
            this.all_rb.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(442, 21);
            this.label1.TabIndex = 11;
            this.label1.Text = "Пожалуйста, укажите вариант восстановления работы приложения";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(462, 45);
            this.panel2.TabIndex = 12;
            // 
            // RegenerationDialogView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 277);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel12);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RegenerationDialogView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Восстановление состояния приложения";
            this.TopMost = true;
            this.panel12.ResumeLayout(false);
            this.panel16.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton all_rb;
        private System.Windows.Forms.RadioButton load_rb;
        private System.Windows.Forms.RadioButton null_rb;
    }
}