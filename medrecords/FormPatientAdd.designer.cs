namespace medrecords
{
    partial class FormPatientAdd
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.t_fam = new System.Windows.Forms.TextBox();
            this.Button1 = new System.Windows.Forms.Button();
            this.Button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.t_im = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.t_otch = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.d_dr = new System.Windows.Forms.DateTimePicker();
            this.MemoDocPasp = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.DateDocPasp = new System.Windows.Forms.DateTimePicker();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.MaskNdocPasp = new System.Windows.Forms.MaskedTextBox();
            this.MaskSnils = new System.Windows.Forms.MaskedTextBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.MaskOms = new System.Windows.Forms.MaskedTextBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.cb_sex = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cb_socstatus = new System.Windows.Forms.ComboBox();
            this.ChkPasp = new System.Windows.Forms.CheckBox();
            this.ChkSnils = new System.Windows.Forms.CheckBox();
            this.ChkOms = new System.Windows.Forms.CheckBox();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.Pb1 = new System.Windows.Forms.ProgressBar();
            this.label12 = new System.Windows.Forms.Label();
            this.GroupBox1.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.GroupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 54);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Фамилия";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(473, 123);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Дата рождения";
            // 
            // t_fam
            // 
            this.t_fam.Location = new System.Drawing.Point(91, 54);
            this.t_fam.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.t_fam.Name = "t_fam";
            this.t_fam.Size = new System.Drawing.Size(339, 23);
            this.t_fam.TabIndex = 1;
            this.t_fam.TextChanged += new System.EventHandler(this.t_fam_TextChanged);
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(1081, 451);
            this.Button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(112, 27);
            this.Button1.TabIndex = 13;
            this.Button1.Text = "Закрыть окно";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Button2
            // 
            this.Button2.Location = new System.Drawing.Point(695, 451);
            this.Button2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(150, 27);
            this.Button2.TabIndex = 12;
            this.Button2.Text = "Добавить в ЕЦП";
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 36);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(159, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Серия Номер (Уникальное)";
            // 
            // t_im
            // 
            this.t_im.Location = new System.Drawing.Point(91, 87);
            this.t_im.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.t_im.Name = "t_im";
            this.t_im.Size = new System.Drawing.Size(339, 23);
            this.t_im.TabIndex = 2;
            this.t_im.TextChanged += new System.EventHandler(this.t_im_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(47, 87);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "Имя";
            // 
            // t_otch
            // 
            this.t_otch.Location = new System.Drawing.Point(88, 123);
            this.t_otch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.t_otch.Name = "t_otch";
            this.t_otch.Size = new System.Drawing.Size(341, 23);
            this.t_otch.TabIndex = 3;
            this.t_otch.TextChanged += new System.EventHandler(this.t_otch_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 123);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "Отчество";
            // 
            // d_dr
            // 
            this.d_dr.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.d_dr.Location = new System.Drawing.Point(603, 119);
            this.d_dr.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.d_dr.Name = "d_dr";
            this.d_dr.Size = new System.Drawing.Size(134, 23);
            this.d_dr.TabIndex = 6;
            // 
            // MemoDocPasp
            // 
            this.MemoDocPasp.Location = new System.Drawing.Point(428, 22);
            this.MemoDocPasp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MemoDocPasp.Multiline = true;
            this.MemoDocPasp.Name = "MemoDocPasp";
            this.MemoDocPasp.Size = new System.Drawing.Size(524, 63);
            this.MemoDocPasp.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(326, 22);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 15);
            this.label6.TabIndex = 13;
            this.label6.Text = "Кем выдан";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(107, 65);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 15);
            this.label7.TabIndex = 15;
            this.label7.Text = "Когда выдан";
            // 
            // DateDocPasp
            // 
            this.DateDocPasp.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DateDocPasp.Location = new System.Drawing.Point(209, 62);
            this.DateDocPasp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.DateDocPasp.Name = "DateDocPasp";
            this.DateDocPasp.Size = new System.Drawing.Size(149, 23);
            this.DateDocPasp.TabIndex = 8;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.MaskNdocPasp);
            this.GroupBox1.Controls.Add(this.MemoDocPasp);
            this.GroupBox1.Controls.Add(this.DateDocPasp);
            this.GroupBox1.Controls.Add(this.label6);
            this.GroupBox1.Controls.Add(this.label3);
            this.GroupBox1.Controls.Add(this.label7);
            this.GroupBox1.Location = new System.Drawing.Point(223, 319);
            this.GroupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GroupBox1.Size = new System.Drawing.Size(970, 103);
            this.GroupBox1.TabIndex = 17;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "ПАСПОРТНЫЕ ДАННЫЕ";
            this.GroupBox1.Visible = false;
            // 
            // MaskNdocPasp
            // 
            this.MaskNdocPasp.Location = new System.Drawing.Point(209, 32);
            this.MaskNdocPasp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaskNdocPasp.Mask = "AAAA AAAAAA";
            this.MaskNdocPasp.Name = "MaskNdocPasp";
            this.MaskNdocPasp.Size = new System.Drawing.Size(100, 23);
            this.MaskNdocPasp.TabIndex = 7;
            // 
            // MaskSnils
            // 
            this.MaskSnils.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.MaskSnils.Location = new System.Drawing.Point(162, 27);
            this.MaskSnils.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaskSnils.Mask = "000-000-000 00";
            this.MaskSnils.Name = "MaskSnils";
            this.MaskSnils.Size = new System.Drawing.Size(136, 23);
            this.MaskSnils.TabIndex = 10;
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(17, 30);
            this.Label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(126, 15);
            this.Label8.TabIndex = 18;
            this.Label8.Text = "СНИЛС (Уникальное)";
            // 
            // MaskOms
            // 
            this.MaskOms.Location = new System.Drawing.Point(191, 20);
            this.MaskOms.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaskOms.Mask = "0000 0000 0000 0000";
            this.MaskOms.Name = "MaskOms";
            this.MaskOms.Size = new System.Drawing.Size(136, 23);
            this.MaskOms.TabIndex = 11;
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(19, 23);
            this.Label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(153, 15);
            this.Label9.TabIndex = 20;
            this.Label9.Text = "Номер ОМС (Уникальное)";
            // 
            // cb_sex
            // 
            this.cb_sex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_sex.FormattingEnabled = true;
            this.cb_sex.Location = new System.Drawing.Point(603, 50);
            this.cb_sex.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cb_sex.Name = "cb_sex";
            this.cb_sex.Size = new System.Drawing.Size(271, 23);
            this.cb_sex.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(542, 57);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(30, 15);
            this.label10.TabIndex = 23;
            this.label10.Text = "Пол";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(456, 87);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(115, 15);
            this.label11.TabIndex = 25;
            this.label11.Text = "Социальный статус";
            // 
            // cb_socstatus
            // 
            this.cb_socstatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_socstatus.FormattingEnabled = true;
            this.cb_socstatus.Location = new System.Drawing.Point(603, 84);
            this.cb_socstatus.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cb_socstatus.Name = "cb_socstatus";
            this.cb_socstatus.Size = new System.Drawing.Size(401, 23);
            this.cb_socstatus.TabIndex = 5;
            // 
            // ChkPasp
            // 
            this.ChkPasp.AutoSize = true;
            this.ChkPasp.Location = new System.Drawing.Point(18, 308);
            this.ChkPasp.Name = "ChkPasp";
            this.ChkPasp.Size = new System.Drawing.Size(139, 19);
            this.ChkPasp.TabIndex = 57;
            this.ChkPasp.Text = "Паспортные данные";
            this.ChkPasp.UseVisualStyleBackColor = true;
            this.ChkPasp.Visible = false;
            this.ChkPasp.CheckedChanged += new System.EventHandler(this.Chk_pasp_CheckedChanged);
            // 
            // ChkSnils
            // 
            this.ChkSnils.AutoSize = true;
            this.ChkSnils.Location = new System.Drawing.Point(18, 182);
            this.ChkSnils.Name = "ChkSnils";
            this.ChkSnils.Size = new System.Drawing.Size(68, 19);
            this.ChkSnils.TabIndex = 58;
            this.ChkSnils.Text = "СНИЛС";
            this.ChkSnils.UseVisualStyleBackColor = true;
            this.ChkSnils.CheckedChanged += new System.EventHandler(this.Chk_snils_CheckedChanged);
            // 
            // ChkOms
            // 
            this.ChkOms.AutoSize = true;
            this.ChkOms.Location = new System.Drawing.Point(391, 182);
            this.ChkOms.Name = "ChkOms";
            this.ChkOms.Size = new System.Drawing.Size(95, 19);
            this.ChkOms.TabIndex = 59;
            this.ChkOms.Text = "Номер ОМС";
            this.ChkOms.UseVisualStyleBackColor = true;
            this.ChkOms.Visible = false;
            this.ChkOms.CheckedChanged += new System.EventHandler(this.Chk_oms_CheckedChanged);
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.MaskSnils);
            this.GroupBox2.Controls.Add(this.Label8);
            this.GroupBox2.Location = new System.Drawing.Point(18, 219);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(346, 61);
            this.GroupBox2.TabIndex = 60;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "СНИЛС";
            this.GroupBox2.Visible = false;
            // 
            // GroupBox3
            // 
            this.GroupBox3.Controls.Add(this.Label9);
            this.GroupBox3.Controls.Add(this.MaskOms);
            this.GroupBox3.Location = new System.Drawing.Point(391, 219);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(346, 61);
            this.GroupBox3.TabIndex = 61;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "OMC";
            this.GroupBox3.Visible = false;
            // 
            // Pb1
            // 
            this.Pb1.Location = new System.Drawing.Point(18, 514);
            this.Pb1.Name = "Pb1";
            this.Pb1.Size = new System.Drawing.Size(176, 23);
            this.Pb1.TabIndex = 62;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(214, 522);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(10, 15);
            this.label12.TabIndex = 65;
            this.label12.Text = ".";
            // 
            // FormPatientAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1272, 549);
            this.ControlBox = false;
            this.Controls.Add(this.label12);
            this.Controls.Add(this.Pb1);
            this.Controls.Add(this.GroupBox3);
            this.Controls.Add(this.GroupBox2);
            this.Controls.Add(this.ChkOms);
            this.Controls.Add(this.ChkSnils);
            this.Controls.Add(this.ChkPasp);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cb_socstatus);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cb_sex);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.d_dr);
            this.Controls.Add(this.t_otch);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.t_im);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.t_fam);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FormPatientAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавить пациента";
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox t_fam;
        private System.Windows.Forms.Button Button1;
        private System.Windows.Forms.Button Button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox t_im;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox t_otch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker d_dr;
        private System.Windows.Forms.TextBox MemoDocPasp;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker DateDocPasp;
        private System.Windows.Forms.GroupBox GroupBox1;
        private System.Windows.Forms.MaskedTextBox MaskNdocPasp;
        private System.Windows.Forms.MaskedTextBox MaskSnils;
        private System.Windows.Forms.Label Label8;
        private System.Windows.Forms.MaskedTextBox MaskOms;
        private System.Windows.Forms.Label Label9;
        private System.Windows.Forms.ComboBox cb_sex;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cb_socstatus;
        private System.Windows.Forms.CheckBox ChkPasp;
        private System.Windows.Forms.CheckBox ChkSnils;
        private System.Windows.Forms.CheckBox ChkOms;
        private System.Windows.Forms.GroupBox GroupBox2;
        private System.Windows.Forms.GroupBox GroupBox3;
        private System.Windows.Forms.ProgressBar Pb1;
        private System.Windows.Forms.Label label12;
    }
}