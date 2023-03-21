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

namespace DataProcessing
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        
        //Registration with Json
        private async void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == textBox2.Text)
            {
                string myJson = JsonSerializer.Serialize(new
                {
                    email = textBox4.Text,
                    user = textBox1.Text,
                    password = textBox2.Text
                }); ;

                using (var client = new HttpClient())
                {
                    var response = await client.PostAsync(
                        "",
                         new StringContent(myJson, Encoding.UTF8, "application/json"));
                }
            }
            else
            {
                label5.Text = "Password confirmation must be correct";
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
            if (textBox3.Text == textBox2.Text)
            {
                var xml = new XmlDocument();
                var credentials = xml.CreateElement("Credentials");
                var email = xml.CreateElement("email");
                email.InnerText = textBox4.Text;
                var username = xml.CreateElement("username");
                username.InnerText = textBox1.Text;
                var password = xml.CreateElement("password");
                password.InnerText = textBox3.Text;
                credentials.AppendChild(email);
                credentials.AppendChild(username);
                credentials.AppendChild(password);

                using (var client = new HttpClient())
                {
                    var response = await client.PostAsync(
                        "",
                         new StringContent(credentials.OuterXml, Encoding.UTF8, "application/xml"));
                }
            }
            else
            {
                label5.Text = "Password confirmation must be correct";
            }
        }
    }
    }
