using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;

namespace medrecords
{
    public partial class Form2rel : Form
    {

        //FbDataAdapter dad1, dad2;
        //DataSet ds;
        //DataRelation rel_1, rel_2;
        DataSet ds1;
        DataTable dt1, dt2, dt3;

        public Form2rel()
        {
            InitializeComponent();

            ds1 = new DataSet();

            dt1 = Info.ds.Tables["patient"].Copy();
            ds1.Tables.Add(dt1);
            ds1.Tables[0].TableName = "patient";

            dt2 = Info.ds.Tables["direction"].Copy();
            ds1.Tables.Add(dt2);
            ds1.Tables[1].TableName = "direction";

            dt3 = Info.ds.Tables["evnplbase"].Copy();
            ds1.Tables.Add(dt3);
            ds1.Tables[2].TableName = "evnplbase";


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = ds1.Tables[0];


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.dataGridView2.DataSource = ds1.Tables[1];


        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.dataGridView3.DataSource = ds1.Tables[2];



        }

        private void button4_Click(object sender, EventArgs e)
        {

            this.CreateRelation();


        }

        private void CreateRelation()
        {

            DataColumn parentColumn = ds1.Tables[0].Columns[0];
            DataColumn childColumn = ds1.Tables[1].Columns[1];
            DataColumn childColumn1 = ds1.Tables[2].Columns[1];

            //DataRelation rel_1 = new DataRelation("rel_1", ds1.Tables[0].Columns[0], ds1.Tables[1].Columns[1]);
            DataRelation rel_1 = new DataRelation("rel_1", parentColumn, childColumn);
            ds1.Relations.Add(rel_1);
            DataRelation rel_2 = new DataRelation("rel_2", parentColumn, childColumn1);
            ds1.Relations.Add(rel_2);

            BindingSource parent = new BindingSource();
            BindingSource detail_1 = new BindingSource();
            BindingSource detail_2 = new BindingSource();
            
            // родительская
            parent.DataSource = ds1;
            parent.DataMember = "patient";

            //  details
            detail_1.DataSource = parent;
            detail_1.DataMember = "rel_1";
            detail_2.DataSource = parent;
            detail_2.DataMember = "rel_2";

            this.dataGridView1.DataSource = parent;
            this.dataGridView2.DataSource = detail_1;
            this.dataGridView3.DataSource = detail_2;


        }


    }
}
