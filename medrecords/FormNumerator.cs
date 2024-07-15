using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace medrecords
{
    public partial class FormNumerator : Form
    {

        public FormNumerator()
        {
            InitializeComponent();

            this.numericUpDown1.Value = Info.max_num_tap;
            this.numericUpDown2.Value = Info.max_num_dir;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Info.max_num_tap = (int) this.numericUpDown1.Value;
            Info.max_num_dir = (int) this.numericUpDown2.Value;
            this.Close();

        }


    }
}
