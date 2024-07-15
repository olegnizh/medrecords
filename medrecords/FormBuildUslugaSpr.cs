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
    public partial class FormBuildUslugaSpr : Form
    {

        DataTable dt;


        public FormBuildUslugaSpr(DataTable dt)
        {
            InitializeComponent();

            this.dt = dt.Clone();

            this.dataGridView1.DataSource = Info.ds.Tables["uslugacomplex"];
            this.lb_a.Text = Info.ds.Tables["uslugacomplex"].Rows.Count.ToString();


        }


        private void button1_Click(object sender, EventArgs e)
        {
            // 2 fill A
            string s = "";

            for (int i = 1; i < 100; i++)
            {
                if (i < 10)
                    s = "0" + i.ToString();
                else
                    s = i.ToString();

                s = this.textBox1.Text.ToString().Trim() + "." + s;
                DataRow[] result = Info.ds.Tables["uslugacomplex"].Select("CODE_SPR = '" + s + "'");
               
                if (result.Length == 0)
                {
                    //MessageBox.Show("Записей не отобрано - нечего добавлять");
                    //return;
                    ;

                }
                else
                {
                    TreeNode selectedNode = this.treeView1.SelectedNode;
                    selectedNode.Nodes.Add(result[0]["CODE_SPR"].ToString() + ' ' + result[0]["NAME_SPR"].ToString());

                }
                
            }
               
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // A =           
            DataRow[] result = Info.ds.Tables["uslugacomplex"].Select("enddate is null and code_spr = '" + this.textBox1.Text.ToString().Trim() + "'");
            this.dt.Clear();
            foreach (DataRow dr in result) this.dt.ImportRow(dr);
            this.dataGridView3.DataSource = this.dt;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // like A
            DataRow[] result = Info.ds.Tables["uslugacomplex"].Select("enddate is null and code_spr like '" + this.textBox1.Text.ToString().Trim() + "*'");
            //DataRow[] result = Info.ds.Tables["uslugacomplex"].Select("enddate is null and code_spr like '" + this.textBox1.Text.ToString().Trim() + "*'");            
            //DataRow[] result = Info.ds.Tables["uslugacomplex"].Select("enddate is null");
            this.dt.Clear();
            foreach (DataRow dr in result) this.dt.ImportRow(dr);
            this.dataGridView3.DataSource = this.dt;

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string s = e.Node.Text;

            /*
            string[] strs;
            strs = s.Split(' ');
            this.label2.Text = strs[0];

            DataRow[] result = Info.ds.Tables["uslugacomplex"].Select("code_spr = '" + strs[0] + "'");
            this.label5.Text = result[0]["id_spr"].ToString();
            */

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
            // add node ГОСТ
            TreeNode selectedNode = this.treeView1.SelectedNode;
            selectedNode.Nodes.Add(this.dataGridView3["CODE_SPR", this.dataGridView3.CurrentCell.RowIndex].Value.ToString() + ' ' + this.dataGridView3["NAME_SPR", this.dataGridView3.CurrentCell.RowIndex].Value.ToString());

        }

        private void button6_Click(object sender, EventArgs e)
        {
            // save tree
            using (Stream file = File.Open("sprecp\\usluga_dir.bin", FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(file, this.treeView1.Nodes.Cast<TreeNode>().ToList());
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            // view tree main
            using (Stream file = File.Open("sprecp\\usluga_dir.bin", FileMode.Open))
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

        private void button9_Click(object sender, EventArgs e)
        {
            // 2 fill B
            string s = "";

            for (int i = 1; i < 100; i++)
            {
                if (i < 10)
                   s = "00" + i.ToString();

                if ((i > 9) && (i < 100))
                   s = "0" + i.ToString();                

                s = this.textBox1.Text.ToString().Trim() + "." + s;
                DataRow[] result = Info.ds.Tables["uslugacomplex"].Select("CODE_SPR = '" + s + "'");

                if (result.Length == 0)
                {
                    //MessageBox.Show("Записей не отобрано - нечего добавлять");
                    //return;
                    ;

                }
                else
                {
                    TreeNode selectedNode = this.treeView1.SelectedNode;
                    selectedNode.Nodes.Add(result[0]["CODE_SPR"].ToString() + ' ' + result[0]["NAME_SPR"].ToString());

                }

            }

        }

        private void button10_Click(object sender, EventArgs e)
        {
            // test A
            
            //this.treeView1.Nodes[0].Nodes.Add(new TreeNode("Планшеты"));
            //this.treeView1.Nodes[0].Nodes[0].Nodes.Add(new TreeNode("Планшеты"));

            //this.treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes.Add(new TreeNode("Планшеты"));
            //MessageBox.Show(this.treeView1.Nodes[0].Nodes[0].Nodes[0].Text);

            string s = "";
            string[] strs;
            int i = 0;
            //int k = 0;
            

            //for (int i = 0; i < 100; i++)
            foreach (TreeNode n1 in this.treeView1.Nodes[0].Nodes)
            {
                //for (int k = 0; k < 100; k++)
                foreach (TreeNode n2 in n1.Nodes)
                {
                    s = n2.Text;
                    strs = s.Split(' ');
                    DataRow[] result = Info.ds.Tables["uslugacomplex"].Select("enddate is null and code_spr like '" + strs[0] + ".*'");
                    MessageBox.Show(strs[0]);
                    foreach (DataRow dr in result)
                    {
                        n2.Nodes.Add(new TreeNode(dr["CODE_SPR"].ToString() + " " + dr["NAME_SPR"].ToString()));
                        i++;
                        this.label3.Text = i.ToString();

                    }
                         
                    
                }

            }
            
        }

        private void button11_Click(object sender, EventArgs e)
        {
            // test B
            string s = "";
            string[] strs;
            int i = 0;
            //int k = 0;


            //for (int i = 0; i < 100; i++)
            foreach (TreeNode n1 in this.treeView1.Nodes[1].Nodes)
            {
                //for (int k = 0; k < 100; k++)
                foreach (TreeNode n2 in n1.Nodes)
                {
                    s = n2.Text;
                    strs = s.Split(' ');
                    DataRow[] result = Info.ds.Tables["uslugacomplex"].Select("enddate is null and code_spr like '" + strs[0] + ".*'");
                    MessageBox.Show(strs[0]);
                    foreach (DataRow dr in result)
                    {
                        n2.Nodes.Add(new TreeNode(dr["CODE_SPR"].ToString() + " " + dr["NAME_SPR"].ToString()));
                        i++;
                        this.label4.Text = i.ToString();

                    }


                }

            }

        }

        private void button12_Click(object sender, EventArgs e)
        {
            // add node ТФОМС
            TreeNode selectedNode = this.treeView1.SelectedNode;
            //selectedNode.Nodes.Add(this.dataGridView3["CODE_SPR", this.dataGridView3.CurrentCell.RowIndex].Value.ToString() + ' ' + this.dataGridView3["NAME_SPR", this.dataGridView3.CurrentCell.RowIndex].Value.ToString());
            //selectedNode.Nodes.Add(this.dataGridView3["NAME_SPR", this.dataGridView3.CurrentCell.RowIndex].Value.ToString());

            string s = "";
            DataRow[] result;

            for (int i = 1; i <= 64; i++)
            {
                if (i < 10)
                    s = "0" + i.ToString();
                else
                    s = i.ToString();

                result = Info.ds.Tables["uslugacomplex"].Select("enddate is null and code_spr = '" + s + "'");

                if (result.Length != 0)
                    selectedNode.Nodes.Add(result[0]["name_spr"].ToString().Trim());
                else continue;

            }

        }

        private void button13_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = this.treeView1.SelectedNode;            
            selectedNode.Nodes.Add(this.dataGridView3["NAME_SPR", this.dataGridView3.CurrentCell.RowIndex].Value.ToString());

        }

        private void button14_Click(object sender, EventArgs e)
        {
            // open AB to selected
            TreeNode selectedNode = this.treeView1.SelectedNode;
            using (Stream file = File.Open("sprecp\\usluga_dir — AB.bin", FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                object obj = bf.Deserialize(file);

                TreeNode[] nodeList = (obj as IEnumerable<TreeNode>).ToArray();
                selectedNode.Nodes.AddRange(nodeList);
            }

        }



    }
}
