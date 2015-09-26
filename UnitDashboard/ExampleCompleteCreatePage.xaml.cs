using System;
using System.Windows.Controls;

namespace UnitDashboard
{
    public partial class ExampleCompleteCreatePage : Page
    {
        public ExampleCompleteCreatePage()
        {
            InitializeComponent();

            FBlock_0.Source = new Uri("Ticker.xaml", UriKind.Relative);
            //FBlock_2.Source = ;
            FBlock_2.Source = new Uri("Service/ExchangeRatesPage.xaml", UriKind.Relative);
            FBlock_3.Source = new Uri("Service/WeatherPage.xaml", UriKind.Relative);
        }
    }
}
