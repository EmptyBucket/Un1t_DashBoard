namespace UnitDashboard.Service
{
    public class ServiceType
    {
        public const string Weather = "Погода";
        public const string EcxhangeRates = "Курс валют";

        public static readonly string[] ServiceArray = new string[] { Weather, EcxhangeRates }; 
    }
}
