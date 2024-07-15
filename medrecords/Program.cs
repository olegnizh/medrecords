using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;


namespace medrecords
{
    static class Program
    {
 
        [STAThread]
        static void Main()
        {

            /*
            // init emb            
            Info.path_db = "db\\DIRECTIONS.FDB";
            Info.str_connect = "User=SYSDBA;Password=masterkey;Database=db\\DIRECTIONS.FDB;DataSource=localhost;ServerType=1;Port=3050;Charset=UTF8";

            // init srv
            Info.str_connect = "User=SYSDBA;Password=masterkey;Database=C:\\db\\DIRECTIONS.FDB;DataSource=localhost;ServerType=0;Port=3050;Charset=UTF8";
            */

            Info.str_connect = ConfigurationSettings.AppSettings["StringConnection"];
            //MessageBox.Show(Info.str_connect);
            
            string s = ConfigurationSettings.AppSettings["newkey"];
            //MessageBox.Show(s);
            if (s == "0") MessageBox.Show("Режим по умолчанию - Флаг режима изменен");
            if (s == "1") MessageBox.Show("Режим нормальный");
            if (s == "1")
            {
                Info.MedStaffFact_sid = ConfigurationSettings.AppSettings["MedStaffFact_sid"];
                //MessageBox.Show(Info.MedStaffFact_sid);
                Info.Lpu_sid = ConfigurationSettings.AppSettings["Lpu_sid"];
                //MessageBox.Show(Info.Lpu_sid);
                Info.LpuSection_sid = ConfigurationSettings.AppSettings["LpuSection_sid"];
                //MessageBox.Show(Info.LpuSection_sid);
                Info.Login = ConfigurationSettings.AppSettings["Login"];
                //MessageBox.Show(Info.Login);
                Info.Password = ConfigurationSettings.AppSettings["Password"];
                //MessageBox.Show(Info.Password);
                Info.Domain = ConfigurationSettings.AppSettings["Domain"];
                //MessageBox.Show(Info.Domain);
                //
                Info.strloginecp = @"https://" + Info.Domain + @".mznn.ru/api/user/login?Login=" + Info.Login + "&Password=" + Info.Password;
                //MessageBox.Show(Info.strloginecp);
            }
            
            // test connection bd
            bool is_connect = Info.is_connect_db();
            if (!is_connect)
            {
                //MessageBox.Show("Program - При соединении с БД произошел сбой");
                Environment.Exit(0);
            }
            Info.conn.Close();

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMenu());
            //Application.Run(new Form1());

            /*
            DialogResult result;
            using (var p = new FormLogin())
            {
                result = p.ShowDialog();
                if (result == DialogResult.OK)
                    Application.Run(new FormMenu());
                else
                {
                    MessageBox.Show("Вы не вошли в систему ...");
                    Application.Exit();
                }
            }
            */

        }
    }
}
