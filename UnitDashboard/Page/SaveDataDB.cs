using DataBase.PageOptions;
using System;
using System.Windows.Controls;

namespace UnitDashboard.Page
{
    class SaveDataDB
    {
        public SaveDataDB(ComboBox[] CBoxs, Border[] Boxs)
        {
            BlockOptions blockOptions = new BlockOptions(@"Data Source=App_Data\DataBase\PageOptions\PageOptions.sdf");
            blockOptions.Delete();

            for (int i = 0; i < CBoxs.Length; i++)
            {
                string type = CBoxs[i].SelectedItem.ToString();
                string content = String.Empty;
                switch (type)
                {
                    case BlockType.TitleImage:
                    case BlockType.TitleText:
                        ComboBox cb = (Boxs[i].Child as Grid).Children[0] as ComboBox;
                        TextBox tb = (Boxs[i].Child as Grid).Children[1] as TextBox;
                        content = cb.SelectedItem.ToString() + ";" + tb.Text;
                        break;
                    case BlockType.ChartLine:
                    case BlockType.ChartPie:
                    case BlockType.ChartBar:
                    case BlockType.ChartArea:
                    case BlockType.ChartColumn:
                        TextBox tb_0 = (Boxs[i].Child as Grid).Children[0] as TextBox;
                        TextBox tb_1 = (Boxs[i].Child as Grid).Children[1] as TextBox;
                        content = tb_0.Text + ";" + tb_1.Text;
                        break;
                    case BlockType.Ticker:
                        TextBox tBTicker_0 = (Boxs[i].Child as Grid).Children[0] as TextBox;
                        TextBox tBTicker_1 = (Boxs[i].Child as Grid).Children[1] as TextBox;
                        content = tBTicker_0.Text + ";" + tBTicker_1.Text;
                        break;
                    case BlockType.Video:
                    case BlockType.Text:
                    case BlockType.Image:
                    case BlockType.Table:
                        content = (Boxs[i].Child as TextBox).Text;
                        break;
                    case BlockType.Service:
                        content = (Boxs[i].Child as ComboBox).SelectedItem.ToString();
                        break;
                    }
                blockOptions.Insert(new Block(content, type));
            }
        }
    }
}
