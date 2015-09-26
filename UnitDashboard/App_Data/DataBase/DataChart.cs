namespace UnitDashboard.App_Data.DataBase.Staff
{
    public class DataChart
    {
        public DataChart(string key, double value)
        {
            this.key = key;
            this.value = value;
        }
        public readonly string key;
        public readonly double value;
    }
}
