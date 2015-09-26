using UnitDashboard.Page;

namespace UnitDashboard.SetPage_0
{
    public partial class CreatePage_0 : System.Windows.Controls.Page
    {
        public CreatePage_0()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            const int valueBlock = 5;
            OptionsMenu optionsMenu = new OptionsMenu(ref box, valueBlock);
        }
    }
}
