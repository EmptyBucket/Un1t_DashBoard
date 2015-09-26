using DataBase.PageOptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Media.Imaging;
using Ticker;
using UnitDashboard.App_Data.DataBase.Staff;

namespace UnitDashboard.Page
{
    public class FillContent
    {
        private void FillChart(Block data, ref Border block)
        {
            List<DataChart> worker = new List<DataChart>();
            using (SqlCeConnection connStaff = new SqlCeConnection(@"Data Source=C:\GitHub\UnitDashboard\UnitDashboard\App_Data\DataBase\Staff\Staff.sdf"))
            {
                DataTable dtX = new DataTable();
                DataTable dtY = new DataTable();

                string columnX = new Regex("(.+);.+").Match(data.content).Groups[1].Value;
                string columnY = new Regex(".+;(.+)").Match(data.content).Groups[1].Value;
                SqlCeCommand selectX = new SqlCeCommand(columnX, connStaff);
                SqlCeDataAdapter sdaX = new SqlCeDataAdapter(selectX);
                sdaX.Fill(dtX);
                SqlCeCommand selectY = new SqlCeCommand(columnY, connStaff);
                SqlCeDataAdapter sdaY = new SqlCeDataAdapter(selectY);
                sdaY.Fill(dtY);

                for (int j = 0; j < dtX.Rows.Count; j++)
                {
                    string dataX = dtX.Rows[j].ItemArray[0].ToString();
                    double dataY = Convert.ToDouble(dtY.Rows[j].ItemArray[0].ToString());
                    worker.Add(new DataChart(dataX, dataY));
                }
            }

            List<KeyValuePair<string, double>> valueList = new List<KeyValuePair<string, double>>();
            foreach (DataChart item in worker)
            {
                valueList.Add(new KeyValuePair<string, double>(item.key, item.value));
            }

            Chart chart = new Chart();
            chart.Title = "График продаж";
            switch(data.type)
            {
                case BlockType.ChartLine:
                    LineSeries lineSeries = new LineSeries();
                    lineSeries.DependentValuePath = "Value";
                    lineSeries.IndependentValuePath = "Key";
                    lineSeries.ItemsSource = valueList;
                    chart.Series.Add(lineSeries);
                    break;
                case BlockType.ChartArea:
                    AreaSeries areaSeries = new AreaSeries();
                    areaSeries.DependentValuePath = "Value";
                    areaSeries.IndependentValuePath = "Key";
                    areaSeries.ItemsSource = valueList;
                    chart.Series.Add(areaSeries);
                    break;
                case BlockType.ChartBar:
                    BarSeries barSeries = new BarSeries();
                    barSeries.DependentValuePath = "Value";
                    barSeries.IndependentValuePath = "Key";
                    barSeries.ItemsSource = valueList;
                    chart.Series.Add(barSeries);
                    break;
                case BlockType.ChartColumn:
                    ColumnSeries columnSeries = new ColumnSeries();
                    columnSeries.DependentValuePath = "Value";
                    columnSeries.IndependentValuePath = "Key";
                    columnSeries.ItemsSource = valueList;
                    chart.Series.Add(columnSeries);
                    break;
                case BlockType.ChartPie:
                    PieSeries pieSeries = new PieSeries();
                    pieSeries.DependentValuePath = "Value";
                    pieSeries.IndependentValuePath = "Key";
                    pieSeries.ItemsSource = valueList;
                    chart.Series.Add(pieSeries);
                    break;
            }
            block.Child = chart;
        }

        private void FillImage(Block data, ref Border block)
        {
            BitmapImage imageContent = new BitmapImage(new Uri(data.content, UriKind.Absolute));
            Image image = new Image();
            image.Stretch = System.Windows.Media.Stretch.Fill;
            image.Source = imageContent;
            block.Child = image;
        }

        private void FillService(Block data, ref Border block)
        {
            Frame frame = new Frame();
            switch (data.content)
            {
                case UnitDashboard.Service.ServiceType.Weather:
                    frame.Source = new Uri(@"Service References\Service\ServicePage\WeatherPage.xaml", UriKind.Relative);
                    break;
                case UnitDashboard.Service.ServiceType.EcxhangeRates:
                    frame.Source = new Uri(@"Service References\Service\ServicePage\ExchangeRatesPage.xaml", UriKind.Relative);
                    break;
            }
            block.Child = frame;
        }

        private void FillText(Block data, ref Border block)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.FontFamily = new System.Windows.Media.FontFamily("Times New Roman");
            textBlock.Text = data.content;
            Viewbox viewBox = new Viewbox();
            viewBox.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            viewBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            viewBox.Child = textBlock;
            block.Child = viewBox;
        }

        private void FillVideo(Block data, ref Border block)
        {
            MediaElement video = new MediaElement();
            video.Volume = 0;
            video.Stretch = System.Windows.Media.Stretch.Fill;
            video.MediaEnded += _video_MediaEnded;
            video.Source = new Uri(data.content, UriKind.Absolute);
            block.Child = video;
        }

        private void FillTable(Block data, ref Border block)
        {
            DataGrid dataGrid = new DataGrid();
            using (SqlCeConnection connStaf = new SqlCeConnection(@"Data Source=App_Data\DataBase\Staff\Staff.sdf"))
            {
                SqlCeCommand select = new SqlCeCommand(data.content, connStaf);
                SqlCeDataAdapter sda = new SqlCeDataAdapter(select);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGrid.ItemsSource = dt.DefaultView;
            }
            block.Child = dataGrid;
        }

        private void FillTicker(Block data, ref Border block)
        {
            string text = new Regex("(.+);.+").Match(data.content).Groups[1].Value.ToString();
            double speed = Convert.ToDouble(new Regex(".+;(.+)").Match(data.content).Groups[1].Value.ToString());
            TextBlock tb = new TextBlock();
            tb.FontSize = 30;
            tb.Text = text;
            ContentTicker contentTicker = new ContentTicker();
            contentTicker.Rate = speed;
            contentTicker.Direction = TickerDirection.West;
            contentTicker.Content = tb;

            block.Child = contentTicker;
        }

        private void FillTitleText(Block data, ref Border block)
        {
            string location = new Regex("(.+);.+").Match(data.content).Groups[1].Value;
            string content = new Regex(".+;(.+)").Match(data.content).Groups[1].Value;

            TextBlock textBlock = new TextBlock();
            textBlock.Text = content;
            textBlock.FontFamily = new System.Windows.Media.FontFamily("Times New Roman");
            Viewbox viewBox = new Viewbox();
            viewBox.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            switch (location)
            {
                case Location.Left:
                    viewBox.HorizontalAlignment = HorizontalAlignment.Left;
                    break;
                case Location.Centre:
                    viewBox.HorizontalAlignment = HorizontalAlignment.Center;
                    break;
                case Location.Right:
                    viewBox.HorizontalAlignment = HorizontalAlignment.Right;
                    break;
            }
            viewBox.Child = textBlock;
            block.Child = viewBox;
        }

        private void FillTitleImage(Block data, ref Border block)
        {
            string location = new Regex("(.+);.+").Match(data.content).Groups[1].Value;
            string content = new Regex(".+;(.+)").Match(data.content).Groups[1].Value;

            BitmapImage imageContent = new BitmapImage(new Uri(data.content, UriKind.Absolute));
            Image image = new Image();
            switch (location)
            {
                case Location.Left:
                    image.HorizontalAlignment = HorizontalAlignment.Left;
                    break;
                case Location.Centre:
                    image.HorizontalAlignment = HorizontalAlignment.Center;
                    break;
                case Location.Right:
                    image.HorizontalAlignment = HorizontalAlignment.Right;
                    break;
            }
            image.Source = imageContent;
            block.Child = image;
        }

        public FillContent(Block[] data, ref Border[] blocks)
        {
            int count = blocks.Length;

            for (int i = 0; i < count; i++)
            {
                switch (data[i].type)
                {
                    case BlockType.Empty:
                        break;
                    case BlockType.TitleText:
                        this.FillTitleText(data[i], ref blocks[i]);
                        break;
                    case BlockType.TitleImage:
                        this.FillTitleImage(data[i], ref blocks[i]);
                        break;
                    case BlockType.ChartLine:
                    case BlockType.ChartColumn:
                    case BlockType.ChartPie:
                    case BlockType.ChartBar:
                    case BlockType.ChartArea:
                        this.FillChart(data[i], ref blocks[i]);
                        break;
                    case BlockType.Image:
                        this.FillImage(data[i], ref blocks[i]);
                        break;
                    case BlockType.Service:
                        this.FillService(data[i], ref blocks[i]);
                        break;
                    case BlockType.Text:
                        this.FillText(data[i], ref blocks[i]);
                        break;
                    case BlockType.Video:
                        this.FillVideo(data[i], ref blocks[i]);
                        break;
                    case BlockType.Table:
                        this.FillTable(data[i], ref blocks[i]);
                        break;
                    case BlockType.Ticker:
                        this.FillTicker(data[i], ref blocks[i]);
                        break;
                }
            }
        }

        private static void _video_MediaEnded(object sender, RoutedEventArgs e)
        {
            MediaElement video = sender as MediaElement;
            video.Position = TimeSpan.Zero;
        }
    }
}
