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
using System.Net.Http;
using System.Net.Http.Headers;


namespace medrecords
{

    public partial class FormPatientAdd : Form
    {

        DataTable dt;
        bool f_log;
        string pathLog = "";

        LoginEcp loginEcp;
        RootPersonObject rootPersonObject;


        public FormPatientAdd()
        {
            InitializeComponent();

            this.f_log = true;
            this.pathLog = Info.path_app + "log-patient.txt";

            this.cb_sex.DataSource = Info.ds.Tables["sex"];
            this.cb_sex.DisplayMember = "name_spr";
            this.cb_sex.ValueMember = "id_spr";

            this.cb_socstatus.DataSource = Info.ds.Tables["socstatus"];
            this.cb_socstatus.DisplayMember = "name_spr";
            this.cb_socstatus.ValueMember = "id_spr";

            this.dt = Info.ds.Tables["patient"].Clone();
            //this.dataGridView1.DataSource = this.dt;

        }


        string AddMenToDb(string ecp_id)
        {

            string sql_part_fields = "ID_PATIENT,FIO_FAM,FIO_NAME,FIO_OTCH,SEX,SOCSTATUS,DR";
            string sql_part_params = "@ID_PATIENT,@FIO_FAM,@FIO_NAME,@FIO_OTCH,@SEX,@SOCSTATUS,@DR";
            if (this.ChkSnils.Checked)
            {
                sql_part_fields = sql_part_fields + ",SS"; sql_part_params = sql_part_params + ",@SS";
            }
            if (this.ChkOms.Checked)
            {
                sql_part_fields = sql_part_fields + ",OMS"; sql_part_params = sql_part_params + ",@OMS";
            }
            if (this.ChkPasp.Checked)
            {
                sql_part_fields = sql_part_fields + ",PDOC,PDOC_A,PDOC_B"; sql_part_params = sql_part_params + ",@PDOC,@PDOC_A,@PDOC_B";
            }
            if (this.ChkPasp.Checked)
            {
                sql_part_fields = sql_part_fields + ",PDOC,PDOC_A,PDOC_B"; sql_part_params = sql_part_params + ",@PDOC,@PDOC_A,@PDOC_B";
            }
            if (ecp_id != "")
            {
                sql_part_fields = sql_part_fields + ",ECP_ID_PATIENT"; sql_part_params = sql_part_params + ",@ECP_ID_PATIENT";
            }

            string sql = "INSERT INTO PATIENT (" + sql_part_fields + ") VALUES (" + sql_part_params + ")";
            this.SaveLogString(this.pathLog, "AddMenToDb", sql);

            FbCommand cmd = new FbCommand(sql, Info.conn);
            cmd.Parameters.Clear();
            string ret = Guid.NewGuid().ToString();
            cmd.Parameters.AddWithValue("@ID_PATIENT", ret);
            cmd.Parameters.AddWithValue("@FIO_FAM", this.dt.Rows[0]["FIO_FAM"].ToString());
            cmd.Parameters.AddWithValue("@FIO_NAME", this.dt.Rows[0]["FIO_NAME"].ToString());
            cmd.Parameters.AddWithValue("@FIO_OTCH", this.dt.Rows[0]["FIO_OTCH"].ToString());
            cmd.Parameters.AddWithValue("@SEX", this.dt.Rows[0]["SEX"].ToString());
            cmd.Parameters.AddWithValue("@SOCSTATUS", this.dt.Rows[0]["SOCSTATUS"].ToString());
            cmd.Parameters.AddWithValue("@DR", this.dt.Rows[0]["DR"].ToString());
            if (this.ChkSnils.Checked)
                cmd.Parameters.AddWithValue("@SS", this.dt.Rows[0]["SS"].ToString());
            if (this.ChkOms.Checked)
                cmd.Parameters.AddWithValue("@OMS", this.dt.Rows[0]["OMS"].ToString());
            if (this.ChkPasp.Checked)
            {
                cmd.Parameters.AddWithValue("@PDOC", this.dt.Rows[0]["PDOC"].ToString());
                cmd.Parameters.AddWithValue("@PDOC_A", this.dt.Rows[0]["PDOC_A"].ToString());
                cmd.Parameters.AddWithValue("@PDOC_B", this.dt.Rows[0]["PDOC_B"].ToString());
            }
            if (ecp_id != "")            
                cmd.Parameters.AddWithValue("@ECP_ID_PATIENT", ecp_id);               

            try
            {
                int rows = cmd.ExecuteNonQuery();
            }
            catch (FbException ex)
            {
                MessageBox.Show(ex.Message + " - Возможно повтор уникальных данных");
                this.SaveLogString(this.pathLog, "AddMenToDb", "Exception " + ex.Message + " - Возможно повтор уникальных данных");
                ret = "";
            }
            return ret;
        }

        public async Task<string> LoginEcpAsync()
        {
            string ret = "";
            this.SaveLogString(this.pathLog, "LoginEcpAsync", Info.strloginecp);
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(Info.strloginecp);
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    ret = content;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("LoginEcpAsync : Exception : " + ex.Message);
                    this.SaveLogString(this.pathLog, "LoginEcpAsync", "Exception : " + ex.Message);
                }
            }
            return ret;
        }

        private async void Button2_Click(object sender, EventArgs e)
        {
            // add men to ecp
            // проверки 
            if (this.t_fam.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Вы не ввели Фамилию");
                this.t_fam.Focus();
                return;
            }
            if (this.t_im.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Вы не ввели Имя");
                this.t_im.Focus();
                return;
            }
            if (this.d_dr.Value.ToString() == "")
            {
                MessageBox.Show("Вы не ввели дату рождения");
                this.d_dr.Focus();
                return;
            }
            if ((this.MaskSnils.Text.ToString().Trim() == "-   -") && (this.ChkSnils.Checked))
            {
                MessageBox.Show("Вы не ввели СНИЛС");
                this.MaskSnils.Focus();
                return;
            }
            if ((this.MaskOms.Text.ToString().Trim() == "") && (this.ChkOms.Checked))
            {
                MessageBox.Show("Вы не ввели номер ОМС");
                this.MaskOms.Focus();
                return;
            }
            if (this.ChkPasp.Checked)
            {
                if (this.MaskNdocPasp.Text.ToString().Trim() == "")
                {
                    MessageBox.Show("Вы не ввели номер Паспорта");
                    this.MaskNdocPasp.Focus();
                    return;
                }
                if (this.DateDocPasp.Value.ToString() == "")
                {
                    MessageBox.Show("Вы не ввели дату выдачи Паспорта");
                    this.DateDocPasp.Focus();
                    return;
                }
                if (this.MemoDocPasp.Text.Trim() == "")
                {
                    MessageBox.Show("Вы не ввели кем выдан Паспорт");
                    this.MemoDocPasp.Focus();
                    return;
                }
            }
            // проверки 

            this.Button1.Enabled = false;
            this.Button2.Enabled = false;
            this.Pb1.Style = ProgressBarStyle.Marquee;
            this.label12.Text = "Добавляю человека в ЕЦП";

            // add record to dt
            this.AddRecordData();

            // add data to ECP
            if (this.f_log) this.SaveLogString(this.pathLog, "add patient in ecp", "begin");

            // login ecp ====================================================================
            Task<string> t1 = this.LoginEcpAsync();
            Info.raw_login_ecp = await t1;
            // err login ecp
            // 1
            if (Info.raw_login_ecp == "")
            {
                MessageBox.Show("LoginEcpAsync : raw_login_ecp пустое - Пришла пустая строка");
                if (this.f_log) this.SaveLogString(this.pathLog, "LoginEcpAsync", "raw_login_ecp пустое - Пришла пустая строка");                                        
                this.Button1.Enabled = true;
                this.Button2.Enabled = true;
                this.Pb1.Style = ProgressBarStyle.Blocks;
                this.Pb1.Refresh();
                this.label12.Text = "";                              
                return;
            }
            // 2
            try
            {
                this.loginEcp = JsonConvert.DeserializeObject<LoginEcp>(Info.raw_login_ecp);
            }
            catch (Exception ex)
            {
                MessageBox.Show("raw_login_ecp : deserialize : Исключение JsonConvert : Возможно что нет подключения к БД : " + ex.Message);
                if (this.f_log) this.SaveLogString(this.pathLog, "LoginEcpAsync", "raw_login_ecp : deserialize : Исключение JsonConvert : Возможно что нет подключения к БД : " + ex.Message);
                this.Button1.Enabled = true;
                this.Button2.Enabled = true;
                this.Pb1.Style = ProgressBarStyle.Blocks;
                this.Pb1.Refresh();
                this.label12.Text = "";                
                return;
            }
            // 3
            if (this.loginEcp != null)
                Info.sess_id = this.loginEcp.sess_id.ToString();
            else
            {
                MessageBox.Show("Объект loginEcp пустой");
                if (this.f_log) this.SaveLogString(this.pathLog, "LoginEcpAsync", "Объект loginEcp пустой");
                this.Button1.Enabled = true;
                this.Button2.Enabled = true;
                this.Pb1.Style = ProgressBarStyle.Blocks;
                this.Pb1.Refresh();
                this.label12.Text = "";               
                return;
            }
            // err login ecp

            // get men =================================================
            string isMenEcpAsync = @"https://" + Info.Domain + @".mznn.ru/api/Person?sess_id=" + Info.sess_id +
                                        "&PersonSurName_SurName=" + this.dt.Rows[0]["FIO_FAM"].ToString() +
                                        "&PersonFirName_FirName=" + this.dt.Rows[0]["FIO_NAME"].ToString() +
                                        "&PersonBirthDay_BirthDay=" + Convert.ToDateTime(this.dt.Rows[0]["DR"]).ToString("dd.MM.yyyy");
            Task<string> t2 = this.IsMenEcpAsync(isMenEcpAsync);
            Info.raw_ismen_ecp = await t2;
            // err get men
            // 1
            if (Info.raw_ismen_ecp == "")
            {
                MessageBox.Show("raw_ismen_ecp пустое - Пришла пустая строка");
                if (this.f_log) this.SaveLogString(this.pathLog, "IsMenEcpAsync", "raw_ismen_ecp пустое - Пришла пустая строка");                   
                this.Button1.Enabled = true;
                this.Button2.Enabled = true;
                this.Pb1.Style = ProgressBarStyle.Blocks;
                this.Pb1.Refresh();
                this.label12.Text = "";               
                return;
            }
            // 2
            try
            {
                this.rootPersonObject = JsonConvert.DeserializeObject<RootPersonObject>(Info.raw_ismen_ecp);
            }
            catch (Exception ex)
            {
                MessageBox.Show("raw_ismen_ecp : deserialize : Исключение JsonConvert " + ex.Message);
                if (this.f_log) this.SaveLogString(this.pathLog, "IsMenEcpAsync", "raw_ismen_ecp : deserialize : Исключение JsonConvert " + ex.Message);
                this.Button1.Enabled = true;
                this.Button2.Enabled = true;
                this.Pb1.Style = ProgressBarStyle.Blocks;
                this.Pb1.Refresh();
                this.label12.Text = "";                
                return;
            }
            // 3
            if (this.rootPersonObject.data.Length == 0)
            {
                // Не найдено ни одной записи
                if (this.f_log)
                {
                    this.SaveLogString(this.pathLog, "IsMenEcpAsync", "Человек не найден в ЕЦП");
                }
            }
            else 
            {
                if (this.f_log)
                {
                    this.SaveLogString(this.pathLog, "IsMenEcpAsync", "найдено записей - " + this.rootPersonObject.data.Length.ToString());
                    this.SaveLogString(this.pathLog, "IsMenEcpAsync", JsonConvert.SerializeObject(this.rootPersonObject));
                }
                MessageBox.Show("Такой пациент уже есть");
                this.Button1.Enabled = true;
                this.Button2.Enabled = true;
                this.Pb1.Style = ProgressBarStyle.Blocks;
                this.Pb1.Refresh();
                this.label12.Text = "";
                return;
            }
            // err get men

            // create men
            string addMenEcpAsync = @"https://" + Info.Domain + @".mznn.ru/api/Person?sess_id=" + Info.sess_id +
                             "&PersonSurName_SurName=" + this.dt.Rows[0]["FIO_FAM"].ToString() +
                             "&PersonFirName_FirName=" + this.dt.Rows[0]["FIO_NAME"].ToString() +
                             "&PersonBirthDay_BirthDay=" + Convert.ToDateTime(this.dt.Rows[0]["DR"]).ToString("dd.MM.yyyy") +
                             "&SocStatus_id=" + this.dt.Rows[0]["SOCSTATUS"].ToString() +
                             "&Person_Sex_id=" + this.dt.Rows[0]["SEX"].ToString();
            string ss = this.dt.Rows[0]["SS"].ToString();
            var charsToRemove = new string[] { " ", "-" };
            foreach (var c in charsToRemove)
            {
                ss = ss.Replace(c, string.Empty);
            }
            if (this.ChkSnils.Checked) addMenEcpAsync = addMenEcpAsync + "&PersonSnils_Snils=" + ss;
            Task<string> t3 = this.AddMenEcpAsync(addMenEcpAsync);
            Info.raw_men_ecp = await t3;
            // err create men
            // 1
            if (Info.raw_men_ecp == "")
            {
                MessageBox.Show("raw_men_ecp пустое - Пришла пустая строка");
                if (this.f_log) this.SaveLogString(this.pathLog, "AddMenEcpAsync", "raw_men_ecp пустое - Пришла пустая строка");
                this.Button1.Enabled = true;
                this.Button2.Enabled = true;
                this.Pb1.Style = ProgressBarStyle.Blocks;
                this.Pb1.Refresh();
                this.label12.Text = "";               
                return;
            }
            // 2
            try
            {
                this.rootPersonObject = JsonConvert.DeserializeObject<RootPersonObject>(Info.raw_men_ecp);
            }
            catch (Exception ex)
            {
                MessageBox.Show("raw_men_ecp : deserialize : Исключение JsonConvert " + ex.Message);
                if (this.f_log) this.SaveLogString(this.pathLog, "AddMenEcpAsync", "raw_men_ecp : deserialize : Исключение JsonConvert " + ex.Message);
                this.Button1.Enabled = true;
                this.Button2.Enabled = true;
                this.Pb1.Style = ProgressBarStyle.Blocks;
                this.Pb1.Refresh();
                this.label12.Text = "";
                return;
            }
            // 3
            string id_patient_db = "";
            if (this.rootPersonObject.error_code == 0)
            {
                //Info.ecp_id_patient = this.rootPersonObject.data[0].Person_id;                
                //this.dt.Rows[0]["ECP_ID_PATIENT"] = this.rootPersonObject.data[0].Person_id;
                if (this.f_log) this.SaveLogString(this.pathLog, "AddMenEcpAsync", JsonConvert.SerializeObject(this.rootPersonObject));
                // save
                bool is_connect = Info.is_connect_db();
                if (!is_connect) return;
                id_patient_db = this.AddMenToDb(this.rootPersonObject.data[0].Person_id);
                Info.conn.Close();
                // sync
                if (id_patient_db != "")
                {
                    this.dt.Rows[0]["ID_PATIENT"] = id_patient_db;
                    this.dt.Rows[0]["ECP_ID_PATIENT"] = this.rootPersonObject.data[0].Person_id;
                    foreach (DataRow dr in this.dt.Rows)
                    {
                        //Info.ds.Tables["patient"].Rows.Add(dr.ItemArray);
                        Info.ds.Tables["patient"].ImportRow(dr);
                    }                   
                }                
            }
            else
            {
                if (this.f_log) this.SaveLogString(this.pathLog, "AddMenEcpAsync", "error_msg " + this.rootPersonObject.error_msg);
                MessageBox.Show("Ошибка добавления человека в БД - error_msg " + this.rootPersonObject.error_msg);
            }

            if (id_patient_db == "")
            {
                if (this.f_log) this.SaveLogString(this.pathLog, "AddMenEcpAsync", "Запись по пациенту не сохранена в БД");
            }
            else
                MessageBox.Show("Человек благополучно добавлен в ЕЦП");

            this.Button1.Enabled = true;
            this.Button2.Enabled = true;
            this.Pb1.Style = ProgressBarStyle.Blocks;
            this.Pb1.Refresh();
            this.label12.Text = "";
            
            //this.Button2.DialogResult = DialogResult.OK;
            //this.Close();

        }

        private async Task<string> AddMenEcpAsync(string s_url)
        {
            string ret = "";
            if (this.f_log) this.SaveLogString(this.pathLog, "AddMenEcpAsync", s_url);
            using (var client = new HttpClient())
            {
                try
                {
                    HttpClientHandler handler = new HttpClientHandler();
                    HttpClient httpClient = new HttpClient(handler);
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, s_url);
                    HttpResponseMessage response = await httpClient.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    //var content = await response.Content.ReadAsStringAsync();
                    ret = await response.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("AddMenEcpAsync : Exception : " + ex.Message);
                    if (this.f_log) this.SaveLogString(this.pathLog, "AddMenEcpAsync", "Exception : " + ex.Message);
                }
            }
            return ret;
        }

        private async Task<string> IsMenEcpAsync(string s_url)
        {
            string ret = "";                       
            if (this.f_log) this.SaveLogString(this.pathLog, "IsMenEcpAsync", s_url);
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(s_url);
                    response.EnsureSuccessStatusCode();
                    //var content = await response.Content.ReadAsStringAsync();
                    ret = await response.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("IsMenEcpAsync : Exception : " + ex.Message);
                    if (this.f_log) this.SaveLogString(this.pathLog, "IsMenEcpAsync", "Exception : " + ex.Message);
                }
            }
            return ret;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            // cancel patient
            this.Button1.DialogResult = DialogResult.Cancel;
            this.Close();

        }



        private void Chk_pasp_CheckedChanged(object sender, EventArgs e)
        {
            // check pasp
            CheckBox chk_pasp = (CheckBox)sender;
            if (chk_pasp.Checked)
                this.GroupBox1.Visible = true;
            else
                this.GroupBox1.Visible = false;
        }

        private void Chk_snils_CheckedChanged(object sender, EventArgs e)
        {
            // check snils
            CheckBox chk_snils = (CheckBox)sender;
            if (chk_snils.Checked)
                this.GroupBox2.Visible = true;
            else
                this.GroupBox2.Visible = false;

        }

        private void Chk_oms_CheckedChanged(object sender, EventArgs e)
        {
            // check omc
            CheckBox chk_omc = (CheckBox)sender;
            if (chk_omc.Checked)
                this.GroupBox3.Visible = true;
            else
                this.GroupBox3.Visible = false;

        }

        private void AddRecordData()
        {
            this.dt.Clear();
            DataRow row = this.dt.NewRow();
            row["ID_PATIENT"] = "PASP";
            row["FIO_FAM"] = this.t_fam.Text.ToString().Trim();
            row["FIO_NAME"] = this.t_im.Text.ToString().Trim();
            row["FIO_OTCH"] = this.t_otch.Text.ToString().Trim();
            row["SEX"] = this.cb_sex.SelectedValue.ToString();
            row["SOCSTATUS"] = this.cb_socstatus.SelectedValue.ToString();
            row["DR"] = this.d_dr.Value.ToString("dd.MM.yyyy");

            if (this.ChkPasp.Checked)
            {
                row["PDOC"] = this.MaskNdocPasp.Text.ToString();
                row["PDOC_A"] = this.MemoDocPasp.Text.ToString().Trim();
                row["PDOC_B"] = this.DateDocPasp.Value.ToString("dd.MM.yyyy");
            }

            if (this.ChkSnils.Checked) row["SS"] = this.MaskSnils.Text.ToString();

            if (this.ChkOms.Checked) row["OMS"] = this.MaskOms.Text.ToString();

            this.dt.Rows.Add(row);

        }


        private void SaveLogString(string pathLog, string NameFunc, string Mess)
        {
            string s = DateTime.Now.ToString("dd.MM.yyyy:HH.mm.ss") + " | " + NameFunc + " | " + Mess;
            using (StreamWriter sw = new StreamWriter(pathLog, true, System.Text.Encoding.UTF8))
            {
                sw.WriteLine(s);
            }
        }

        private void t_fam_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string s = textBox.Text.ToString();            
            this.t_fam.Text = UppercaseFirst(s);
            this.t_fam.SelectionStart = this.t_fam.Text.Length;
        }

        private void t_im_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string s = textBox.Text.ToString();            
            this.t_im.Text = UppercaseFirst(s);
            this.t_im.SelectionStart = this.t_im.Text.Length;
        }

        private void t_otch_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string s = textBox.Text.ToString();
            this.t_otch.Text = UppercaseFirst(s);
            this.t_otch.SelectionStart = this.t_otch.Text.Length;
        }

        static string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        
    }
}
