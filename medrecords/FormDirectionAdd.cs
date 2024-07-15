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
using System.Net.Http;
using System.Net.Http.Headers;


namespace medrecords
{   

    public partial class FormDirectionAdd : Form
    {
        
        string id_evnplbase;
        string id_direction;
        string id_patient;

        DataTable dt_prescriptiontype;
        DataTable dt_dirtype;

        DataTable dt_dir;
        DataTable dt_tap;

        bool f_log = false;
        string pathLog = "";
        string pathTapLog = "";
        string pathDirLog = "";

        LoginEcp loginEcp;
        RootDirectionObject rootDirectionObject;
        RootEvnplbaseObject rootEvnplbaseObject;

        //bool evnplbaseIsnew = false;

        static Random random = new Random();

        bool isTap = false;

        //public static DataDirection[] datadir;

        public FormDirectionAdd(string id_patient, bool IsTap, DateTime d)
        {
            InitializeComponent();            

            this.d_dir.Value = d;

            this.isTap = IsTap;

            this.f_log = true;
            this.pathTapLog = Info.path_app + "log-tap.txt";
            this.pathDirLog = Info.path_app + "log-direction.txt";

            /*
            this.id_evnplbase = EvnplbaseIsnew(this.id_patient);
            if (this.id_evnplbase == "")
            {
                this.evnplbaseIsnew = true;
                this.id_evnplbase = Guid.NewGuid().ToString();
            }
            else
            {
                this.evnplbaseIsnew = false;
                this.label9.Visible = false;
                this.label3.Visible = false;
                this.button4.Visible = false;
                this.button5.Visible = false;
                this.textBox1.Visible = false;
                this.textBox2.Visible = false;
            }
            */


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

            this.dt_tap = Info.ds.Tables["evnplbase"].Clone();
            if (this.isTap)
            {
                DataRow[] result_tap = Info.ds.Tables["EVNPLBASE"].Select("ID_PATIENT_EVNPLBASE = '" + id_patient + "'");
                if (result_tap.Length != 0)
                    this.dt_tap.ImportRow(result_tap[0]);
            }
            this.dgv2.DataSource = this.dt_tap;

            this.dt_dir = Info.ds.Tables["direction"].Clone();
            this.dgv1.DataSource = this.dt_dir;

            if (this.isTap)
            {
                this.button5.Enabled = false;
                this.textBox2.Enabled = false;
                this.d_dir.Enabled = false;
                this.id_evnplbase = this.dt_tap.Rows[0]["ID_EVNPLBASE"].ToString().Trim();
            }   
            else
                this.id_evnplbase = Guid.NewGuid().ToString();

            this.id_patient = id_patient;            
            this.id_direction = Guid.NewGuid().ToString();

        }

        string EvnplbaseIsnew(string id_patient)
        {
            string ret = "";
            DataRow[] result_tap = Info.ds.Tables["EVNPLBASE"].Select("ID_PATIENT_EVNPLBASE = '" + id_patient + "'");
            if (result_tap.Length != 0)
            {
                this.dt_tap = Info.ds.Tables["evnplbase"].Clone();
                this.dt_tap.ImportRow(result_tap[0]);
                ret = result_tap[0].ToString();
            }               
            return ret;
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
            this.cb_prof.SelectedValue = "520101000000027";

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
            this.cb_uslmp.SelectedValue = 2;

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

        }
        
        string SaveDirectionToDb(string ecp_id_direction, string ecp_id_evn, string ecp_id_evnqueue, string ecp_id_evnprescr)        
        {
            string ret = this.id_direction;
            FbCommand cmd = new FbCommand("INSERT INTO DIRECTION (ID_DIRECTION,ID_PATIENT_DIRECTION,ID_EVNPLBASE_DIRECTION,NUM_DIR,DATE_DIR,DIRTYPE_ID,PRESCRIPTIONTYPE_ID,DIAG_ID,LPU_DID,LPUSECTIONPROFILE_ID,USLUGACOMPLEXMEDSERVICE_ID,LPUUNITTYPE_ID,PAYTYPE_ID,EVNPRESCR_ISCITO,ECP_ID_DIRECTION,ECP_ID_EVN,ECP_ID_EVNQUEUE,ECP_ID_EVNPRESCR,DIRECTION_DESCR) " +
                                  "VALUES (@ID_DIRECTION,@ID_PATIENT_DIRECTION,@ID_EVNPLBASE_DIRECTION,@NUM_DIR,@DATE_DIR,@DIRTYPE_ID,@PRESCRIPTIONTYPE_ID,@DIAG_ID,@LPU_DID,@LPUSECTIONPROFILE_ID,@USLUGACOMPLEXMEDSERVICE_ID,@LPUUNITTYPE_ID,@PAYTYPE_ID,@EVNPRESCR_ISCITO,@ECP_ID_DIRECTION,@ECP_ID_EVN,@ECP_ID_EVNQUEUE,@ECP_ID_EVNPRESCR,@DIRECTION_DESCR)", Info.conn);

            cmd.Parameters.Clear();

            cmd.Parameters.AddWithValue("@ID_DIRECTION", this.id_direction);
            cmd.Parameters.AddWithValue("@ID_PATIENT_DIRECTION", this.id_patient);            
            cmd.Parameters.AddWithValue("@ID_EVNPLBASE_DIRECTION", this.id_evnplbase);

            cmd.Parameters.AddWithValue("@NUM_DIR", this.dt_dir.Rows[0]["NUM_DIR"].ToString());
            cmd.Parameters.AddWithValue("@DATE_DIR", this.dt_dir.Rows[0]["DATE_DIR"]);
            cmd.Parameters.AddWithValue("@DIRTYPE_ID", this.dt_dir.Rows[0]["DIRTYPE_ID"].ToString());
            cmd.Parameters.AddWithValue("@PRESCRIPTIONTYPE_ID", this.dt_dir.Rows[0]["PRESCRIPTIONTYPE_ID"].ToString());
            cmd.Parameters.AddWithValue("@DIAG_ID", this.dt_dir.Rows[0]["DIAG_ID"].ToString());
            cmd.Parameters.AddWithValue("@LPU_DID", Info.Lpu_sid);
            cmd.Parameters.AddWithValue("@LPUSECTIONPROFILE_ID", this.dt_dir.Rows[0]["LPUSECTIONPROFILE_ID"].ToString());
            cmd.Parameters.AddWithValue("@USLUGACOMPLEXMEDSERVICE_ID", this.dt_dir.Rows[0]["USLUGACOMPLEXMEDSERVICE_ID"].ToString());
            cmd.Parameters.AddWithValue("@LPUUNITTYPE_ID", this.dt_dir.Rows[0]["LPUUNITTYPE_ID"].ToString());
            cmd.Parameters.AddWithValue("@PAYTYPE_ID", this.dt_dir.Rows[0]["PAYTYPE_ID"].ToString());
            cmd.Parameters.AddWithValue("@EVNPRESCR_ISCITO", this.dt_dir.Rows[0]["EVNPRESCR_ISCITO"].ToString());

            cmd.Parameters.AddWithValue("@DIRECTION_DESCR", this.dt_dir.Rows[0]["DIRECTION_DESCR"].ToString());

            cmd.Parameters.AddWithValue("@ECP_ID_DIRECTION", ecp_id_direction);
            cmd.Parameters.AddWithValue("@ECP_ID_EVN", ecp_id_evn);
            cmd.Parameters.AddWithValue("@ECP_ID_EVNQUEUE", ecp_id_evnqueue);
            cmd.Parameters.AddWithValue("@ECP_ID_EVNPRESCR", ecp_id_evnprescr);

            //cmd.Parameters.AddWithValue("@ECP_ID_EVNLABREQUEST", ECP_ID_EVNLABREQUEST);

            try
            {
                int rows = cmd.ExecuteNonQuery();
            }
            catch (FbException ex)
            {
                MessageBox.Show(ex.Message);
                this.SaveLogString(this.pathLog, "SaveDirectionToDb", ex.Message);
                ret = "";
            }

            return ret;
        }


        string SaveEvnplbaseToDb(string evnvizitpl_id, string evnplbase_id)
        {
            string ret = this.id_evnplbase;
            FbCommand cmd = new FbCommand("INSERT INTO EVNPLBASE (ID_EVNPLBASE,ID_PATIENT_EVNPLBASE,EVNPL_NUMCARD,EVNPL_ISFINISH,EVN_DATETIME,LPUSECTION_ID,TREATMENTCLASS_ID,SERVICETYPE_ID,VIZITTYPE_ID,MEDICALCAREKIND_ID,LPU_ID,MEDSTAFFFACT_ID,PAYTYPE_ID,DIAG_ID,USLUGACOMPLEX_ID,EVNVIZITPL_ID,EVNPLBASE_ID) " +
                                   "VALUES (@ID_EVNPLBASE,@ID_PATIENT_EVNPLBASE,@EVNPL_NUMCARD,@EVNPL_ISFINISH,@EVN_DATETIME,@LPUSECTION_ID,@TREATMENTCLASS_ID,@SERVICETYPE_ID,@VIZITTYPE_ID,@MEDICALCAREKIND_ID,@LPU_ID,@MEDSTAFFFACT_ID,@PAYTYPE_ID,@DIAG_ID,@USLUGACOMPLEX_ID,@EVNVIZITPL_ID,@EVNPLBASE_ID)", Info.conn);
            cmd.Parameters.Clear();

            cmd.Parameters.AddWithValue("@ID_EVNPLBASE", this.id_evnplbase);
            cmd.Parameters.AddWithValue("@ID_PATIENT_EVNPLBASE", this.id_patient);

            cmd.Parameters.AddWithValue("@EVNPL_NUMCARD", this.dt_tap.Rows[0]["evnpl_numcard"].ToString());
            cmd.Parameters.AddWithValue("@EVNPL_ISFINISH", this.dt_tap.Rows[0]["evnpl_isfinish"].ToString());
            cmd.Parameters.AddWithValue("@EVN_DATETIME", this.dt_tap.Rows[0]["evn_datetime"]);
            cmd.Parameters.AddWithValue("@LPUSECTION_ID", Info.LpuSection_sid);
            cmd.Parameters.AddWithValue("@TREATMENTCLASS_ID", this.dt_tap.Rows[0]["treatmentclass_id"].ToString());
            cmd.Parameters.AddWithValue("@SERVICETYPE_ID", this.dt_tap.Rows[0]["servicetype_id"].ToString());
            cmd.Parameters.AddWithValue("@VIZITTYPE_ID", this.dt_tap.Rows[0]["vizittype_id"].ToString());
            cmd.Parameters.AddWithValue("@MEDICALCAREKIND_ID", this.dt_tap.Rows[0]["medicalcarekind_id"].ToString());
            cmd.Parameters.AddWithValue("@LPU_ID", Info.Lpu_sid);
            cmd.Parameters.AddWithValue("@MEDSTAFFFACT_ID", Info.MedStaffFact_sid);
            cmd.Parameters.AddWithValue("@PAYTYPE_ID", this.dt_tap.Rows[0]["PayType_id"].ToString());
            cmd.Parameters.AddWithValue("@DIAG_ID", this.dt_tap.Rows[0]["Diag_id"].ToString());
            cmd.Parameters.AddWithValue("@USLUGACOMPLEX_ID", this.dt_tap.Rows[0]["uslugacomplex_id"].ToString());

            cmd.Parameters.AddWithValue("@EVNVIZITPL_ID", evnvizitpl_id);
            cmd.Parameters.AddWithValue("@EVNPLBASE_ID", evnplbase_id);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (FbException ex)
            {
                MessageBox.Show(ex.Message);
                this.SaveLogString(this.pathLog, "SaveEvnplbaseToDb", ex.Message);
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

        private async void button1_Click(object sender, EventArgs e)
        {
            // add direction to ecp
            if (this.textBox1.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Вы не ввели услугу");
                this.textBox1.Focus();
                return;
            }
            if (!this.isTap)
            {
                if (this.textBox2.Text.ToString().Trim() == "")
                {
                    MessageBox.Show("Вы не ввели диагноз");
                    this.textBox2.Focus();
                    return;
                }
            }
            if (this.textBox3.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Вы не ввели Обоснование направления");
                this.textBox3.Focus();
                return;
            }            
            
            // add record to dt
            if (!this.isTap) this.AddRecordTapDt();
            this.AddRecordDirDt();

            //return;
            // add data to ECP
            // begin log
            if (this.f_log)
            {
                //this.lb_log.Items.Clear();
                //this.lb_log.Items.Add(DateTime.Now.ToString("dd.MM.yyyy:HH.mm.ss"));
            }

            this.Button1.Enabled = false;
            this.Button2.Enabled = false;
            this.Pb1.Style = ProgressBarStyle.Marquee;
            this.label12.Text = "Добавляю направление в ЕЦП";

            // login ecp tap ====================================================================
            if (this.f_log) this.pathLog = this.pathTapLog;
            Task<string> t1 = this.LoginEcpAsync();
            Info.raw_login_ecp = await t1;
            // err login ecp
            // 1
            if (Info.raw_login_ecp == "")
            {
                MessageBox.Show("raw_login_ecp пустое - Пришла пустая строка");
                if (this.f_log)
                {
                    //this.lb_log.Items.Add("raw_login_ecp пустое - Пришла пустая строка");
                    //this.SaveTapLog();
                    this.SaveLogString(this.pathLog, "LoginEcpAsync", "raw_login_ecp пустое - Пришла пустая строка");
                }
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
                if (this.f_log)
                {
                    //this.lb_log.Items.Add("raw_login_ecp : deserialize : Исключение JsonConvert : Возможно что нет подключения к БД : " + ex.Message);
                    //this.SaveTapLog();
                    this.SaveLogString(this.pathLog, "LoginEcpAsync", "raw_login_ecp : deserialize : Исключение JsonConvert : Возможно что нет подключения к БД : " + ex.Message);
                }
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
                if (this.f_log)
                {
                    //this.lb_log.Items.Add("Объект loginEcp пустой");
                    //this.SaveTapLog();
                    this.SaveLogString(this.pathLog, "LoginEcpAsync", "Объект loginEcp пустой");
                }
                this.Button1.Enabled = true;
                this.Button2.Enabled = true;
                this.Pb1.Style = ProgressBarStyle.Blocks;
                this.Pb1.Refresh();
                this.label12.Text = "";
                return;
            }
            // login ecp ====================================================================
            if (!this.isTap)
            {
                // TAP 1 
                Task<string> t2 = this.CreateEvnplbaseEcpAsync();
                Info.raw_evnplbase_ecp = await t2;
                // err add TAP 1
                // 1
                if (Info.raw_evnplbase_ecp == "")
                {
                    MessageBox.Show("raw_evnplbase_ecp пустое - Пришла пустая строка");
                    if (this.f_log)
                    {
                        //this.lb_log.Items.Add("raw_evnplbase_ecp пустое - Пришла пустая строка");
                        //this.SaveTapLog();
                        this.SaveLogString(this.pathLog, "CreateEvnplbaseEcpAsync", "raw_evnplbase_ecp пустое - Пришла пустая строка");
                    }
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
                    this.rootEvnplbaseObject = JsonConvert.DeserializeObject<RootEvnplbaseObject>(Info.raw_evnplbase_ecp);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("raw_evnplbase_ecp : deserialize : Исключение JsonConvert " + ex.Message);
                    if (this.f_log)
                    {
                        //this.lb_log.Items.Add("raw_evnplbase_ecp : deserialize : Исключение JsonConvert " + ex.Message);
                        //this.SaveTapLog();
                        this.SaveLogString(this.pathLog, "CreateEvnplbaseEcpAsync", "raw_evnplbase_ecp : deserialize : Исключение JsonConvert " + ex.Message);
                    }
                    this.Button1.Enabled = true;
                    this.Button2.Enabled = true;
                    this.Pb1.Style = ProgressBarStyle.Blocks;
                    this.Pb1.Refresh();
                    this.label12.Text = "";
                    return;
                }
                // 3
                string id_evnplbase_db = "";
                if (this.rootEvnplbaseObject.error_code == 0)
                {
                    //Info.evnvizitpl_id = this.rootEvnplbaseObject.data.EvnVizitPL_id; 
                    //Info.evnplbase_id = this.rootEvnplbaseObject.data.EvnPLBase_id;
                    //this.dt_tap.Rows[0]["evnplbase_id"] 
                    if (this.f_log)
                    {
                        //this.lb_log.Items.Add("create_evnplbase");
                        //this.lb_log.Items.Add(JsonConvert.SerializeObject(this.rootEvnplbaseObject));
                        this.SaveLogString(this.pathLog, "CreateEvnplbaseEcpAsync", JsonConvert.SerializeObject(this.rootEvnplbaseObject));
                    }
                    // save
                    bool is_connect = Info.is_connect_db();
                    if (!is_connect) return;
                    id_evnplbase_db = this.SaveEvnplbaseToDb(this.rootEvnplbaseObject.data.EvnVizitPL_id, this.rootEvnplbaseObject.data.EvnPLBase_id);
                    Info.conn.Close();
                    //
                    if (id_evnplbase_db != "")
                    {
                        //this.dt_tap.Rows[0]["ID_EVNPLBASE"] = id_evnplbase_db;
                        this.dt_tap.Rows[0]["EVNPLBASE_ID"] = this.rootEvnplbaseObject.data.EvnPLBase_id;
                        this.dt_tap.Rows[0]["EVNVIZITPL_ID"] = this.rootEvnplbaseObject.data.EvnVizitPL_id;
                        foreach (DataRow dr_tap in this.dt_tap.Rows)
                        {
                            //Info.ds.Tables["patient"].Rows.Add(dr.ItemArray);
                            Info.ds.Tables["EVNPLBASE"].ImportRow(dr_tap);
                        }
                    }
                }
                else
                {
                    if (this.f_log)
                    {
                        //this.lb_log.Items.Add("create_evnplbase - error");
                        //this.lb_log.Items.Add(JsonConvert.SerializeObject(this.rootEvnplbaseObject));
                        this.SaveLogString(this.pathLog, "CreateEvnplbaseEcpAsync - error_msg" + this.rootEvnplbaseObject.error_msg, JsonConvert.SerializeObject(this.rootEvnplbaseObject));
                    }
                    //Info.max_num_tap--;
                    MessageBox.Show("Ошибка добавления посещения в БД - " + this.rootEvnplbaseObject.error_msg);
                }

                //if (id_evnplbase_db == "")
                //{
                //    if (this.f_log) this.lb_log.Items.Add("Запись по посещению не сохранена в БД");
                //}

                //if (this.f_log) this.SaveTapLog();

                //if (this.f_log)
                //{
                //    this.lb_log.Items.Clear();
                //    this.lb_log.Items.Add(DateTime.Now.ToString("dd.MM.yyyy:HH.mm.ss"));
                //}
            }

            // direction add
            this.pathLog = this.pathDirLog;
            Task<string> t3 = this.CreateDirectionEcpAsync();
            Info.raw_direction_ecp = await t3;
            // err add TAP 1
            // 1
            if (Info.raw_direction_ecp == "")
            {
                MessageBox.Show("raw_direction_ecp пустое - Пришла пустая строка");
                if (this.f_log) this.SaveLogString(this.pathLog, "CreateDirectionEcpAsync", "raw_direction_ecp пустое - Пришла пустая строка");
                //if (this.f_log) this.lb_log.Items.Add("raw_direction_ecp пустое - Пришла пустая строка");
                //if (this.f_log) this.SaveTapLog();
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
                this.rootDirectionObject = JsonConvert.DeserializeObject<RootDirectionObject>(Info.raw_direction_ecp);                
            }
            catch (Exception ex)
            {
                MessageBox.Show("raw_direction_ecp : deserialize : Исключение JsonConvert " + ex.Message);
                if (this.f_log) this.SaveLogString(this.pathLog, "CreateDirectionEcpAsync", "raw_direction_ecp : deserialize : Исключение JsonConvert " + ex.Message);
                //if (this.f_log) this.lb_log.Items.Add("raw_direction_ecp : deserialize : Исключение JsonConvert " + ex.Message);
                //if (this.f_log) this.SaveTapLog();
                this.Button1.Enabled = true;
                this.Button2.Enabled = true;
                this.Pb1.Style = ProgressBarStyle.Blocks;
                this.Pb1.Refresh();
                this.label12.Text = "";
                return;
            }
            // 3
            string id_direction_db = "";
            if (this.rootDirectionObject.error_code == 0)
            { 
                if (this.f_log)
                {
                    //this.lb_log.Items.Add("create_direction");
                    //this.lb_log.Items.Add(JsonConvert.SerializeObject(this.rootDirectionObject));
                    this.SaveLogString(this.pathLog, "CreateDirectionEcpAsync error_code = 0", JsonConvert.SerializeObject(this.rootDirectionObject));
                }                
                // save
                bool is_connect = Info.is_connect_db();
                if (!is_connect) return;
                id_direction_db = this.SaveDirectionToDb(this.rootDirectionObject.data.EvnDirection_id, this.rootDirectionObject.data.Evn_id, this.rootDirectionObject.data.EvnQueue_id, this.rootDirectionObject.data.EvnPrescr_id);
                Info.conn.Close();

                // save ???
                if (id_direction_db != "")
                {
                    //this.dt_tap.Rows[0]["ID_EVNPLBASE"] = id_evnplbase_db;

                    // + Direction_Descr
                    this.dt_dir.Rows[0]["ECP_ID_DIRECTION"] = this.rootDirectionObject.data.EvnDirection_id;
                    this.dt_dir.Rows[0]["ECP_ID_EVN"] = this.rootDirectionObject.data.Evn_id;
                    this.dt_dir.Rows[0]["ECP_ID_EVNQUEUE"] = this.rootDirectionObject.data.EvnQueue_id;
                    this.dt_dir.Rows[0]["ECP_ID_EVNPRESCR"] = this.rootDirectionObject.data.EvnPrescr_id;
                    //

                    foreach (DataRow dr_dir in this.dt_dir.Rows)
                    {
                        //Info.ds.Tables["patient"].Rows.Add(dr.ItemArray);
                        Info.ds.Tables["DIRECTION"].ImportRow(dr_dir);
                    }
                }

                MessageBox.Show("Направление благополучно добавлено в ЕЦП");
            }
            else
            {
                if (this.f_log)
                {
                    //this.lb_log.Items.Add("create_direction - error");
                    //this.lb_log.Items.Add(JsonConvert.SerializeObject(this.rootDirectionObject));
                    this.SaveLogString(this.pathLog, "CreateDirectionEcpAsync", "error_code = " + this.rootDirectionObject.error_code.ToString() + " сообщение - " + this.rootDirectionObject.error_msg);
                }

                //Info.max_num_tap--;
                MessageBox.Show("Ошибка добавления направления в БД : код ошибки - " + this.rootDirectionObject.error_code.ToString() + " сообщение - " + this.rootDirectionObject.error_msg);
            }                        
            
            this.Button1.Enabled = true;
            this.Button2.Enabled = true;
            this.Pb1.Style = ProgressBarStyle.Blocks;
            this.Pb1.Refresh();
            this.label12.Text = "";
            
            /*
                       this.Button1.DialogResult = DialogResult.OK;
                       this.Close();
            */

        }

        private async Task<string> CreateEvnplbaseEcpAsync()
        {
            string ret = "";
            DataRow[] result = Info.ds.Tables["PATIENT"].Select("ID_PATIENT = '" + this.id_patient + "'");
            string s_url = @"https://" + Info.Domain + @".mznn.ru/api/EvnPLBase?Person_id=" + result[0]["ECP_ID_PATIENT"].ToString() +
                             "&sess_id=" + Info.sess_id +
                             "&EvnPL_NumCard=" + this.dt_tap.Rows[0]["EVNPL_NUMCARD"].ToString() +
                             "&EvnPL_IsFinish=" + this.dt_tap.Rows[0]["EVNPL_ISFINISH"].ToString() +
                             "&Evn_setDT=" + Convert.ToDateTime(this.dt_tap.Rows[0]["EVN_DATETIME"]).ToString("dd.MM.yyyy HH:mm:ss") +
                             "&LpuSection_id=" + this.dt_tap.Rows[0]["LPUSECTION_ID"].ToString() +
                             "&TreatmentClass_id=" + this.dt_tap.Rows[0]["TREATMENTCLASS_ID"].ToString() +
                             "&MedStaffFact_id=" + Info.MedStaffFact_sid +
                             "&ServiceType_id=" + this.dt_tap.Rows[0]["SERVICETYPE_ID"].ToString() +
                             "&VizitType_id=" + this.dt_tap.Rows[0]["VIZITTYPE_ID"].ToString() +
                             "&MedicalCareKind_id=" + this.dt_tap.Rows[0]["MEDICALCAREKIND_ID"].ToString() +
                             "&PayType_id=" + this.dt_tap.Rows[0]["PAYTYPE_ID"].ToString() +
                             "&Diag_id=" + this.dt_tap.Rows[0]["DIAG_ID"].ToString() +
                             "&UslugaComplex_uid=" + this.dt_tap.Rows[0]["USLUGACOMPLEX_ID"].ToString();

            //if (this.f_log) this.lb_log.Items.Add(s_url);
            if (this.f_log) this.SaveLogString(this.pathLog, "CreateEvnplbaseEcpAsync", s_url);
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
                    MessageBox.Show("CreateEvnplbaseEcpAsync : Exception : " + ex.Message);
                    if (this.f_log) this.SaveLogString(this.pathLog, "CreateEvnplbaseEcpAsync", "Exception : " + ex.Message);
                }
            }
            return ret;
        }

        private async Task<string> CreateDirectionEcpAsync()
        {
            string ret = "";
            DataRow[] result = Info.ds.Tables["PATIENT"].Select("ID_PATIENT = '" + this.id_patient + "'");
            string s_url = "https://" + Info.Domain + ".mznn.ru/api/EvnDirection?Person_id=" + result[0]["ECP_ID_PATIENT"].ToString() +
                             "&sess_id=" + Info.sess_id +
                             "&EvnDirection_Num=" + this.dt_dir.Rows[0]["NUM_DIR"].ToString() +
                             "&EvnDirection_setDate=" + Convert.ToDateTime(this.dt_dir.Rows[0]["DATE_DIR"]).ToString("dd.MM.yyyy") +
                             "&DirType_id=" + this.dt_dir.Rows[0]["DIRTYPE_ID"].ToString() +
                             "&PrescriptionType_id=" + this.dt_dir.Rows[0]["PRESCRIPTIONTYPE_ID"].ToString() +
                             "&Diag_id=" + this.dt_dir.Rows[0]["DIAG_ID"].ToString() +
                             //
                             "&EvnDirection_Descr=" + this.dt_dir.Rows[0]["DIRECTION_DESCR"].ToString() +
                             //
                             "&Lpu_sid=" + Info.Lpu_sid +
                             "&MedStaffFact_id=" + Info.MedStaffFact_sid +
                             "&Lpu_did=" + Info.Lpu_sid +
                             "&UslugaComplexMedService_ResId=" + this.dt_dir.Rows[0]["USLUGACOMPLEXMEDSERVICE_ID"].ToString() +
                             "&LpuSectionProfile_id=" + this.dt_dir.Rows[0]["LPUSECTIONPROFILE_ID"].ToString() +
                             "&LpuUnitType_id=" + this.dt_dir.Rows[0]["LPUUNITTYPE_ID"].ToString() +
                             "&PayType_id=" + this.dt_dir.Rows[0]["PAYTYPE_ID"].ToString() +
                             "&EvnPrescr_IsCito=" + this.dt_dir.Rows[0]["EVNPRESCR_ISCITO"].ToString() +
                             "&Evn_pid=" + this.dt_tap.Rows[0]["EVNPLBASE_ID"].ToString();

            //if (this.f_log) this.lb_log.Items.Add(s_url);
            if (this.f_log) this.SaveLogString(this.pathLog, "CreateDirectionEcpAsync", s_url);
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
                    MessageBox.Show("CreateDirectionEcpAsync : Exception : " + ex.Message);
                    this.SaveLogString(this.pathLog, "CreateDirectionEcpAsync", "Exception : " + ex.Message);
                }
            }
            return ret;
        }



        private void button2_Click(object sender, EventArgs e)
        {
            this.Button2.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        //private string AddRecordDirDt(bool is_new_evnplbase)
        private string AddRecordDirDt()
        {
             // create record direction
            this.dt_dir.Clear();
            DataRow row = this.dt_dir.NewRow();
            row["ID_DIRECTION"] = this.id_direction;
            row["ID_EVNPLBASE_DIRECTION"] = this.id_evnplbase;

            //Info.max_num_dir++;
            //row["NUM_DIR"] = Info.max_num_dir.ToString();
            row["NUM_DIR"] = RandomString(10);

            row["DATE_DIR"] = this.d_dir.Value.ToString("dd.MM.yyyy");
            row["DIRTYPE_ID"] = this.cb_dirtype.SelectedValue.ToString();
            row["PRESCRIPTIONTYPE_ID"] = this.cb_typep.SelectedValue.ToString();

            if (this.isTap)
            {
                row["DIAG"] = this.dt_tap.Rows[0]["DIAG"].ToString().Trim();
                row["DIAG_ID"] = this.dt_tap.Rows[0]["DIAG_ID"].ToString().Trim();
                row["DATE_DIR"] = this.dt_tap.Rows[0]["EVN_DATETIME"].ToString().Trim(); 
            }
            else
            {
                string[] strs_diag = this.textBox2.Text.Split(' ');
                DataRow[] result_diag = Info.ds.Tables["diag"].Select("code_spr = '" + strs_diag[0] + "'");
                if (result_diag.Length == 0)
                {
                    MessageBox.Show("Диагноз не выбран или не найден");
                    return "";
                }
                row["DIAG"] = result_diag[0]["name_spr"].ToString().Trim();
                row["DIAG_ID"] = result_diag[0]["id_spr"].ToString().Trim();
            }           

            //Info.Lpu_sid;
            //Info.MedStaffFact_sid;
            //Info.Lpu_did = this.cb_in.SelectedValue.ToString();
            //row["LPU_DID"] = Info.Lpu_sid;

            row["LPUSECTIONPROFILE_ID"] = this.cb_prof.SelectedValue.ToString();

            //if (is_new_evnplbase)
            //{
                string[] strs_uslugacomplex = this.textBox1.Text.Split(' ');
                DataRow[] result_uslugacomplex = Info.ds.Tables["uslugacomplex"].Select("enddate is null and code_spr = '" + strs_uslugacomplex[0] + "'");
                if (result_uslugacomplex.Length == 0)
                {
                    MessageBox.Show("Услуга не выбрана или не найдена");
                    return "";
                }
                row["USLUGACOMPLEX"] = result_uslugacomplex[0]["name_spr"].ToString().Trim();
                row["USLUGACOMPLEXMEDSERVICE_ID"] = result_uslugacomplex[0]["id_spr"].ToString();
            //}
            //else 
            //{
            //    row["USLUGACOMPLEX"] = this.dt_tap.Rows[0]["USLUGACOMPLEX"].ToString().Trim();
            //    row["USLUGACOMPLEXMEDSERVICE_ID"] = this.dt_tap.Rows[0]["USLUGACOMPLEX_ID"].ToString().Trim();
            //}
            
            row["LPUUNITTYPE_ID"] = this.cb_uslmp.SelectedValue.ToString();
            row["PAYTYPE_ID"] = this.cb_payt.SelectedValue.ToString();
            row["EVNPRESCR_ISCITO"] = this.cb_cito.SelectedValue.ToString();

            row["DIRECTION_DESCR"] = this.textBox3.Text;

            this.dt_dir.Rows.Add(row);
            return this.id_direction;
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

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        private string AddRecordTapDt()
        {
            this.dt_tap.Clear();
            DataRow row = this.dt_tap.NewRow();
            row["ID_EVNPLBASE"] = this.id_evnplbase;
            row["ID_PATIENT_EVNPLBASE"] = this.id_patient;

            //Info.max_num_tap++;
            //row["EVNPL_NUMCARD"] = Info.max_num_tap.ToString();
            row["EVNPL_NUMCARD"] = RandomString(10);

            row["EVNPL_ISFINISH"] = "0";
            row["EVN_DATETIME"] = this.d_dir.Value;
            row["LPUSECTION_ID"] = Info.LpuSection_sid;
            row["MEDSTAFFFACT_ID"] = Info.MedStaffFact_sid;
            row["TREATMENTCLASS_ID"] = this.cb_treatmentclass.SelectedValue;
            row["SERVICETYPE_ID"] = this.cb_servicetype.SelectedValue;
            row["VIZITTYPE_ID"] = this.cb_vizittype.SelectedValue;
            row["MEDICALCAREKIND_ID"] = this.cb_medicalcarekind.SelectedValue;
            row["PAYTYPE_ID"] = this.cb_payt.SelectedValue;

            string[] strs_diag;
            strs_diag = this.textBox2.Text.Split(' ');
            DataRow[] result_diag = Info.ds.Tables["diag"].Select("code_spr = '" + strs_diag[0] + "'");
            if (result_diag.Length == 0)
            {
                MessageBox.Show("Диагноз не выбран или не найден");
                return "";
            }
            row["diag"] = result_diag[0]["name_spr"].ToString().Trim();
            row["diag_id"] = result_diag[0]["id_spr"].ToString();

            string[] strs_uslugacomplex;
            strs_uslugacomplex = this.textBox1.Text.Split(' ');
            DataRow[] result_uslugacomplex = Info.ds.Tables["uslugacomplex"].Select("enddate is null and code_spr = '" + strs_uslugacomplex[0] + "'");
            if (result_uslugacomplex.Length == 0)
            {
                MessageBox.Show("Услуга не выбрана или не найдена");
                return "";
            }
            row["uslugacomplex"] = result_uslugacomplex[0]["name_spr"].ToString().Trim();
            row["uslugacomplex_id"] = result_uslugacomplex[0]["id_spr"].ToString();

            this.dt_tap.Rows.Add(row);
            return this.id_evnplbase;
        }

        private void SaveDirLog()
        {
            using (var sw = new StreamWriter(this.pathDirLog, true))
                foreach (var item in lb_log.Items) sw.Write(item.ToString() + Environment.NewLine);
        }

        private void SaveTapLog()
        {
            using (var sw = new StreamWriter(this.pathTapLog, true))
                foreach (var item in lb_log.Items) sw.Write(item.ToString() + Environment.NewLine);
        }

        private void SaveLogString(string pathLog, string NameFunc, string Mess)
        {
            string s = DateTime.Now.ToString("dd.MM.yyyy:HH.mm.ss") + " | " + NameFunc + " | " + Mess;
            using (StreamWriter sw = new StreamWriter(pathLog, true, System.Text.Encoding.UTF8))
            {
                sw.WriteLine(s);
            }
        }

    }
}
