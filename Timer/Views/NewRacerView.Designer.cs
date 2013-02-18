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
            this.panel1 = new System.Windows.Forms.Panel();
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
            this.carNameTB = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.carClassesCB = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.racersDGV = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.racersDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.completeRegistrationBT);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 353);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1055, 55);
            this.panel1.TabIndex = 0;
            // 
            // completeRegistrationBT
            // 
            this.completeRegistrationBT.Location = new System.Drawing.Point(890, 14);
            this.completeRegistrationBT.Name = "completeRegistrationBT";
            this.completeRegistrationBT.Size = new System.Drawing.Size(149, 27);
            this.completeRegistrationBT.TabIndex = 2;
            this.completeRegistrationBT.Text = "Закончить регистрацию";
            this.completeRegistrationBT.UseVisualStyleBackColor = true;
            this.completeRegistrationBT.Click += new System.EventHandler(this.completeRegistrationBT_Click);
            // 
            // newRacerB
            // 
            this.newRacerB.Location = new System.Drawing.Point(216, 234);
            this.newRacerB.Name = "newRacerB";
            this.newRacerB.Size = new System.Drawing.Size(86, 27);
            this.newRacerB.TabIndex = 1;
            this.newRacerB.Text = "Добавить";
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
            this.groupBox2.Controls.Add(this.carNameTB);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.carClassesCB);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(12, 127);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(290, 91);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Автомобиль участника";
            // 
            // carNameTB
            // 
            this.carNameTB.Location = new System.Drawing.Point(102, 25);
            this.carNameTB.Name = "carNameTB";
            this.carNameTB.Size = new System.Drawing.Size(170, 20);
            this.carNameTB.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Наименование";
            // 
            // carClassesCB
            // 
            this.carClassesCB.FormattingEnabled = true;
            this.carClassesCB.Location = new System.Drawing.Point(103, 54);
            this.carClassesCB.Name = "carClassesCB";
            this.carClassesCB.Size = new System.Drawing.Size(169, 21);
            this.carClassesCB.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Класс";
            // 
            // racersDGV
            // 
            this.racersDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.racersDGV.BackgroundColor = System.Drawing.Color.White;
            this.racersDGV.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.racersDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.racersDGV.Location = new System.Drawing.Point(319, 12);
            this.racersDGV.Name = "racersDGV";
            this.racersDGV.Size = new System.Drawing.Size(724, 330);
            this.racersDGV.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1055, 408);
            this.panel2.TabIndex = 4;
            // 
            // NewRacerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1055, 408);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.racersDGV);
            this.Controls.Add(this.newRacerB);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "NewRacerView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавление участников";
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.racersDGV)).EndInit();
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
        private System.Windows.Forms.TextBox carNameTB;
        private System.Windows.Forms.Label label5;
    }
}