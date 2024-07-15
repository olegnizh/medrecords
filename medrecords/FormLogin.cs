using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;


namespace medrecords
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
            PassTextBox.PasswordChar = '•';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string user = "admin";
            string pass = "admin";
            if (user == this.UserTextBox.Text && pass == this.PassTextBox.Text)
            {
                //MessageBox.Show("Username and password is correct");
                //this.Hide();
                //Form2 f2 = new Form2();
                //f2.ShowDialog();

            }
            else
            {
                MessageBox.Show("Неправильные Пользовател и/или Пароль");
                return;
            }
            /*
            try
            {
                FbDataAdapter adapterx = new FbDataAdapter("select * from users order by Id_user", Info.conn);
                adapterx.Fill(Info.dt_users);

                //    string MyConnection = "datasource=localhost;port=3307;username=root;password=root";
                //    MySqlConnection MyConn = new MySqlConnection(MyConnection);
                //    MySqlCommand MyCommand = new MySqlCommand("select * from student.studentinfo where user_name='" + this.UserTextBox.Text + "' and password='" + this.PassTextBox.Text + "' ;", MyConn);
                //    MySqlDataReader MyReader;

                //    MyConn.Open();
                //    MyReader = MyCommand.ExecuteReader();
                //    int count = 0;
                //    while (MyReader.Read())
                //    {
                //        Console.WriteLine(MyReader[count]);
                //        count++;
                //    }
                //    MessageBox.Show("Username and password is correct");
                //    this.Hide();
                //    Form2 f2 = new Form2();
                //    f2.ShowDialog();
                //    //if (count == 1)
                //    //{
                //    //    MessageBox.Show("Username and password is correct");
                //    //    this.Hide();
                //    //    Form2 f2 = new Form2();
                //    //    f2.ShowDialog();
                //    //}
                //    //else if (count > 1)
                //    //{

                //    //    MessageBox.Show("Duplicate Username and passwor.\nAccess denied.");
                //    //}
                //    //else
                //    //{
                //    //    MessageBox.Show("Username and password is incorrect.\nPleas try again.");
                //    //}
                //    MyConn.Close();

            }
            catch (Exception ex)
            {
                     MessageBox.Show("Ошибка взятия таблицы пользователей - " + ex.Message);
                     Info.conn.Close();
                     Application.Exit();
            }
            */
            //Info.is_login = true;
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Info.is_login = false;
            this.Close();
        }

    }
}
