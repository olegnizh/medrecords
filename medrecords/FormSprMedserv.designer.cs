namespace medrecords
{
    partial class FormSprMedserv
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
            this.button6 = new System.Windows.Forms.Button();
            this.dgv_medtest = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.button13 = new System.Windows.Forms.Button();
            this.pb1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_medtest)).BeginInit();
            this.SuspendLayout();
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(917, 684);
            this.button6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(105, 27);
            this.button6.TabIndex = 8;
            this.button6.Text = "Закрыть";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // dgv_medtest
            // 
            this.dgv_medtest.AllowUserToDeleteRows = false;
            this.dgv_medtest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_medtest.Location = new System.Drawing.Point(22, 78);
            this.dgv_medtest.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dgv_medtest.Name = "dgv_medtest";
            this.dgv_medtest.ReadOnly = true;
            this.dgv_medtest.Size = new System.Drawing.Size(1000, 561);
            this.dgv_medtest.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(605, 46);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 20);
            this.label1.TabIndex = 31;
            this.label1.Text = ".";
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(22, 23);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(184, 43);
            this.button13.TabIndex = 35;
            this.button13.Text = "MEDTEST из csv";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // pb1
            // 
            this.pb1.Location = new System.Drawing.Point(605, 11);
            this.pb1.Name = "pb1";
            this.pb1.Size = new System.Drawing.Size(320, 26);
            this.pb1.TabIndex = 37;
            // 
            // FormSprMedserv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1047, 732);
            this.Controls.Add(this.pb1);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgv_medtest);
            this.Controls.Add(this.button6);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "FormSprMedserv";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "СПРАВОЧНИК МЕДИЦИНСКИХ  ТЕСТОВ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormSprMedserv_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_medtest)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.DataGridView dgv_medtest;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.ProgressBar pb1;
    }
}