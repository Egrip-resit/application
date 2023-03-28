using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net.Http.Headers;

namespace DataProcessing
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        //Registration with Json
        private async void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == textBox3.Text)
            {
                string json = $"{{\"user\":{{\"{textBox1.Text}\":{{\"credentials\":{{\"email\":\"{textBox4.Text}\",\"password\":\"{textBox2.Text}\"}}}}}}}}";
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri("http://127.0.0.1:5000/api/signup"),
                        Content = content
                    };
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var response = await client.SendAsync(request);
                    var responseString = await response.Content.ReadAsStringAsync();
                    label5.Text = responseString;
                }
            }
            else
            {
                label5.Text = "Please confirm your password!";
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.ShowDialog();
        }

        //Registration in XML format
        private async void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == textBox3.Text)
            {
                var xml = new XmlDocument();

                var user = xml.CreateElement("user");

                var name = xml.CreateElement("name");
                name.InnerText = textBox1.Text;
                user.AppendChild(name);

                var credentials = xml.CreateElement("credentials");

                var email = xml.CreateElement("email");
                email.InnerText = textBox4.Text;
                credentials.AppendChild(email);

                var password = xml.CreateElement("password");
                password.InnerText = textBox2.Text;
                credentials.AppendChild(password);

                user.AppendChild(credentials);

                xml.AppendChild(user);

                var content = new StringContent(xml.OuterXml, Encoding.UTF8, "application/xml");

                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = new Uri("http://127.0.0.1:5000/api/signup"),
                        Content = content
                    };
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");

                    var response = await client.SendAsync(request);
                    var responseString = await response.Content.ReadAsStringAsync();
                    label5.Text = responseString;
                }
            }
            else
            {
                label5.Text = "Please confirm your password!";
            }
        }
    }
}
