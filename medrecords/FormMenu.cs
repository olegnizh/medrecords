using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;


namespace medrecords
{
    public partial class FormMenu : Form
    {

        public FormMenu()
        {
            InitializeComponent();
            Info.path_app = AppDomain.CurrentDomain.BaseDirectory;
            //this.Text = "Медицинские записи";


            // init local            
            //Info.path_db = "db\\DIRECTIONS.FDB";
            //Info.str_connect = "User=SYSDBA;Password=masterkey;Database="+ Info.path_db + ";DataSource=localhost;ServerType=1;Port=3050;Charset=UTF8";

            //MessageBox.Show("--- "+Info.str_connect);

        }

        private void add_Click(object sender, EventArgs e)
        {

            FormDirection f = new FormDirection();
            this.Hide();
            f.Show();
        }

        private void edit_Click(object sender, EventArgs e)
        {
            FormNsiEcp f = new FormNsiEcp();
            this.Hide();
            f.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormSprMedserv f = new FormSprMedserv();
            this.Hide();
            f.Show();
        }

        private void unload_Click(object sender, EventArgs e)
        {

        }

        private void info_Click(object sender, EventArgs e)
        {
            //FormProbe f = new FormProbe();
            //this.Hide();
            //f.Show();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            if ((Info.conn != null) && (Info.conn.State == ConnectionState.Open))
            {
                Info.conn.Close();
                Info.conn.Dispose();
                Info.conn = null;
            }

            if (Info.ds != null)
            {
                Info.ds.Dispose();
                Info.ds = null;
            }
            
            Application.Exit();

        }

        private void button2_Click(object sender, EventArgs e)
        {

            DataTable dt = new DataTable("settings");
            dt.Clear();
            dt.Columns.Add("MAX_NUM_DIR");
            dt.Columns.Add("FIELD2");
            DataRow dr = dt.NewRow();
            dr["MAX_NUM_DIR"] = 1;
            dr["FIELD2"] = "null";
            dt.Rows.Add(dr);
            

            dt.WriteXml("settings.xml");

            

        }



    }
}
