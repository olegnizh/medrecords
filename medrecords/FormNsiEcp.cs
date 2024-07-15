using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
//using System.Web;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;
using Csv;
using ExcelDataReader;



namespace medrecords
{

    public partial class FormNsiEcp : Form
    {

        DataSet ds;
        DataTable dt_refbooklist;
        DataTable dt_spr;

        string spr_name = "";
        string s_url = "";
        string table_name = "";
        string titleForm = "НСИ ЕЦП";

        LoginEcp loginEcp;
        string pathLog = "";

        //private static List<DatumRefbook> listrefbook;                        

        private static RootRefbookObject rootRefbookObject;
        private static DatumRefbook[] refbooks;

        private static RootSprObject rootSprObject;
        private static DatumSpr[] sprs;

        //private static DatumLpu[] lpus;
        private static RootLpuListObject rootLpuListObject;
        private static DatumLpuList[] lpuls;

        FbCommand CommandSprs;
        FbCommandBuilder BuilderSprs;
        FbDataAdapter AdapterSprs;

        public FormNsiEcp()
        {
           
            InitializeComponent();
            //
            this.Text = this.titleForm;
            Info.path_app = AppDomain.CurrentDomain.BaseDirectory;
            this.pathLog = Info.path_app + "log-nsi.txt";

            // local
            //Info.path_db = "db\\DIRECTIONS.FDB";
            //Info.str_connect = "User=SYSDBA;Password=masterkey;Database="+ Info.path_db + ";DataSource=localhost;ServerType=1;Port=3050;Charset=UTF8";
            //MessageBox.Show("--- "+Info.str_connect);

            if (Info.ds != null)
            {
                Info.ds.Dispose();
                Info.ds = null;
            }
            Info.ds = new DataSet("medic");

            // ==============================================
            // standalone
            //if (Info.conn.State == ConnectionState.Closed)
            //{
                bool is_connect = Info.is_connect_db();
                if (!is_connect) return;
            //}
               
            CommandSprs = new FbCommand("SELECT * FROM sprs ORDER BY name_spr ASC", Info.conn);
            AdapterSprs = new FbDataAdapter(CommandSprs);
            BuilderSprs = new FbCommandBuilder(AdapterSprs);
            AdapterSprs.Fill(Info.ds, "sprs");
            this.dgv_sprs.DataSource = Info.ds.Tables["sprs"];
            this.dgv_sprs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                                
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // сохранить изменения для таблицы справочников ЕЦП
            this.AdapterSprs.Update(Info.ds.Tables["sprs"]);
            this.dgv_sprs.ReadOnly = true;
            //this.Text = this.titleForm;
            this.label3.Text = "SPRS";

        }

        void add_record_refbooklist_to_db(string field1, string field2, string field3, string field4)
        {

            FbCommand cmd = new FbCommand("INSERT INTO REFBOOKLIST (REFBOOK_CODE,REFBOOK_NAME,REFBOOKTYPE_ID,REFBOOK_TABLENAME) VALUES (@REFBOOK_CODE,@REFBOOK_NAME,@REFBOOKTYPE_ID,@REFBOOK_TABLENAME)", Info.conn);
            cmd.Parameters.Clear();

            cmd.Parameters.AddWithValue("@REFBOOK_CODE", field1);
            cmd.Parameters.AddWithValue("@REFBOOK_NAME", field2);
            cmd.Parameters.AddWithValue("@REFBOOKTYPE_ID", field3);
            cmd.Parameters.AddWithValue("@REFBOOK_TABLENAME", field4);            

            try
            {
                int rows = cmd.ExecuteNonQuery();
            }
            catch (FbException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        void add_record_spr_to_db(string field1, string field2, string field3, string field4)
        {

            FbCommand cmd = new FbCommand("INSERT INTO REFBOOKLIST (REFBOOK_CODE,REFBOOK_NAME,REFBOOKTYPE_ID,REFBOOK_TABLENAME) VALUES (@REFBOOK_CODE,@REFBOOK_NAME,@REFBOOKTYPE_ID,@REFBOOK_TABLENAME)", Info.conn);
            cmd.Parameters.Clear();

            cmd.Parameters.AddWithValue("@REFBOOK_CODE", field1);
            cmd.Parameters.AddWithValue("@REFBOOK_NAME", field2);
            cmd.Parameters.AddWithValue("@REFBOOKTYPE_ID", field3);
            cmd.Parameters.AddWithValue("@REFBOOK_TABLENAME", field4);

            try
            {
                int rows = cmd.ExecuteNonQuery();
            }
            catch (FbException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private void button6_Click(object sender, EventArgs e)
        {
            if (Info.conn.State == ConnectionState.Open)
                Info.conn.Close();

            FormMenu f = new FormMenu();
            this.Hide();
            f.Show();

            //this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string sql_str = "";

            if (Info.conn.State == ConnectionState.Closed)
            {
                bool is_connect = Info.is_connect_db();
                if (!is_connect)
                    return;
            }

            sql_str = "DELETE * FROM socstatus WHERE (len(socstatus.id_spr) = 3)";
            Info.make_query_db(sql_str);

            MessageBox.Show("Корректировка прошла успешно");

        }

        private void dgv_sprs_DoubleClick(object sender, EventArgs e)
        {
            //this.textBox1.Text = this.dgv_sprs[0, this.dgv_sprs.CurrentCell.RowIndex].Value.ToString().Trim();           
            this.spr_name = this.dgv_sprs[0, this.dgv_sprs.CurrentCell.RowIndex].Value.ToString().Trim();


        }

        private void dgv_ref_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            this.textBox2.Text = this.dgv_ref.Rows[rowIndex].Cells[1].Value.ToString();
            //this.dgv_ref[1, this.dgv_ref.CurrentCell.RowIndex].Value.ToString().Trim();
        }

 
        private void button3_Click(object sender, EventArgs e)
        {
            // редактирование таблицы справочников
            this.dgv_sprs.ReadOnly = false;
            //this.Text = this.Text.ToString() + " - РЕЖИМ РЕДАКТИРОВАНИЯ";
            this.label3.Text = "SPRS - РЕЖИМ РЕДАКТИРОВАНИЯ";

        }


        private int view_stdspr()
        {
            // показать текщий справочник
            // verify
            if (!File.Exists(Info.path_app + "sprecp/" + this.spr_name + ".txt"))
            {
                MessageBox.Show(this.spr_name + ".txt не найден");
                return 1;
            }
            string jsonString = File.ReadAllText(Info.path_app + "sprecp/" + this.spr_name + ".txt");
            try
            {
                rootSprObject = JsonConvert.DeserializeObject<RootSprObject>(jsonString);
            }
            catch (Exception ex)
            {
                MessageBox.Show("DeserializeObject : Exception : " + ex.Message);
                return 1;
            }
            //
            if (rootSprObject.data.Length == 0)
            {
                MessageBox.Show("Данных справочника нет - пустой массив");
                return 1;
            }
            else
                sprs = rootSprObject.data;
            
            if (this.dt_spr != null)
            {
                dt_spr.Dispose();
                dt_spr = null;
            }
            dt_spr = new DataTable();
            this.dt_spr.Columns.Add("id", typeof(string));
            this.dt_spr.Columns.Add("Name", typeof(string));
            this.dt_spr.Columns.Add("Code", typeof(string));          
            foreach (DatumSpr d in sprs)
            {
                DataRow row = this.dt_spr.NewRow();
                row["id"] = d.id.ToString();
                row["Name"] = d.Name.ToString();
                if (d.Code != null) row["Code"] = d.Code.ToString();
                this.dt_spr.Rows.Add(row);
            }

            return 0;

        }

        private int view_lpulist()
        {
            // показать текщий справочник
            // verify
            if (!File.Exists(Info.path_app + "sprecp/" + this.spr_name + ".txt"))
            {
                MessageBox.Show(this.spr_name + ".txt не найден");
                return 1;
            }
            string jsonString = File.ReadAllText(Info.path_app + "sprecp/" + this.spr_name + ".txt");
            try
            {
                rootLpuListObject = JsonConvert.DeserializeObject<RootLpuListObject>(jsonString);
            }
            catch (Exception ex)
            {
                MessageBox.Show("DeserializeObject : Exception : " + ex.Message);
                return 1;
            }
            //
            if (rootLpuListObject.data.Length == 0)
            {
                MessageBox.Show("Данных справочника нет - пустой массив");
                return 1;
            }
            else
                lpuls = rootLpuListObject.data;
            //
            if (this.dt_spr != null)
            {
                dt_spr.Dispose();
                dt_spr = null;
            }
            dt_spr = new DataTable();
            this.dt_spr.Columns.Add("Lpu_id", typeof(string));
            this.dt_spr.Columns.Add("Org_Name", typeof(string));
            this.dt_spr.Columns.Add("Org_Nick", typeof(string));
            this.dgv_spr.DataSource = this.dt_spr;
            this.dgv_spr.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;            
            foreach (DatumLpuList d in lpuls)
            {
                DataRow row = this.dt_spr.NewRow();
                row["Lpu_id"] = d.Lpu_id.ToString();
                row["Org_Name"] = d.Org_Name.ToString();
                //if (d.Code != null)
                    row["Org_Nick"] = d.Org_Nick.ToString();
                this.dt_spr.Rows.Add(row);
            }
            this.dgv_spr.DataSource = this.dt_spr;
            this.dgv_spr.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            return 0;
        }


        private async void button10_Click(object sender, EventArgs e)
        {
           
            if (this.spr_name == "")
            {
                MessageBox.Show("Вы не выбрали таблицу для просмотра");
                return;
            }
            if (this.spr_name == "lpulist")
            {
                MessageBox.Show("Вы выбрали таблицу для просмотра " + this.spr_name);
                return;
            }

            this.toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
            this.label6.Text = this.spr_name;
            //this.toolStripLabel1.Text = "Загружаю НСИ и данные";

            Task<long> taskValue = ViewStdsprAsync();
            long value = await taskValue;

            /*
            switch (this.spr_name)
            {
                case "lpulist":
                    i = this.view_lpulist();
                    if (i != 0) return;
                    break;

                default:
                    i = this.view_stdspr();
                    if (i != 0) return;
                    break;

            }
            //this.label6.Text = this.spr_name;
            //this.label1.Text = "";
            */

            this.toolStripProgressBar1.Style = ProgressBarStyle.Blocks;            
            //this.toolStripLabel1.Text = "";

            this.dgv_spr.DataSource = this.dt_spr;
            this.dgv_spr.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //MessageBox.Show(this.dt_spr.Rows.Count.ToString());

        }

        protected Task<long> ViewStdsprAsync()
        {
            return Task.Run(() =>
            {
                long ret = 0;
                this.view_stdspr();
                return ret;
            });
        }

        private void dgv_sprs_Click(object sender, EventArgs e)
        {
            this.spr_name = this.dgv_sprs[0, this.dgv_sprs.CurrentCell.RowIndex].Value.ToString().Trim();

        }

        private void button9_Click(object sender, EventArgs e)
        {

            if (this.dt_refbooklist != null)
            {
                dt_refbooklist.Dispose();
                dt_refbooklist = null;
            }
            dt_refbooklist = new DataTable();
            this.dt_refbooklist.Columns.Add("Refbook_Code", typeof(string));
            this.dt_refbooklist.Columns.Add("Refbook_Name", typeof(string));
            this.dt_refbooklist.Columns.Add("RefbookType_id", typeof(string));
            this.dt_refbooklist.Columns.Add("Refbook_TableName", typeof(string));
            this.dgv_ref.DataSource = this.dt_refbooklist;
            this.dgv_ref.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            string jsonString1 = File.ReadAllText(Info.path_app + "sprecp/refbooklist.txt");
            rootRefbookObject = JsonConvert.DeserializeObject<RootRefbookObject>(jsonString1);
            refbooks = rootRefbookObject.data;
            foreach (DatumRefbook d in refbooks)
            {
                DataRow row = this.dt_refbooklist.NewRow();
                row["Refbook_Code"] = d.Refbook_Code.ToString();
                row["Refbook_Name"] = d.Refbook_Name.ToString();
                row["RefbookType_id"] = d.RefbookType_id.ToString();
                row["Refbook_TableName"] = d.Refbook_TableName.ToString();
                this.dt_refbooklist.Rows.Add(row);
            }
            
        }


        void todb_stdspr_sync()
        {
            FbCommand Command;
            FbCommandBuilder Builder;
            FbDataAdapter Adapter;

            if (Info.conn.State == ConnectionState.Closed)
            {
                bool is_connect = Info.is_connect_db();
                if (!is_connect) return;
            }           

            bool is_table = Info.existsTable(this.spr_name);
            int err = 0;
            if (!is_table)
            {
                err = Info.createTable("CREATE TABLE " + this.spr_name + " (ID_SPR VARCHAR(36),ORGTYPE_ID VARCHAR(36),NAME_SPR VARCHAR(500) CHARACTER SET UTF8,CODE_SPR VARCHAR(36),KLRGN_ID VARCHAR(36),BEGDATE DATE,ENDDATE DATE)");
                if (err == 1)
                {
                    Info.conn.Close();
                    return;
                }
            }               
            else
                Info.make_query_db("DELETE FROM " + this.spr_name);

            Thread.Sleep(1000);

            Command = new FbCommand("SELECT * FROM " + this.spr_name, Info.conn);
            Adapter = new FbDataAdapter(Command);
            Builder = new FbCommandBuilder(Adapter);
            Adapter.Fill(Info.ds, this.spr_name);

            string jsonString = File.ReadAllText(Info.path_app + "sprecp/" + this.spr_name + ".txt");
            rootSprObject = JsonConvert.DeserializeObject<RootSprObject>(jsonString);
            sprs = rootSprObject.data;

            int y = 1;
            foreach (DatumSpr d in sprs)
            {
                DataRow row = Info.ds.Tables[this.spr_name].NewRow();
                row[0] = d.id.ToString();
                if (d.OrgType_id != null) row[1] = d.OrgType_id.ToString();
                row[2] = d.Name.ToString();
                if (d.Code != null) try { row[3] = d.Code.ToString(); } catch { };
                if (d.KLRgn_id != null) row[4] = d.KLRgn_id.ToString();
                if (d.begDate != null) row[5] = Convert.ToDateTime(d.begDate.ToString());
                if (d.endDate != null) row[6] = Convert.ToDateTime(d.endDate.ToString());
                Info.ds.Tables[this.spr_name].Rows.Add(row);

                Application.DoEvents();
                this.label1.Text = (y++).ToString();
            }

            Application.DoEvents();
            this.label1.Text = "Сохраняю данные в БД ...";

            int i = 0;
            try
            {
                i = Adapter.Update(Info.ds.Tables[this.spr_name]);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Сделано " + i.ToString() + ": " + ex.Message);
                return;
            }
            if (i > 0) MessageBox.Show("Изменения успешно сохранены в базе данных");

            Application.DoEvents();
            this.label1.Text = "Данные благополучно сохранены в БД";

        }


        protected Task<string> TodbStdsprAsync()
        {
            return Task.Run(() =>
            {
                string ret = "";
                ret = this.todb_stdspr();
                return ret;
            });
        }

        string todb_stdspr()
        {
            string ret = "";
            FbCommand Command;
            FbCommandBuilder Builder;
            FbDataAdapter Adapter;

            Command = new FbCommand("SELECT * FROM " + this.spr_name, Info.conn);
            Adapter = new FbDataAdapter(Command);
            Builder = new FbCommandBuilder(Adapter);
            Adapter.Fill(Info.ds, this.spr_name);

            string jsonString = File.ReadAllText(Info.path_app + "sprecp/" + this.spr_name + ".txt");
            rootSprObject = JsonConvert.DeserializeObject<RootSprObject>(jsonString);
            sprs = rootSprObject.data;

            foreach (DatumSpr d in sprs)
            {
                DataRow row = Info.ds.Tables[this.spr_name].NewRow();
                row[0] = d.id.ToString();
                if (d.OrgType_id != null) row[1] = d.OrgType_id.ToString();
                row[2] = d.Name.ToString();
                if (d.Code != null) try { row[3] = d.Code.ToString(); } catch { };
                if (d.KLRgn_id != null) row[4] = d.KLRgn_id.ToString();
                if (d.begDate != null) row[5] = Convert.ToDateTime(d.begDate.ToString());
                if (d.endDate != null) row[6] = Convert.ToDateTime(d.endDate.ToString());
                Info.ds.Tables[this.spr_name].Rows.Add(row);

            }
 
            int i = 0;
            try
            {
                i = Adapter.Update(Info.ds.Tables[this.spr_name]);
                ret = " Сохранено записей " + i.ToString();
            }
            catch (Exception ex)
            {
                ret = ex.Message + " - Ошибочка : сохранено записей " + i.ToString();
            }

            return ret;

        }

        void todb_lpulist()
        {

            FbCommand Command;
            FbCommandBuilder Builder;
            FbDataAdapter Adapter;

            if (Info.conn.State == ConnectionState.Closed)
            {
                bool is_connect = Info.is_connect_db();
                if (!is_connect)
                    return;
            }
            // ===
            bool is_table = Info.existsTable(this.spr_name);
            int err = 0;
            if (!is_table)
            {
                err = Info.createTable("CREATE TABLE " + this.spr_name + " (LPU_ID VARCHAR(36),ORG_NAME VARCHAR(500) CHARACTER SET UTF8,ORG_NICK VARCHAR(50) CHARACTER SET UTF8)");
                if (err == 1)
                {
                    Info.conn.Close();
                    return;
                }
            }
            else
                Info.make_query_db("DELETE FROM " + this.spr_name);

            Thread.Sleep(1000);

            Command = new FbCommand("SELECT * FROM " + this.spr_name, Info.conn);
            Adapter = new FbDataAdapter(Command);
            Builder = new FbCommandBuilder(Adapter);
            Adapter.Fill(Info.ds, this.spr_name);

            string jsonString = File.ReadAllText(Info.path_app + "sprecp/" + this.spr_name + ".txt");
            rootLpuListObject = JsonConvert.DeserializeObject<RootLpuListObject>(jsonString);
            lpuls = rootLpuListObject.data;

            int y = 1;
            foreach (DatumLpuList d in lpuls)
            {
                DataRow row = Info.ds.Tables[this.spr_name].NewRow();
                row[0] = d.Lpu_id.ToString();
                row[1] = d.Org_Name.ToString();
                row[2] = d.Org_Nick.ToString();
                Info.ds.Tables[this.spr_name].Rows.Add(row);

                Application.DoEvents();
                this.label1.Text = (y++).ToString();
            }

            int i = Adapter.Update(Info.ds.Tables[this.spr_name]);
            if (i > 0) MessageBox.Show("Изменения успешно сохранены в базе данных");

        }

        private async void button11_Click(object sender, EventArgs e)
        {
            // to db
            if (this.spr_name == "")
            {
                MessageBox.Show("Вы не выбрали таблицу для сохранения");
                return;
            }
            if (this.spr_name == "lpulist")
            {
                MessageBox.Show("Вы выбрали таблицу для сохранения " + this.spr_name);
                return;
            }

            if (Info.conn.State == ConnectionState.Closed)
            {
                bool is_connect = Info.is_connect_db();
                if (!is_connect) return;
            }

            bool is_table = Info.existsTable(this.spr_name);
            int err = 0;
            if (!is_table)
            {
                err = Info.createTable("CREATE TABLE " + this.spr_name + " (ID_SPR VARCHAR(36),ORGTYPE_ID VARCHAR(36),NAME_SPR VARCHAR(500) CHARACTER SET UTF8,CODE_SPR VARCHAR(36),KLRGN_ID VARCHAR(36),BEGDATE DATE,ENDDATE DATE)");
                if (err == 1)
                {
                    Info.conn.Close();
                    return;
                }
            }
            else
                Info.make_query_db("DELETE FROM " + this.spr_name);

            Thread.Sleep(1000);

            this.toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
            //this.toolStripLabel1.Text = "Загружаю НСИ и данные";

            Task<string> taskValue = TodbStdsprAsync();
            string value = await taskValue;


            /*
            switch (this.spr_name)
            {
                case "lpulist":
                    this.todb_lpulist();
                    break;

                default:
                    this.todb_stdspr();
                    break;

            }
            */

            this.toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
            //this.toolStripLabel1.Text = "";

            MessageBox.Show(value);

        }


        private void button12_Click(object sender, EventArgs e)
        {
            // save to bd refbooklist
            FbCommand Command;
            FbCommandBuilder Builder;
            FbDataAdapter Adapter;
            this.spr_name = "refbooklist";

            if (Info.conn.State == ConnectionState.Closed)
            {
                bool is_connect = Info.is_connect_db();
                if (!is_connect)
                    return;
            }

            // ===
            bool is_table = Info.existsTable(this.spr_name);
            int err = 0;
            if (!is_table)
            {
                err = Info.createTable("CREATE TABLE refbooklist (REFBOOK_CODE VARCHAR(50) CHARACTER SET UTF8,REFBOOK_NAME VARCHAR(500) CHARACTER SET UTF8,REFBOOKTYPE_ID VARCHAR(50) CHARACTER SET UTF8,REFBOOK_TABLENAME VARCHAR(50) CHARACTER SET UTF8)");
                if (err == 1)
                {
                    Info.conn.Close();
                    return;
                }
            }
            else
                Info.make_query_db("DELETE FROM " + this.spr_name);

            Thread.Sleep(1000);

            Command = new FbCommand("SELECT * FROM " + this.spr_name, Info.conn);
            Adapter = new FbDataAdapter(Command);
            Builder = new FbCommandBuilder(Adapter);
            Adapter.Fill(Info.ds, this.spr_name);

            string jsonString1 = File.ReadAllText(Info.path_app + "sprecp/refbooklist.txt");
            rootRefbookObject = JsonConvert.DeserializeObject<RootRefbookObject>(jsonString1);
            refbooks = rootRefbookObject.data;

            int y = 1;
            foreach (DatumRefbook d in refbooks)
            {
                DataRow row = Info.ds.Tables[this.spr_name].NewRow();
                row["Refbook_Code"] = d.Refbook_Code.ToString();
                row["Refbook_Name"] = d.Refbook_Name.ToString();
                row["RefbookType_id"] = d.RefbookType_id.ToString();
                row["Refbook_TableName"] = d.Refbook_TableName.ToString();
                Info.ds.Tables[this.spr_name].Rows.Add(row);

                Application.DoEvents();
                this.label2.Text = (y++).ToString();
            }

            int i = Adapter.Update(Info.ds.Tables[this.spr_name]);
            if (i > 0) MessageBox.Show("Изменения успешно сохранены в базе данных");
        }
        
        private void button13_Click(object sender, EventArgs e)
        {
          /*
            string nametable = "";
            OpenFileDialog OPF = new OpenFileDialog();
            OPF.Filter = "Файлы csv|*.csv";
            if (OPF.ShowDialog() == DialogResult.OK)
            {
                //MessageBox.Show(OPF.FileName);
                //MessageBox.Show(Path.GetFileNameWithoutExtension(ofd.FileName));
                nametable = Path.GetFileNameWithoutExtension(OPF.FileName);
                //MessageBox.Show(nametable);
                // clear table db
                bool is_connect = Info.is_connect_db();
                if (!is_connect) return;
                string sql_str = "DELETE FROM " + nametable;
                Info.make_query_db(sql_str);
                // clear DataTable
                Info.ds.Tables[nametable].Clear();                
                this.dgv_sprs.DataSource = Info.ds.Tables[nametable];

                var csv = File.ReadAllText(OPF.FileName);
                foreach (var line in CsvReader.ReadFromText(csv))
                {
                    DataRow row = Info.ds.Tables[nametable].NewRow();
                    row["NAME_SPR"] = line["namespr"];
                    row["DESCRIPTION"] = line["description"];
                    row["REFBOOK_TABLENAME"] = line["Refbook_TableName"];
                    row["URL"] = line["url"];
                    row["UPD_DATE"] = line["upd_date"];
                    Info.ds.Tables[nametable].Rows.Add(row);

                    Application.DoEvents();
                }
                this.label3.Text = "SPRS - РЕЖИМ РЕДАКТИРОВАНИЯ";

            }
            else MessageBox.Show("Вы не выбрали ничего");
         */
        }
        

        public async Task<string> get_responseAsync(string surl)
        {
            string ret = "";
            this.SaveLogString(this.pathLog, "get_responseAsync", surl);
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(surl);
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    ret = content;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("get_responseAsync : Exception : " + ex.Message);
                    this.SaveLogString(this.pathLog, "get_responseAsync", "Exception : " + ex.Message);
                }
            }

            //this.listBox1.Items.Add(DateTime.Now.ToString("dd.MM.yyyy:HH.mm.ss"));
            //his.listBox1.Items.Add(surl);            
            //this.lb_log.Text = surl;

            return ret;
        }

        private async void button14_Click(object sender, EventArgs e)
        {
            // from ECP to DB
            Info.f_row = this.dgv_sprs.CurrentCell.RowIndex;
            this.table_name = this.dgv_sprs[2, Info.f_row].Value.ToString();
            this.s_url = this.dgv_sprs[3, Info.f_row].Value.ToString();
            this.spr_name = this.dgv_sprs[0, Info.f_row].Value.ToString();

            //
            this.button14.Enabled = false;
            this.pb1.Style = ProgressBarStyle.Marquee;

            //this.listBox1.Items.Clear();

            // login ecp
            Task<string> t1 = this.get_responseAsync(Info.strloginecp);
            Info.raw_login_ecp = await t1;
            try
            {
                this.loginEcp = JsonConvert.DeserializeObject<LoginEcp>(Info.raw_login_ecp);
            }
            catch (Exception ex)
            {
                MessageBox.Show("raw_login_ecp : deserialize : Исключение JsonConvert " + ex.Message);
                this.SaveLogString(this.pathLog, "get_responseAsync", "raw_login_ecp : deserialize : Исключение JsonConvert " + ex.Message);
                this.pb1.Style = ProgressBarStyle.Blocks;
                this.button14.Enabled = true;
                return;
            }
            // log
            if (this.loginEcp != null)
                Info.sess_id = this.loginEcp.sess_id.ToString();
            else
            {
                this.pb1.Style = ProgressBarStyle.Blocks;
                this.button14.Enabled = true;
                MessageBox.Show("loginEcp is null");
                this.SaveLogString(this.pathLog, "get_responseAsync", "loginEcp is null");
                return;
            }
            // get spr info
            Task<string> t2;
            switch (this.spr_name)
            {
                case "lpulist":
                    t2 = this.get_responseAsync(this.s_url + "?sess_id=" + Info.sess_id + "&Region_id=52" + "&limit=0");
                    Info.raw_info_spr = await t2;
                    break;

                default:
                    t2 = this.get_responseAsync(this.s_url + "?sess_id=" + Info.sess_id + "&Refbook_TableName=" + this.table_name + "&limit=0");
                    Info.raw_info_spr = await t2;                    
                    break;

            }
            // save
            if (File.Exists(Info.path_app + "sprecp/" + this.spr_name + "_bak.txt")) File.Delete(Info.path_app + "sprecp/" + this.spr_name + "_bak.txt");
            if (File.Exists(Info.path_app + "sprecp/" + this.spr_name + ".txt")) File.Copy(Info.path_app + "sprecp/" + this.spr_name + ".txt", Info.path_app + "sprecp/" + this.spr_name + "_bak.txt");
            File.WriteAllText(Info.path_app + "sprecp/" + this.spr_name + ".txt", Info.raw_info_spr);
            MessageBox.Show("Справочник " + this.spr_name + " сохранен");
            this.SaveLogString(this.pathLog, "get_responseAsync - из ЕЦП сохранить txt", "Справочник " + this.spr_name + " сохранен");

            this.button14.Enabled = true;
            this.pb1.Style = ProgressBarStyle.Blocks;

        }

        private async void button15_Click(object sender, EventArgs e)
        {
            // get refbooklist save
            this.spr_name = "refbooklist";

            //
            this.button15.Enabled = false;
            this.pb1.Style = ProgressBarStyle.Marquee;

            // login ecp
            Task<string> t1 = this.get_responseAsync(Info.strloginecp);
            Info.raw_login_ecp = await t1;
            try
            {
                this.loginEcp = JsonConvert.DeserializeObject<LoginEcp>(Info.raw_login_ecp);
            }
            catch (Exception ex)
            {
                MessageBox.Show("raw_login_ecp : deserialize : Исключение JsonConvert " + ex.Message);
                this.SaveLogString(this.pathLog, "get refbooklist", "raw_login_ecp : deserialize : Исключение JsonConvert " + ex.Message);
                this.pb1.Style = ProgressBarStyle.Blocks;
                this.button15.Enabled = true;
                return;
            }
            // log
            if (this.loginEcp != null)
                Info.sess_id = this.loginEcp.sess_id.ToString();
            else
            {
                this.pb1.Style = ProgressBarStyle.Blocks;
                this.button15.Enabled = true;
                MessageBox.Show("loginEcp is null");
                this.SaveLogString(this.pathLog, "get refbooklist", "loginEcp is null");
                return;
            }
            // get spr info
            //Task<string> t2 = this.get_responseAsync(this.tb_url_refbooklist.Text.ToString() + "?sess_id=" + Info.sess_id + "&Region_id=52" + "&limit=0");
            Task<string> t2 = this.get_responseAsync(this.tb_url_refbooklist.Text.ToString() + "?sess_id=" + Info.sess_id + "&Region_id=52");
            Info.raw_info_spr = await t2;
            // save
            if (File.Exists(Info.path_app + "sprecp/" + this.spr_name + "_bak.txt")) File.Delete(Info.path_app + "sprecp/" + this.spr_name + "_bak.txt");
            if (File.Exists(Info.path_app + "sprecp/" + this.spr_name + ".txt")) File.Copy(Info.path_app + "sprecp/" + this.spr_name + ".txt", Info.path_app + "sprecp/" + this.spr_name + "_bak.txt");
            File.WriteAllText(Info.path_app + "sprecp/" + this.spr_name + ".txt", Info.raw_info_spr);
            MessageBox.Show("Справочник " + this.spr_name + " сохранен");

            this.button15.Enabled = true;
            this.pb1.Style = ProgressBarStyle.Blocks;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pathLog = Info.path_app + "log-nsi.txt";
            using (var sw = new StreamWriter(pathLog, true))
                foreach (var item in this.listBox1.Items) sw.Write(item.ToString() + Environment.NewLine);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // load excel
            if (this.ds != null) ds.Dispose();
            this.ds = null;

            using (var stream = File.Open("sprecp\\usluga_complex.xlsx", FileMode.Open, FileAccess.Read))
            {
                //using (var reader = ExcelReaderFactory.CreateReader(stream))
                //{
                //    var result = reader.AsDataSet();
                //    this.dataGridView1.DataSource = result.Tables[0];
                //}

                using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var conf = new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true
                        }
                    };
                    this.ds = reader.AsDataSet(conf);
                    this.dataGridView1.DataSource = this.ds.Tables[0];

                }
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            // build usluga spr
            FormBuildUslugaSpr fbsu = new FormBuildUslugaSpr(Info.ds.Tables["uslugacomplex"]);
            fbsu.ShowDialog();

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
