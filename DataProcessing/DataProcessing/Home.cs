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
        private void button2_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void Home_Load(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel3.Visible = false;
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
            string latitude = double.Parse(textBox4.Text).ToString();
            string longitude = double.Parse(textBox5.Text).ToString();

            string json = $"{{\"place\":{{\"{textBox1.Text}\":{{\"coordinates\":{{\"latitude\":{latitude},\"longitude\":{longitude}}}}}}}}}";
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            Map map = new Map();
            map.ShowDialog();
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
