using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;


namespace medrecords
{
    public partial class FormTAPedit : Form
    {

        DataTable dt1;
        string id_evnplbase;

        public FormTAPedit(string id)
        {
            InitializeComponent();
            //

            this.id_evnplbase = id;
           
            // Вид обращения
            this.cb_treatmentclass.DataSource = Info.ds.Tables["treatmentclass"];
            this.cb_treatmentclass.DisplayMember = "Name_spr";
            this.cb_treatmentclass.ValueMember = "id_spr";
            // Цель посещения
            this.cb_vizittype.DataSource = Info.ds.Tables["vizittype"];
            this.cb_vizittype.DisplayMember = "Name_spr";
            this.cb_vizittype.ValueMember = "id_spr";
            //this.cb_vizittype.SelectedValue = "1";
            // место обслуживания (посещения)
            this.cb_servicetype.DataSource = Info.ds.Tables["servicetype"];
            this.cb_servicetype.DisplayMember = "Name_spr";
            this.cb_servicetype.ValueMember = "Code_spr";
            // вид медицинской помощи
            this.cb_medicalcarekind.DataSource = Info.ds.Tables["medicalcarekind"];
            this.cb_medicalcarekind.DisplayMember = "Name_spr";
            this.cb_medicalcarekind.ValueMember = "code_spr";
            // вид оплаты
            this.cb_payt.DataSource = Info.ds.Tables["paytype"];
            this.cb_payt.DisplayMember = "Name_spr";
            this.cb_payt.ValueMember = "id_spr";

            //this.cb_diag.DataSource = Info.ds.Tables["diag"];
            //this.cb_diag.DisplayMember = "Name_spr";
            //this.cb_diag.ValueMember = "id_spr";

            //this.cb_test.DataSource = Info.ds.Tables["uslugacomplex"];
            //this.cb_test.DisplayMember = "Name_spr";
            //this.cb_test.ValueMember = "id_spr";

            this.cb_openclose.DataSource = Info.ds.Tables["openclosetap"];
            this.cb_openclose.DisplayMember = "Name_spr";
            this.cb_openclose.ValueMember = "id_spr";

            //this.cb_lpusection.DataSource = Info.ds.Tables["lpusection"];
            //this.cb_lpusection.DisplayMember = "Name_spr";
            //this.cb_lpusection.ValueMember = "id_spr";

            //this.cb_vizitclass.DataSource = Info.ds.Tables["vizitclass"];
            //this.cb_vizitclass.DisplayMember = "Name_spr";
            //this.cb_vizitclass.ValueMember = "code_spr";

            //this.cb_lpusection.DataSource = Info.ds.Tables["lpusection"];
            //this.cb_lpusection.DisplayMember = "Name_spr";
            //this.cb_lpusection.ValueMember = "id_spr";


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.button1.DialogResult = DialogResult.Cancel;
            this.Close();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            // is double data	
            //DataRow[] result = Info.ds.Tables["evnplbase"].Select("EVNPL_NUMCARD = '" + this.textBox1.Text.ToString().Trim() + "'");
            //if (result.Length != 0)
            //{
            //    MessageBox.Show("Такой НОМЕР ТАЛОНА уже существует");
            //    return;
            //}

            // evnplbase change record
            //row["ID_EVNPLBASE"] = Guid.NewGuid().ToString();  
            //row["ID_PATIENT_EVNPLBASE"] = Info.id_patient;
            this.dt1.Rows[0]["EVNPL_NUMCARD"] = this.textBox1.Text.ToString().Trim();
            this.dt1.Rows[0]["EVNPL_ISFINISH"] = this.cb_openclose.SelectedValue;
            this.dt1.Rows[0]["EVN_DATETIME"] = this.dateTimePicker1.Value;
            //this.dt1.Rows[0]["EVN_DATE"] = this.dateTimePicker1.Value.Date;
            //this.dt1.Rows[0]["EVN_HH"] = this.dateTimePicker1.Value.TimeOfDay.Hours;
            //this.dt1.Rows[0]["EVN_MM"] = this.dateTimePicker1.Value.TimeOfDay.Minutes;
            //this.dt1.Rows[0]["EVN_SS"] = this.dateTimePicker1.Value.TimeOfDay.Seconds;
            //row["EVN_SETDT_TIME"] = this.dateTimePicker1.Value.TimeOfDay;
            this.dt1.Rows[0]["LPUSECTION_ID"] = Info.LpuSection_sid;
            this.dt1.Rows[0]["MEDSTAFFFACT_ID"] = Info.MedStaffFact_sid;
            this.dt1.Rows[0]["TREATMENTCLASS_ID"] = this.cb_treatmentclass.SelectedValue;
            this.dt1.Rows[0]["SERVICETYPE_ID"] = this.cb_servicetype.SelectedValue;
            this.dt1.Rows[0]["VIZITTYPE_ID"] = this.cb_vizittype.SelectedValue;
            this.dt1.Rows[0]["MEDICALCAREKIND_ID"] = this.cb_medicalcarekind.SelectedValue;
            this.dt1.Rows[0]["PAYTYPE_ID"] = this.cb_payt.SelectedValue;

            //this.dt1.Rows[0]["DIAG_ID"] = this.cb_diag.SelectedValue;
            string[] strs_diag;
            strs_diag = this.textBox4.Text.Split(' ');
            DataRow[] result_diag = Info.ds.Tables["diag"].Select("code_spr = '" + strs_diag[0] + "'");
            this.dt1.Rows[0]["diag"] = result_diag[0]["name_spr"].ToString().Trim();
            this.dt1.Rows[0]["diag_id"] = result_diag[0]["id_spr"].ToString();

            //this.dt1.Rows[0]["USLUGACOMPLEX_ID"] = this.cb_test.SelectedValue;
            string[] strs_uslugacomplex;
            strs_uslugacomplex = this.textBox3.Text.Split(' ');
            DataRow[] result_uslugacomplex = Info.ds.Tables["uslugacomplex"].Select("code_spr = '" + strs_uslugacomplex[0] + "'");
            this.dt1.Rows[0]["USLUGACOMPLEX"] = result_uslugacomplex[0]["name_spr"].ToString().Trim();
            this.dt1.Rows[0]["USLUGACOMPLEX_ID"] = result_uslugacomplex[0]["id_spr"].ToString();

            //string s = add_record_evnplbase_to_db();
            //MessageBox.Show(s);

        }

        string update_evnplbase_db(string id_evnplbase)
        {
            string ret = id_evnplbase;

            FbCommand cmd = new FbCommand("UPDATE EVNPLBASE SET EVNPL_NUMCARD='" +
                 this.dt1.Rows[0]["EVNPL_NUMCARD"].ToString() + "', EVNPL_ISFINISH=" +
                 this.dt1.Rows[0]["EVNPL_ISFINISH"] + ", EVN_DATETIME='" +
                 Convert.ToDateTime(this.dt1.Rows[0]["EVN_DATETIME"]).ToString("dd.MM.yyyy HH:mm:ss") + "', LPUSECTION_ID='" +
                 //Convert.ToDateTime(this.dt1.Rows[0]["EVN_DATE"]).ToString("dd.MM.yyy") + "', EVN_HH=" +
                 //this.dt1.Rows[0]["EVN_HH"] + ", EVN_MM=" +
                 //this.dt1.Rows[0]["EVN_MM"] + ", EVN_SS=" +
                 //this.dt1.Rows[0]["EVN_SS"] + ", LPUSECTION_ID='" +
                 this.dt1.Rows[0]["LPUSECTION_ID"].ToString() + "', TREATMENTCLASS_ID='" +
                 this.dt1.Rows[0]["TREATMENTCLASS_ID"].ToString() + "', SERVICETYPE_ID='" +
                 this.dt1.Rows[0]["SERVICETYPE_ID"].ToString() + "', VIZITTYPE_ID='" +
                 this.dt1.Rows[0]["VIZITTYPE_ID"].ToString() + "', MEDSTAFFFACT_ID='" +
                 this.dt1.Rows[0]["MEDSTAFFFACT_ID"].ToString() + "', PAYTYPE_ID='" +
                 this.dt1.Rows[0]["PAYTYPE_ID"].ToString() + "', DIAG_ID='" +
                 this.dt1.Rows[0]["DIAG_ID"].ToString() + "', USLUGACOMPLEX_ID='" +
                 this.dt1.Rows[0]["USLUGACOMPLEX_ID"].ToString() +
                 "', MEDICALCAREKIND_ID='" + this.dt1.Rows[0]["MEDICALCAREKIND_ID"].ToString() + "' WHERE ID_EVNPLBASE='" + id_evnplbase + "'", Info.conn);

            //cmd.Parameters.Clear();
            //cmd.Parameters.AddWithValue("@ID_EVNPLBASE", id_evnplbase);

            // ================================================================== Clipboard
            this.textBox2.Text = "";
            this.textBox2.Text = cmd.CommandText;
            Clipboard.SetText(this.textBox2.Text);


            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (FbException ex)
            {
                MessageBox.Show(ex.Message);
                ret = "";
            }

            return ret;
        }



        private void button3_Click(object sender, EventArgs e)
        {
            // save and close
            bool is_connect = Info.is_connect_db();
            if (!is_connect) return;

           
            string res = this.update_evnplbase_db(this.dt1.Rows[0][0].ToString());
            Info.conn.Close();

            // sync - save in Info.ds
            if (res != "")
            {
                DataRow[] result = Info.ds.Tables["evnplbase"].Select("id_evnplbase = '" + this.dt1.Rows[0][0].ToString() + "'");
                for (int i = 2; i < this.dt1.Columns.Count; i++)
                {
                    result[0][this.dt1.Columns[i].ColumnName] = this.dt1.Rows[0][this.dt1.Columns[i].ColumnName];
                }

                //result[0]["EVNPL_NUMCARD"] = this.dt1.Rows[0]["EVNPL_NUMCARD"];
                //result[0]["EVNPL_ISFINISH"] = this.dt1.Rows[0]["EVNPL_ISFINISH"];

                MessageBox.Show("Запись благополучно изменена в БД");

            }
            else MessageBox.Show("Запись не изменена в БД");

            this.button3.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void FormTAPedit_Load(object sender, EventArgs e)
        {
            this.dt1 = Info.ds.Tables["evnplbase"].Clone();
            DataRow[] result = Info.ds.Tables["evnplbase"].Select("id_evnplbase = '" + this.id_evnplbase + "'");
            foreach (DataRow dr in result)
            {
                this.dt1.ImportRow(dr);
            }
            //this.dt1.Rows.Add(result[0].ItemArray);
            this.dgv1.DataSource = this.dt1;

            // get field value
            this.textBox1.Text = this.dt1.Rows[0]["EVNPL_NUMCARD"].ToString();

            //string evn_date = Convert.ToDateTime(this.dt1.Rows[0]["EVN_DATE"]).ToString("dd.MM.yyyy");
            //evn_date = evn_date + " " + this.dt1.Rows[0]["EVN_HH"].ToString() + ":" + this.dt1.Rows[0]["EVN_MM"].ToString() + ":" + this.dt1.Rows[0]["EVN_SS"].ToString();
            //this.dateTimePicker1.Text = evn_date;

            this.dateTimePicker1.Value = Convert.ToDateTime(this.dt1.Rows[0]["EVN_DATETIME"]);

            //this.cb_diag.SelectedValue = this.dt1.Rows[0]["DIAG_ID"].ToString();
            DataRow[] result_diag = Info.ds.Tables["diag"].Select("id_spr = '" + this.dt1.Rows[0]["diag_id"].ToString() + "'");
            this.textBox4.Text = result_diag[0]["code_spr"].ToString() + " " + result_diag[0]["name_spr"].ToString().Trim();

            this.cb_medicalcarekind.SelectedValue = this.dt1.Rows[0]["MEDICALCAREKIND_ID"].ToString();
            this.cb_payt.SelectedValue = this.dt1.Rows[0]["PAYTYPE_ID"].ToString();
            this.cb_servicetype.SelectedValue = this.dt1.Rows[0]["SERVICETYPE_ID"].ToString();
            this.cb_treatmentclass.SelectedValue = this.dt1.Rows[0]["TREATMENTCLASS_ID"].ToString();
            this.cb_vizittype.SelectedValue = this.dt1.Rows[0]["VIZITTYPE_ID"].ToString();

            //this.cb_test.SelectedValue = this.dt1.Rows[0]["USLUGACOMPLEX_ID"].ToString();
            DataRow[] result_uslugacomplex = Info.ds.Tables["uslugacomplex"].Select("id_spr = '" + this.dt1.Rows[0]["uslugacomplex_id"].ToString() + "'");
            this.textBox3.Text = result_uslugacomplex[0]["code_spr"].ToString() + " " + result_uslugacomplex[0]["name_spr"].ToString().Trim();

            this.cb_openclose.SelectedValue = this.dt1.Rows[0]["EVNPL_ISFINISH"].ToString();


        }

        private void button4_Click(object sender, EventArgs e)
        {
            // select usluga
            using (FormSprUsluga fsu = new FormSprUsluga(this.textBox1.Text))
            {
                fsu.Text = "Выберите услугу";
                DialogResult result = fsu.ShowDialog();
                if (result == DialogResult.OK) this.textBox3.Text = Info.selected_usluga;

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // select diag
            using (FormSprDiag fsd = new FormSprDiag(this.textBox2.Text))
            {
                fsd.Text = "Выберите диагноз";
                DialogResult result = fsd.ShowDialog();
                if (result == DialogResult.OK) this.textBox4.Text = Info.selected_diag;

            }
        }




    }
}
