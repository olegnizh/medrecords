using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using FirebirdSql.Data.FirebirdClient;


namespace medrecords
{   

    public partial class FormDirectionEdit : Form
    {
        
        string id_direction;

        //bool is_tap_ecp = false;

        DataTable dt_prescriptiontype;
        DataTable dt_dirtype;

        DataTable dt_dir;
        DataTable dt_tap;

        


        public FormDirectionEdit(string id_direction)
        {
            InitializeComponent();

            this.d_dir.Value = DateTime.Now;

            this.id_direction = id_direction;

            this.dt_dirtype = Info.ds.Tables["dirtype"].Clone();
            foreach (DataRow dr1 in Info.ds.Tables["dirtype"].Rows)
            {
                DataRow row1 = this.dt_dirtype.NewRow();
                row1["id_spr"] = dr1["id_spr"].ToString();
                row1["name_spr"] = dr1["id_spr"].ToString() + ". " + dr1["name_spr"].ToString();
                this.dt_dirtype.Rows.Add(row1);
            }

            this.dt_prescriptiontype = Info.ds.Tables["prescriptiontype"].Clone();
            foreach (DataRow dr2 in Info.ds.Tables["prescriptiontype"].Rows)
            {
                DataRow row2 = this.dt_prescriptiontype.NewRow();
                row2["id_spr"] = dr2["id_spr"].ToString();
                row2["name_spr"] = dr2["id_spr"].ToString() + ". " + dr2["name_spr"].ToString();
                this.dt_prescriptiontype.Rows.Add(row2); 
            }
            
            this.load_data_dir();


        }

        void load_data_dir()
        {
            // ================================================================== посещение
            // Вид обращения
            this.cb_treatmentclass.DataSource = Info.ds.Tables["treatmentclass"];
            this.cb_treatmentclass.DisplayMember = "Name_spr";
            this.cb_treatmentclass.ValueMember = "id_spr";
            this.cb_treatmentclass.SelectedValue = 1;
            this.cb_treatmentclass.Enabled = false;
            // Цель посещения
            this.cb_vizittype.DataSource = Info.ds.Tables["vizittype"];
            this.cb_vizittype.DisplayMember = "Name_spr";
            this.cb_vizittype.ValueMember = "id_spr";
            this.cb_vizittype.SelectedValue = 520101000000016;
            this.cb_vizittype.Enabled = false;
            // место обслуживания (посещения)
            this.cb_servicetype.DataSource = Info.ds.Tables["servicetype"];
            this.cb_servicetype.DisplayMember = "Name_spr";
            this.cb_servicetype.ValueMember = "code_spr";
            this.cb_servicetype.SelectedValue = 6;
            this.cb_servicetype.Enabled = false;
            // вид медицинской помощи
            this.cb_medicalcarekind.DataSource = Info.ds.Tables["medicalcarekind"];
            this.cb_medicalcarekind.DisplayMember = "Name_spr";
            this.cb_medicalcarekind.ValueMember = "code_spr";
            this.cb_medicalcarekind.SelectedValue = 1;
            this.cb_medicalcarekind.Enabled = false;



            // ================================================================= направление
            //this.cb_dirtype.DataSource = Info.ds.Tables["dirtype"];
            this.cb_dirtype.DataSource = this.dt_dirtype;
            this.cb_dirtype.DisplayMember = "Name_spr";
            this.cb_dirtype.ValueMember = "id_spr";
            this.cb_dirtype.SelectedValue = 10;
            this.cb_dirtype.Enabled = false;

            this.cb_typep.DataSource = this.dt_prescriptiontype;            
            //this.cb_typep.DataSource = Info.ds.Tables["prescriptiontype"];
            this.cb_typep.DisplayMember = "name_spr";
            this.cb_typep.ValueMember = "id_spr";
            this.cb_typep.SelectedValue = 11;
            this.cb_typep.Enabled = false;

            //this.cb_diag.DataSource = Info.ds.Tables["diag"];
            //this.cb_diag.DisplayMember = "Name_spr";
            //this.cb_diag.ValueMember = "id_spr";

            this.cb_prof.DataSource = Info.ds.Tables["lpusectionprofile"];
            this.cb_prof.DisplayMember = "Name_spr";
            this.cb_prof.ValueMember = "id_spr";


                //this.cb_test.DataSource = Info.ds.Tables["uslugacomplex"];
                //this.cb_test.DisplayMember = "Name_spr";
                //this.cb_test.ValueMember = "id_spr";


                //this.cb_test.DataSource = Info.ds.Tables["medtest"];
                //this.cb_test.DisplayMember = "uslugacomplex_name";
                //this.cb_test.ValueMember = "uslugacomplex_id";


            //this.cb_issled.DataSource = Info.ds.Tables["uslugacomplexfull2"];
            //this.cb_issled.DisplayMember = "uslugacomplex_name";
            //this.cb_issled.ValueMember = "uslugacomplex_id";

            this.cb_uslmp.DataSource = Info.ds.Tables["lpuunittype"];
            this.cb_uslmp.DisplayMember = "Name_spr";
            this.cb_uslmp.ValueMember = "id_spr";

            this.cb_payt.DataSource = Info.ds.Tables["paytype"];
            this.cb_payt.DisplayMember = "Name_spr";
            this.cb_payt.ValueMember = "id_spr";
            this.cb_payt.SelectedValue = 520101000000008;
            this.cb_payt.Enabled = false;

            //this.cb_in.DataSource = Info.ds.Tables["lpulist"];
            //this.cb_in.DisplayMember = "Org_Name";
            //this.cb_in.ValueMember = "Lpu_id";

            this.cb_cito.DataSource = Info.ds.Tables["cito"];
            this.cb_cito.DisplayMember = "Name_spr";
            this.cb_cito.ValueMember = "id_spr";
            this.cb_cito.SelectedValue = "0";

            //this.dt_tap = Info.ds.Tables["evnplbase"].Clone();
            //this.dgv_tap.DataSource = this.dt_tap;

            //this.dt_dir = Info.ds.Tables["direction"].Clone();
            //this.dgv_dir.DataSource = this.dt_dir;


        }



        string update_evnplbase_db(string id_evnplbase)
        {
            string ret = id_evnplbase;

            FbCommand cmd = new FbCommand("UPDATE EVNPLBASE SET EVNPL_NUMCARD='" +
                 this.dt_tap.Rows[0]["EVNPL_NUMCARD"].ToString() + "', EVNPL_ISFINISH=" +
                 this.dt_tap.Rows[0]["EVNPL_ISFINISH"] + ", EVN_DATETIME='" +
                 Convert.ToDateTime(this.dt_tap.Rows[0]["EVN_DATETIME"]).ToString("dd.MM.yyyy HH:mm:ss") + "', LPUSECTION_ID='" +
                 //Convert.ToDateTime(this.dt1.Rows[0]["EVN_DATE"]).ToString("dd.MM.yyy") + "', EVN_HH=" +
                 //this.dt1.Rows[0]["EVN_HH"] + ", EVN_MM=" +
                 //this.dt1.Rows[0]["EVN_MM"] + ", EVN_SS=" +
                 //this.dt1.Rows[0]["EVN_SS"] + ", LPUSECTION_ID='" +
                 this.dt_tap.Rows[0]["LPUSECTION_ID"].ToString() + "', TREATMENTCLASS_ID='" +
                 this.dt_tap.Rows[0]["TREATMENTCLASS_ID"].ToString() + "', SERVICETYPE_ID='" +
                 this.dt_tap.Rows[0]["SERVICETYPE_ID"].ToString() + "', VIZITTYPE_ID='" +
                 this.dt_tap.Rows[0]["VIZITTYPE_ID"].ToString() + "', MEDSTAFFFACT_ID='" +
                 this.dt_tap.Rows[0]["MEDSTAFFFACT_ID"].ToString() + "', PAYTYPE_ID='" +
                 this.dt_tap.Rows[0]["PAYTYPE_ID"].ToString() + "', DIAG_ID='" +
                 this.dt_tap.Rows[0]["DIAG_ID"].ToString() + "', USLUGACOMPLEX_ID='" +
                 this.dt_tap.Rows[0]["USLUGACOMPLEX_ID"].ToString() +
                 "', MEDICALCAREKIND_ID='" + this.dt_tap.Rows[0]["MEDICALCAREKIND_ID"].ToString() + "' WHERE ID_EVNPLBASE='" + id_evnplbase + "'", Info.conn);

            //cmd.Parameters.Clear();
            //cmd.Parameters.AddWithValue("@ID_EVNPLBASE", id_evnplbase);

            // ================================================================== Clipboard
            //this.textBox2.Text = "";
            //this.textBox2.Text = cmd.CommandText;
            //Clipboard.SetText(this.textBox2.Text);


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

        string update_record_direction_to_db(string id_direction)
        {
            string ret = id_direction;

            //string[] strs_uslugacomplex;
            //strs_uslugacomplex = this.textBox1.Text.Split(' ');
            //DataRow[] result_uslugacomplex = Info.ds.Tables["uslugacomplex"].Select("code_spr = '" + strs_uslugacomplex[0] + "'");
            //string uslugacomplex_id = result_uslugacomplex[0]["id_spr"].ToString();

            //string[] strs_diag;
            //strs_diag = this.textBox2.Text.Split(' ');
            //DataRow[] result_diag = Info.ds.Tables["diag"].Select("code_spr = '" + strs_diag[0] + "'");
            //string diag_id = result_diag[0]["id_spr"].ToString();

            FbCommand cmd = new FbCommand("UPDATE DIRECTION SET NUM_DIR='" +
                 this.dt_dir.Rows[0]["NUM_DIR"].ToString() + "', DATE_DIR='" +
                 Convert.ToDateTime(this.dt_dir.Rows[0]["DATE_DIR"]).ToString("dd.MM.yyyy") + "', DIRTYPE_ID='" +
                 this.dt_dir.Rows[0]["DIRTYPE_ID"].ToString() + "', PRESCRIPTIONTYPE_ID='" +
                 this.dt_dir.Rows[0]["PRESCRIPTIONTYPE_ID"].ToString() + "', DIAG_ID='" +
                 this.dt_dir.Rows[0]["DIAG_ID"].ToString() + "', LPU_DID='" +
                 Info.Lpu_sid + "', LPUSECTIONPROFILE_ID='" +
                 this.dt_dir.Rows[0]["LPUSECTIONPROFILE_ID"].ToString() + "', USLUGACOMPLEXMEDSERVICE_ID='" +
                 this.dt_dir.Rows[0]["USLUGACOMPLEXMEDSERVICE_ID"].ToString() + "', LPUUNITTYPE_ID='" +
                 this.dt_dir.Rows[0]["LPUUNITTYPE_ID"].ToString() + "', PAYTYPE_ID='" +
                 this.dt_dir.Rows[0]["PAYTYPE_ID"].ToString() + "', EVNPRESCR_ISCITO='" +
                 this.dt_dir.Rows[0]["EVNPRESCR_ISCITO"].ToString() + "' WHERE ID_DIRECTION='" + id_direction + "'", Info.conn);

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

        private void button1_Click(object sender, EventArgs e)
        {
            // save and close evnplbase
            bool is_connect = false;
            string res = "";

            // проверка подключения db
            is_connect = Info.is_connect_db();
            if (!is_connect)
            {
                MessageBox.Show("is-connect : Нет подключения к БД");
                return;
            }

            //if (this.is_tap_ecp)
            //{
                this.button6.PerformClick();
                res = this.update_evnplbase_db(this.dt_tap.Rows[0][0].ToString());
                if (res != "")
                {
                    DataRow[] result_tap = Info.ds.Tables["evnplbase"].Select("id_evnplbase = '" + this.dt_tap.Rows[0][0].ToString() + "'");
                    for (int i = 2; i < this.dt_tap.Columns.Count; i++)
                    {
                        result_tap[0][this.dt_tap.Columns[i].ColumnName] = this.dt_tap.Rows[0][this.dt_tap.Columns[i].ColumnName];          
                    }
                }
                else
                {
                    MessageBox.Show("Запись по посещению не изменена в БД");
                    Info.conn.Close();
                    return;
                }
            //}

            this.button3.PerformClick();
            res = this.update_record_direction_to_db(this.dt_dir.Rows[0][0].ToString());                      
            if (res != "")
            {
                DataRow[] result_dir = Info.ds.Tables["direction"].Select("id_direction = '" + this.dt_dir.Rows[0][0].ToString() + "'");
                for (int i = 2; i < this.dt_dir.Columns.Count; i++)
                {
                    result_dir[0][this.dt_dir.Columns[i].ColumnName] = this.dt_dir.Rows[0][this.dt_dir.Columns[i].ColumnName];
                }
            }
            else
            {
                MessageBox.Show("Запись по направлению не изменена в БД");
                Info.conn.Close();
                return;
            }

            Info.conn.Close();
            this.button1.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.button2.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void tb_numdir_Leave(object sender, EventArgs e)
        {
            //DataRow[] result = table.Select("Size >= 230 AND Sex = 'm'");
            DataRow[] result = Info.ds.Tables["direction"].Select("num_dir = '" + this.num_dir.ToString().Trim() + "'");
            if (result.Length != 0)
            {
                MessageBox.Show("Такой НОМЕР НАПРАВЛЕНИЯ уже существует");
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // change record napr
            this.dt_dir.Rows[0]["NUM_DIR"] = this.num_dir.Text.ToString().Trim();
            this.dt_dir.Rows[0]["DATE_DIR"] = this.d_dir.Value.ToString("dd.MM.yyyy");
 
            //if (this.is_tap_ecp)
            //{

                //this.dt1.Rows[0]["DIAG"] = this.cb_diag.Text.ToString();
                string[] strs_diag;
                strs_diag = this.textBox2.Text.Split(' ');
                DataRow[] result_diag = Info.ds.Tables["diag"].Select("code_spr = '" + strs_diag[0] + "'");
                this.dt_dir.Rows[0]["diag"] = result_diag[0]["name_spr"].ToString().Trim();
                this.dt_dir.Rows[0]["diag_id"] = result_diag[0]["id_spr"].ToString();

                string[] strs_uslugacomplex;
                strs_uslugacomplex = this.textBox1.Text.Split(' ');
                DataRow[] result_uslugacomplex = Info.ds.Tables["uslugacomplex"].Select("enddate is null and code_spr = '" + strs_uslugacomplex[0] + "'");
                this.dt_dir.Rows[0]["USLUGACOMPLEX"] = result_uslugacomplex[0]["name_spr"].ToString().Trim();
                this.dt_dir.Rows[0]["USLUGACOMPLEXMEDSERVICE_ID"] = result_uslugacomplex[0]["id_spr"].ToString();

            //}

            this.dt_dir.Rows[0]["LPUSECTIONPROFILE_ID"] = this.cb_prof.SelectedValue.ToString();
            this.dt_dir.Rows[0]["LPUUNITTYPE_ID"] = this.cb_uslmp.SelectedValue.ToString();
            this.dt_dir.Rows[0]["EVNPRESCR_ISCITO"] = this.cb_cito.SelectedValue.ToString();


        }

        private void button4_Click(object sender, EventArgs e)
        {
            // select usluga
            using (FormSprUsluga fsu = new FormSprUsluga(this.textBox1.Text))
            {
                fsu.Text = "Выберите услугу";
                DialogResult result = fsu.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.textBox1.Text = Info.selected_usluga;

                }
                        
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            // select diag
            using (FormSprDiag fsd = new FormSprDiag(this.textBox2.Text))
            {
                fsd.Text = "Выберите диагноз";
                DialogResult result = fsd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.textBox2.Text = Info.selected_diag;

                }

            }

        }



        private void button6_Click(object sender, EventArgs e)
        {
            // TAP add 1
            this.dt_tap.Rows[0]["EVNPL_NUMCARD"] = this.num_tap.Text.ToString().Trim();
            this.dt_tap.Rows[0]["EVNPL_ISFINISH"] = "0"; //  this.cb_openclose.SelectedValue;
            this.dt_tap.Rows[0]["EVN_DATETIME"] = this.d_dir.Value;
            //this.dt1.Rows[0]["EVN_DATE"] = this.dateTimePicker1.Value.Date;
            //this.dt1.Rows[0]["EVN_HH"] = this.dateTimePicker1.Value.TimeOfDay.Hours;
            //this.dt1.Rows[0]["EVN_MM"] = this.dateTimePicker1.Value.TimeOfDay.Minutes;
            //this.dt1.Rows[0]["EVN_SS"] = this.dateTimePicker1.Value.TimeOfDay.Seconds;
            //row["EVN_SETDT_TIME"] = this.dateTimePicker1.Value.TimeOfDay;
            this.dt_tap.Rows[0]["LPUSECTION_ID"] = Info.LpuSection_sid;
            this.dt_tap.Rows[0]["MEDSTAFFFACT_ID"] = Info.MedStaffFact_sid;
            this.dt_tap.Rows[0]["TREATMENTCLASS_ID"] = this.cb_treatmentclass.SelectedValue;
            this.dt_tap.Rows[0]["SERVICETYPE_ID"] = this.cb_servicetype.SelectedValue;
            this.dt_tap.Rows[0]["VIZITTYPE_ID"] = this.cb_vizittype.SelectedValue;
            this.dt_tap.Rows[0]["MEDICALCAREKIND_ID"] = this.cb_medicalcarekind.SelectedValue;
            this.dt_tap.Rows[0]["PAYTYPE_ID"] = this.cb_payt.SelectedValue;

            //this.dt1.Rows[0]["DIAG_ID"] = this.cb_diag.SelectedValue;
            string[] strs_diag;
            strs_diag = this.textBox2.Text.Split(' ');
            DataRow[] result_diag = Info.ds.Tables["diag"].Select("code_spr = '" + strs_diag[0] + "'");
            this.dt_tap.Rows[0]["diag"] = result_diag[0]["name_spr"].ToString().Trim();
            this.dt_tap.Rows[0]["diag_id"] = result_diag[0]["id_spr"].ToString();

            //this.dt1.Rows[0]["USLUGACOMPLEX_ID"] = this.cb_test.SelectedValue;
            string[] strs_uslugacomplex;
            strs_uslugacomplex = this.textBox1.Text.Split(' ');
            DataRow[] result_uslugacomplex = Info.ds.Tables["uslugacomplex"].Select("enddate is null and code_spr = '" + strs_uslugacomplex[0] + "'");
            this.dt_tap.Rows[0]["USLUGACOMPLEX"] = result_uslugacomplex[0]["name_spr"].ToString().Trim();
            this.dt_tap.Rows[0]["USLUGACOMPLEX_ID"] = result_uslugacomplex[0]["id_spr"].ToString();


        }

        private void FormDirectionEdit_Load(object sender, EventArgs e)
        {

            // direction
            this.dt_dir = Info.ds.Tables["direction"].Clone();
            DataRow[] result_dir = Info.ds.Tables["direction"].Select("id_direction = '" + this.id_direction + "'");
            foreach (DataRow dr_dir in result_dir)
            {
                this.dt_dir.ImportRow(dr_dir);
            }
            this.dgv_dir.DataSource = this.dt_dir;

            // tap
            this.dt_tap = Info.ds.Tables["evnplbase"].Clone();
            DataRow[] result_tap = Info.ds.Tables["evnplbase"].Select("id_evnplbase = '" + result_dir[0][1] + "'");
            foreach (DataRow dr_tap in result_tap)
            {
                this.dt_tap.ImportRow(dr_tap);
            }
            this.dgv_tap.DataSource = this.dt_tap;

            // is_tap_ecp ===================================================================
            //if (this.dt_tap.Rows[0][17].ToString() != "")
            //    this.is_tap_ecp = true;
            //else
            //    this.is_tap_ecp = false;
            //MessageBox.Show(this.dt_tap.Rows[0][17].ToString());

            // usluga diag
            DataRow[] result_uslugacomplex = Info.ds.Tables["uslugacomplex"].Select("id_spr = '" + this.dt_dir.Rows[0]["uslugacomplexmedservice_id"].ToString() + "'");
            this.textBox1.Text = result_uslugacomplex[0]["code_spr"].ToString() + " " + result_uslugacomplex[0]["name_spr"].ToString().Trim();

            DataRow[] result_diag = Info.ds.Tables["diag"].Select("id_spr = '" + this.dt_dir.Rows[0]["diag_id"].ToString() + "'");
            this.textBox2.Text = result_diag[0]["code_spr"].ToString() + " " + result_diag[0]["name_spr"].ToString().Trim();

            // data
            this.num_tap.Text = this.dt_tap.Rows[0]["EVNPL_NUMCARD"].ToString();
            this.cb_medicalcarekind.SelectedValue = this.dt_tap.Rows[0]["MEDICALCAREKIND_ID"].ToString();
            this.cb_servicetype.SelectedValue = this.dt_tap.Rows[0]["SERVICETYPE_ID"].ToString();
            this.cb_treatmentclass.SelectedValue = this.dt_tap.Rows[0]["TREATMENTCLASS_ID"].ToString();
            this.cb_vizittype.SelectedValue = this.dt_tap.Rows[0]["VIZITTYPE_ID"].ToString();
            //this.cb_openclose.SelectedValue = this.dt_tap.Rows[0]["EVNPL_ISFINISH"].ToString();

            // tap data not visible
            /*
            if (this.is_tap_ecp)
            {
                this.label19.Visible = false;
                this.num_tap.Visible = false;
                this.button6.Visible = false;
                this.dgv_tap.Visible = false;

                this.label9.Visible = false;
                this.button4.Visible = false;
                this.textBox1.Visible = false;
                this.label3.Visible = false;
                this.button5.Visible = false;
                this.textBox2.Visible = false;
            }
            */

            // dir data
            this.num_dir.Text = this.dt_dir.Rows[0]["NUM_DIR"].ToString();
            this.d_dir.Text = Convert.ToDateTime(this.dt_dir.Rows[0]["DATE_DIR"]).ToString("dd.MM.yyyy");
            this.cb_dirtype.SelectedValue = this.dt_dir.Rows[0]["DIRTYPE_ID"].ToString();
            this.cb_typep.SelectedValue = this.dt_dir.Rows[0]["PRESCRIPTIONTYPE_ID"].ToString();          
            //Info.Lpu_sid;
            //Info.MedStaffFact_sid;
            this.cb_prof.SelectedValue = this.dt_dir.Rows[0]["LPUSECTIONPROFILE_ID"].ToString();
            this.cb_uslmp.SelectedValue = this.dt_dir.Rows[0]["LPUUNITTYPE_ID"].ToString();
            this.cb_payt.SelectedValue = this.dt_dir.Rows[0]["PAYTYPE_ID"].ToString();
            this.cb_cito.SelectedValue = this.dt_dir.Rows[0]["EVNPRESCR_ISCITO"].ToString();


        }


    }
}
