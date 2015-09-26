using DataBase.PageOptions;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using UnitDashboard.Service;

namespace UnitDashboard.Page
{
    public class OptionsMenu
    {
        private void AddComboBox()
        {
            Grid externalGrid = new Grid();
            Grid.SetRow(externalGrid, 0);

            for (int i = 0; i < this.valueBlock; i++)
            {   
                externalGrid.RowDefinitions.Add(new RowDefinition());

                Grid internalGrid = new Grid();
                Grid.SetRow(internalGrid, i);
                internalGrid.RowDefinitions.Add(new RowDefinition());
                internalGrid.RowDefinitions.Add(new RowDefinition());
                ColumnDefinition columnDef = new ColumnDefinition();
                columnDef.Width = new System.Windows.GridLength(0.3, System.Windows.GridUnitType.Star);
                internalGrid.ColumnDefinitions.Add(columnDef);
                internalGrid.ColumnDefinitions.Add(new ColumnDefinition());

                Viewbox newViewBox = new Viewbox();
                newViewBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                TextBlock newTextBlock = new TextBlock();
                newTextBlock.FontFamily = new System.Windows.Media.FontFamily("Times new Roman");
                if (i == 0)
                    newTextBlock.Text = "Title";
                else
                    newTextBlock.Text = String.Format("Block_{0}", i - 1);
                newViewBox.Child = newTextBlock;
                Grid.SetRow(newViewBox, 0);
                Grid.SetColumn(newViewBox, 0);
                internalGrid.Children.Add(newViewBox);

                ComboBox newComboBox = new ComboBox();
                newComboBox.Margin = new System.Windows.Thickness(1);
                switch(i)
                {
                    case 0:
                        newComboBox.ItemsSource = BlockType.TitleBlockType;
                        break;
                    case 1:
                        newComboBox.ItemsSource = BlockType.LongBlockType;
                        break;
                    default:
                        newComboBox.ItemsSource = BlockType.CommonBlockType;
                        break;
                }
                newComboBox.SelectedItem = BlockType.Empty;
                newComboBox.SelectionChanged += CBox_SelectionChanged;
                Grid.SetRow(newComboBox, 1);
                Grid.SetColumn(newComboBox, 0);
                internalGrid.Children.Add(newComboBox);

                Border newBorder = new Border();
                newBorder.Margin = new System.Windows.Thickness(1);
                Grid.SetRow(newBorder, 1);
                Grid.SetColumn(newBorder, 1);
                internalGrid.Children.Add(newBorder);

                externalGrid.Children.Add(internalGrid);
            }

            this.grid.Children.Add(externalGrid);
        }

        private void AddCompleteButton()
        {
            Button button = new Button();
            button.Content = "Готово";
            button.Margin = new System.Windows.Thickness(0, 3, 0, 0);
            button.Click += CompleteButton_Click;
            Grid.SetRow(button, 1);
            this.grid.Children.Add(button);
        }

        public OptionsMenu(ref Border box, int valueBlock)
        {
            this.valueBlock = valueBlock;

            this.grid.RowDefinitions.Add(new RowDefinition());
            RowDefinition rowDef = new RowDefinition();
            rowDef.Height = new System.Windows.GridLength(0.1, System.Windows.GridUnitType.Star);
            this.grid.RowDefinitions.Add(rowDef);

            this.AddComboBox();
            this.AddCompleteButton();

            box.Child = this.grid;
        }

        private void CBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Border brother = ((sender as ComboBox).Parent as Grid).Children[2] as Border;
            string type = (sender as ComboBox).SelectedItem.ToString();
            NewControl newControl = new NewControl(ref brother, type);
            Grid.SetColumn(newControl.GetBorder(), 1);
            Grid.SetRow(newControl.GetBorder(), 1);
        }

        private void NextPage()
        {
            var parent = VisualTreeHelper.GetParent(this.grid);
            while (!(parent is System.Windows.Controls.Page))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            System.Windows.Controls.Page page = parent as System.Windows.Controls.Page;

            SelectTemplate selectTemplate = new SelectTemplate(@"Data Source=App_Data\DataBase\PageOptions\PageOptions.sdf");
            int? template = selectTemplate.Select();
            switch (template)
            {
                case TemplateType.template_0:
                    page.NavigationService.Navigate(new Uri("Page/SetPage_0/CompletePage_0.xaml", UriKind.Relative));
                    break;
                case TemplateType.template_1:
                    page.NavigationService.Navigate(new Uri("Page/SetPage_1/CompletePage_1.xaml", UriKind.Relative));
                    break;
                case TemplateType.template_2:
                    break;
                case TemplateType.template_3:
                    break;
                case TemplateType.template_4:
                    break;
                case TemplateType.template_5:
                    break;
            }
        }

        private void CompleteButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ComboBox[] typeArray = new ComboBox[this.valueBlock];
            Border[] contentArray = new Border[this.valueBlock];

            Grid gridBoxs = this.grid.Children[0] as Grid;
            for (int i = 0; i < gridBoxs.Children.Count;i++ )
            {
                Grid box = gridBoxs.Children[i] as Grid;
                typeArray[i] = box.Children[1] as ComboBox;
                contentArray[i] = box.Children[2] as Border;
            }

            SaveDataDB save = new SaveDataDB(typeArray, contentArray);

            this.NextPage();
        }

        private int valueBlock { get; set; }
        private Grid grid = new Grid();
    }

    public class NewControl
    {
        public Border GetBorder()
        {
            return this.box;
        }

        private void AddEmptyControl()
        {
            this.box.Child = null;
        }

        private void AddChartControl()
        {
            Grid newControlGrid = new Grid();
            newControlGrid.ColumnDefinitions.Add(new ColumnDefinition());
            newControlGrid.ColumnDefinitions.Add(new ColumnDefinition());
            TextBox newControlTextBox1 = new TextBox();
            newControlTextBox1.Margin = new System.Windows.Thickness(0, 0, 1, 0);
            TextBox newControlTextBox2 = new TextBox();
            newControlTextBox2.Margin = new System.Windows.Thickness(1, 0, 0, 0);
            newControlTextBox1.TextWrapping = System.Windows.TextWrapping.Wrap;
            newControlTextBox2.TextWrapping = System.Windows.TextWrapping.Wrap;
            newControlTextBox1.Text = "Введите запрос к БД для ключей";
            newControlTextBox2.Text = "Введите запрос к БД для значений ключей";
            Grid.SetColumn(newControlTextBox1, 0);
            Grid.SetColumn(newControlTextBox2, 1);
            newControlGrid.Children.Add(newControlTextBox1);
            newControlGrid.Children.Add(newControlTextBox2);
            this.box.Child = newControlGrid;
        }

        private void AddServiceConrol()
        {
            ComboBox newControlComboBox = new ComboBox();
            newControlComboBox.ItemsSource = ServiceType.ServiceArray;
            newControlComboBox.SelectedItem = ServiceType.Weather;
            this.box.Child = newControlComboBox;
        }

        private void AddVideoControl()
        {
            TextBox newControlTextBox = new TextBox();
            newControlTextBox.TextWrapping = System.Windows.TextWrapping.Wrap;
            newControlTextBox.Text = "Введите путь к видео записи";
            this.box.Child = newControlTextBox;
        }

        private void AddTextControl()
        {
            TextBox newControlTextBox = new TextBox();
            newControlTextBox.TextWrapping = System.Windows.TextWrapping.Wrap;
            newControlTextBox.Text = "Введите текст";
            this.box.Child = newControlTextBox;
        }

        private void AddImageControl()
        {
            TextBox newControlTextBox = new TextBox();
            newControlTextBox.TextWrapping = System.Windows.TextWrapping.Wrap;
            newControlTextBox.Text = "Введите путь к изображению";
            this.box.Child = newControlTextBox;
        }

        private void AddTableControl()
        {
            TextBox newControlTextBox = new TextBox();
            newControlTextBox.TextWrapping = System.Windows.TextWrapping.Wrap;
            newControlTextBox.Text = "Введите запрос к базе данных для таблицы";
            this.box.Child = newControlTextBox;
        }

        private void AddTickerControl()
        {
            Grid newGrid = new Grid();
            newGrid.ColumnDefinitions.Add(new ColumnDefinition());
            ColumnDefinition columnDef = new ColumnDefinition();
            columnDef.Width = new System.Windows.GridLength(0.2, System.Windows.GridUnitType.Star);
            newGrid.ColumnDefinitions.Add(columnDef);
            TextBox tb1 = new TextBox();
            tb1.TextWrapping = System.Windows.TextWrapping.Wrap;
            tb1.Text = "Введите текст для бегущей строки";
            Grid.SetColumn(tb1, 0);
            TextBox tb2 = new TextBox();
            tb2.TextWrapping = System.Windows.TextWrapping.Wrap;
            tb2.Text = "100";
            Grid.SetColumn(tb2, 1);
            newGrid.Children.Add(tb1);
            newGrid.Children.Add(tb2);
            this.box.Child = newGrid;
        }

        private void AddTitleTextControl()
        {
            Grid grid = new Grid();
            ColumnDefinition column = new ColumnDefinition();
            column.Width = new System.Windows.GridLength(0.3, System.Windows.GridUnitType.Star);
            grid.ColumnDefinitions.Add(column);
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            ComboBox comboBox = new ComboBox();
            string[] comboBoxItems = Location.ArrayLocation;
            comboBox.SelectedItem = Location.Centre;
            comboBox.Margin = new System.Windows.Thickness(0, 0, 1, 0);
            comboBox.ItemsSource = comboBoxItems;
            Grid.SetColumn(comboBox, 0);
            grid.Children.Add(comboBox);

            TextBox textBox = new TextBox();
            textBox.Margin = new System.Windows.Thickness(1, 0, 0, 0);
            textBox.TextWrapping = System.Windows.TextWrapping.Wrap;
            textBox.Text = "Введите текст заголовка";
            Grid.SetColumn(textBox, 1);
            grid.Children.Add(textBox);

            this.box.Child = grid;
        }

        private void AddTitleImageControl()
        {
            Grid grid = new Grid();
            ColumnDefinition column = new ColumnDefinition();
            column.Width = new System.Windows.GridLength(0.3, System.Windows.GridUnitType.Star);
            grid.ColumnDefinitions.Add(column);
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            ComboBox comboBox = new ComboBox();
            comboBox.Margin = new System.Windows.Thickness(0, 0, 1, 0);
            string[] comboBoxItems = Location.ArrayLocation;
            comboBox.SelectedItem = Location.Centre;
            comboBox.ItemsSource = comboBoxItems;
            Grid.SetColumn(comboBox, 0);
            grid.Children.Add(comboBox);

            TextBox textBox = new TextBox();
            textBox.TextWrapping = System.Windows.TextWrapping.Wrap;
            textBox.Margin = new System.Windows.Thickness(1, 0, 0, 0);
            textBox.Text = "Введите путь к изображению для заголовка";
            Grid.SetColumn(textBox, 1);
            grid.Children.Add(textBox);

            this.box.Child = grid;
        }

        public NewControl(ref Border box, string type)
        {
            this.box = box;

            switch (type)
            {
                case BlockType.Empty:
                    this.AddEmptyControl();
                    break;
                case BlockType.ChartLine:
                case BlockType.ChartArea:
                case BlockType.ChartBar:
                case BlockType.ChartColumn:
                case BlockType.ChartPie:
                    this.AddChartControl();
                    break;
                case BlockType.Service:
                    this.AddServiceConrol();
                    break;
                case BlockType.Video:
                    this.AddVideoControl();
                    break;
                case BlockType.Ticker:
                    this.AddTickerControl();
                    break;
                case BlockType.Text:
                    this.AddTextControl();
                    break;
                case BlockType.Image:
                    this.AddImageControl();
                    break;
                case BlockType.Table:
                    this.AddTableControl();
                    break;
                case BlockType.TitleText:
                    this.AddTitleTextControl();
                    break;
                case BlockType.TitleImage:
                    this.AddTitleImageControl();
                    break;
            }
        }

        private Border box { get; set; }
    }
}
