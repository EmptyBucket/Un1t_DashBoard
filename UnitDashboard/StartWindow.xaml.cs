using DataBase.PageOptions;
using System;
using System.Windows;

namespace UnitDashboard
{
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();

            SelectTemplate conn = new SelectTemplate(@"Data Source=App_Data\DataBase\PageOptions\PageOptions.sdf");
            int? select = conn.Select();
            switch (select)
            {
                case null:
                    FBlock.Source = new Uri("SelectPage.xaml", UriKind.Relative);
                    break;
                case TemplateType.template_0:
                    FBlock.Source = new Uri("Page/SetPage_0/CompletePage_0.xaml", UriKind.Relative);
                    break;
                case TemplateType.template_1:
                    FBlock.Source = new Uri("Page/SetPage_1/CompletePage_1.xaml", UriKind.Relative);
                    break;
            }
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.F2)
                FBlock.Source = new Uri("SelectPage.xaml", UriKind.Relative);
        }
    }
}
