using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DataProcessing
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        public bool jsonValidator()
        {
            JSchema schema = JSchema.Parse(@"{
                'type': 'object',
                'properties': {
                 'name': {'type':'string'}
                }
              }");

            JObject user = JObject.Parse(@"{
             'name': 'Alex'
            }");

            bool valid = user.IsValid(schema);

            return valid;
        }


            private void button2_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        private void Home_Load(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            String result = jsonValidator().ToString();
            textBox1.Text = result;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            Map map = new Map();
            map.ShowDialog();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
