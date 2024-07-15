using System;
using System.Data;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using FirebirdSql.Data.FirebirdClient;


namespace medrecords
{
    public static class Info
    {

        // patient
        public static string id_patient { get; set; }
        public static string ecp_id_patient { get; set; }

        // direction
        public static string id_direction { get; set; }
        
        // init parameters - in Program
        public static string MedStaffFact_sid = "";
        public static string Lpu_sid = "";
        public static string LpuSection_sid = "";
        public static string Login = "";
        public static string Password = "";
        public static string Domain = "";

        public static string strloginecp = "";

        public static string ecp_id_direction { get; set; }
        public static string ecp_id_evn { get; set; }
        public static string ecp_id_evnqueue { get; set; }
        public static string ecp_id_evnlabrequest { get; set; }
        public static string ecp_id_evnprescr { get; set; }

        // evnplbase
        public static string id_evnplbase { get; set; }
        public static string evnvizitpl_id { get; set; }
        public static string evnplbase_id { get; set; }

        //
        public static int f_row     = 0;
        public static int d_row     = 0;
        public static int e_row     = 0;
        
        public static string patients = "patients.xml";
        public static string uslugs   = "uslugs.xml";

        public static DataTable dt_users = new DataTable();

        public static DataSet      ds   = null;
        public static FbConnection conn = null;
        public static string str_connect = "";

        public static string path_app = "";
        public static string path_db = "";                

        public static string raw_info_spr = "";
        public static string raw_login_ecp = "";
        public static string raw_ismen_ecp = "";
        public static string raw_men_ecp = "";

        public static string raw_direction_ecp = "";
        public static string raw_evnplbase_ecp = "";
        public static string raw_direction_resultat = "";


        public static string sess_id    = "";

        public static string selected_usluga_code = "";
        public static string selected_usluga_id   = "";
        public static string selected_usluga      = "";

        public static string selected_diag      = "";
        public static string selected_diag_id   = "";
        public static string selected_diag_code = "";

        // settings
        public static DataTable dt_settings;
        public static int max_num_dir = 0;
        public static int max_num_tap = 0;



        // is connect there
        public static bool is_connect_db()
        {
            bool ret = true;
            conn = new FbConnection(str_connect);
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ret = false;
            }
            return ret;
        }


        public static bool existsTable(string tablename)
        {
            bool ret = false;
            FbCommand cmd = new FbCommand("SELECT RDB$RELATION_NAME FROM RDB$RELATIONS WHERE RDB$RELATION_NAME=@TABLENAME", Info.conn);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@TABLENAME", tablename.ToUpper());
            FbDataReader rd = cmd.ExecuteReader();
            string s = "";
            if (rd.Read())
            {
                s = rd.GetString(0);
                ret = true;
            }

            return ret;
        }

        //"CREATE TABLE "+tablename+" (LPU_ID BIGINT,ORG_NAME VARCHAR(500) CHARACTER SET UTF8,ORG_NICK VARCHAR(50) CHARACTER SET UTF8)"
        //"CREATE TABLE "+tablename+" (REFBOOK_CODE VARCHAR(50) CHARACTER SET UTF8,REFBOOK_NAME VARCHAR(500) CHARACTER SET UTF8,REFBOOKTYPE_ID VARCHAR(50) CHARACTER SET UTF8,REFBOOK_TABLENAME VARCHAR(50) CHARACTER SET UTF8)"
        //"CREATE TABLE "+tablename+" (ID_SPR BIGINT,ORGTYPE_ID BIGINT,NAME_SPR VARCHAR(500) CHARACTER SET UTF8,CODE_SPR BIGINT,KLRGN_ID BIGINT,BEGDATE DATE,ENDDATE DATE)"
        public static int createTable(string str_query)
        {
            int ret = 0;
            FbCommand cmd = new FbCommand(str_query, Info.conn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при создании таблицы :" + ex.Message);
                ret = 1;
            }
            return ret;
        }

        public static int doMyCmd(string str_query)
        {
            int ret = 0;
            FbCommand cmd = new FbCommand(str_query, Info.conn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка в процессе запроса SQL - " + str_query + " : " + ex.Message);
                ret = 1;
            }
            return ret;
        }

        public static void make_query_db(string qstr)
        {
            FbCommand cmd = new FbCommand(qstr, Info.conn);
            cmd.ExecuteNonQuery();
                
        }

        public static void GetDirectionData(FbConnection connection, string querystring1, string querystring2, string querystring3)
        {
            FbCommand Command1, Command2, Command3;
            FbDataAdapter Adapter1, Adapter2, Adapter3;

            Command1 = new FbCommand(querystring1, connection);
            Adapter1 = new FbDataAdapter(Command1);
            Adapter1.Fill(Info.ds, "patient");

            Command2 = new FbCommand(querystring2, connection);
            Adapter2 = new FbDataAdapter(Command2);
            Adapter2.Fill(Info.ds, "direction");

            Command3 = new FbCommand(querystring3, connection);
            Adapter3 = new FbDataAdapter(Command3);
            Adapter3.Fill(Info.ds, "evnplbase");

        }

        public static void GetSimpleTable(FbConnection connection, string nametable, string querystring)
        {
            FbCommand Command;
            //FbCommandBuilder Builder;
            FbDataAdapter Adapter;          

            DataTable dt = new DataTable(nametable);
            Info.ds.Tables.Add(dt);
            Command = new FbCommand(querystring, connection);
            Adapter = new FbDataAdapter(Command);
            //Builder = new FbCommandBuilder(Adapter);
            Adapter.Fill(Info.ds, nametable);
        }




    }
    
}
