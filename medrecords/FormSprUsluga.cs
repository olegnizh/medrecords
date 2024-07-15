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
    public partial class FormSprUsluga : Form
    {

        string node_text = "";

        public FormSprUsluga(string s)
        {
            InitializeComponent();

            this.node_text = s;

        }

        private void FormSprUsluga_Load(object sender, EventArgs e)
        {
            // view tree
            using (Stream file = File.Open("sprecp\\usluga_dir.bin", FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                object obj = bf.Deserialize(file);

                TreeNode[] nodeList = (obj as IEnumerable<TreeNode>).ToArray();
                this.treeView1.Nodes.AddRange(nodeList);

            }

            this.button3.PerformClick();

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
            Info.selected_usluga = e.Node.Text;

            string[] strs = Info.selected_usluga.Split(' ');
            DataRow[] result = Info.ds.Tables["uslugacomplex"].Select("enddate is null and code_spr = '" + strs[0] + "'");

            this.listBox1.Items.Clear();
            foreach (DataRow dr in result)
            {
                this.listBox1.Items.Add(dr["id_spr"].ToString());
                if (dr["id_spr"].ToString().Length == 15)
                    Info.selected_usluga_id = dr["id_spr"].ToString();
                else
                    Info.selected_usluga_id = "";
            }

            if (Info.selected_usluga_id == "")
                this.label2.Text = "ВЫ НЕ МОЖЕТЕ ВЫБРАТЬ ЭТУ УСЛУГУ";
            else
                this.label2.Text = "";


            //Info.selected_usluga_code = strs[0];
            //if (this.listBox1.Items.Count == 1) Info.selected_usluga_id = listBox1.Items[0].ToString();
            //if (this.listBox1.Items.Count == 2) Info.selected_usluga_id = listBox1.Items[1].ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Info.selected_usluga_id = this.listBox1.SelectedItem.ToString();


        }

        private List<TreeNode> CurrentNodeMatches = new List<TreeNode>();
        private int LastNodeIndex = 0;
        private string LastSearchText;

        private void button3_Click(object sender, EventArgs e)
        {
            // search
            string searchText = this.node_text;
            if (String.IsNullOrEmpty(searchText))
            {
                return;
            };
            if (LastSearchText != searchText)
            {
                //It's a new Search
                CurrentNodeMatches.Clear();
                LastSearchText = searchText;
                LastNodeIndex = 0;
                SearchNodes(searchText, treeView1.Nodes[0]);
            }
            if (LastNodeIndex >= 0 && CurrentNodeMatches.Count > 0 && LastNodeIndex < CurrentNodeMatches.Count)
            {
                TreeNode selectedNode = CurrentNodeMatches[LastNodeIndex];
                LastNodeIndex++;
                this.treeView1.SelectedNode = selectedNode;
                this.treeView1.SelectedNode.Expand();
                this.treeView1.Select();
            }
        }

        private void SearchNodes(string SearchText, TreeNode StartNode)
        {
            //TreeNode node = null;
            while (StartNode != null)
            {
                if (StartNode.Text.ToLower().Contains(SearchText.ToLower()))
                {
                    CurrentNodeMatches.Add(StartNode);
                };
                if (StartNode.Nodes.Count != 0)
                {
                    SearchNodes(SearchText, StartNode.Nodes[0]);//Recursive Search
                };
                StartNode = StartNode.NextNode;
            };
        }

        private void FormSprUsluga_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((this.DialogResult == DialogResult.OK) && (Info.selected_usluga_id == ""))
                e.Cancel = true;
            else
                e.Cancel = false;

        }



    }
}
