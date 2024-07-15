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
    public partial class FormTAP : Form
    {

        DataTable dt1;
        string id_patient_evnplbase;
        

        public FormTAP(string id_patient_evnplbase)
        {
            InitializeComponent();
            //

            this.dateTimePicker1.Value = DateTime.Now;
            this.id_patient_evnplbase = id_patient_evnplbase;


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

            //this.cb_lpusection.DataSource = Info.ds.Tables["lpusection"];
            //this.cb_lpusection.DisplayMember = "Name_spr";
            //this.cb_lpusection.ValueMember = "id_spr";

            //this.cb_vizitclass.DataSource = Info.ds.Tables["vizitclass"];
            //this.cb_vizitclass.DisplayMember = "Name_spr";
            //this.cb_vizitclass.ValueMember = "code_spr";

            //this.cb_lpusection.DataSource = Info.ds.Tables["lpusection"];
            //this.cb_lpusection.DisplayMember = "Name_spr";
            //this.cb_lpusection.ValueMember = "id_spr";

            this.dt1 = Info.ds.Tables["evnplbase"].Clone();           
            this.dgv1.DataSource = this.dt1;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.button1.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        string add_record_evnplbase_to_db()
        {
            string ret = "";

            //FbCommand cmd = new FbCommand("INSERT INTO EVNPLBASE (ID_EVNPLBASE,ID_PATIENT_EVNPLBASE,EVNPL_NUMCARD,EVNPL_ISFINISH,EVN_DATE,EVN_HH,EVN_MM,EVN_SS,LPUSECTION_ID,TREATMENTCLASS_ID,SERVICETYPE_ID,VIZITTYPE_ID,MEDICALCAREKIND_ID,LPU_ID,MEDSTAFFFACT_ID,PAYTYPE_ID,DIAG_ID,USLUGACOMPLEX_ID,EVNVIZITPL_ID,EVNPLBASE_ID) " +
            //                       "VALUES (@ID_EVNPLBASE,@ID_PATIENT_EVNPLBASE,@EVNPL_NUMCARD,@EVNPL_ISFINISH,@EVN_DATE,@EVN_HH,@EVN_MM,@EVN_SS,@LPUSECTION_ID,@TREATMENTCLASS_ID,@SERVICETYPE_ID,@VIZITTYPE_ID,@MEDICALCAREKIND_ID,@LPU_ID,@MEDSTAFFFACT_ID,@PAYTYPE_ID,@DIAG_ID,@USLUGACOMPLEX_ID,@EVNVIZITPL_ID,@EVNPLBASE_ID)", Info.conn);
            FbCommand cmd = new FbCommand("INSERT INTO EVNPLBASE (ID_EVNPLBASE,ID_PATIENT_EVNPLBASE,EVNPL_NUMCARD,EVNPL_ISFINISH,EVN_DATETIME,LPUSECTION_ID,TREATMENTCLASS_ID,SERVICETYPE_ID,VIZITTYPE_ID,MEDICALCAREKIND_ID,LPU_ID,MEDSTAFFFACT_ID,PAYTYPE_ID,DIAG_ID,USLUGACOMPLEX_ID,EVNVIZITPL_ID,EVNPLBASE_ID) " +
                                   "VALUES (@ID_EVNPLBASE,@ID_PATIENT_EVNPLBASE,@EVNPL_NUMCARD,@EVNPL_ISFINISH,@EVN_DATETIME,@LPUSECTION_ID,@TREATMENTCLASS_ID,@SERVICETYPE_ID,@VIZITTYPE_ID,@MEDICALCAREKIND_ID,@LPU_ID,@MEDSTAFFFACT_ID,@PAYTYPE_ID,@DIAG_ID,@USLUGACOMPLEX_ID,@EVNVIZITPL_ID,@EVNPLBASE_ID)", Info.conn);

            cmd.Parameters.Clear();
            ret = Guid.NewGuid().ToString();

            cmd.Parameters.AddWithValue("@ID_EVNPLBASE", ret);
            cmd.Parameters.AddWithValue("@ID_PATIENT_EVNPLBASE", this.id_patient_evnplbase);
            cmd.Parameters.AddWithValue("@EVNPL_NUMCARD", this.dt1.Rows[0]["evnpl_numcard"].ToString());
            cmd.Parameters.AddWithValue("@EVNPL_ISFINISH", this.dt1.Rows[0]["evnpl_isfinish"].ToString());
            cmd.Parameters.AddWithValue("@EVN_DATETIME", this.dt1.Rows[0]["evn_datetime"]);
            //cmd.Parameters.AddWithValue("@EVN_DATE", this.dt1.Rows[0]["evn_date"]);
            //cmd.Parameters.AddWithValue("@EVN_HH", this.dt1.Rows[0]["evn_hh"]);
            //cmd.Parameters.AddWithValue("@EVN_MM", this.dt1.Rows[0]["evn_mm"]);
            //cmd.Parameters.AddWithValue("@EVN_SS", this.dt1.Rows[0]["evn_ss"]);
            //cmd.Parameters.AddWithValue("@EVN_SETDT_TIME", this.dt1.Rows[0]["evn_setdt_time"]);
            cmd.Parameters.AddWithValue("@LPUSECTION_ID", Info.LpuSection_sid);
            cmd.Parameters.AddWithValue("@TREATMENTCLASS_ID", this.dt1.Rows[0]["treatmentclass_id"].ToString());
            cmd.Parameters.AddWithValue("@SERVICETYPE_ID", this.dt1.Rows[0]["servicetype_id"].ToString());
            cmd.Parameters.AddWithValue("@VIZITTYPE_ID", this.dt1.Rows[0]["vizittype_id"].ToString());
            cmd.Parameters.AddWithValue("@MEDICALCAREKIND_ID", this.dt1.Rows[0]["medicalcarekind_id"].ToString());
            cmd.Parameters.AddWithValue("@LPU_ID", Info.Lpu_sid);
            //cmd.Parameters.AddWithValue("@RESULTCLASS_ID", this.dt1.Rows[0]["resultclass_id"].ToString());
            //cmd.Parameters.AddWithValue("@RESULTDESEASETYPE_ID", this.dt1.Rows[0]["resultdeseasetype_id"].ToString());
            //cmd.Parameters.AddWithValue("@VIZITCLASS_ID", this.dt1.Rows[0]["vizitclass_id"].ToString());
            cmd.Parameters.AddWithValue("@MEDSTAFFFACT_ID", Info.MedStaffFact_sid);
            cmd.Parameters.AddWithValue("@PAYTYPE_ID", this.dt1.Rows[0]["PayType_id"].ToString());

            cmd.Parameters.AddWithValue("@DIAG_ID", this.dt1.Rows[0]["Diag_id"].ToString());

            cmd.Parameters.AddWithValue("@USLUGACOMPLEX_ID", this.dt1.Rows[0]["uslugacomplex_id"].ToString());

            //cmd.Parameters.AddWithValue("@EVNVIZITPL_TIME", this.dt1.Rows[0]["evnvizitpl_time"].ToString());
            //cmd.Parameters.AddWithValue("@DESEASETYPE_ID", this.dt1.Rows[0]["deseasetype_id"].ToString());
            cmd.Parameters.AddWithValue("@EVNVIZITPL_ID", this.dt1.Rows[0]["evnvizitpl_id"].ToString());
            cmd.Parameters.AddWithValue("@EVNPLBASE_ID", this.dt1.Rows[0]["evnplbase_id"].ToString());

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

        private void button2_Click(object sender, EventArgs e)
        {
            // is double data	
            //DataRow[] result = Info.ds.Tables["evnplbase"].Select("EVNPL_NUMCARD = '" + this.textBox1.Text.ToString().Trim() + "'");
            //if (result.Length != 0)
            //{
            //    MessageBox.Show("Такой НОМЕР ТАЛОНА уже существует");
            //    return;
            //}

            // evnplbase save data record
            this.dt1.Clear();
            //this.dt1.Rows.Add();
            DataRow row = this.dt1.NewRow();

            row["ID_EVNPLBASE"] = Guid.NewGuid().ToString();
            row["ID_PATIENT_EVNPLBASE"] = this.id_patient_evnplbase;

            //row["EVNPL_NUMCARD"] = this.textBox1.Text.ToString().Trim();
            row["EVNPL_NUMCARD"] = Guid.NewGuid().ToString();


            row["EVNPL_ISFINISH"] = 0;
            row["EVN_DATETIME"] = this.dateTimePicker1.Value;
            //row["EVN_DATE"] = this.dateTimePicker1.Value.Date;
            //row["EVN_HH"] = this.dateTimePicker1.Value.TimeOfDay.Hours;
            //row["EVN_MM"] = this.dateTimePicker1.Value.TimeOfDay.Minutes;
            //row["EVN_SS"] = this.dateTimePicker1.Value.TimeOfDay.Seconds;
            //row["EVN_SETDT_TIME"] = this.dateTimePicker1.Value.TimeOfDay;
            row["LPUSECTION_ID"] = Info.LpuSection_sid;
            row["MEDSTAFFFACT_ID"] = Info.MedStaffFact_sid;
            row["TREATMENTCLASS_ID"] = this.cb_treatmentclass.SelectedValue;
            row["SERVICETYPE_ID"] = this.cb_servicetype.SelectedValue;
            row["VIZITTYPE_ID"] = this.cb_vizittype.SelectedValue;
            row["MEDICALCAREKIND_ID"] = this.cb_medicalcarekind.SelectedValue;
            row["PAYTYPE_ID"] = this.cb_payt.SelectedValue;

            //row["DIAG_ID"] = this.cb_diag.SelectedValue;
            string[] strs_diag;
            strs_diag = this.textBox3.Text.Split(' ');
            DataRow[] result_diag = Info.ds.Tables["diag"].Select("code_spr = '" + strs_diag[0] + "'");
            if (result_diag.Length == 0)
            {
                MessageBox.Show("Диагноз не выбран или не найден");
                return;
            }
            row["diag"] = result_diag[0]["name_spr"].ToString().Trim();
            row["diag_id"] = result_diag[0]["id_spr"].ToString();

            //row["USLUGACOMPLEX_ID"] = this.cb_test.SelectedValue;
            string[] strs_uslugacomplex;
            strs_uslugacomplex = this.textBox2.Text.Split(' ');
            DataRow[] result_uslugacomplex = Info.ds.Tables["uslugacomplex"].Select("code_spr = '" + strs_uslugacomplex[0] + "'");
            if (result_uslugacomplex.Length == 0)
            {
                MessageBox.Show("Услуга не выбрана или не найдена");
                return;
            }
            row["uslugacomplex"] = result_uslugacomplex[0]["name_spr"].ToString().Trim();
            row["uslugacomplex_id"] = result_uslugacomplex[0]["id_spr"].ToString();

            this.dt1.Rows.Add(row);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // save and close
            if (this.dt1.Rows.Count == 0)
            {
                MessageBox.Show("Вы не создали посещение");
                return;
            }

            // проверка подключения db
            bool is_connect = Info.is_connect_db();
            if (!is_connect) return;
            
            string id_evnplbase = this.add_record_evnplbase_to_db();
            Info.conn.Close();
            
            // sync - save in Info.ds
            if (id_evnplbase != "")
            {
                this.dt1.Rows[0][0] = id_evnplbase;    // fact db id
                foreach (DataRow dr in this.dt1.Rows)
                {
                    //Info.ds.Tables["evnplbase"].Rows.Add(dr.ItemArray);
                    Info.ds.Tables["evnplbase"].ImportRow(dr);
                }
                MessageBox.Show("Запаись благополучно сохранена в БД");
            }
            else MessageBox.Show("Запаись не сохранена в БД");

            this.button3.DialogResult = DialogResult.OK;
            this.Close();        

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // select usluga
            using (FormSprUsluga fsu = new FormSprUsluga(this.textBox1.Text))
            {
                fsu.Text = "Выберите услугу";
                DialogResult result = fsu.ShowDialog();
                if (result == DialogResult.OK) this.textBox2.Text = Info.selected_usluga;

            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            // select diag
            using (FormSprDiag fsd = new FormSprDiag(this.textBox2.Text))
            {
                fsd.Text = "Выберите диагноз";
                DialogResult result = fsd.ShowDialog();
                if (result == DialogResult.OK) this.textBox3.Text = Info.selected_diag;

            }
        }




    }
}
