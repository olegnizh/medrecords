using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;



namespace medrecords
{
    public partial class FormBuildDiagSpr : Form
    {

        DataTable dt;


        public FormBuildDiagSpr(DataTable dt)
        {
            InitializeComponent();

            this.dt = dt.Clone();

            this.dataGridView1.DataSource = Info.ds.Tables["diag"];
            this.lb_a.Text = Info.ds.Tables["diag"].Rows.Count.ToString();


        }

        private string get_strNumber(int number)
        {
            string ret = "";

            if (number < 10)
                ret = "0" + number.ToString();
            else
                ret = number.ToString();

            return ret;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //
            string s_num = "";
            string s_code = "";
            //int i1 = Convert.ToInt32(this.textBox3.Text);
            //int i2 = Convert.ToInt32(this.textBox4.Text);

            //string node_txt = e.Node.Text;
            TreeNode selectedNode = this.treeView1.SelectedNode;
            //selectedNode.Nodes.Add(
            
            string[] strs_main;
            strs_main = selectedNode.Text.Split(' ');
            string code = strs_main[0];

            string[] strs_code;
            strs_code = code.Split("-");
            string s1 = strs_code[0];
            string s2 = strs_code[1];

            // code
            //char ss = s1[0];
            string s = s1.Substring(0, 1);
            int i1 = Convert.ToInt32(s1.Substring(1, 2));
            int i2 = Convert.ToInt32(s2.Substring(1, 2));

            //MessageBox.Show(s + " " + i1.ToString() + " " + i2.ToString());

            
            for (int i = i1; i <= i2; i++)
            {
                
                if (i < 10)
                    s_num = "0" + i.ToString();
                else
                    s_num = i.ToString();

                //s = this.textBox1.Text.ToString().Trim() + s;
                s_code = "";
                s_code = s + s_num;
                //MessageBox.Show(s_code);
                DataRow[] result = Info.ds.Tables["diag"].Select("code_spr = '" + s_code + "'");
               
                if (result.Length == 0)
                {
                    //MessageBox.Show("Записей не отобрано - нечего добавлять");
                    //return;
                    continue;

                }
                else
                {
                    //TreeNode selectedNode = this.treeView1.SelectedNode;
                    selectedNode.Nodes.Add(result[0]["CODE_SPR"].ToString() + ' ' + result[0]["NAME_SPR"].ToString());

                }
                
            }
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // A =           
            DataRow[] result = Info.ds.Tables["diag"].Select("enddate is null and code_spr = '" + this.textBox1.Text.ToString().Trim() + "'");
            this.dt.Clear();
            foreach (DataRow dr in result) this.dt.ImportRow(dr);
            this.dataGridView3.DataSource = this.dt;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // like A
            DataRow[] result = Info.ds.Tables["diag"].Select("enddate is null and code_spr like '" + this.textBox1.Text.ToString().Trim() + "*'");
            //DataRow[] result = Info.ds.Tables["uslugacomplex"].Select("enddate is null and code_spr like '" + this.textBox1.Text.ToString().Trim() + "*'");            
            //DataRow[] result = Info.ds.Tables["uslugacomplex"].Select("enddate is null");
            this.dt.Clear();
            foreach (DataRow dr in result) this.dt.ImportRow(dr);
            this.dataGridView3.DataSource = this.dt;

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string s = e.Node.Text;
            string[] strs;

            strs = s.Split(' ');
            this.label2.Text = strs[0];

            DataRow[] result = Info.ds.Tables["diag"].Select("code_spr = '" + strs[0] + "'");
            this.label5.Text = result[0]["id_spr"].ToString();

            //MessageBox.Show(strs[0]);

            //MessageBox.Show(e.Node.Text);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // add root node
            this.treeView1.Nodes.Add(this.dataGridView3["CODE_SPR", this.dataGridView3.CurrentCell.RowIndex].Value.ToString()+' '+ this.dataGridView3["NAME_SPR", this.dataGridView3.CurrentCell.RowIndex].Value.ToString());
             
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // add node 
            TreeNode selectedNode = this.treeView1.SelectedNode;
            selectedNode.Nodes.Add(this.dataGridView3["CODE_SPR", this.dataGridView3.CurrentCell.RowIndex].Value.ToString() + ' ' + this.dataGridView3["NAME_SPR", this.dataGridView3.CurrentCell.RowIndex].Value.ToString());

        }

        private void button6_Click(object sender, EventArgs e)
        {
            // save tree
            using (Stream file = File.Open("sprecp\\diag_dir.bin", FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(file, this.treeView1.Nodes.Cast<TreeNode>().ToList());
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            // view tree
            using (Stream file = File.Open("sprecp\\diag_dir.bin", FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                object obj = bf.Deserialize(file);

                TreeNode[] nodeList = (obj as IEnumerable<TreeNode>).ToArray();
                this.treeView1.Nodes.AddRange(nodeList);
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            // delete node
            TreeNode selectedNode = this.treeView1.SelectedNode;
            this.treeView1.Nodes.Remove(selectedNode);

        }



        private void button12_Click(object sender, EventArgs e)
        {
            //string s = "";
            DataRow[] result;

            foreach (string line in textBox2.Lines)
            {
                
                result = Info.ds.Tables["diag"].Select("enddate is null and code_spr = '" + line.Trim() + "'");
                this.treeView1.Nodes.Add(result[0]["code_spr"].ToString() + " " + result[0]["name_spr"].ToString());

            }


        }

        private void button13_Click(object sender, EventArgs e)
        {

            DataRow[] result;

            TreeNode selectedNode = this.treeView1.SelectedNode;
            foreach (string line in textBox2.Lines)
            {

                result = Info.ds.Tables["diag"].Select("enddate is null and code_spr = '" + line.Trim() + "'");
                if (result.Length != 0)
                    selectedNode.Nodes.Add(result[0]["code_spr"].ToString() + " " + result[0]["name_spr"].ToString());
                else
                {
                    MessageBox.Show(line.Trim() + " не найден");
                    return;
                }

            }


            //TreeNode selectedNode = this.treeView1.SelectedNode;
            //selectedNode.Nodes.Add(this.dataGridView3["CODE_SPR", this.dataGridView3.CurrentCell.RowIndex].Value.ToString() + ' ' + this.dataGridView3["NAME_SPR", this.dataGridView3.CurrentCell.RowIndex].Value.ToString());


        }

        private void button14_Click(object sender, EventArgs e)
        {
            // add to end diag
            //List<String> char_diag = new List<string>();

            string[] strs;

            foreach (TreeNode n1 in this.treeView1.Nodes)
            {
                //MessageBox.Show(n1.Text);
                //strs1 = n1.Text.Split(" ");
                foreach (TreeNode n2 in n1.Nodes)
                {
                    //MessageBox.Show(n2.Text);
                    foreach (TreeNode n3 in n2.Nodes)
                    {
                        strs = n3.Text.Split(" ");
                        for (int i = 0; i <= 99; i++)
                        {
                            //DataRow[] result = Info.ds.Tables["diag"].Select("code_spr like '" + strs[0] + "*'");
                            DataRow[] result = Info.ds.Tables["diag"].Select("code_spr = '" + strs[0] + "." + i.ToString() + "'");
                            if (result.Length != 0)
                            {
                                foreach (DataRow dr in result) n3.Nodes.Add(dr["code_spr"].ToString() + " " + dr["name_spr"].ToString());
                            }
                            else continue;

                        }
                        //break;   
                    }
                    //break;
                }
                //break;
            }
            MessageBox.Show("end");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            //
            string s_num = "";
            string s_code = "";
            int i1 = Convert.ToInt32(this.textBox3.Text);
            int i2 = Convert.ToInt32(this.textBox4.Text);

            for (int i = i1; i <= i2; i++)
            {

                if (i < 10)
                    s_num = "0" + i.ToString();
                else
                    s_num = i.ToString();

                s_code = this.textBox1.Text.ToString().Trim();
                s_code = s_code + s_num;

                DataRow[] result = Info.ds.Tables["diag"].Select("code_spr = '" + s_code + "'");

                if (result.Length == 0)
                {
                    //MessageBox.Show("Записей не отобрано - нечего добавлять");
                    //return;
                    continue;

                }
                else
                {
                    TreeNode selectedNode = this.treeView1.SelectedNode;
                    selectedNode.Nodes.Add(result[0]["CODE_SPR"].ToString() + ' ' + result[0]["NAME_SPR"].ToString());

                }

            }



        }

        


    }
}
