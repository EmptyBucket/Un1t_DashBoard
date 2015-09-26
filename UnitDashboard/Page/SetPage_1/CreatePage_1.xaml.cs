using UnitDashboard.Page;

namespace UnitDashboard.SetPage_1
{
    public partial class CreatePage_1 : System.Windows.Controls.Page
    {
        public CreatePage_1()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            const int valueBlock = 8;
            OptionsMenu optionsMenu = new OptionsMenu(ref box, valueBlock);
        }
    }
}
