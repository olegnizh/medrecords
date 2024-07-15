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
using System.Configuration;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using FirebirdSql.Data.FirebirdClient;


namespace medrecords
{
    public partial class FormDirection : Form
    {


        bool f_log;
        string pathLog = "";
        string pathDirResLog = "";
        string titleForm = "МЕДИЦИНСКИЕ ЗАПИСИ - ПОСЕЩЕНИЕ НАПРАВЛЕНИЕ";

        LoginEcp loginEcp;
        RootPersonObject rootPersonObject;
        RootDirectionObject rootDirectionObject;
        RootEvnplbaseObject rootEvnplbaseObject;

        RootDirectionResultatObject rootDirectionResultatObject;
        string stringDirectionResult;
        FormDirectionResultat fdr;

        BindingSource parent = new BindingSource();
        BindingSource detail_tap = new BindingSource();
        BindingSource detail_dir = new BindingSource();
        
        //FbCommand command;
        //FbCommandBuilder builder;
        //FbDataAdapter adapter;

        public FormDirection()
        {
            InitializeComponent();

            Info.path_app = AppDomain.CurrentDomain.BaseDirectory;
            this.f_log = true;
            this.pathDirResLog = Info.path_app + "log-dirres.txt";

            //MessageBox.Show(Info.path_app);

            if (Info.ds != null)
            {
                Info.ds.Dispose();
                Info.ds = null;
            }
            Info.ds = new DataSet("medic");        

            this.makeLoadDataAsync();            

            this.Text = this.titleForm;

            //this.dgvParent.AutoResizeColumns();
            //this.dgvParent.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            //this.dgvDetail.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            // read setting
            /*
            Info.dt_settings = new DataTable("settings");
            Info.dt_settings.Clear();
            Info.dt_settings.Columns.Add("MAX_NUM_DIR");
            Info.dt_settings.Columns.Add("MAX_NUM_TAP");
            Info.dt_settings.Columns.Add("FIELD2");
            Info.dt_settings.ReadXml("settings.xml");

            Info.max_num_dir = Convert.ToInt32(Info.dt_settings.Rows[0]["MAX_NUM_DIR"].ToString());
            Info.max_num_tap = Convert.ToInt32(Info.dt_settings.Rows[0]["MAX_NUM_TAP"].ToString());
            */

        }

        private async void makeLoadDataAsync()
        {
            this.button1.Visible = false;
            this.button3.Visible = false;
            this.button2.Visible = false;
            this.button12.Visible = false;
            this.button13.Visible = false;
            this.textBox1.Visible = false;

            this.toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
            this.toolStripLabel1.Text = "Загружаю НСИ и данные";

            Task<long> taskValue = LoadDataAsync();
            long value = await taskValue;
            if (value == 0)
            {
                MessageBox.Show("Данные загружены");

                this.CreateRelation_tap();
                this.CreateRelation_dir();

                this.dgvParent.DataSource = this.parent;
                this.dgvEvnplbase.DataSource = this.detail_tap;
                this.dgvDetail.DataSource = this.detail_dir;

                this.dgvParent_prepare();
                this.dgvEvnplbase_prepare();
                this.dgvDetail_prepare();

                this.view_columns_parent();
                this.view_columns_evnplbase();
                this.view_columns_direction();

                // =========================
                //this.fam.DataBindings.Add("Text", Info.ds.Tables["PATIENT"], "fio_fam",true);
                //this.fam.Text = this.parent.DataMember()
                this.fam.DataBindings.Add("Text", this.parent, "fio_fam", true);
                this.im.DataBindings.Add("Text", this.parent, "fio_name", true);
                this.otch.DataBindings.Add("Text", this.parent, "fio_otch", true);
               
            }

            this.toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
            this.toolStripLabel1.Text = "";

            this.button1.Visible = true;
            this.button3.Visible = true;
            this.button2.Visible = true;
            this.button12.Visible = true;
            this.button13.Visible = true;
            this.textBox1.Visible = true;

        }

        protected Task<long> LoadDataAsync()
        {
            return Task.Run(() =>
            {
                long ret = 0;
                bool is_connect = Info.is_connect_db();
                if (!is_connect)
                {
                    MessageBox.Show("FormDirection: LoadDataAsync - При соединении с БД произошел сбой - вы не сможете работать");
                    ret = 1;
                }
                else  this.load_data();
                Info.conn.Close();
                return ret;
            });
        }


        void load_data()
        {

            // spr
            Info.GetSimpleTable(Info.conn, "sex", "SELECT * FROM sex ORDER BY name_spr ASC");
            Info.GetSimpleTable(Info.conn, "socstatus", "SELECT * FROM socstatus ORDER BY name_spr ASC");
            //
            Info.GetSimpleTable(Info.conn, "dirtype", "SELECT * FROM dirtype ORDER BY id_spr ASC");
            Info.GetSimpleTable(Info.conn, "lpuunittype", "SELECT * FROM lpuunittype ORDER BY Name_spr ASC");
            Info.GetSimpleTable(Info.conn, "lpusectionprofile", "SELECT * FROM lpusectionprofile ORDER BY Name_spr ASC");
            Info.GetSimpleTable(Info.conn, "prescriptiontype", "SELECT * FROM prescriptiontype ORDER BY id_spr ASC");
            Info.GetSimpleTable(Info.conn, "paytype", "SELECT * FROM paytype ORDER BY Name_spr ASC");
            Info.GetSimpleTable(Info.conn, "lpusection", "SELECT * FROM lpusection ORDER BY Name_spr ASC");
            Info.GetSimpleTable(Info.conn, "diag", "SELECT * FROM diag ORDER BY Name_spr ASC");
            Info.GetSimpleTable(Info.conn, "treatmentclass", "SELECT * FROM treatmentclass WHERE orgtype_id is null ORDER BY Name_spr ASC");
            Info.GetSimpleTable(Info.conn, "servicetype", "SELECT * FROM servicetype WHERE ORGTYPE_ID='pol' ORDER BY Name_spr ASC");
            Info.GetSimpleTable(Info.conn, "vizittype", "SELECT * FROM vizittype WHERE ORGTYPE_ID='pol' ORDER BY Name_spr ASC");
            Info.GetSimpleTable(Info.conn, "medicalcarekind", "SELECT * FROM medicalcarekind ORDER BY Name_spr ASC");
            // add
            Info.GetSimpleTable(Info.conn, "lpu", "SELECT * FROM lpu ORDER BY Name_spr ASC");

            //Info.GetSimpleTable(Info.conn, "vizitclass", "SELECT * FROM vizitclass ORDER BY code_spr ASC");
            //Info.GetSimpleTable(Info.conn, "lpulist", "SELECT * FROM lpulist ORDER BY Org_Name ASC");

            //Info.GetSimpleTable(Info.conn, "uslugacomplex", "SELECT * FROM uslugacomplex WHERE (code_spr <> '') AND not (code_spr CONTAINING 'rmis') AND not (name_spr CONTAINING '(до') AND (name_spr CONTAINING 'исследование') ORDER BY Name_spr ASC");
            //Info.GetSimpleTable(Info.conn, "uslugacomplex", "SELECT * FROM uslugacomplex WHERE (code_spr <> '') AND not (code_spr CONTAINING 'rmis') ORDER BY Name_spr ASC");
            //Info.GetSimpleTable(Info.conn, "uslugacomplex", "SELECT * FROM uslugacomplex WHERE (code_spr <> '') AND (char_length(id_spr) = 15) ORDER BY Name_spr ASC");

            // spr uslug designer
            //Info.GetSimpleTable(Info.conn, "uslugacomplex", "SELECT * FROM uslugacomplex WHERE (code_spr <> '') and (enddate is null) ORDER BY id_spr ASC");
            Info.GetSimpleTable(Info.conn, "uslugacomplex", "SELECT * FROM uslugacomplex WHERE (code_spr <> '') ORDER BY id_spr ASC");

            //Info.GetSimpleTable(Info.conn, "medtest", "SELECT * FROM medtest ORDER BY uslugacomplex_name ASC");


            // openclosetap TAP
            DataTable openclosetap = new DataTable("openclosetap");
            openclosetap.Columns.Add("id_spr", typeof(string));
            openclosetap.Columns.Add("name_spr", typeof(string));
            openclosetap.Rows.Add("1", "ДА");
            openclosetap.Rows.Add("0", "НЕТ");
            Info.ds.Tables.Add(openclosetap);

            // cito
            DataTable cito = new DataTable("cito");
            cito.Columns.Add("id_spr", typeof(string));
            cito.Columns.Add("name_spr", typeof(string));
            cito.Rows.Add("1", "ДА");
            cito.Rows.Add("0", "НЕТ");
            Info.ds.Tables.Add(cito);

            string qstring0 = "SELECT * FROM patient ORDER BY fio_fam ASC";

            string qstring2 = "SELECT ID_EVNPLBASE,ID_PATIENT_EVNPLBASE,EVNPL_NUMCARD,EVNPL_ISFINISH,EVN_DATETIME,LPUSECTION_ID,TREATMENTCLASS_ID,SERVICETYPE_ID,VIZITTYPE_ID,MEDICALCAREKIND_ID,MEDSTAFFFACT_ID,PAYTYPE_ID,DIAG_ID,USLUGACOMPLEX_ID,DIAG.NAME_SPR AS DIAG,USLUGACOMPLEX.NAME_SPR AS USLUGACOMPLEX,EVNVIZITPL_ID,EVNPLBASE_ID FROM evnplbase JOIN uslugacomplex ON evnplbase.USLUGACOMPLEX_ID=uslugacomplex.ID_SPR JOIN diag ON evnplbase.DIAG_ID=diag.ID_SPR ORDER BY evnpl_numcard ASC";

            string qstring1 = "SELECT ID_DIRECTION,ID_EVNPLBASE_DIRECTION,NUM_DIR,DATE_DIR,DIRTYPE_ID,PRESCRIPTIONTYPE_ID,DIAG.NAME_SPR AS DIAG,DIAG_ID,LPUSECTIONPROFILE_ID,USLUGACOMPLEX.NAME_SPR AS USLUGACOMPLEX,USLUGACOMPLEXMEDSERVICE_ID,LPUUNITTYPE_ID,PAYTYPE_ID,EVNPRESCR_ISCITO,ECP_ID_DIRECTION,ECP_ID_EVN,ECP_ID_EVNQUEUE,ECP_ID_EVNLABREQUEST,ECP_ID_EVNPRESCR,DIRECTION_DESCR FROM direction JOIN uslugacomplex ON direction.USLUGACOMPLEXMEDSERVICE_ID=uslugacomplex.ID_SPR JOIN diag ON direction.DIAG_ID=diag.ID_SPR ORDER BY num_dir ASC";

            Info.GetDirectionData(Info.conn, qstring0, qstring1, qstring2);

            // set primary key
            Info.ds.Tables["patient"].PrimaryKey = new DataColumn[] { Info.ds.Tables["patient"].Columns["id_patient"] };
            Info.ds.Tables["evnplbase"].PrimaryKey = new DataColumn[] { Info.ds.Tables["evnplbase"].Columns["id_evnplbase"] };
            Info.ds.Tables["direction"].PrimaryKey = new DataColumn[] { Info.ds.Tables["direction"].Columns["id_direction"] };



        }

        void view_columns_parent()
        {

            this.dgvParent.Columns[0].HeaderText = "Идентификатор пациента";
            this.dgvParent.Columns[1].HeaderText = "Фамилия";
            this.dgvParent.Columns[2].HeaderText = "Имя";
            this.dgvParent.Columns[3].HeaderText = "Отчество";
            this.dgvParent.Columns[4].HeaderText = "Пол";
            this.dgvParent.Columns[5].HeaderText = "Социальный статус";
            this.dgvParent.Columns[6].HeaderText = "Дата рождения";
            this.dgvParent.Columns[7].HeaderText = "Паспорт серия номер";
            this.dgvParent.Columns[8].HeaderText = "Паспорт кем выдан";
            this.dgvParent.Columns[9].HeaderText = "Паспорт когда выдан";
            this.dgvParent.Columns[10].HeaderText = "СНИЛС";
            this.dgvParent.Columns[11].HeaderText = "ОМС номер";
            //this.dgvParent.Columns[12].HeaderText = "ЕЦП идентификатор пациента";

        }


        void view_columns_direction()
        {
           
            this.dgvDetail.Columns[0].HeaderText = "Идентификатор напрвления";
            this.dgvDetail.Columns[1].HeaderText = "Идентификатор пациента";
            this.dgvDetail.Columns[2].HeaderText = "Номер направления";
            this.dgvDetail.Columns[3].HeaderText = "Дата направления";

            this.dgvDetail.Columns[6].HeaderText = "Диагноз";

            this.dgvDetail.Columns[9].HeaderText = "Услуга";

            this.dgvDetail.Columns[19].HeaderText = "Обоснование направления";

        }

        private void CreateRelation_tap()
        {

            DataColumn parentColumn = Info.ds.Tables["patient"].Columns[0];
            DataColumn childColumn = Info.ds.Tables["evnplbase"].Columns[1];

            DataRelation rel_tap = new DataRelation("rel_tap", parentColumn, childColumn);
            Info.ds.Relations.Add(rel_tap);

            this.parent.DataSource = Info.ds;
            this.parent.DataMember = "patient";

            this.detail_tap.DataSource = this.parent;
            this.detail_tap.DataMember = "rel_tap";

            this.dgvParent.DataSource = this.parent;
            this.dgvEvnplbase.DataSource = this.detail_tap;


        }

        private void CreateRelation_dir()
        {

            DataColumn parentColumn = Info.ds.Tables["evnplbase"].Columns[0];
            DataColumn childColumn = Info.ds.Tables["direction"].Columns[1];

            DataRelation rel_dir = new DataRelation("rel_dir", parentColumn, childColumn);
            Info.ds.Relations.Add(rel_dir);

            this.detail_dir.DataSource = this.detail_tap;
            this.detail_dir.DataMember = "rel_dir";

            this.dgvDetail.DataSource = this.detail_dir;


        }


        private void CreateRelation_a()
        {

            DataColumn parentColumn = Info.ds.Tables["patient"].Columns[0];
            DataColumn childColumn = Info.ds.Tables["direction"].Columns[1];
            DataColumn childColumn1 = Info.ds.Tables["evnplbase"].Columns[1];

            //DataRelation rel_1 = new DataRelation("rel_1", ds1.Tables[0].Columns[0], ds1.Tables[1].Columns[1]);
            DataRelation rel_1 = new DataRelation("rel_1", parentColumn, childColumn);
            Info.ds.Relations.Add(rel_1);
            DataRelation rel_2 = new DataRelation("rel_2", parentColumn, childColumn1);
            Info.ds.Relations.Add(rel_2);

            BindingSource parent = new BindingSource();
            BindingSource detail_1 = new BindingSource();
            BindingSource detail_2 = new BindingSource();

            // parent
            parent.DataSource = Info.ds;
            parent.DataMember = "patient";

            // details
            detail_1.DataSource = parent;
            detail_1.DataMember = "rel_1";
            detail_2.DataSource = parent;
            detail_2.DataMember = "rel_2";

            this.dgvParent.DataSource = parent;
            this.dgvDetail.DataSource = detail_1;
            this.dgvEvnplbase.DataSource = detail_2;



        }

        void dgvParent_prepare()
        {
            DataGridViewComboBoxColumn colbox111 = new DataGridViewComboBoxColumn();
            colbox111.Name = "Пол";
            colbox111.DataSource = Info.ds.Tables["sex"];
            colbox111.DisplayMember = "name_spr";
            colbox111.ValueMember = "id_spr";
            colbox111.DataPropertyName = "sex";
            colbox111.DisplayIndex = 5;
            colbox111.FlatStyle = FlatStyle.Flat;
            colbox111.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            this.dgvParent.Columns.Add(colbox111);

            DataGridViewComboBoxColumn colbox122 = new DataGridViewComboBoxColumn();
            colbox122.Name = "Социальный статус";
            colbox122.DataSource = Info.ds.Tables["socstatus"];
            colbox122.DisplayMember = "name_spr";
            colbox122.ValueMember = "id_spr";
            colbox122.DataPropertyName = "socstatus";
            colbox122.DisplayIndex = 6;
            colbox122.Width = 200;
            colbox122.FlatStyle = FlatStyle.Flat;
            colbox122.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            this.dgvParent.Columns.Add(colbox122);

            // id not view
            this.dgvParent.Columns["id_patient"].Visible = false;
            this.dgvParent.Columns["sex"].Visible = false;
            this.dgvParent.Columns["socstatus"].Visible = false;
            this.dgvParent.Columns["ecp_id_patient"].Visible = false;

        }

        void view_columns_evnplbase()
        {
            // 

            this.dgvEvnplbase.Columns[2].HeaderText = "Номер талона";
            this.dgvEvnplbase.Columns[3].HeaderText = "Признак закрытия";
            this.dgvEvnplbase.Columns[4].HeaderText = "Дата посещения";

            this.dgvEvnplbase.Columns[14].HeaderText = "Диагноз";
            this.dgvEvnplbase.Columns[15].HeaderText = "Услуга";


        }

        void dgvEvnplbase_prepare()
        {

            this.dgvEvnplbase.Columns["ID_EVNPLBASE"].Visible = false;
            this.dgvEvnplbase.Columns["ID_PATIENT_EVNPLBASE"].Visible = false;
            this.dgvEvnplbase.Columns["EVNVIZITPL_ID"].Visible = false;
            this.dgvEvnplbase.Columns["EVNPLBASE_ID"].Visible = false;

            this.dgvEvnplbase.Columns["PAYTYPE_ID"].Visible = false;
            this.dgvEvnplbase.Columns["LPUSECTION_ID"].Visible = false;
            this.dgvEvnplbase.Columns["TREATMENTCLASS_ID"].Visible = false;
            this.dgvEvnplbase.Columns["SERVICETYPE_ID"].Visible = false;
            this.dgvEvnplbase.Columns["VIZITTYPE_ID"].Visible = false;
            this.dgvEvnplbase.Columns["MEDICALCAREKIND_ID"].Visible = false;

            this.dgvEvnplbase.Columns["DIAG_ID"].Visible = false;
            this.dgvEvnplbase.Columns["DIAG"].Visible = true;
            this.dgvEvnplbase.Columns["USLUGACOMPLEX_ID"].Visible = false;
            this.dgvEvnplbase.Columns["USLUGACOMPLEX"].Visible = true;

            this.dgvEvnplbase.Columns["MEDSTAFFFACT_ID"].Visible = false;

            DataGridViewComboBoxColumn colbox222 = new DataGridViewComboBoxColumn();
            colbox222.Name = "Тип оплаты";
            colbox222.DataSource = Info.ds.Tables["paytype"];
            colbox222.DisplayMember = "name_spr";
            colbox222.ValueMember = "id_spr";
            colbox222.DataPropertyName = "PAYTYPE_ID";
            colbox222.DisplayIndex = 12;
            colbox222.Width = 200;
            colbox222.FlatStyle = FlatStyle.Flat;
            colbox222.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            this.dgvEvnplbase.Columns.Add(colbox222);

            /*
            DataGridViewComboBoxColumn colbox229 = new DataGridViewComboBoxColumn();
            colbox229.Name = "Код посещения";
            colbox229.DataSource = Info.ds.Tables["uslugacomplex"];
            colbox229.DisplayMember = "name_spr";
            colbox229.ValueMember = "id_spr";
            colbox229.DataPropertyName = "USLUGACOMPLEX_ID";
            colbox229.DisplayIndex = 11;
            colbox229.Width = 200;
            colbox229.FlatStyle = FlatStyle.Flat;
            colbox229.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            this.dgvEvnplbase.Columns.Add(colbox229);
            */
            /*
            DataGridViewComboBoxColumn colbox228 = new DataGridViewComboBoxColumn();
            colbox228.Name = "Диагноз";
            colbox228.DataSource = Info.ds.Tables["diag"];
            colbox228.DisplayMember = "name_spr";
            colbox228.ValueMember = "id_spr";
            colbox228.DataPropertyName = "DIAG_ID";
            colbox228.DisplayIndex = 10;
            colbox228.Width = 200;
            colbox228.FlatStyle = FlatStyle.Flat;
            colbox228.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            this.dgvEvnplbase.Columns.Add(colbox228);
            */

            DataGridViewComboBoxColumn colbox227 = new DataGridViewComboBoxColumn();
            colbox227.Name = "Вид медицинской помощи";
            colbox227.DataSource = Info.ds.Tables["medicalcarekind"];
            colbox227.DisplayMember = "name_spr";
            colbox227.ValueMember = "code_spr";
            colbox227.DataPropertyName = "MEDICALCAREKIND_ID";
            colbox227.DisplayIndex = 9;
            colbox227.Width = 200;
            colbox227.FlatStyle = FlatStyle.Flat;
            colbox227.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            this.dgvEvnplbase.Columns.Add(colbox227);

            DataGridViewComboBoxColumn colbox226 = new DataGridViewComboBoxColumn();
            colbox226.Name = "Цель посещения";
            colbox226.DataSource = Info.ds.Tables["vizittype"];
            colbox226.DisplayMember = "name_spr";
            colbox226.ValueMember = "id_spr";
            colbox226.DataPropertyName = "VIZITTYPE_ID";
            colbox226.DisplayIndex = 8;
            colbox226.Width = 200;
            colbox226.FlatStyle = FlatStyle.Flat;
            colbox226.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            this.dgvEvnplbase.Columns.Add(colbox226);

            DataGridViewComboBoxColumn colbox225 = new DataGridViewComboBoxColumn();
            colbox225.Name = "Место посещения";
            colbox225.DataSource = Info.ds.Tables["servicetype"];
            colbox225.DisplayMember = "name_spr";
            colbox225.ValueMember = "Code_spr";
            colbox225.DataPropertyName = "SERVICETYPE_ID";
            colbox225.DisplayIndex = 7;
            colbox225.Width = 200;
            colbox225.FlatStyle = FlatStyle.Flat;
            colbox225.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            this.dgvEvnplbase.Columns.Add(colbox225);

            DataGridViewComboBoxColumn colbox224 = new DataGridViewComboBoxColumn();
            colbox224.Name = "Вид обращения";
            colbox224.DataSource = Info.ds.Tables["treatmentclass"];
            colbox224.DisplayMember = "name_spr";
            colbox224.ValueMember = "id_spr";
            colbox224.DataPropertyName = "TREATMENTCLASS_ID";
            colbox224.DisplayIndex = 6;
            colbox224.Width = 200;
            colbox224.FlatStyle = FlatStyle.Flat;
            colbox224.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            this.dgvEvnplbase.Columns.Add(colbox224);

            DataGridViewComboBoxColumn colbox223 = new DataGridViewComboBoxColumn();
            colbox223.Name = "Отделение";
            colbox223.DataSource = Info.ds.Tables["lpusection"];
            colbox223.DisplayMember = "name_spr";
            colbox223.ValueMember = "id_spr";
            colbox223.DataPropertyName = "LPUSECTION_ID";
            colbox223.DisplayIndex = 5;
            colbox223.Width = 200;
            colbox223.FlatStyle = FlatStyle.Flat;
            colbox223.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            this.dgvEvnplbase.Columns.Add(colbox223);



        }

        void dgvDetail_prepare()
        {
            // detail
            /*
            DataGridViewComboBoxColumn colbox1 = new DataGridViewComboBoxColumn();
            colbox1.Name = "Фамилия пациента";
            colbox1.DataSource = parent;
            colbox1.DisplayMember = "fio_fam";
            colbox1.ValueMember = "id_patient";
            colbox1.DataPropertyName = "id_patient_direction";
            colbox1.DisplayIndex = 2;
            colbox1.Width = 200;
            colbox1.FlatStyle = FlatStyle.Flat;
            colbox1.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            this.dgvDetail.Columns.Add(colbox1);

            DataGridViewComboBoxColumn colbox2 = new DataGridViewComboBoxColumn();
            colbox2.Name = "Имя пациента";
            colbox2.DataSource = parent;
            colbox2.DisplayMember = "fio_name";
            colbox2.ValueMember = "id_patient";
            colbox2.DataPropertyName = "id_patient_direction";
            colbox2.DisplayIndex = 3;
            colbox2.FlatStyle = FlatStyle.Flat;
            colbox2.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            this.dgvDetail.Columns.Add(colbox2);

            DataGridViewComboBoxColumn colbox3 = new DataGridViewComboBoxColumn();
            colbox3.Name = "Отчество пациента";
            colbox3.DataSource = parent;
            colbox3.DisplayMember = "fio_otch";
            colbox3.ValueMember = "id_patient";
            colbox3.DataPropertyName = "id_patient_direction";
            colbox3.DisplayIndex = 4;
            colbox3.Width = 200;
            colbox3.FlatStyle = FlatStyle.Flat;
            colbox3.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            this.dgvDetail.Columns.Add(colbox3);
            */

            DataGridViewComboBoxColumn colbox0 = new DataGridViewComboBoxColumn();
            colbox0.Name = "Тип направления";
            colbox0.DataSource = Info.ds.Tables["dirtype"];
            colbox0.DisplayMember = "name_spr";
            colbox0.ValueMember = "id_spr";
            colbox0.DataPropertyName = "dirtype_id";
            colbox0.DisplayIndex = 5;
            colbox0.Width = 200;
            colbox0.FlatStyle = FlatStyle.Flat;
            colbox0.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            this.dgvDetail.Columns.Add(colbox0);

            DataGridViewComboBoxColumn colbox4 = new DataGridViewComboBoxColumn();
            colbox4.Name = "Тип назначения";
            colbox4.DataSource = Info.ds.Tables["prescriptiontype"];
            colbox4.DisplayMember = "name_spr";
            colbox4.ValueMember = "id_spr";
            colbox4.DataPropertyName = "prescriptiontype_id";
            colbox4.DisplayIndex = 6;
            colbox4.Width = 200;
            colbox4.FlatStyle = FlatStyle.Flat;
            colbox4.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            this.dgvDetail.Columns.Add(colbox4);
            /*
            DataGridViewComboBoxColumn colbox5 = new DataGridViewComboBoxColumn();
            colbox5.Name = "Диагноз";
            colbox5.DataSource = Info.ds.Tables["diag"];
            colbox5.DisplayMember = "name_spr";
            colbox5.ValueMember = "id_spr";
            colbox5.DataPropertyName = "diag_id";
            colbox5.DisplayIndex = 7;
            colbox5.Width = 500;
            colbox5.FlatStyle = FlatStyle.Flat;
            colbox5.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            this.dgvDetail.Columns.Add(colbox5);
            */
            /*
            DataGridViewComboBoxColumn colbox6 = new DataGridViewComboBoxColumn();
            colbox6.Name = "ЛПУ, куда направили";
            colbox6.DataSource = Info.ds.Tables["lpulist"];
            colbox6.DisplayMember = "org_name";
            colbox6.ValueMember = "lpu_id";
            colbox6.DataPropertyName = "lpu_did";
            colbox6.DisplayIndex = 10;
            colbox6.Width = 500;
            colbox6.FlatStyle = FlatStyle.Flat;
            colbox6.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            this.dgvDetail.Columns.Add(colbox6);
            */
            DataGridViewComboBoxColumn colbox7 = new DataGridViewComboBoxColumn();
            colbox7.Name = "Профиль, куда направили";
            colbox7.DataSource = Info.ds.Tables["lpusectionprofile"];
            colbox7.DisplayMember = "name_spr";
            colbox7.ValueMember = "id_spr";
            colbox7.DataPropertyName = "lpusectionprofile_id";
            colbox7.DisplayIndex = 8;
            colbox7.Width = 200;
            colbox7.FlatStyle = FlatStyle.Flat;
            colbox7.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            this.dgvDetail.Columns.Add(colbox7);
            /*
            DataGridViewComboBoxColumn colbox8 = new DataGridViewComboBoxColumn();
            colbox8.Name = "Исследование";
            colbox8.DataSource = Info.ds.Tables["uslugacomplex"];
            colbox8.DisplayMember = "name_spr";
            colbox8.ValueMember = "id_spr";
            colbox8.DataPropertyName = "uslugacomplexmedservice_id";
            colbox8.DisplayIndex = 9;
            colbox8.Width = 200;
            colbox8.FlatStyle = FlatStyle.Flat;
            colbox8.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            this.dgvDetail.Columns.Add(colbox8);
            */
            DataGridViewComboBoxColumn colbox9 = new DataGridViewComboBoxColumn();
            colbox9.Name = "Условия оказания медицинской помощи";
            colbox9.DataSource = Info.ds.Tables["lpuunittype"];
            colbox9.DisplayMember = "name_spr";
            colbox9.ValueMember = "id_spr";
            colbox9.DataPropertyName = "lpuunittype_id";
            colbox9.DisplayIndex = 10;
            colbox9.Width = 200;
            colbox9.FlatStyle = FlatStyle.Flat;
            colbox9.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            this.dgvDetail.Columns.Add(colbox9);

            DataGridViewComboBoxColumn colbox10 = new DataGridViewComboBoxColumn();
            colbox10.Name = "Тип оплаты";
            colbox10.DataSource = Info.ds.Tables["paytype"];
            colbox10.DisplayMember = "name_spr";
            colbox10.ValueMember = "id_spr";
            colbox10.DataPropertyName = "paytype_id";
            colbox10.DisplayIndex = 11;
            colbox10.Width = 200;
            colbox10.FlatStyle = FlatStyle.Flat;
            colbox10.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            this.dgvDetail.Columns.Add(colbox10);

            DataGridViewComboBoxColumn colbox11 = new DataGridViewComboBoxColumn();
            colbox11.Name = "Признак Cito";
            colbox11.DataSource = Info.ds.Tables["cito"];
            colbox11.DisplayMember = "name_spr";
            colbox11.ValueMember = "id_spr";
            colbox11.DataPropertyName = "evnprescr_iscito";
            colbox11.DisplayIndex = 12;
            colbox11.Width = 50;
            colbox11.FlatStyle = FlatStyle.Flat;
            colbox11.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            this.dgvDetail.Columns.Add(colbox11);


            // id not visible
            this.dgvDetail.Columns["ECP_ID_DIRECTION"].Visible = false;
            this.dgvDetail.Columns["ECP_ID_EVN"].Visible = false;
            this.dgvDetail.Columns["ECP_ID_EVNQUEUE"].Visible = false;
            this.dgvDetail.Columns["ECP_ID_EVNPRESCR"].Visible = false;
            this.dgvDetail.Columns["ECP_ID_EVNLABREQUEST"].Visible = false;

            this.dgvDetail.Columns["id_direction"].Visible = false;
            //this.dgvDetail.Columns["id_patient_direction"].Visible = false;
            this.dgvDetail.Columns["id_evnplbase_direction"].Visible = false;
            this.dgvDetail.Columns["dirtype_id"].Visible = false;
            this.dgvDetail.Columns["prescriptiontype_id"].Visible = false;
            this.dgvDetail.Columns["diag_id"].Visible = false;
            this.dgvDetail.Columns["diag"].Visible = true;
            //this.dgvDetail.Columns["lpu_did"].Visible = false;
            this.dgvDetail.Columns["lpusectionprofile_id"].Visible = false;

            this.dgvDetail.Columns["uslugacomplexmedservice_id"].Visible = false;
            this.dgvDetail.Columns["uslugacomplex"].Visible = true;

            this.dgvDetail.Columns["lpuunittype_id"].Visible = false;
            this.dgvDetail.Columns["paytype_id"].Visible = false;
            this.dgvDetail.Columns["evnprescr_iscito"].Visible = false;


            //this.dgvParent.EditMode = DataGridViewEditMode.EditOnEnter;
            this.dgvParent.EditMode = DataGridViewEditMode.EditProgrammatically;
            this.dgvDetail.EditMode = DataGridViewEditMode.EditProgrammatically;


        }


        private void button1_Click(object sender, EventArgs e)
        {
            // add new patient
            DialogResult result;

            using (FormPatientAdd fp = new FormPatientAdd())
            {
                fp.Text = "Добавление нового пациента";
                result = fp.ShowDialog();
                //if (result == DialogResult.Cancel) return;
            }

        }


        string update_ecpid_patient_to_db(string id_patient)
        {
            string ret = id_patient;
            string sSql = "UPDATE PATIENT SET ECP_ID_PATIENT='" + Info.ecp_id_patient + "' WHERE ID_PATIENT=@ID_PATIENT";

            if (this.f_log)
            {
                this.lb_log.Items.Add(sSql);
            }

            FbCommand cmd = new FbCommand(sSql, Info.conn);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ID_PATIENT", ret);

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

        string update_ecpid_direction_to_db(string iddirection)
        {
            string ret = iddirection;

            string sSql = "UPDATE DIRECTION SET " +
                          "ECP_ID_DIRECTION = '" + Info.ecp_id_direction +
                          "', ECP_ID_EVN = '" + Info.ecp_id_evn +
                          "', ECP_ID_EVNQUEUE = '" + Info.ecp_id_evnqueue +
                          "' WHERE ID_DIRECTION=@ID_DIRECTION";

            if (this.f_log) this.lb_log.Items.Add(sSql);

            FbCommand cmd = new FbCommand(sSql, Info.conn);

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ID_DIRECTION", iddirection);

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


        private void button3_Click(object sender, EventArgs e)
        {
            // add direction
            DialogResult result;

            if (this.dgvParent.Rows.Count == 0)
            {
                MessageBox.Show("В списке пациентов нет записей");
                return;
            }
            if (this.dgvParent.CurrentRow == null)
            {
                MessageBox.Show("Вы не выбрали пациента - выберите запись пациента");
                return;
            }

            string fio_fam = this.dgvParent["fio_fam", this.dgvParent.CurrentCell.RowIndex].Value.ToString();
            string fio_name = this.dgvParent["fio_name", this.dgvParent.CurrentCell.RowIndex].Value.ToString();
            string fio_otch = this.dgvParent["fio_otch", this.dgvParent.CurrentCell.RowIndex].Value.ToString();
            result = MessageBox.Show("Вы хотите добавить направление пациенту " + fio_fam + " " + fio_name + " " + fio_otch, "Добавить направление", MessageBoxButtons.YesNo);
            if (result == DialogResult.No) return;

            string id_patient = this.dgvParent[0, this.dgvParent.CurrentCell.RowIndex].Value.ToString();

            DateTime dEVN_DATETIME;
            DateTime dNow;
            if (this.dgvEvnplbase.Rows.Count != 0)
            {
                this.dgvEvnplbase.CurrentCell = this.dgvEvnplbase.Rows[0].Cells[4];
                //EVN_DATETIME
                dEVN_DATETIME = Convert.ToDateTime(this.dgvEvnplbase["EVN_DATETIME", this.dgvEvnplbase.CurrentCell.RowIndex].Value);
                dNow = DateTime.Now;
            }
            else
                dEVN_DATETIME = DateTime.Now;

            //if (dEVN_DATETIME.ToString("dd.MM.yyyy") != dNow.ToString("dd.MM.yyyy"))
            //{
            //    MessageBox.Show("Вы не можете ввести направление по посещению не текущей даты");
            //    return;
            //}                

            bool IsTap = false;
            if (this.detail_tap.Count != 0) IsTap = true; // посещение уже есть             

            using (FormDirectionAdd f = new FormDirectionAdd(id_patient, IsTap, dEVN_DATETIME))
            {
                f.Text = "Добавление направления пациенту " + fio_fam + " " + fio_name + " " + fio_otch;
                result = f.ShowDialog();
                //if (result == DialogResult.Cancel) return;
            }

        }

        private string get_sess_id()
        {
            // ecp login

            string ret = "";

            return ret;

        }

        private int deserialize_LoginEcp(string rawresponse)
        {
            int ret = 0;
            try {
                this.loginEcp = JsonConvert.DeserializeObject<LoginEcp>(rawresponse);
            }
            catch (Exception ex) {
                MessageBox.Show("deserialize_LoginEcp: Исключение JsonConvert " + ex.Message + ", rawresponse = " + rawresponse);
                ret = 1;
            }
            return ret;
        }

        public async Task<string> get_loginecpAsync()
        {
            string ret = "";
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(Info.strloginecp);
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    //this.tb_ecp.Text = content;
                    ret = content;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("get_loginecpAsync : Exception : " + ex.Message);
                }
            }
            return ret;
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            // make men in ecp
            //MessageBox.Show(this.dgvParent[1, Info.f_row].Value.ToString() + " "+Convert.ToDateTime(this.dgvParent[6, Info.f_row].Value).ToString("dd.MM.yyyy"));
            //MessageBox.Show("Пол "+this.dgvParent[4, Info.f_row].Value.ToString());
            //MessageBox.Show("Соц. статус " + this.dgvParent[5, Info.f_row].Value.ToString());

            Info.f_row = this.dgvParent.CurrentCell.RowIndex;
            if (Info.f_row == -1)
            {
                MessageBox.Show("Вы не выбрали человека");
                return;
            }

            this.button4.Enabled = false;
            this.toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
            this.toolStripLabel1.Text = "Добавляю человека в ЕЦП";

            Info.f_row = this.dgvParent.CurrentCell.RowIndex;
            //Info.ecp_id_patient = this.dgvParent[12, Info.f_row].Value.ToString();
            Info.id_patient = this.dgvParent[0, Info.f_row].Value.ToString();

            if (this.f_log)
            {
                this.lb_log.Items.Clear();
                this.lb_log.Items.Add(DateTime.Now.ToString("dd.MM.yyyy:HH.mm.ss"));
            }

            // login ecp
            Task<string> t1 = this.get_loginecpAsync();
            Info.raw_login_ecp = await t1;
            try
            {
                this.loginEcp = JsonConvert.DeserializeObject<LoginEcp>(Info.raw_login_ecp);
            }
            catch (Exception ex)
            {
                MessageBox.Show("raw_login_ecp : deserialize : Исключение JsonConvert : Возможно что нет подключения к БД : " + ex.Message);
                this.toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
                this.toolStripLabel1.Text = "";
                this.button4.Enabled = true;
                return;
            }

            if (this.loginEcp != null)
                Info.sess_id = this.loginEcp.sess_id.ToString();
            else
            {
                this.toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
                this.toolStripLabel1.Text = "";
                this.button4.Enabled = true;
                return;
            }


            // get men
            Task<string> t2 = this.is_menEcpAsync();
            Info.raw_ismen_ecp = await t2;
            try
            {
                this.rootPersonObject = JsonConvert.DeserializeObject<RootPersonObject>(Info.raw_ismen_ecp);
            }
            catch (Exception ex)
            {
                MessageBox.Show("raw_ismen_ecp : deserialize : Исключение JsonConvert " + ex.Message);
                this.toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
                this.toolStripLabel1.Text = "";
                this.button4.Enabled = true;
                return;
            }


            if (this.rootPersonObject.data.Length != 0)
            {
                MessageBox.Show(this.dgvParent[1, Info.f_row].Value.ToString() + " " + this.dgvParent[2, Info.f_row].Value.ToString() + " " + this.dgvParent[3, Info.f_row].Value.ToString() + " " + Convert.ToDateTime(this.dgvParent[6, Info.f_row].Value).ToString("dd.MM.yyyy") + " найден в ЕЦП - можно обновить идентификатор ЕЦП");

                // is update id ecp
                Info.ecp_id_patient = this.rootPersonObject.data[0].Person_id;
                //MessageBox.Show("id = " + Info.ecp_id_patient);
                DialogResult result = MessageBox.Show("Вы хотите обновить идентификатор ЕЦП?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    this.savePatientIdEcpToDb(Info.id_patient);
                    if (this.f_log)
                    {
                        this.lb_log.Items.Add("getmen - найден - " + this.rootPersonObject.data.Length.ToString() + " - обновить идентификатор ЕЦП");
                        this.lb_log.Items.Add(JsonConvert.SerializeObject(this.rootPersonObject));
                    }
                }
                else if (result == DialogResult.No)
                {
                    if (this.f_log)
                    {
                        this.lb_log.Items.Add("getmen - найден - " + this.rootPersonObject.data.Length.ToString() + " - не обновлять идентификатор ЕЦП");
                        this.lb_log.Items.Add(JsonConvert.SerializeObject(this.rootPersonObject));
                    }
                }

                // save log
                if (f_log)
                {
                    this.pathLog = Info.path_app + "log-patient.txt";
                    using (var sw = new StreamWriter(this.pathLog, true))
                        foreach (var item in lb_log.Items) sw.Write(item.ToString() + Environment.NewLine);
                }
                return;
            }
            else
            {
                MessageBox.Show(this.dgvParent[1, Info.f_row].Value.ToString() + " " + this.dgvParent[2, Info.f_row].Value.ToString() + " " + this.dgvParent[3, Info.f_row].Value.ToString() + " " + Convert.ToDateTime(this.dgvParent[6, Info.f_row].Value).ToString("dd.MM.yyyy") + " не найден в ЕЦП - будет создан");
                if (this.f_log)
                {
                    this.lb_log.Items.Add("getmen - не найден");
                    this.lb_log.Items.Add(JsonConvert.SerializeObject(this.rootPersonObject));
                }
            }


            // create men
            Task<string> t3 = this.create_menEcpAsync();
            Info.raw_men_ecp = await t3;
            try
            {
                this.rootPersonObject = JsonConvert.DeserializeObject<RootPersonObject>(Info.raw_men_ecp);
            }
            catch (Exception ex)
            {
                MessageBox.Show("raw_men_ecp : deserialize : Исключение JsonConvert " + ex.Message);
                this.toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
                this.toolStripLabel1.Text = "";
                this.button4.Enabled = true;
                return;
            }

            if (this.rootPersonObject.error_code == 0)
            {
                Info.ecp_id_patient = this.rootPersonObject.data[0].Person_id;
                this.savePatientIdEcpToDb(Info.id_patient);
                if (this.f_log)
                {
                    this.lb_log.Items.Add("createmen");
                    this.lb_log.Items.Add(JsonConvert.SerializeObject(this.rootPersonObject));
                }
            }
            else
            {
                if (this.f_log)
                {
                    this.lb_log.Items.Add("createmen - error");
                    this.lb_log.Items.Add(JsonConvert.SerializeObject(this.rootPersonObject));
                }
                MessageBox.Show("Ошибка добавления человека - " + this.rootPersonObject.error_msg);
            }


            // save log
            if (f_log)
            {
                this.pathLog = Info.path_app + "log-patient.txt";
                using (var sw = new StreamWriter(this.pathLog, true))
                    foreach (var item in lb_log.Items) sw.Write(item.ToString() + Environment.NewLine);
            }

            this.toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
            this.toolStripLabel1.Text = "";
            this.button4.Enabled = true;



        }

        private string saveDirectionIdEcpToDb(string cur_direction)
        {
            // save to db
            string ret = "";

            bool is_connect = Info.is_connect_db();
            if (!is_connect) return ret;

            // return id_direction
            ret = this.update_ecpid_direction_to_db(cur_direction);
            if (ret == "")
            {
                MessageBox.Show("Направление не обновлено ...");
                Info.conn.Close();
                return ret;
            }

            Info.conn.Close();
            return ret;


        }



        private void savePatientIdEcpToDb(string cur_idpatient)
        {
            // save to db
            Info.f_row = this.dgvParent.CurrentCell.RowIndex;
            Info.id_patient = this.dgvParent[0, Info.f_row].Value.ToString();

            bool is_connect = Info.is_connect_db();
            if (!is_connect) return;

            string id_patient = "";
            id_patient = this.update_ecpid_patient_to_db(cur_idpatient);
            if (id_patient == "")
            {
                MessageBox.Show("Пациент не обновлен ...");
                Info.conn.Close();
                return;
            }

            Info.conn.Close();
            this.dgvParent[12, Info.f_row].Value = Info.ecp_id_patient;
            MessageBox.Show("Данные благополучно обновлены");

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public string personget(string PersonSurName, string PersonBirthDay, string sessid)
        {
			// ==========================
            string url = "";
            string url_full = url + "?" + "sess_id=" + sessid +
                             "&PersonSurName_SurName=" + PersonSurName +
                             "&PersonBirthDay_BirthDay=" + PersonBirthDay;
            return url_full;
        }

        private async Task<string> is_menEcpAsync()
        {
            string ret = "";
            string surl = this.personget(this.dgvParent[1, Info.f_row].Value.ToString(),
                                       Convert.ToDateTime(this.dgvParent[6, Info.f_row].Value).ToString("dd.MM.yyyy"),
                                       Info.sess_id);

            if (this.f_log) this.lb_log.Items.Add(surl);

            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(surl);
                    response.EnsureSuccessStatusCode();
                    //var content = await response.Content.ReadAsStringAsync();
                    ret = await response.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("2 getmen : Exception : " + ex.Message);
                }
            }
            return ret;
        }

        public string direction_create(string personid,
                                      string sessid,
                                      string numdir,
                                      string datedir,
                                      string dirtypeid,
                                      string prescriptiontypeid,
                                      string diagid,
                                      string lpusid,
                                      string medstafffactid,
                                      string lpudid,
                                      string uslugacomplexmedserviceid,
                                      string lpusectionprofileid,
                                      string lpuunittypeid,
                                      string paytypeid,
                                      string evnprescriscito,
                                      string evnpid)


        {
            string url = "";
            string url_full = url + "?Person_id=" + personid +
                             "&sess_id=" + sessid +
                             "&EvnDirection_Num=" + numdir +
                             "&EvnDirection_setDate=" + datedir +
                             "&DirType_id=" + dirtypeid +
                             "&PrescriptionType_id=" + prescriptiontypeid +
                             "&Diag_id=" + diagid +
                             "&Lpu_sid=" + lpusid +
                             "&MedStaffFact_id=" + medstafffactid +
                             "&Lpu_did=" + lpudid +
                             "&UslugaComplexMedService_ResId=" + uslugacomplexmedserviceid +
                             "&LpuSectionProfile_id=" + lpusectionprofileid +
                             "&LpuUnitType_id=" + lpuunittypeid +
                             "&PayType_id=" + paytypeid +
                             "&EvnPrescr_IsCito=" + evnprescriscito +
                             "&Evn_pid=" + evnpid;
            return url_full;
        }

        private async Task<string> create_directionEcpAsync(string id_direction, string id_evnplbase, string id_patient)
        {
            string ret = "";
            string surl = "";


            DataRow[] resultd = Info.ds.Tables["direction"].Select("id_direction = '" + id_direction + "'");
            DataRow[] resulte = Info.ds.Tables["evnplbase"].Select("id_evnplbase = '" + id_evnplbase + "'");
            DataRow[] resultp = Info.ds.Tables["patient"].Select("id_patient = '" + id_patient + "'");

            surl = this.direction_create(resultp[0]["ECP_ID_PATIENT"].ToString(),
                                            Info.sess_id,
                                            resultd[0]["NUM_DIR"].ToString(),
                                            Convert.ToDateTime(resultd[0]["DATE_DIR"]).ToString("dd.MM.yyyy"),
                                            resultd[0]["DIRTYPE_ID"].ToString(),
                                            resultd[0]["PRESCRIPTIONTYPE_ID"].ToString(),
                                            resultd[0]["DIAG_ID"].ToString(),
                                            Info.Lpu_sid,//lpusid
                                            Info.MedStaffFact_sid,//medstafffactid
                                            Info.Lpu_sid, // lpudid
                                            resultd[0]["USLUGACOMPLEXMEDSERVICE_ID"].ToString(),
                                            resultd[0]["LPUSECTIONPROFILE_ID"].ToString(),
                                            resultd[0]["LPUUNITTYPE_ID"].ToString(),
                                            resultd[0]["PAYTYPE_ID"].ToString(),
                                            resultd[0]["EVNPRESCR_ISCITO"].ToString(),
                                            resulte[0]["EVNPLBASE_ID"].ToString());


            if (this.f_log) this.lb_log.Items.Add(surl);

            using (var client = new HttpClient())
            {
                try
                {
                    HttpClientHandler handler = new HttpClientHandler();
                    HttpClient httpClient = new HttpClient(handler);
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, surl);
                    HttpResponseMessage response = await httpClient.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    //var content = await response.Content.ReadAsStringAsync();
                    ret = await response.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("direction_create : Exception : " + ex.Message);
                }
            }

            return ret;
        }


        public string person_create(string PersonSurName, string PersonBirthDay, string idsex, string idsocstat, string sessid)
        {
            string url = "";
            string url_full = url + "?sess_id=" + sessid +
                             "&PersonSurName_SurName=" + PersonSurName +
                             "&PersonBirthDay_BirthDay=" + PersonBirthDay +
                             "&SocStatus_id=" + idsocstat +
                             "&Person_Sex_id=" + idsex;
            return url_full;
        }

        private async Task<string> create_menEcpAsync()
        {
            string ret = "";
            string surl = this.person_create(this.dgvParent[1, Info.f_row].Value.ToString(),
                                            Convert.ToDateTime(this.dgvParent[6, Info.f_row].Value).ToString("dd.MM.yyyy"),
                                            this.dgvParent[4, Info.f_row].Value.ToString(),
                                            this.dgvParent[5, Info.f_row].Value.ToString(),
                                            Info.sess_id);

            if (this.f_log) this.lb_log.Items.Add(surl);

            using (var client = new HttpClient())
            {
                try
                {
                    HttpClientHandler handler = new HttpClientHandler();
                    HttpClient httpClient = new HttpClient(handler);
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, surl);
                    HttpResponseMessage response = await httpClient.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    //var content = await response.Content.ReadAsStringAsync();
                    ret = await response.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("person_create : Exception : " + ex.Message);
                }
            }

            return ret;

        }


        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {

            // save settings
            //Info.dt_settings.Rows[0]["MAX_NUM_DIR"] = Info.max_num_dir;
            //Info.dt_settings.Rows[0]["MAX_NUM_TAP"] = Info.max_num_tap;
            //Info.dt_settings.WriteXml("settings.xml");

            if (Info.conn.State == ConnectionState.Open) Info.conn.Close();
            FormMenu f = new FormMenu();
            this.Hide();
            f.Show();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            // edit napr
            DialogResult result;

            if (this.dgvParent.Rows.Count == 0)
            {
                MessageBox.Show("В списке пациентов нет записей");
                return;
            }
            if (this.dgvParent.CurrentRow == null)
            {
                MessageBox.Show("Вы не выбрали пациента - выберите запись пациента");
                return;
            }
            if (this.detail_tap.Count == 0)
            {
                MessageBox.Show("Запись по посещению у пациента не добавлена - направление должно быть тоже");
                return;
            }
            //if (this.dgvEvnplbase[17, this.dgvEvnplbase.CurrentCell.RowIndex].Value.ToString() != "")
            //{
            //    MessageBox.Show("Вы не можете редактировать - посещение уже было добавлено в ЕЦП");
            //    return;
            //}
            if (this.detail_dir.Count == 0)
            {
                MessageBox.Show("Запись по направлению у пациента не добавлена");
                return;
            }
            if (this.dgvDetail.CurrentCell == null)
            {
                MessageBox.Show("Вы не выбрали направление");
                return;
            }
            if (this.dgvDetail[15, this.dgvDetail.CurrentCell.RowIndex].Value.ToString() != "")
            {
                MessageBox.Show("Вы не можете редактировать - запись была добавлена в ЕЦП");
                return;
            }

            string fio_fam = this.dgvParent["fio_fam", this.dgvParent.CurrentCell.RowIndex].Value.ToString();
            string fio_name = this.dgvParent["fio_name", this.dgvParent.CurrentCell.RowIndex].Value.ToString();
            string fio_otch = this.dgvParent["fio_otch", this.dgvParent.CurrentCell.RowIndex].Value.ToString();

            result = MessageBox.Show("Вы хотите редактировать данные направления пациента " + fio_fam + " " + fio_name + " " + fio_otch, "Редактирование данных направления", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                //do something
                //return;
            }
            else if (result == DialogResult.No) return;

            //string id_patient = this.dgvParent[0, this.dgvParent.CurrentCell.RowIndex].Value.ToString();
            //string id_evnplbase = this.dgvEvnplbase[0, this.dgvEvnplbase.CurrentCell.RowIndex].Value.ToString();
            string id_direction = this.dgvDetail[0, this.dgvDetail.CurrentCell.RowIndex].Value.ToString();

            using (FormDirectionEdit fd = new FormDirectionEdit(id_direction))
            {
                fd.Text = "Редактирование данных направления пациента - " + fio_fam + " " + fio_name + " " + fio_otch;
                result = fd.ShowDialog();
                //if (result == DialogResult.Cancel) return;

            }


        }

        void writeLogLoginEcp(StreamWriter sw)
        {
            sw.Write("error_code=" + this.loginEcp.error_code.ToString() + " sess_id=" + this.loginEcp.sess_id.ToString());
            sw.Write(sw.NewLine);
        }


        private void button8_Click(object sender, EventArgs e)
        {
            // create talon - TAP + посещение
            DialogResult result;

            if (this.dgvParent.Rows.Count == 0)
            {
                MessageBox.Show("В списке пациентов нет записей");
                return;
            }
            if (this.dgvParent.Columns[this.dgvParent.CurrentCell.ColumnIndex].HeaderText != "Фамилия")
            {
                MessageBox.Show("Вы не выбрали пациента - выберите Фамилию пациента");
                return;
            }

            string fio_fam = this.dgvParent["fio_fam", this.dgvParent.CurrentCell.RowIndex].Value.ToString();
            string fio_name = this.dgvParent["fio_name", this.dgvParent.CurrentCell.RowIndex].Value.ToString();
            string fio_otch = this.dgvParent["fio_otch", this.dgvParent.CurrentCell.RowIndex].Value.ToString();
            result = MessageBox.Show("Вы хотите добавить посещение пациенту " + fio_fam + " " + fio_name + " " + fio_otch, "Добавление посещения", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                //do something
                //return;
            }
            else if (result == DialogResult.No) return;

            // идентификатор пациента
            string id_patient = this.dgvParent[0, this.dgvParent.CurrentCell.RowIndex].Value.ToString();

            FormTAP ftap = new FormTAP(id_patient)
            {
                Text = "ПОСЕЩЕНИЕ - " + fio_fam + " " + fio_name + " " + fio_otch
            };
            ftap.ShowDialog();


        }

        private void button9_Click(object sender, EventArgs e)
        {
            //редактировать данные талона
            if (this.dgvParent.Rows.Count == 0)
            {
                MessageBox.Show("В списке пациентов нет записей");
                return;
            }
            if (this.dgvEvnplbase.Rows.Count == 0)
            {
                MessageBox.Show("В списке посещений нет записей");
                return;
            }
            //MessageBox.Show(this.dgvEvnplbase[17, this.dgvEvnplbase.CurrentCell.RowIndex].Value.ToString());
            if (this.dgvEvnplbase[17, this.dgvEvnplbase.CurrentCell.RowIndex].Value.ToString() != "")
            {
                MessageBox.Show("Вы не можете редактировать - запись была добавлена в ЕЦП");
                return;
            }
            Info.f_row = this.dgvParent.CurrentCell.RowIndex;
            Info.e_row = this.dgvEvnplbase.CurrentCell.RowIndex;
            if (Info.e_row == -1)
            {
                MessageBox.Show("Вы не выбрали посещение");
                return;
            }

            DialogResult result;
            string fio_fam = this.dgvParent["fio_fam", this.dgvParent.CurrentCell.RowIndex].Value.ToString();
            string fio_name = this.dgvParent["fio_name", this.dgvParent.CurrentCell.RowIndex].Value.ToString();
            string fio_otch = this.dgvParent["fio_otch", this.dgvParent.CurrentCell.RowIndex].Value.ToString();
            result = MessageBox.Show("Вы хотите редактировать посещение пациента " + fio_fam + " " + fio_name + " " + fio_otch, "Редактирование данных посещения", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                //do something
                //return;
            }
            else if (result == DialogResult.No) return;

            string id_evnplbase = this.dgvEvnplbase[0, Info.e_row].Value.ToString();
            //Info.id_patient = this.dgvEvnplbase[1, Info.e_row].Value.ToString();

            using (FormTAPedit ft = new FormTAPedit(id_evnplbase))
            {
                ft.Text = "Редактирование данных посещения пациента - " + fio_fam + " " + fio_name + " " + fio_otch;
                result = ft.ShowDialog();
                //if (result == DialogResult.Cancel) return;
            }


        }


        private void button10_Click(object sender, EventArgs e)
        {
            Form2rel f2rel = new Form2rel();
            f2rel.Show();


        }

        public string evnplbase_create(string person_id_ecp,
                       string sess_id,
                       string EvnPL_NumCard,
                       string EvnPL_IsFinish,
                       string Evn_setDT,
                       string LpuSection_id,
                       string TreatmentClass_id,
                       string MedStaffFact_id,
                       string ServiceType_id,
                       string VizitType_id,
                       string MedicalCareKind_id,
                       string PayType_id,
                       string Diag_id)
        {
            string url = "";
            string url_full = url + "?Person_id=" + person_id_ecp +
                             "&sess_id=" + sess_id +
                             "&EvnPL_NumCard=" + EvnPL_NumCard +
                             "&EvnPL_IsFinish=" + EvnPL_IsFinish +
                             "&Evn_setDT=" + Evn_setDT +
                             "&LpuSection_id=" + LpuSection_id +
                             "&TreatmentClass_id=" + TreatmentClass_id +
                             "&MedStaffFact_id=" + MedStaffFact_id +
                             "&ServiceType_id=" + ServiceType_id +
                             "&VizitType_id=" + VizitType_id +
                             "&MedicalCareKind_id=" + MedicalCareKind_id +
                             "&PayType_id=" + PayType_id +
                             "&Diag_id=" + Diag_id;
            return url_full;
        }

        public string evnplbase_createU(string person_id_ecp,
                               string sess_id,
                               string EvnPL_NumCard,
                               string EvnPL_IsFinish,
                               string Evn_setDT,
                               string LpuSection_id,
                               string TreatmentClass_id,
                               string MedStaffFact_id,
                               string ServiceType_id,
                               string VizitType_id,
                               string MedicalCareKind_id,
                               string PayType_id,
                               string Diag_id,
                               string USLUGACOMPLEX_ID)
        {
            string url = "";
            string url_full = url + "?Person_id=" + person_id_ecp +
                             "&sess_id=" + sess_id +
                             "&EvnPL_NumCard=" + EvnPL_NumCard +
                             "&EvnPL_IsFinish=" + EvnPL_IsFinish +
                             "&Evn_setDT=" + Evn_setDT +
                             "&LpuSection_id=" + LpuSection_id +
                             "&TreatmentClass_id=" + TreatmentClass_id +
                             "&MedStaffFact_id=" + MedStaffFact_id +
                             "&ServiceType_id=" + ServiceType_id +
                             "&VizitType_id=" + VizitType_id +
                             "&MedicalCareKind_id=" + MedicalCareKind_id +
                             "&PayType_id=" + PayType_id +
                             "&Diag_id=" + Diag_id +
                             "&UslugaComplex_uid=" + USLUGACOMPLEX_ID;
            return url_full;
        }

        private async Task<string> create_evnplbaseEcpAsync(string id_evnplbase)
        {
            string ret = "";
            string surl = "";

            DataRow[] result = Info.ds.Tables["evnplbase"].Select("id_evnplbase = '" + id_evnplbase + "'");
            if (result[0]["PAYTYPE_ID"].ToString() == "520101000000009") // OMS
            {
                surl = this.evnplbase_createU(Info.ecp_id_patient,
                     Info.sess_id,
                     result[0]["EVNPL_NUMCARD"].ToString(),
                     result[0]["EVNPL_ISFINISH"].ToString(),
                     Convert.ToDateTime(result[0]["EVN_DATETIME"]).ToString("dd.MM.yyyy HH:mm:ss"),
                     result[0]["LPUSECTION_ID"].ToString(),
                     result[0]["TREATMENTCLASS_ID"].ToString(),
                     Info.MedStaffFact_sid,
                     result[0]["SERVICETYPE_ID"].ToString(),
                     result[0]["VIZITTYPE_ID"].ToString(),
                     result[0]["MEDICALCAREKIND_ID"].ToString(),
                     result[0]["PAYTYPE_ID"].ToString(),
                     result[0]["DIAG_ID"].ToString(),
                     result[0]["USLUGACOMPLEX_ID"].ToString());
            }
            else
            {
                surl = this.evnplbase_create(Info.ecp_id_patient,
                     Info.sess_id,
                     result[0]["EVNPL_NUMCARD"].ToString(),
                     result[0]["EVNPL_ISFINISH"].ToString(),
                     Convert.ToDateTime(result[0]["EVN_DATETIME"]).ToString("dd.MM.yyyy HH:mm:ss"),
                     result[0]["LPUSECTION_ID"].ToString(),
                     result[0]["TREATMENTCLASS_ID"].ToString(),
                     Info.MedStaffFact_sid,
                     result[0]["SERVICETYPE_ID"].ToString(),
                     result[0]["VIZITTYPE_ID"].ToString(),
                     result[0]["MEDICALCAREKIND_ID"].ToString(),
                     result[0]["PAYTYPE_ID"].ToString(),
                     result[0]["DIAG_ID"].ToString());
            }
            if (this.f_log) this.lb_log.Items.Add(surl);
            using (var client = new HttpClient())
            {
                try
                {
                    HttpClientHandler handler = new HttpClientHandler();
                    HttpClient httpClient = new HttpClient(handler);
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, surl);
                    HttpResponseMessage response = await httpClient.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    //var content = await response.Content.ReadAsStringAsync();
                    ret = await response.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("evnplbase_create : Exception : " + ex.Message);
                }
            }
            return ret;
        }

        string update_ecpid_evnplbase_to_db(string idevnplbase)
        {
            string ret = idevnplbase;

            string sSql = "UPDATE EVNPLBASE SET " +
                          "EvnVizitPL_id = '" + Info.evnvizitpl_id +
                          "', EvnPLBase_id = '" + Info.evnplbase_id +
                          "' WHERE ID_EVNPLBASE='" + idevnplbase + "'";

            if (this.f_log) this.lb_log.Items.Add(sSql);

            FbCommand cmd = new FbCommand(sSql, Info.conn);

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

        private string saveEvnplbaseIdEcpToDb(string cur_id)
        {
            // save to db
            string ret = "";

            bool is_connect = Info.is_connect_db();
            if (!is_connect) return ret;

            ret = this.update_ecpid_evnplbase_to_db(cur_id);
            if (ret == "")
            {
                MessageBox.Show("Данные посещения не обновлены ...");
                Info.conn.Close();
                return ret;
            }

            Info.conn.Close();
            return ret;

        }

        private async void button11_Click(object sender, EventArgs e)
        {
            // посещение в ЕЦП           
            if (this.dgvEvnplbase.CurrentCell == null)
            {
                //MessageBox.Show("Вы не выбрали направление");
                MessageBox.Show("У пациента нет посещений для создания в ЕЦП");
                return;
            }

            // begin
            this.button11.Enabled = false;
            this.toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
            this.toolStripLabel1.Text = "Создаю посещение";

            Info.f_row = this.dgvParent.CurrentCell.RowIndex;
            Info.ecp_id_patient = this.dgvParent[12, Info.f_row].Value.ToString();
            Info.e_row = this.dgvEvnplbase.CurrentCell.RowIndex;
            Info.id_evnplbase = this.dgvEvnplbase[0, Info.e_row].Value.ToString();

            //string strLog = Environment.CurrentDirectory + "\\log.txt";
            //StreamWriter sw = new StreamWriter(strLog, false);

            if (this.f_log)
            {
                this.lb_log.Items.Clear();
                this.lb_log.Items.Add(DateTime.Now.ToString("dd.MM.yyyy:HH.mm.ss"));
            }

            // login ecp
            Task<string> t1 = this.get_loginecpAsync();
            Info.raw_login_ecp = await t1;

            try
            {
                this.loginEcp = JsonConvert.DeserializeObject<LoginEcp>(Info.raw_login_ecp);
            }
            catch (Exception ex)
            {
                MessageBox.Show("raw_login_ecp : deserialize : Исключение JsonConvert " + ex.Message);
                this.button11.Enabled = true;
                this.toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
                this.toolStripLabel1.Text = "";
                return;
            }

            if (this.loginEcp != null)
                Info.sess_id = this.loginEcp.sess_id.ToString();
            else
            {
                this.toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
                this.toolStripLabel1.Text = "";
                this.button11.Enabled = true;
                return;
            }

            Task<string> t2 = this.create_evnplbaseEcpAsync(Info.id_evnplbase);
            Info.raw_evnplbase_ecp = await t2;

            try
            {
                this.rootEvnplbaseObject = JsonConvert.DeserializeObject<RootEvnplbaseObject>(Info.raw_evnplbase_ecp);
            }
            catch (Exception ex)
            {
                MessageBox.Show("raw_evnplbase_ecp : deserialize : Исключение JsonConvert " + ex.Message);
                this.toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
                this.toolStripLabel1.Text = "";
                this.button11.Enabled = true;
                return;
            }

            string id_evnplb = "";
            if (this.rootEvnplbaseObject.error_code == 0)
            {

                Info.evnvizitpl_id = this.rootEvnplbaseObject.data.EvnVizitPL_id;  // идентификатор посещения
                Info.evnplbase_id = this.rootEvnplbaseObject.data.EvnPLBase_id;    // идентификатор события

                id_evnplb = this.saveEvnplbaseIdEcpToDb(Info.id_evnplbase);
                //this.dgvEvnplbase[16, Info.e_row].Value = Info.EvnVizitPL_id;
                //this.dgvEvnplbase[17, Info.e_row].Value = Info.EvnPLBase_id;
                if (id_evnplb != "")
                {
                    this.dgvEvnplbase["EVNVIZITPL_ID", Info.e_row].Value = Info.evnvizitpl_id;
                    this.dgvEvnplbase["EVNPLBASE_ID", Info.e_row].Value = Info.evnplbase_id;
                    MessageBox.Show("Данные полученных идентификаторов по посещению 1 благополучно обновлены");

                }

                if (this.f_log)
                {
                    this.lb_log.Items.Add("create_evnplbase");
                    this.lb_log.Items.Add(JsonConvert.SerializeObject(this.rootEvnplbaseObject));
                }

            }
            else
            {
                MessageBox.Show("Ошибка добавления посещения - " + this.rootEvnplbaseObject.error_msg);
                if (this.f_log)
                {
                    this.lb_log.Items.Add("create_evnplbase - error");
                    this.lb_log.Items.Add(JsonConvert.SerializeObject(this.rootEvnplbaseObject));
                }
            }

            // end
            this.button11.Enabled = true;
            this.toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
            this.toolStripLabel1.Text = "";

            // save log
            if (f_log)
            {
                this.pathLog = Info.path_app + "log-tap.txt";
                using (var sw = new StreamWriter(this.pathLog, true))
                    foreach (var item in lb_log.Items) sw.Write(item.ToString() + Environment.NewLine);
            }



        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            // designer spr diag - direction
            FormBuildDiagSpr fbsd = new FormBuildDiagSpr(Info.ds.Tables["diag"]);
            fbsd.ShowDialog();

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            // designer spr uslug - direction
            FormBuildUslugaSpr fbsu = new FormBuildUslugaSpr(Info.ds.Tables["uslugacomplex"]);
            fbsu.ShowDialog();

        }


        private void button12_Click(object sender, EventArgs e)
        {
            //this.studentBindingSource.Filter = "adres='" + textBox2.Text + "'";

            //this.parent.Filter = "fio_fam='" + this.textBox1.Text + "'";

            this.parent.Filter = "fio_fam like '" + this.textBox1.Text + "%'";



        }

        private void button13_Click(object sender, EventArgs e)
        {
            this.parent.Filter = "";

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            int i = this.parent.Find("fio_fam", this.textBox1.Text);
            if (i == -1)
            {
                DataView dv = new DataView(Info.ds.Tables["PATIENT"] as DataTable);
                dv.RowFilter = string.Format("fio_fam LIKE '{0}*'", this.textBox1.Text);
                if (dv.Count != 0)
                {
                    i = this.parent.Find("fio_fam", dv[0]["fio_fam"]);
                }
                dv.Dispose();
                this.parent.Position = i;
            }
            this.parent.Position = i;


        }

        public async Task<string> DirectionResultatEcpAsync()
        {
            string ret = "";

            string id_direction = this.dgvDetail[0, this.dgvDetail.CurrentCell.RowIndex].Value.ToString();
            DataRow[] resultd = Info.ds.Tables["direction"].Select("id_direction = '" + id_direction + "'");
            string usluga_id = resultd[0]["USLUGACOMPLEXMEDSERVICE_ID"].ToString();
            string s = "" + "sess_id=" + Info.sess_id + "&UslugaTest_id=" + usluga_id;

            if (this.f_log)
                this.lb_log.Items.Add(s);

            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(s);
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    ret = content;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("DirectionResultatEcpAsync : Exception : " + ex.Message);
                }
            }
            return ret;
        }

        public async Task<string> LoginEcpAsync()
        {
            string ret = "";

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
                }
            }
            return ret;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            // direction resultat
            if (this.detail_dir.Count == 0)
            {
                MessageBox.Show("Запись по направлению у пациента не добавлена");
                return;
            }
            if (this.dgvDetail.CurrentCell == null)
            {
                MessageBox.Show("Вы не выбрали направление");
                return;
            }
            
            //FormDirectionResultat fr = new FormDirectionResultat("");
            //fr.ShowDialog();
            //return;
            // =================================================================================== test direction resultat

            this.button1.Enabled = false;
            this.button3.Enabled = false;
            this.toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
            this.toolStripLabel1.Text = "Результат направления в ЕЦП";

            if (this.f_log)
            {
                this.lb_log.Items.Clear();
                this.lb_log.Items.Add(DateTime.Now.ToString("dd.MM.yyyy:HH.mm.ss"));
            }

            // login ecp ====================================================================
            Task<string> t1 = this.LoginEcpAsync();
            Info.raw_login_ecp = await t1;
            // err login ecp
            // 1
            if (Info.raw_login_ecp == "")
            {
                MessageBox.Show("raw_login_ecp пустое - Пришла пустая строка");
                if (this.f_log)
                {
                    this.lb_log.Items.Add("raw_login_ecp пустое - Пришла пустая строка");
                    this.SaveDirResLog();
                }
                this.button1.Enabled = true;
                this.button3.Enabled = true;
                this.toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
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
                    this.lb_log.Items.Add("raw_login_ecp : deserialize : Исключение JsonConvert : Возможно что нет подключения к БД : " + ex.Message);
                    this.SaveDirResLog();
                }
                this.button1.Enabled = true;
                this.button2.Enabled = true;
                this.toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
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
                    this.lb_log.Items.Add("Объект loginEcp пустой");
                    this.SaveDirResLog();
                }
                this.button1.Enabled = true;
                this.button2.Enabled = true;
                this.toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
                return;
            }

            // get resultat
            Task<string> t2 = this.DirectionResultatEcpAsync();
            Info.raw_direction_resultat = await t2;
            // error
            // 1

            //this.textBox2.Text = "";
            //this.textBox2.Text = Info.raw_direction_resultat;

            if (Info.raw_direction_resultat == "")
            {
                MessageBox.Show("raw_direction_resultat пустое - Пришла пустая строка");
                if (this.f_log)
                {
                    this.lb_log.Items.Add("raw_direction_resultat пустое - Пришла пустая строка");
                    this.SaveDirResLog();
                }
                this.button1.Enabled = true;
                this.button3.Enabled = true;
                this.toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
                return;
            }
            // 2
            try
            {
                this.rootDirectionResultatObject = JsonConvert.DeserializeObject<RootDirectionResultatObject>(Info.raw_direction_resultat);
            }
            catch (Exception ex)
            {
                MessageBox.Show("raw_direction_resultat : deserialize : Исключение JsonConvert " + ex.Message);
                if (this.f_log)
                {
                    this.lb_log.Items.Add("raw_direction_resultat : deserialize : Исключение JsonConvert " + ex.Message);
                    this.SaveDirResLog();
                }
                this.button1.Enabled = true;
                this.button3.Enabled = true;
                this.toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
                return;
            }
            // 3
            //string id_evnplbase_db = "";
            if (this.rootDirectionResultatObject.error_code == 0)
            {
                //Info.evnvizitpl_id = this.rootEvnplbaseObject.data.EvnVizitPL_id; 
                //Info.evnplbase_id = this.rootEvnplbaseObject.data.EvnPLBase_id;
                //this.dt_tap.Rows[0]["evnplbase_id"] 

                this.stringDirectionResult = JsonConvert.SerializeObject(this.rootDirectionResultatObject);

                if (this.f_log)
                {
                    this.lb_log.Items.Add("DirectionResultatEcpAsync");
                    this.lb_log.Items.Add(this.stringDirectionResult);                   
                    this.SaveDirResLog();
                }

                this.button1.Enabled = true;
                this.button3.Enabled = true;
                this.toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
                this.toolStripLabel1.Text = "";
                
                if (this.fdr != null)
                {
                    this.fdr.Dispose();
                    this.fdr = null;
                }                    
                this.fdr = new FormDirectionResultat(this.stringDirectionResult); // string direction resultat data to
                this.fdr.ShowDialog();

                //this.rootDirectionResultatObject.data;
                //directionResultat[0].Lpu_id;
            }
            else
            {

                if (this.f_log)
                {
                    this.lb_log.Items.Add("DirectionResultatEcpAsync - error");
                    this.lb_log.Items.Add(JsonConvert.SerializeObject(this.rootDirectionResultatObject));
                    this.SaveDirResLog();
                }
                MessageBox.Show("Код ошибки - " + this.rootDirectionResultatObject.error_code.ToString());

                this.button1.Enabled = true;
                this.button3.Enabled = true;
                this.toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
                this.toolStripLabel1.Text = "";

            }

        }

        private void SaveDirResLog()
        {
              using (var sw = new StreamWriter(this.pathDirResLog, true))
                 foreach (var item in lb_log.Items) sw.Write(item.ToString() + Environment.NewLine);
        }
        
        
    }
    
}
