using GMap.NET.WindowsForms;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace DataProcessing
{
    public partial class Map : Form
    {
        List<PointLatLng> points = new List<PointLatLng>();
        GMapOverlay routes = new GMapOverlay("routes");


        public Map()
        {
            InitializeComponent();


        }

        public void Map_Load(object sender, EventArgs e)
        {
            gmap.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            gmap.SetPositionByKeywords("Paris, France");


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home home = new Home();
            home.ShowDialog();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri("http://127.0.0.1:5000/api/resources/place?id=" + textBox1.Text),
                        Content = new StringContent("", Encoding.UTF8, "application/json")
                    };

                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var response = await client.SendAsync(request);
                    var responseString = await response.Content.ReadAsStringAsync();

                    var jsonObject = JObject.Parse(responseString);
                    var placesArray = jsonObject["places"] as JArray;

                    if (placesArray != null && placesArray.Count > 0)
                    {
                        var placeObject = placesArray?[0] as JObject;
                        var cityName = placeObject.Properties().FirstOrDefault()?.Name;
                        var coordinatesObject = placeObject.Value<JObject>()[cityName]?["coordinates"]; if (coordinatesObject != null)
                        {
                            var latitude = (double)coordinatesObject["latitude"];
                            var longitude = (double)coordinatesObject["longitude"];
                            points.Add(new PointLatLng(latitude, longitude));
                            label1.Text = "Location Added!";
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter an ID for the location");
            }
        }

        private async void button5_click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri("http://127.0.0.1:5000/api/resources/place?id=" + textBox1.Text),
                        Content = new StringContent("", Encoding.UTF8, "application/xml")
                    };

                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");

                    var response = await client.SendAsync(request);
                    var responseString = await response.Content.ReadAsStringAsync();

                    var xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(responseString);

                    var placeNode = xmlDoc.SelectSingleNode("//place");

                    if (placeNode != null)
                    {
                        var cityName = placeNode.SelectSingleNode("name")?.InnerText;

                        var coordinatesNode = placeNode.SelectSingleNode("coordinates");

                        if (coordinatesNode != null)
                        {
                            var latitude = double.Parse(coordinatesNode.SelectSingleNode("latitude").InnerText);
                            var longitude = double.Parse(coordinatesNode.SelectSingleNode("longitude").InnerText);

                            points.Add(new PointLatLng(latitude, longitude));
                            label1.Text = "Location Added!";
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please enter an ID for the location");
            }
        }



        private void button3_Click(object sender, EventArgs e)
        {
            GMapOverlay routesOverlay = gmap.Overlays.FirstOrDefault(o => o.Id == "routes");

            // If it exists, remove it from the map's overlays collection
            if (routesOverlay != null)
            {
                gmap.Overlays.Remove(routesOverlay);
            }

            points.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (points.Count < 2)
            {
                MessageBox.Show("Please select at least two points to create a route.");
                return;
            }

            GMapRoute route = new GMapRoute(points, "A generated route");
            route.Stroke = new Pen(Color.Red, 3);
            routes.Routes.Add(route);
            GMap.NET.RectLatLng bounds = new GMap.NET.RectLatLng(points[0].Lat, points[0].Lng, 0.1, 0.1);
            for (int i = 1; i < points.Count; i++)
            {
                bounds = GMap.NET.RectLatLng.Union(bounds, new GMap.NET.RectLatLng(points[i].Lat, points[i].Lng, 0.1, 0.1));
            }

            gmap.Overlays.Add(routes);

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

