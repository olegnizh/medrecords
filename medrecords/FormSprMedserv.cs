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
//using RestSharp;
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


namespace medrecords
{

    public partial class FormSprMedserv : Form
    {

        FbCommand command;
        FbCommandBuilder builder;
        FbDataAdapter adapter;

        public FormSprMedserv()
        {

            InitializeComponent();
            Info.path_app = AppDomain.CurrentDomain.BaseDirectory;

            // local           
            //Info.path_db = "db\\DIRECTIONS.FDB";
            //Info.str_connect = "User=SYSDBA;Password=masterkey;Database=" + Info.path_db + ";DataSource=localhost;ServerType=1;Port=3050;Charset=UTF8";
            //MessageBox.Show("--- "+Info.str_connect);

            bool is_connect = Info.is_connect_db();
            if (!is_connect) return;

            bool is_table = Info.existsTable("medtest");
            if (!is_table)
            {
                MessageBox.Show("Таблица MEDTEST не существует");
                Info.conn.Close();
                return;
            }
            else
            {
                this.loadSprMedservAsync();
            }


        }


        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        protected Task<long> fromCsvMedtestAsync(string tname)
        {
            return Task.Run(() =>
            {

                Info.ds = new DataSet("medic");
                DataTable dt = new DataTable("medtest");
                Info.ds.Tables.Add(dt);

                command = new FbCommand("SELECT * FROM medtest ORDER BY uslugacomplex_name", Info.conn);
                adapter = new FbDataAdapter(command);
                builder = new FbCommandBuilder(adapter);
                adapter.Fill(Info.ds, "medtest");

                long ind = 0;
                var csv = File.ReadAllText(tname);
                foreach (var line in CsvReader.ReadFromText(csv))
                {
                    DataRow row = Info.ds.Tables["medtest"].NewRow();
                    ind++;

                    if (line["uslugacomplex_id"] != "")
                        row["USLUGACOMPLEX_ID"] = line["uslugacomplex_id"].ToString();
                    else
                        row["USLUGACOMPLEX_ID"] = 0;

                    if (line["uslugacomplex_pid"] != "")
                        row["USLUGACOMPLEX_PID"] = line["uslugacomplex_pid"].ToString();
                    else
                        row["USLUGACOMPLEX_PID"] = 0;

                    row["USLUGACOMPLEX_CODE"] = line["uslugacomplex_code"].ToString();
                    row["USLUGACOMPLEX_NAME"] = line["uslugacomplex_name"].ToString();

                    Info.ds.Tables["medtest"].Rows.Add(row);

                }

                this.adapter.Update(Info.ds.Tables["medtest"]);
                return ind;

            });
        }

        private async void button13_Click(object sender, EventArgs e)
        {
            int err = 0;

            // create medtest
            bool is_connect = Info.is_connect_db();
            if (!is_connect) return;

            bool is_table = Info.existsTable("medtest");
            if (is_table)
            {
                MessageBox.Show("Данные MEDTEST будут удалены");
                err = 0;
                err = Info.doMyCmd("DELETE FROM MEDTEST");
                if (err == 1)
                {
                    MessageBox.Show("Ошбка удаления данных MEDTEST");
                    Info.conn.Close();
                    return;
                }
                this.dgv_medtest.DataSource = null;
                Thread.Sleep(1000);
            }
            else
            {
                MessageBox.Show("Таблица MEDTEST не найдена");
                Info.conn.Close();
                return;
            }

            OpenFileDialog OPF = new OpenFileDialog();
            OPF.Filter = "Файлы csv|*.csv";
            if (OPF.ShowDialog() == DialogResult.OK)
            {

                this.pb1.Style = ProgressBarStyle.Marquee;
                this.label1.Text = "Сохраняю записи";

                Task<long> taskValue = fromCsvMedtestAsync(OPF.FileName);
                long value = await taskValue;

                MessageBox.Show("Сохранено " + value + " записей");

                this.dgv_medtest.DataSource = null;
                this.dgv_medtest.DataSource = Info.ds.Tables["medtest"];
                this.dgv_medtest.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                this.pb1.Style = ProgressBarStyle.Blocks;
                this.label1.Text = ".";

            }
            else MessageBox.Show("Вы не выбрали ничего");

            Info.conn.Close();

        }

        private void FormSprMedserv_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            if (Info.conn.State == ConnectionState.Open)
                Info.conn.Close();

            FormMenu f = new FormMenu();
            this.Hide();
            f.Show();

        }

        protected Task<long> loadMedtest()
        {
            return Task.Run(() =>
            {
                Info.ds = new DataSet("medic");
                DataTable dt = new DataTable("medtest");
                Info.ds.Tables.Add(dt);

                command = new FbCommand("SELECT * FROM medtest ORDER BY uslugacomplex_name", Info.conn);
                adapter = new FbDataAdapter(command);
                builder = new FbCommandBuilder(adapter);
                long rec = adapter.Fill(Info.ds, "medtest");
                return rec;
            });
        }

        private async void loadSprMedservAsync()
        {
            this.pb1.Style = ProgressBarStyle.Marquee;
            this.label1.Text = "Загружаю данные";

            Task<long> taskValue = loadMedtest();
            long value = await taskValue;

            this.dgv_medtest.DataSource = null;
            this.dgv_medtest.DataSource = Info.ds.Tables["medtest"];
            this.dgv_medtest.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Info.conn.Close();

            MessageBox.Show("Загружено " + value + " записей");

            this.pb1.Style = ProgressBarStyle.Blocks;
            this.label1.Text = ".";

        }


    }
}
