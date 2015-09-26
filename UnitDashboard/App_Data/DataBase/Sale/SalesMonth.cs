using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;

namespace UnitDashboard.App_Data.DataBase.Sale
{
    public class SalesMonth
    {
        public SalesMonth(string connStr)
        {
            SalesMonth._connectionString = new SqlCeConnection(connStr);
            SalesMonth._connectionString.Open();
        }

        ~SalesMonth()
        {
            _connectionString.Close();
        }

        public int Count()
        {
            SqlCeCommand Count = new SqlCeCommand("SELECT COUNT(*) FROM SalesMonth", SalesMonth._connectionString);
            int count = Convert.ToInt32(Count.ExecuteScalar().ToString());
            return count;
        }

        public void InsertRandom(int value)
        {
            Random rand = new Random();
            int month = 1;
            int age = 2000;
            for (int i = 0; i < value; i++)
            {
                SqlCeCommand Insert = new SqlCeCommand("INSERT INTO SalesMonth (Sale, Date) VALUES (@Sale, @Date)", SalesMonth._connectionString);
                Insert.Parameters.AddWithValue("@Sale", rand.Next(1, 100));
                Insert.Parameters.AddWithValue("@Date", new DateTime(age, month, 1));
                Insert.ExecuteNonQuery();
                if (month == 12)
                {
                    month = 1;
                    age++;
                }
                else
                    month++;
            }
        }

        private static SqlCeConnection _connectionString { set; get; }
    }
}
