using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;



namespace medrecords
{

    public partial class FormDirectionResultat : Form
    {

        string stringDirectionResult;
        RootDirectionResultatObject rootDirectionResultatObject;
        DataDirectionResult dataDirectionResult;

        DataTable dt_result;

        Dictionary<string, string> fields;

        public FormDirectionResultat(string res)
        {
            InitializeComponent();

            this.fields = new Dictionary<string, string>();
            this.fields.Add("UslugaTest_id", "идентификатор заготовки для результатов теста в таблице dbo.UslugaTest");
            this.fields.Add("UslugaComplex_id", "идентификатор теста внутренний для Системы");
            this.fields.Add("UslugaTest_ResultValue", "результат выполнения лабораторного исследования для теста");
            this.fields.Add("Unit_id", "идентификатор единицы измерения");
            this.fields.Add("UslugaTest_ResultUnit", "наименование единицы измерения");
            this.fields.Add("UslugaTest_Comment", "дата и время направления на лабораторное исследование");
            this.fields.Add("UslugaTest_setDT", "дата выполнения");
            this.fields.Add("UslugaTest_ResultLower", "нижнее референсное значение");
            this.fields.Add("UslugaTest_ResultUpper", "верхнее референсное значение");
            this.fields.Add("UslugaTest_ResultLowerCrit", "нижнее критическое референсное значение");
            this.fields.Add("UslugaTest_ResultUpperCrit", "верхнее критическое референсное значение");
            this.fields.Add("UslugaTest_ResultApproved", "статус теста (Null – не проводился, 0 – выполнен, 1 – одобрен)");
            this.fields.Add("UslugaTest_deleted", "признак удаления (0 – не удален, 1 – удален)");
            this.fields.Add("UslugaTest_delDT", "дата удаления теста (обязательно для заполнения, если UslugaTest_deleted=1)");
            this.fields.Add("UslugaTest_ResultCancelReason", "причина отмены результата теста");
            this.fields.Add("UslugaTest_ResultAppDate", "дата обновления значения результата выполнения");
            this.fields.Add("LabTest_id", "идентификатор лабораторных тестов (для идентификации кода ФСЛИ по федеральному справочнику 1.2.643.5.1.13.13.11.1080)");
            this.fields.Add("LabTest_Code", "код ФСЛИ (соответствует федеральному справочнику 1.2.643.5.1.13.13.11.1080)");
            this.fields.Add("UslugaTest_pid", "учетный документ, в рамках которого добавлено заболевание");
            this.fields.Add("UslugaComplex_Code", "код НМУ теста");

            this.stringDirectionResult = res;
            this.textBox1.Text = this.stringDirectionResult;
            
            this.rootDirectionResultatObject = JsonConvert.DeserializeObject<RootDirectionResultatObject>(this.stringDirectionResult);
            this.dataDirectionResult = this.rootDirectionResultatObject.data;
            
            //this.testlists = this.dataDirectionResult.TestLists;

            // множественное число а не единичный вариант
            string json_testlists = JsonConvert.SerializeObject(this.dataDirectionResult.TestList);
            this.dt_result = UseNewtonsoftJson(json_testlists);
            
            this.label1.Text = this.dataDirectionResult.EvnLabSample_id.ToString();

            this.textBox2.Text = GetById(this.dataDirectionResult.Lpu_id.ToString(), "lpu");
            this.textBox2.Enabled = false;

            this.label4.Text = this.dataDirectionResult.MedStaffFact_id;

            this.textBox3.Text = GetById(this.dataDirectionResult.PayType_id.ToString() , "paytype");
            this.textBox3.Enabled = false;

            for (int i = 0; i < this.dt_result.Columns.Count; i++)
            {
                //this.dt_result.Columns[i].ColumnName = "field_" + i.ToString();
                this.dt_result.Columns[ i ].ColumnName = fields[ this.dt_result.Columns[ i ].ColumnName ];
            }            
           
            this.dataGridView1.DataSource = this.dt_result;
            foreach(DataRow row in this.dt_result.Rows)
            {
                for (int k = 0; k < this.dt_result.Columns.Count; k++)
                {

                    //row[ k ] = GetByIndexColumn(row[k].ToString(), k );
                    //MessageBox.Show(row[k].ToString());
                    switch (k)
                    {
                        case 0:                            
                            row[k] = GetById( row[k].ToString(), "uslugacomplex" );
                            break;
                        case 1:
                            row[k] = GetById( row[k].ToString(), "uslugacomplex" );
                            break;

                    }

                }
            }

        }

        private string GetByIndexColumn( string id, int i )
        {
            string ret = "";
            switch (i)
            {
                case 0:
                    ret = GetById( id, "uslugacomplex");
                    break;
                case 1:
                    ret = GetById(id, "uslugacomplex");
                    break;

            }

            return ret;
        }

        private string GetById(string id, string nametable)
        {
            DataRow[] result = Info.ds.Tables[ nametable ].Select( "ID_SPR = '" + id + "'" );
            if (result.Length > 1)
                MessageBox.Show("Несколько значений в " + nametable);
            if (result.Length == 0)
                MessageBox.Show("Нет данных в " + nametable);

            return result[ 0 ][ "NAME_SPR" ].ToString().Trim();
        }


        // Convert JSON to DataTable Using Newtonsoft.Json
        public static DataTable? UseNewtonsoftJson(string sampleJson)
        {
            DataTable? dataTable = new DataTable();
            if (string.IsNullOrWhiteSpace(sampleJson))
            {
                return dataTable;
            }
            dataTable = JsonConvert.DeserializeObject<DataTable>(sampleJson);
            return dataTable;
        }



    }
}
