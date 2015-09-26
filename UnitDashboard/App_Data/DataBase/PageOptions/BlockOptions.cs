using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;

namespace DataBase.PageOptions
{
    public class BlockOptions
    {
        public BlockOptions(string connStr)
        {
            BlockOptions._connectionString = new SqlCeConnection(connStr);
            BlockOptions._connectionString.Open();
        }

        ~BlockOptions()
        {
            _connectionString.Close();
        }

        public int Insert(Block bl)
        {
            SqlCeCommand Insert = new SqlCeCommand("INSERT INTO BlockOptions (Content, Type) VALUES (@Content, @Type)", BlockOptions._connectionString);
            Insert.Parameters.AddWithValue("@Content", bl.content);
            Insert.Parameters.AddWithValue("@Type", bl.type);
            int returnQuery = Insert.ExecuteNonQuery();
            return returnQuery;
        }

        public int Count()
        {
            SqlCeCommand Count = new SqlCeCommand("SELECT COUNT(*) FROM BlockOptions", BlockOptions._connectionString);
            int count = Convert.ToInt32(Count.ExecuteScalar().ToString());
            return count;
        }

        public Block[] Select()
        {
            SqlCeCommand Select = new SqlCeCommand("SELECT * FROM BlockOptions", BlockOptions._connectionString);
            SqlCeDataReader reader = Select.ExecuteReader();
            List<Block> bl = new List<Block>();
            while (reader.Read())
            {
                string readContent = reader.GetString(reader.GetOrdinal("Content"));
                string readType = reader.GetString(reader.GetOrdinal("Type"));
                bl.Add(new Block(readContent, readType));
            }
            return bl.ToArray();
        }

        public Block SelectNumber(int number)
        {
            SqlCeCommand SelectNumber = new SqlCeCommand("SELECT * FROM BlockOptions", BlockOptions._connectionString);
            SqlCeDataReader reader = SelectNumber.ExecuteReader();
            for (int i = 0; i <= number; i++)
                reader.Read();
            string readContent = reader.GetString(reader.GetOrdinal("Content"));
            string readType = reader.GetString(reader.GetOrdinal("Type"));
            Block bl = new Block(readContent, readType);
            return bl;
        }

        public int Delete()
        {
            SqlCeCommand Delete = new SqlCeCommand("DELETE FROM BlockOptions", BlockOptions._connectionString);
            int returnQuery = Delete.ExecuteNonQuery();
            return returnQuery;
        }

        private static SqlCeConnection _connectionString;
    }
}
