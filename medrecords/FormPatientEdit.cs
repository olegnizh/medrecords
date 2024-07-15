using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.OleDb;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using FirebirdSql.Data.FirebirdClient;


namespace medrecords
{
    public partial class FormPatientEdit : Form
    {

        DataTable dt1;
        string id_patient;

        public FormPatientEdit(string id)
        {
            InitializeComponent();

            this.id_patient = id;

            // dt_sex
            this.cb_sex.DataSource = Info.ds.Tables["sex"];
            this.cb_sex.DisplayMember = "name_spr";
            this.cb_sex.ValueMember = "id_spr";

            // dt_socstatus
            this.cb_socstatus.DataSource = Info.ds.Tables["socstatus"];
            this.cb_socstatus.DisplayMember = "name_spr";
            this.cb_socstatus.ValueMember = "id_spr";


        }

        string update_record_patient_to_db(string id_patient)
        {
            string ret = id_patient;

            FbCommand cmd = new FbCommand("UPDATE PATIENT SET FIO_FAM='" +
                 this.dt1.Rows[0]["FIO_FAM"].ToString() + "', FIO_NAME='" +
                 this.dt1.Rows[0]["FIO_NAME"].ToString() + "', FIO_OTCH='" +
                 this.dt1.Rows[0]["FIO_OTCH"].ToString() + "', SEX='" +
                 this.dt1.Rows[0]["SEX"].ToString() + "', SOCSTATUS='" +
                 this.dt1.Rows[0]["SOCSTATUS"].ToString() + "', DR='" +
                 Convert.ToDateTime(this.dt1.Rows[0]["DR"]).ToString("dd.MM.yyyy") + "', PDOC='" +
                 this.dt1.Rows[0]["PDOC"].ToString() + "', PDOC_A='" +
                 this.dt1.Rows[0]["PDOC_A"].ToString() + "', PDOC_B='" +
                 Convert.ToDateTime(this.dt1.Rows[0]["PDOC_B"]).ToString("dd.MM.yyyy") + "', SS='" +
                 this.dt1.Rows[0]["SS"].ToString() + "', OMS='" +
                 this.dt1.Rows[0]["OMS"].ToString() + "' WHERE ID_PATIENT=@ID_PATIENT", Info.conn);

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ID_PATIENT", id_patient);

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
            // save and close

            this.button3.PerformClick();

            bool is_connect = Info.is_connect_db();
            if (!is_connect) return;

            string res = this.update_record_patient_to_db(this.dt1.Rows[0][0].ToString());
            Info.conn.Close();

            // sync - save in Info.ds
            if (res != "")
            {
                DataRow[] result = Info.ds.Tables["patient"].Select("id_patient = '" + this.dt1.Rows[0][0].ToString() + "'");
                for (int i = 1; i < this.dt1.Columns.Count; i++)
                {
                    result[0][this.dt1.Columns[i].ColumnName] = this.dt1.Rows[0][this.dt1.Columns[i].ColumnName];
                }

                //result[0]["EVNPL_NUMCARD"] = this.dt1.Rows[0]["EVNPL_NUMCARD"];
                //result[0]["EVNPL_ISFINISH"] = this.dt1.Rows[0]["EVNPL_ISFINISH"];

                MessageBox.Show("Запись благополучно изменена в БД");

            }
            else MessageBox.Show("Запись не изменена в БД");

            this.button2.DialogResult = DialogResult.OK;
            this.Close();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            // cancel patient;
            this.button1.DialogResult = DialogResult.Cancel;
            this.Close();

        }

        private void FormPatientEdit_Load(object sender, EventArgs e)
        {
            this.dt1 = Info.ds.Tables["patient"].Clone();
            DataRow[] result = Info.ds.Tables["patient"].Select("id_patient = '" + this.id_patient + "'");
            foreach (DataRow dr in result)
            {
                this.dt1.ImportRow(dr);
                //this.dt1.Rows.Add(result[0].ItemArray);
            }
            this.dgv1.DataSource = this.dt1;

            // get field value
            this.t_fam.Text = this.dt1.Rows[0]["FIO_FAM"].ToString();
            this.t_im.Text = this.dt1.Rows[0]["FIO_NAME"].ToString();
            this.t_otch.Text = this.dt1.Rows[0]["FIO_OTCH"].ToString();
            this.cb_sex.SelectedValue = this.dt1.Rows[0]["SEX"].ToString();
            this.cb_socstatus.SelectedValue = this.dt1.Rows[0]["SOCSTATUS"].ToString();
            this.d_dr.Value = Convert.ToDateTime(this.dt1.Rows[0]["DR"]);
            this.m_pdoc.Text = this.dt1.Rows[0]["PDOC"].ToString();
            this.t_pdoc_a.Text = this.dt1.Rows[0]["PDOC_A"].ToString();
            this.d_pdoc_b.Value = Convert.ToDateTime(this.dt1.Rows[0]["PDOC_B"]);
            this.m_ss.Text = this.dt1.Rows[0]["SS"].ToString();
            this.m_oms.Text = this.dt1.Rows[0]["OMS"].ToString();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // is double data
            this.dt1.Rows[0]["FIO_FAM"] = this.t_fam.Text.ToString().Trim();
            this.dt1.Rows[0]["FIO_NAME"] = this.t_im.Text.ToString().Trim();
            this.dt1.Rows[0]["FIO_OTCH"] = this.t_otch.Text.ToString().Trim();
            this.dt1.Rows[0]["SEX"] = this.cb_sex.SelectedValue.ToString();
            this.dt1.Rows[0]["SOCSTATUS"] = this.cb_socstatus.SelectedValue.ToString();
            this.dt1.Rows[0]["DR"] = this.d_dr.Value.ToString("dd.MM.yyyy");
            this.dt1.Rows[0]["PDOC"] = this.m_pdoc.Text.ToString();
            this.dt1.Rows[0]["PDOC_A"] = this.t_pdoc_a.Text.ToString().Trim();
            this.dt1.Rows[0]["PDOC_B"] = this.d_pdoc_b.Value.ToString("dd.MM.yyyy");
            this.dt1.Rows[0]["SS"] = this.m_ss.Text.ToString();
            this.dt1.Rows[0]["OMS"] = this.m_oms.Text.ToString();

        }


    }
}
