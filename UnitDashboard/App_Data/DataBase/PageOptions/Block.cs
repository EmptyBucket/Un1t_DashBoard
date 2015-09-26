using System.Collections.Generic;
namespace DataBase.PageOptions
{
    public class Block
    {
        public Block(string content, string type)
        {
            this.content = content;
            this.type = type;
        }
        public readonly string content;
        public readonly string type;
    }

    public class BlockType
    {
        public const string Empty = "Пусто";
        public const string ChartBar = "Вертикальная гистограмма";
        public const string ChartArea = "График площадями";
        public const string ChartLine = "График линиями";
        public const string ChartColumn = "Горизонтальная гистограмма";
        public const string ChartPie = "Диаграмма";
        public const string Text = "Текст";
        public const string Table = "Таблица";
        public const string Image = "Изображение";
        public const string Video = "Видео";
        public const string Service = "Сервис";
        public const string Ticker = "Бегущая строка";

        public static readonly string[] CommonBlockType = new string[] { Empty, ChartLine, ChartBar, ChartColumn, ChartPie, ChartArea, Text, Table, Image, Video, Service };
        public static readonly string[] LongBlockType = new string[] { Empty, Text, Ticker, Image, Video };

        public const string TitleText = "Текст заголовка";
        public const string TitleImage = "Изображение заголовка";
        public static readonly string[] TitleBlockType = new string[] { Empty, TitleText, TitleImage };
    }

    public class Location
    {
        public const string Left = "Слева";
        public const string Centre = "В центре";
        public const string Right = "Справа";

        public static readonly string[] ArrayLocation = new string[] { Left, Centre, Right };
    }
}
