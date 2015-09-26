using DataBase.PageOptions;
using System.Collections.Generic;
using System.Windows.Controls;
using UnitDashboard.Page;

namespace UnitDashboard.SetPage_1
{
    public partial class CompletePage_1 : System.Windows.Controls.Page
    {
        public CompletePage_1()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            BlockOptions blockOptions = new BlockOptions(@"Data Source=App_Data\DataBase\PageOptions\PageOptions.sdf");
            Block[] data = blockOptions.Select();

            List<Border> blocks = new List<Border>();
            blocks.Add(Title);
            blocks.Add(Block_0);
            blocks.Add(Block_1);
            blocks.Add(Block_2);
            blocks.Add(Block_3);
            blocks.Add(Block_4);
            blocks.Add(Block_5);
            blocks.Add(Block_6);

            Border[] blockArray = blocks.ToArray();

            FillContent fill = new FillContent(data, ref blockArray);
        }
    }
}
