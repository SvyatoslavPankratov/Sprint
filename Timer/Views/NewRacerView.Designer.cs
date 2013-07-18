namespace Sprint.Views
{
    partial class NewRacerView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewRacerView));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.completeRegistrationBT = new System.Windows.Forms.Button();
            this.newRacerB = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mnTB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lnTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.fnTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.engine_power_TB = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.engine_size_TB = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.model_TB = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.manufacturer_TB = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.carClassesCB = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.racersDGV = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.racersDGV)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 597);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(899, 70);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.completeRegistrationBT);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(679, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(220, 70);
            this.panel3.TabIndex = 3;
            // 
            // completeRegistrationBT
            // 
            this.completeRegistrationBT.Image = ((System.Drawing.Image)(resources.GetObject("completeRegistrationBT.Image")));
            this.completeRegistrationBT.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.completeRegistrationBT.Location = new System.Drawing.Point(14, 16);
            this.completeRegistrationBT.Name = "completeRegistrationBT";
            this.completeRegistrationBT.Padding = new System.Windows.Forms.Padding(10);
            this.completeRegistrationBT.Size = new System.Drawing.Size(192, 42);
            this.completeRegistrationBT.TabIndex = 2;
            this.completeRegistrationBT.Text = "Закончить регистрацию";
            this.completeRegistrationBT.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.completeRegistrationBT.UseVisualStyleBackColor = true;
            this.completeRegistrationBT.Click += new System.EventHandler(this.completeRegistrationBT_Click);
            // 
            // newRacerB
            // 
            this.newRacerB.Image = ((System.Drawing.Image)(resources.GetObject("newRacerB.Image")));
            this.newRacerB.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.newRacerB.Location = new System.Drawing.Point(764, 127);
            this.newRacerB.Name = "newRacerB";
            this.newRacerB.Padding = new System.Windows.Forms.Padding(10);
            this.newRacerB.Size = new System.Drawing.Size(121, 41);
            this.newRacerB.TabIndex = 6;
            this.newRacerB.Text = "Добавить";
            this.newRacerB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.newRacerB.UseVisualStyleBackColor = true;
            this.newRacerB.Click += new System.EventHandler(this.newRacerB_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.mnTB);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lnTB);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.fnTB);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(290, 109);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Участник";
            // 
            // mnTB
            // 
            this.mnTB.Location = new System.Drawing.Point(78, 74);
            this.mnTB.Name = "mnTB";
            this.mnTB.Size = new System.Drawing.Size(194, 20);
            this.mnTB.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Отчество";
            // 
            // lnTB
            // 
            this.lnTB.Location = new System.Drawing.Point(78, 48);
            this.lnTB.Name = "lnTB";
            this.lnTB.Size = new System.Drawing.Size(194, 20);
            this.lnTB.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Имя";
            // 
            // fnTB
            // 
            this.fnTB.Location = new System.Drawing.Point(78, 22);
            this.fnTB.Name = "fnTB";
            this.fnTB.Size = new System.Drawing.Size(194, 20);
            this.fnTB.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Фамилия";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.engine_power_TB);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.engine_size_TB);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.model_TB);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.manufacturer_TB);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.carClassesCB);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(332, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(553, 109);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Автомобиль участника";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(518, 53);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(24, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "л/с";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(518, 27);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(13, 13);
            this.label11.TabIndex = 14;
            this.label11.Text = "л";
            // 
            // engine_power_TB
            // 
            this.engine_power_TB.Location = new System.Drawing.Point(404, 50);
            this.engine_power_TB.Name = "engine_power_TB";
            this.engine_power_TB.Size = new System.Drawing.Size(108, 20);
            this.engine_power_TB.TabIndex = 13;
            this.engine_power_TB.Text = "0.0";
            this.engine_power_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(283, 53);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(115, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "Мощность двигателя";
            // 
            // engine_size_TB
            // 
            this.engine_size_TB.Location = new System.Drawing.Point(404, 24);
            this.engine_size_TB.Name = "engine_size_TB";
            this.engine_size_TB.Size = new System.Drawing.Size(108, 20);
            this.engine_size_TB.TabIndex = 11;
            this.engine_size_TB.Text = "0.0";
            this.engine_size_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(283, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Объем двигателя";
            // 
            // model_TB
            // 
            this.model_TB.Location = new System.Drawing.Point(102, 50);
            this.model_TB.Name = "model_TB";
            this.model_TB.Size = new System.Drawing.Size(170, 20);
            this.model_TB.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Модель";
            // 
            // manufacturer_TB
            // 
            this.manufacturer_TB.Location = new System.Drawing.Point(102, 24);
            this.manufacturer_TB.Name = "manufacturer_TB";
            this.manufacturer_TB.Size = new System.Drawing.Size(170, 20);
            this.manufacturer_TB.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Марка";
            // 
            // carClassesCB
            // 
            this.carClassesCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.carClassesCB.FormattingEnabled = true;
            this.carClassesCB.Location = new System.Drawing.Point(102, 77);
            this.carClassesCB.Name = "carClassesCB";
            this.carClassesCB.Size = new System.Drawing.Size(170, 21);
            this.carClassesCB.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Класс";
            // 
            // racersDGV
            // 
            this.racersDGV.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.racersDGV.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.racersDGV.BackgroundColor = System.Drawing.Color.DarkGray;
            this.racersDGV.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.racersDGV.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.racersDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.racersDGV.DefaultCellStyle = dataGridViewCellStyle2;
            this.racersDGV.GridColor = System.Drawing.Color.Silver;
            this.racersDGV.Location = new System.Drawing.Point(12, 174);
            this.racersDGV.MultiSelect = false;
            this.racersDGV.Name = "racersDGV";
            this.racersDGV.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.ButtonShadow;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.racersDGV.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.racersDGV.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            this.racersDGV.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.racersDGV.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.racersDGV.Size = new System.Drawing.Size(873, 412);
            this.racersDGV.TabIndex = 3;
            this.racersDGV.DataSourceChanged += new System.EventHandler(this.racersDGV_DataSourceChanged);
            this.racersDGV.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.racersDGV_UserDeletedRow);
            this.racersDGV.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.racersDGV_UserDeletingRow);
            this.racersDGV.Paint += new System.Windows.Forms.PaintEventHandler(this.racersDGV_Paint);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.newRacerB);
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.racersDGV);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(899, 667);
            this.panel2.TabIndex = 4;
            // 
            // NewRacerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(899, 667);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewRacerView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавление участников";
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.racersDGV)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button newRacerB;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView racersDGV;
        private System.Windows.Forms.TextBox mnTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox lnTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox fnTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox carClassesCB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button completeRegistrationBT;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox engine_power_TB;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox engine_size_TB;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox model_TB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox manufacturer_TB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel3;
    }
}