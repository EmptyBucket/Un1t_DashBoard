using System;
using System.Windows.Controls;
namespace UnitDashboard.Service
{
    class ExchangeRatesService
    {
        public ExchangeRatesService(string Currency)
        {
            UnitDashboard.ExchangeRatesServiceReference.ExchangeRatesSoapClient client =
                new UnitDashboard.ExchangeRatesServiceReference.ExchangeRatesSoapClient("ExchangeRatesSoap");
            Decimal thisRate = client.getCurrentExchangeRate(Currency);
            Decimal rubRate = client.getCurrentExchangeRate("RUB");
            this.rate = (float)Math.Round(thisRate / rubRate, 4);
        }
        public float rate { get; set; }
    }

    public partial class ExchangeRatesPage : System.Windows.Controls.Page
    {
        public ExchangeRatesPage()
        {
           
            InitializeComponent();

            try
            {
                ExchangeRatesService CHF = new ExchangeRatesService("CHF");
                ExchangeRatesService USD = new ExchangeRatesService("USD");
                ExchangeRatesService EUR = new ExchangeRatesService("EUR");
                FBExchangeRates_1.Text = System.Convert.ToString(CHF.rate);
                FBExchangeRates_2.Text = System.Convert.ToString(USD.rate);
                FBExchangeRates_3.Text = System.Convert.ToString(EUR.rate);
            }
            catch
            {
                Viewbox vBox = new Viewbox();
                vBox.Margin = new System.Windows.Thickness(3);
                TextBlock tBxox = new TextBlock();
                tBxox.Foreground = System.Windows.Media.Brushes.Red;
                tBxox.Text = "Не удалось подключиться к сервису: \"Курс валют\"";
                vBox.Child = tBxox;
                Box.Child = vBox;
            }
        }
    }
}
