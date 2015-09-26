using DataBase.PageOptions;
using System;

namespace UnitDashboard
{
    public partial class SelectPage : System.Windows.Controls.Page
    {
        public SelectPage()
        {
            InitializeComponent();

            FBlock_0.Source = new Uri("Page/SetPage_0/TemplatePage_0.xaml", UriKind.Relative);
            FBlock_1.Source = new Uri("Page/SetPage_1/TemplatePage_1.xaml", UriKind.Relative);
        }

        private void saveTemplate(int template)
        {
            SelectTemplate selectTemplate = new SelectTemplate(@"Data Source=App_Data\DataBase\PageOptions\PageOptions.sdf");
            selectTemplate.Delete();
            selectTemplate.Insert(template);
        }

        private void Border_MouseDown_1(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            saveTemplate(TemplateType.template_0);
            NavigationService.Navigate(new Uri("Page/SetPage_0/CreatePage_0.xaml", UriKind.Relative));
        }

        private void Border_MouseDown_2(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            saveTemplate(TemplateType.template_1);
            NavigationService.Navigate(new Uri("Page/SetPage_1/CreatePage_1.xaml", UriKind.Relative));
        }

        private void Border_MouseDown_3(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            
        }

        private void Border_MouseDown_4(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            
        }

        private void Border_MouseDown_5(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            
        }

        private void Border_MouseDown_6(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            
        }
    }
}
