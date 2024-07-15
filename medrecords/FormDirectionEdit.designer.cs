﻿namespace medrecords
{
    partial class FormDirectionEdit
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.num_dir = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.d_dir = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.cb_dirtype = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cb_prof = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cb_uslmp = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cb_payt = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cb_typep = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.cb_cito = new System.Windows.Forms.ComboBox();
            this.dgv_dir = new System.Windows.Forms.DataGridView();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.cb_medicalcarekind = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cb_servicetype = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.cb_treatmentclass = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.cb_vizittype = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.dgv_tap = new System.Windows.Forms.DataGridView();
            this.button6 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.num_tap = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_dir)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tap)).BeginInit();
            this.SuspendLayout();
            // 
            // num_dir
            // 
            this.num_dir.Location = new System.Drawing.Point(1180, 118);
            this.num_dir.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.num_dir.Name = "num_dir";
            this.num_dir.Size = new System.Drawing.Size(178, 23);
            this.num_dir.TabIndex = 0;
            this.num_dir.Leave += new System.EventHandler(this.tb_numdir_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(320, 123);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Дата направления";
            // 
            // d_dir
            // 
            this.d_dir.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.d_dir.Location = new System.Drawing.Point(460, 119);
            this.d_dir.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.d_dir.Name = "d_dir";
            this.d_dir.Size = new System.Drawing.Size(130, 23);
            this.d_dir.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(171, 155);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(102, 15);
            this.label10.TabIndex = 25;
            this.label10.Text = "Тип направления";
            this.label10.Visible = false;
            // 
            // cb_dirtype
            // 
            this.cb_dirtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_dirtype.FormattingEnabled = true;
            this.cb_dirtype.Location = new System.Drawing.Point(317, 152);
            this.cb_dirtype.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cb_dirtype.Name = "cb_dirtype";
            this.cb_dirtype.Size = new System.Drawing.Size(326, 23);
            this.cb_dirtype.TabIndex = 24;
            this.cb_dirtype.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(49, 233);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 27;
            this.label3.Text = "Диагноз";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1064, 12);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(182, 67);
            this.button1.TabIndex = 28;
            this.button1.Text = "Сохранить и выйти";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1272, 12);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(81, 30);
            this.button2.TabIndex = 29;
            this.button2.Text = "Отмена";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(49, 92);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1265, 15);
            this.label4.TabIndex = 31;
            this.label4.Text = "Направившее МО , МО, куда направили     ГОСУДАРСТВЕННОЕ БЮДЖЕТНОЕ УЧРЕЖДЕНИЕ ЗДРА" +
    "ВООХРАНЕНИЯ НИЖЕГОРОДСКОЙ ОБЛАСТИ \"НИЖЕГОРОДСКАЯ ОБЛАСТНАЯ КЛИНИЧЕСКАЯ БОЛЬНИЦА " +
    "ИМ. Н.А.СЕМАШКО\" ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(121, 266);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(152, 15);
            this.label6.TabIndex = 35;
            this.label6.Text = "Профиль, куда направили";
            // 
            // cb_prof
            // 
            this.cb_prof.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_prof.FormattingEnabled = true;
            this.cb_prof.Location = new System.Drawing.Point(317, 258);
            this.cb_prof.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cb_prof.Name = "cb_prof";
            this.cb_prof.Size = new System.Drawing.Size(753, 23);
            this.cb_prof.TabIndex = 34;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(36, 290);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(235, 15);
            this.label7.TabIndex = 37;
            this.label7.Text = "Условия оказания медицинской помощи";
            // 
            // cb_uslmp
            // 
            this.cb_uslmp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_uslmp.FormattingEnabled = true;
            this.cb_uslmp.Location = new System.Drawing.Point(317, 287);
            this.cb_uslmp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cb_uslmp.Name = "cb_uslmp";
            this.cb_uslmp.Size = new System.Drawing.Size(753, 23);
            this.cb_uslmp.TabIndex = 36;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(49, 198);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 15);
            this.label9.TabIndex = 43;
            this.label9.Text = "Услуга";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(200, 320);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 15);
            this.label8.TabIndex = 45;
            this.label8.Text = "Тип оплаты";
            this.label8.Visible = false;
            // 
            // cb_payt
            // 
            this.cb_payt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_payt.FormattingEnabled = true;
            this.cb_payt.Location = new System.Drawing.Point(317, 320);
            this.cb_payt.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cb_payt.Name = "cb_payt";
            this.cb_payt.Size = new System.Drawing.Size(449, 23);
            this.cb_payt.TabIndex = 44;
            this.cb_payt.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(685, 155);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(94, 15);
            this.label11.TabIndex = 46;
            this.label11.Text = "Тип назначения";
            this.label11.Visible = false;
            // 
            // cb_typep
            // 
            this.cb_typep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_typep.FormattingEnabled = true;
            this.cb_typep.Location = new System.Drawing.Point(820, 152);
            this.cb_typep.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cb_typep.Name = "cb_typep";
            this.cb_typep.Size = new System.Drawing.Size(326, 23);
            this.cb_typep.TabIndex = 47;
            this.cb_typep.Visible = false;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(28, 127);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(79, 15);
            this.label21.TabIndex = 60;
            this.label21.Text = "Признак Cito";
            // 
            // cb_cito
            // 
            this.cb_cito.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_cito.FormattingEnabled = true;
            this.cb_cito.Location = new System.Drawing.Point(28, 145);
            this.cb_cito.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cb_cito.Name = "cb_cito";
            this.cb_cito.Size = new System.Drawing.Size(108, 23);
            this.cb_cito.TabIndex = 59;
            // 
            // dgv_dir
            // 
            this.dgv_dir.AllowUserToAddRows = false;
            this.dgv_dir.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.dgv_dir.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_dir.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgv_dir.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_dir.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_dir.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgv_dir.Location = new System.Drawing.Point(40, 454);
            this.dgv_dir.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dgv_dir.Name = "dgv_dir";
            this.dgv_dir.ReadOnly = true;
            this.dgv_dir.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgv_dir.RowTemplate.Height = 25;
            this.dgv_dir.Size = new System.Drawing.Size(1313, 74);
            this.dgv_dir.TabIndex = 62;
            this.dgv_dir.Visible = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1158, 27);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(74, 30);
            this.button3.TabIndex = 63;
            this.button3.Text = "make dir";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(318, 195);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(1041, 23);
            this.textBox1.TabIndex = 64;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(129, 191);
            this.button4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(144, 27);
            this.button4.TabIndex = 65;
            this.button4.Text = "Выбрать услугу";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(129, 229);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(144, 23);
            this.button5.TabIndex = 67;
            this.button5.Text = "Выбрать диагноз";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(317, 229);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(1041, 23);
            this.textBox2.TabIndex = 68;
            // 
            // cb_medicalcarekind
            // 
            this.cb_medicalcarekind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_medicalcarekind.FormattingEnabled = true;
            this.cb_medicalcarekind.Location = new System.Drawing.Point(815, 27);
            this.cb_medicalcarekind.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cb_medicalcarekind.Name = "cb_medicalcarekind";
            this.cb_medicalcarekind.Size = new System.Drawing.Size(222, 23);
            this.cb_medicalcarekind.TabIndex = 77;
            this.cb_medicalcarekind.Visible = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(815, 9);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(156, 15);
            this.label13.TabIndex = 76;
            this.label13.Text = "Вид медицинской помощи";
            this.label13.Visible = false;
            // 
            // cb_servicetype
            // 
            this.cb_servicetype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_servicetype.FormattingEnabled = true;
            this.cb_servicetype.Location = new System.Drawing.Point(579, 27);
            this.cb_servicetype.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cb_servicetype.Name = "cb_servicetype";
            this.cb_servicetype.Size = new System.Drawing.Size(224, 23);
            this.cb_servicetype.TabIndex = 73;
            this.cb_servicetype.Visible = false;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(579, 9);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(200, 15);
            this.label22.TabIndex = 72;
            this.label22.Text = "Место посещения - обслуживания";
            this.label22.Visible = false;
            // 
            // cb_treatmentclass
            // 
            this.cb_treatmentclass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_treatmentclass.FormattingEnabled = true;
            this.cb_treatmentclass.Location = new System.Drawing.Point(195, 27);
            this.cb_treatmentclass.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cb_treatmentclass.Name = "cb_treatmentclass";
            this.cb_treatmentclass.Size = new System.Drawing.Size(177, 23);
            this.cb_treatmentclass.TabIndex = 81;
            this.cb_treatmentclass.Visible = false;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(195, 9);
            this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(94, 15);
            this.label23.TabIndex = 80;
            this.label23.Text = "Вид обращения";
            this.label23.Visible = false;
            // 
            // cb_vizittype
            // 
            this.cb_vizittype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_vizittype.FormattingEnabled = true;
            this.cb_vizittype.Location = new System.Drawing.Point(391, 27);
            this.cb_vizittype.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cb_vizittype.Name = "cb_vizittype";
            this.cb_vizittype.Size = new System.Drawing.Size(168, 23);
            this.cb_vizittype.TabIndex = 79;
            this.cb_vizittype.Visible = false;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(391, 9);
            this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(101, 15);
            this.label24.TabIndex = 78;
            this.label24.Text = "Цель посещения";
            this.label24.Visible = false;
            // 
            // dgv_tap
            // 
            this.dgv_tap.AllowUserToAddRows = false;
            this.dgv_tap.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            this.dgv_tap.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_tap.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgv_tap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_tap.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_tap.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgv_tap.Location = new System.Drawing.Point(40, 378);
            this.dgv_tap.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dgv_tap.Name = "dgv_tap";
            this.dgv_tap.ReadOnly = true;
            this.dgv_tap.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgv_tap.RowTemplate.Height = 25;
            this.dgv_tap.Size = new System.Drawing.Size(1313, 70);
            this.dgv_tap.TabIndex = 82;
            this.dgv_tap.Visible = false;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(1080, 28);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(71, 28);
            this.button6.TabIndex = 83;
            this.button6.Text = "mak tap";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1053, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 15);
            this.label1.TabIndex = 91;
            this.label1.Text = "Номер направления";
            // 
            // num_tap
            // 
            this.num_tap.Location = new System.Drawing.Point(859, 119);
            this.num_tap.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.num_tap.Name = "num_tap";
            this.num_tap.Size = new System.Drawing.Size(178, 23);
            this.num_tap.TabIndex = 92;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(741, 125);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(111, 15);
            this.label19.TabIndex = 93;
            this.label19.Text = "Номер посещения";
            // 
            // FormDirectionEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1378, 599);
            this.ControlBox = false;
            this.Controls.Add(this.label19);
            this.Controls.Add(this.num_tap);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgv_tap);
            this.Controls.Add(this.cb_treatmentclass);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.cb_vizittype);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.cb_medicalcarekind);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.cb_servicetype);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dgv_dir);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.cb_cito);
            this.Controls.Add(this.cb_typep);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cb_payt);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cb_uslmp);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cb_prof);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cb_dirtype);
            this.Controls.Add(this.d_dir);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.num_dir);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button3);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FormDirectionEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактирование направления";
            this.Load += new System.EventHandler(this.FormDirectionEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_dir)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox num_dir;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker d_dir;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cb_dirtype;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cb_prof;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cb_uslmp;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cb_payt;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cb_typep;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox cb_cito;
        private System.Windows.Forms.DataGridView dgv_dir;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ComboBox cb_medicalcarekind;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cb_servicetype;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox cb_treatmentclass;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ComboBox cb_vizittype;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.DataGridView dgv_tap;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox num_tap;
        private System.Windows.Forms.Label label19;
    }
}