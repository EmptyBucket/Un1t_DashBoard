using System.Data.SqlServerCe;

namespace DataBase.PageOptions
{
    public class SelectTemplate
    {
        public SelectTemplate(string connStr)
        {
            SelectTemplate._connectionString = new SqlCeConnection(connStr);
            SelectTemplate._connectionString.Open();
        }

        ~SelectTemplate()
        {
            _connectionString.Close();
        }

        public int Insert(int template)
        {
            SqlCeCommand Insert = new SqlCeCommand("INSERT INTO SelectTemplate (Number) VALUES (@Number)", SelectTemplate._connectionString);
            Insert.Parameters.AddWithValue("@Number", template);
            int returnQuery = Insert.ExecuteNonQuery();
            return returnQuery;
        }

        public int? Select()
        {
            SqlCeCommand Select = new SqlCeCommand("SELECT * FROM SelectTemplate", SelectTemplate._connectionString);
            SqlCeDataReader reader = Select.ExecuteReader();
            reader.Read();
            int? readNumberTemplate;
            try
            {
                readNumberTemplate = reader.GetInt32(reader.GetOrdinal("Number"));
            }
            catch
            {
                readNumberTemplate = null;
            }
            return readNumberTemplate;
        }

        public int Delete()
        {
            SqlCeCommand Delete = new SqlCeCommand("DELETE FROM SelectTemplate", SelectTemplate._connectionString);
            int returnQuery = Delete.ExecuteNonQuery();
            return returnQuery;
        }        
        private static SqlCeConnection _connectionString;
    }

    public class TemplateType
    {
        public const int template_0 = 0;
        public const int template_1 = 1;
        public const int template_2 = 2;
        public const int template_3 = 3;
        public const int template_4 = 4;
        public const int template_5 = 5;
    }
}
