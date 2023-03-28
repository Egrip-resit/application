using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml;
using System.Net;
using Newtonsoft.Json.Linq;

namespace DataProcessing
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Registration registration = new Registration();
            registration.ShowDialog();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var xml = new XmlDocument();

            var credentials = xml.CreateElement("credentials");

            var email = xml.CreateElement("email");
            email.InnerText = textBox2.Text;
            credentials.AppendChild(email);

            var password = xml.CreateElement("password");
            password.InnerText = textBox1.Text;
            credentials.AppendChild(password);

            xml.AppendChild(credentials);

            var content = new StringContent(xml.OuterXml, Encoding.UTF8, "application/xml");

            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("http://127.0.0.1:5000/api/login"),
                    Content = content
                };
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");

                var response = await client.SendAsync(request);
                var responseString = await response.Content.ReadAsStringAsync();
                label3.Text = responseString;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    this.Hide();
                    Home home = new Home();
                    home.ShowDialog();

                }
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            string json = "{ \"credentials\": { \"email\": \"" + textBox2.Text + "\", \"password\": \"" + textBox1.Text + "\" } }";

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("http://127.0.0.1:5000/api/login"),
                    Content = content
                };
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.SendAsync(request);
                var responseString = await response.Content.ReadAsStringAsync();
                label3.Text = responseString;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    this.Hide();
                    Home home = new Home();
                    home.ShowDialog();
                }
            }
        }
    }
}
