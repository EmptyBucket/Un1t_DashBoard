using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;

namespace UnitDashboard.App_Data.DataBase.Staff
{
    public class Employees
    {
        public Employees(string connStr)
        {
            Employees._connectionString = new SqlCeConnection(connStr);
            Employees._connectionString.Open();
        }

        ~Employees()
        {
            _connectionString.Close();
        }

        public int Insert(DataChart worker)
        {
            SqlCeCommand Insert = new SqlCeCommand("INSERT INTO Employees (Name, Sale) VALUES (@Name, @Sale)", Employees._connectionString);
            Insert.Parameters.AddWithValue("@Name", worker.key);
            Insert.Parameters.AddWithValue("@Sale", worker.value);
            int returnQuery = Insert.ExecuteNonQuery();
            return returnQuery;
        }

        public DataChart[] Select()
        {
            SqlCeCommand Select = new SqlCeCommand("SELECT * FROM Employees", Employees._connectionString);
            SqlCeDataReader reader = Select.ExecuteReader();
            List<DataChart> worker = new List<DataChart>();
            while (reader.Read())
            {
                int numOrdinal = reader.GetOrdinal("Name");
                string name = reader.GetString(numOrdinal);
                numOrdinal = reader.GetOrdinal("Sale");
                double sale = reader.GetDouble(numOrdinal);
                worker.Add(new DataChart(name, sale));
            }
            return worker.ToArray();
        }

        public void FillRandom(int value)
        {
            Random rand = new Random();
            for (int i = 0; i < value; i++)
            {
                string randomName = rand.Next(1, 1000000).ToString();
                float randomSale = rand.Next(1, 100);
                DataChart worker = new DataChart(randomName, randomSale);
                this.Insert(worker);
            }
        }
        public int Delete()
        {
            SqlCeCommand Delete = new SqlCeCommand("DELETE FROM Employees", Employees._connectionString);
            int returnQuery = Delete.ExecuteNonQuery();
            return returnQuery;
        }

        private static SqlCeConnection _connectionString { set; get; }
    }
}
