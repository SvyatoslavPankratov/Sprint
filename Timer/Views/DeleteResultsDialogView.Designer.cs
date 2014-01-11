namespace Sprint.Views
{
    partial class DeleteResultsDialogView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeleteResultsDialogView));
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel12 = new System.Windows.Forms.Panel();
            this.panel16 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.second_rb = new System.Windows.Forms.RadioButton();
            this.first_rb = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.car_classes_cb = new System.Windows.Forms.ComboBox();
            this.backup_data_cb = new System.Windows.Forms.CheckBox();
            this.panel2.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel16.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(459, 45);
            this.panel2.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(10, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(442, 21);
            this.label1.TabIndex = 11;
            this.label1.Text = "Пожалуйста, укажите вариант удаления результатов у участников";
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel12.Controls.Add(this.panel16);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel12.Location = new System.Drawing.Point(0, 246);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(459, 65);
            this.panel12.TabIndex = 13;
            // 
            // panel16
            // 
            this.panel16.Controls.Add(this.button1);
            this.panel16.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel16.Location = new System.Drawing.Point(238, 0);
            this.panel16.Margin = new System.Windows.Forms.Padding(10);
            this.panel16.Name = "panel16";
            this.panel16.Padding = new System.Windows.Forms.Padding(10);
            this.panel16.Size = new System.Drawing.Size(221, 65);
            this.panel16.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(14, 10);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(10);
            this.button1.Size = new System.Drawing.Size(197, 45);
            this.button1.TabIndex = 2;
            this.button1.Text = "Удаить все результаты";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.second_rb);
            this.groupBox1.Controls.Add(this.first_rb);
            this.groupBox1.Location = new System.Drawing.Point(12, 90);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(438, 107);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Варианты удаления результатов";
            // 
            // second_rb
            // 
            this.second_rb.Location = new System.Drawing.Point(11, 68);
            this.second_rb.Name = "second_rb";
            this.second_rb.Size = new System.Drawing.Size(421, 29);
            this.second_rb.TabIndex = 1;
            this.second_rb.Text = "Удаление всех результатов у участников 2-го заезда.";
            this.second_rb.UseVisualStyleBackColor = true;
            // 
            // first_rb
            // 
            this.first_rb.Location = new System.Drawing.Point(11, 13);
            this.first_rb.Name = "first_rb";
            this.first_rb.Size = new System.Drawing.Size(421, 49);
            this.first_rb.TabIndex = 0;
            this.first_rb.Text = "Удаление всех результатов у участников 1-го заезда.\r\nВНИМАНИЕ: Автоматически буду" +
    "т удалены все участники 2-го заезда со всеми результатами.";
            this.first_rb.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Класс автомобилей";
            // 
            // car_classes_cb
            // 
            this.car_classes_cb.FormattingEnabled = true;
            this.car_classes_cb.Location = new System.Drawing.Point(123, 58);
            this.car_classes_cb.Name = "car_classes_cb";
            this.car_classes_cb.Size = new System.Drawing.Size(135, 21);
            this.car_classes_cb.TabIndex = 17;
            // 
            // backup_data_cb
            // 
            this.backup_data_cb.AutoSize = true;
            this.backup_data_cb.Checked = true;
            this.backup_data_cb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.backup_data_cb.Location = new System.Drawing.Point(13, 212);
            this.backup_data_cb.Name = "backup_data_cb";
            this.backup_data_cb.Size = new System.Drawing.Size(292, 17);
            this.backup_data_cb.TabIndex = 18;
            this.backup_data_cb.Text = "Сделать резервную копию данных перед удалением";
            this.backup_data_cb.UseVisualStyleBackColor = true;
            // 
            // DeleteResultsDialogView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 311);
            this.Controls.Add(this.backup_data_cb);
            this.Controls.Add(this.car_classes_cb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel12);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DeleteResultsDialogView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Удаление результатов у участников";
            this.panel2.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.panel16.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton second_rb;
        private System.Windows.Forms.RadioButton first_rb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox car_classes_cb;
        private System.Windows.Forms.CheckBox backup_data_cb;

    }
}