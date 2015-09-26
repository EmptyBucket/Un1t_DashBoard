using System.Text.RegularExpressions;
using System.Windows.Controls;
namespace UnitDashboard.Service
{
    class WeatherService
    {
        public WeatherService(string country, string city)
        {
            UnitDashboard.WeatherServiceReference.GlobalWeatherSoapClient client =
                new UnitDashboard.WeatherServiceReference.GlobalWeatherSoapClient("GlobalWeatherSoap");
            string str = client.GetWeather(city, country);

            this.country = Regex.Match(str, @"<Location>.+, (.+?) .+</Location>").Groups[1].Value;
            this.city = Regex.Match(str, @"<Location>(.+?),.+</Location>").Groups[1].Value;
            this.temperature = Regex.Match(str, @"<Temperature> (.+?)</Temperature>").Groups[1].Value;
            this.relativeHumidity = Regex.Match(str, @"<RelativeHumidity> (.*?)</RelativeHumidity>").Groups[1].Value;
            this.pressure = Regex.Match(str, @"<Pressure> (.+?)</Pressure>").Groups[1].Value;
        }
        public string country { get; set; }
        public string city { get; set; }
        public string temperature { get; set; }
        public string relativeHumidity { get; set; }
        public string pressure { get; set; }
    }

    public partial class WeatherPage : System.Windows.Controls.Page
    {
        public WeatherPage()
        {
            
                InitializeComponent();
            try
            {
                WeatherService weather = new WeatherService("Russia", "Ekaterinburg");
                FBWeather_1.Text = weather.country;
                FBWeather_2.Text = weather.city;
                FBWeather_3.Text = weather.temperature;
                FBWeather_4.Text = weather.relativeHumidity;
                FBWeather_5.Text = weather.pressure;
            }
            catch
            {
                Viewbox vBox = new Viewbox();
                vBox.Margin = new System.Windows.Thickness(3);
                TextBlock tBxox = new TextBlock();
                tBxox.Foreground = System.Windows.Media.Brushes.Red;
                tBxox.Text = "Не удалось подключиться к сервису: \"Погода\"";
                vBox.Child = tBxox;
                Box.Child = vBox;
            }
        }
    }
}
