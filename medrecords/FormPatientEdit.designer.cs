namespace medrecords
{
    partial class FormPatientEdit
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.t_fam = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.t_im = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.t_otch = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.d_dr = new System.Windows.Forms.DateTimePicker();
            this.t_pdoc_a = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.d_pdoc_b = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.m_pdoc = new System.Windows.Forms.MaskedTextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.m_ss = new System.Windows.Forms.MaskedTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.m_oms = new System.Windows.Forms.MaskedTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cb_sex = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cb_socstatus = new System.Windows.Forms.ComboBox();
            this.dgv1 = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Фамилия";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(505, 114);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Дата рождения";
            // 
            // t_fam
            // 
            this.t_fam.Location = new System.Drawing.Point(114, 28);
            this.t_fam.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.t_fam.Name = "t_fam";
            this.t_fam.Size = new System.Drawing.Size(349, 23);
            this.t_fam.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1003, 528);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(173, 27);
            this.button1.TabIndex = 13;
            this.button1.Text = "Отмена";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(646, 528);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(195, 27);
            this.button2.TabIndex = 12;
            this.button2.Text = "Сохранить и выйти";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 36);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(159, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Серия Номер (Уникальное)";
            // 
            // t_im
            // 
            this.t_im.Location = new System.Drawing.Point(114, 69);
            this.t_im.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.t_im.Name = "t_im";
            this.t_im.Size = new System.Drawing.Size(349, 23);
            this.t_im.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(61, 73);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "Имя";
            // 
            // t_otch
            // 
            this.t_otch.Location = new System.Drawing.Point(114, 106);
            this.t_otch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.t_otch.Name = "t_otch";
            this.t_otch.Size = new System.Drawing.Size(349, 23);
            this.t_otch.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 110);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "Отчество";
            // 
            // d_dr
            // 
            this.d_dr.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.d_dr.Location = new System.Drawing.Point(630, 114);
            this.d_dr.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.d_dr.Name = "d_dr";
            this.d_dr.Size = new System.Drawing.Size(134, 23);
            this.d_dr.TabIndex = 6;
            // 
            // t_pdoc_a
            // 
            this.t_pdoc_a.Location = new System.Drawing.Point(475, 22);
            this.t_pdoc_a.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.t_pdoc_a.Multiline = true;
            this.t_pdoc_a.Name = "t_pdoc_a";
            this.t_pdoc_a.Size = new System.Drawing.Size(506, 74);
            this.t_pdoc_a.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(386, 40);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 15);
            this.label6.TabIndex = 13;
            this.label6.Text = "Кем выдан";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 75);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 15);
            this.label7.TabIndex = 15;
            this.label7.Text = "Когда выдан";
            // 
            // d_pdoc_b
            // 
            this.d_pdoc_b.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.d_pdoc_b.Location = new System.Drawing.Point(125, 75);
            this.d_pdoc_b.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.d_pdoc_b.Name = "d_pdoc_b";
            this.d_pdoc_b.Size = new System.Drawing.Size(149, 23);
            this.d_pdoc_b.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.m_pdoc);
            this.groupBox1.Controls.Add(this.t_pdoc_a);
            this.groupBox1.Controls.Add(this.d_pdoc_b);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Location = new System.Drawing.Point(30, 152);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(1001, 115);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ПАСПОРТНЫЕ ДАННЫЕ";
            // 
            // m_pdoc
            // 
            this.m_pdoc.Location = new System.Drawing.Point(204, 32);
            this.m_pdoc.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.m_pdoc.Mask = "AAAA AAAAAA";
            this.m_pdoc.Name = "m_pdoc";
            this.m_pdoc.Size = new System.Drawing.Size(100, 23);
            this.m_pdoc.TabIndex = 7;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(761, 63);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(203, 27);
            this.button3.TabIndex = 14;
            this.button3.Text = "Изменить данные записи";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // m_ss
            // 
            this.m_ss.Location = new System.Drawing.Point(180, 287);
            this.m_ss.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.m_ss.Mask = "000-000-000 00";
            this.m_ss.Name = "m_ss";
            this.m_ss.Size = new System.Drawing.Size(136, 23);
            this.m_ss.TabIndex = 10;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 292);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(126, 15);
            this.label8.TabIndex = 18;
            this.label8.Text = "СНИЛС (Уникальное)";
            // 
            // m_oms
            // 
            this.m_oms.Location = new System.Drawing.Point(539, 287);
            this.m_oms.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.m_oms.Mask = "0000 0000 0000 0000";
            this.m_oms.Name = "m_oms";
            this.m_oms.Size = new System.Drawing.Size(146, 23);
            this.m_oms.TabIndex = 11;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(358, 291);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(151, 15);
            this.label9.TabIndex = 20;
            this.label9.Text = "номер ОМС (Уникальное)";
            // 
            // cb_sex
            // 
            this.cb_sex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_sex.FormattingEnabled = true;
            this.cb_sex.Location = new System.Drawing.Point(630, 27);
            this.cb_sex.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cb_sex.Name = "cb_sex";
            this.cb_sex.Size = new System.Drawing.Size(292, 23);
            this.cb_sex.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(587, 36);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(30, 15);
            this.label10.TabIndex = 24;
            this.label10.Text = "Пол";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(495, 74);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(115, 15);
            this.label11.TabIndex = 27;
            this.label11.Text = "Социальный статус";
            // 
            // cb_socstatus
            // 
            this.cb_socstatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_socstatus.FormattingEnabled = true;
            this.cb_socstatus.Location = new System.Drawing.Point(630, 73);
            this.cb_socstatus.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cb_socstatus.Name = "cb_socstatus";
            this.cb_socstatus.Size = new System.Drawing.Size(565, 23);
            this.cb_socstatus.TabIndex = 5;
            // 
            // dgv1
            // 
            this.dgv1.AllowUserToAddRows = false;
            this.dgv1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            this.dgv1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv1.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgv1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgv1.Location = new System.Drawing.Point(24, 358);
            this.dgv1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dgv1.Name = "dgv1";
            this.dgv1.ReadOnly = true;
            this.dgv1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgv1.RowTemplate.Height = 25;
            this.dgv1.Size = new System.Drawing.Size(1152, 129);
            this.dgv1.TabIndex = 56;
            this.dgv1.Visible = false;
            // 
            // FormPatientEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1246, 585);
            this.ControlBox = false;
            this.Controls.Add(this.dgv1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cb_socstatus);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cb_sex);
            this.Controls.Add(this.m_oms);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.m_ss);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.d_dr);
            this.Controls.Add(this.t_otch);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.t_im);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.t_fam);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FormPatientEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактирование записи пациента";
            this.Load += new System.EventHandler(this.FormPatientEdit_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox t_fam;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox t_im;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox t_otch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker d_dr;
        private System.Windows.Forms.TextBox t_pdoc_a;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker d_pdoc_b;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MaskedTextBox m_pdoc;
        private System.Windows.Forms.MaskedTextBox m_ss;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.MaskedTextBox m_oms;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cb_sex;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cb_socstatus;
        private System.Windows.Forms.DataGridView dgv1;
        private System.Windows.Forms.Button button3;
    }
}