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
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using System.Net.Http.Headers;
using System.Reflection.Metadata;

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


        private async void button5_Click(object sender, EventArgs e)
        {
            var data = new
            {
                name = textBox1.Text,
                latitude = textBox4.Text,
                longitude = textBox5.Text
            };



            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("http://127.0.0.1:5000/api/resources/place"),
                    Content = content
                };
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.SendAsync(request);
                var responseString = await response.Content.ReadAsStringAsync();
                label2.Text = responseString;
            }
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

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private async void button10_Click(object sender, EventArgs e)
        {
            var xml = new XmlDocument();

            var place = xml.CreateElement("place");

            var name = xml.CreateElement("name");
            name.InnerText = textBox1.Text;
            place.AppendChild(name);

            var coordinates = xml.CreateElement("coordinates");
            var latitude = xml.CreateElement("latitude");
            latitude.InnerText = textBox4.Text;
            coordinates.AppendChild(latitude);
            var longitude = xml.CreateElement("longitude");
            longitude.InnerText = textBox5.Text;
            coordinates.AppendChild(longitude);

            place.AppendChild(coordinates);

            xml.AppendChild(place);

            var content = new StringContent(xml.OuterXml, Encoding.UTF8, "application/xml");

            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("http://127.0.0.1:5000/api/resources/place"),
                    Content = content
                };
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");

                var response = await client.SendAsync(request);
                var responseString = await response.Content.ReadAsStringAsync();
                label2.Text = responseString;
            }
        }
    }
}
