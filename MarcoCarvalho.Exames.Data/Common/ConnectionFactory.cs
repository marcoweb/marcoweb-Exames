using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data.Common;

namespace MarcoCarvalho.Exames.Data.Common
{
    public static class ConnectionFactory
    {
        private static string host;
        private static string dbName;
        private static string port;
        private static string user;
        private static string password;

        public static void executeCreationScript(string sql)
        {
            string connectionString = "Server=" + host + ";User Id=" + user + ";Password=" + password;
            using (DbConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                DbCommand command = connection.CreateCommand();
                command.CommandText = "CREATE DATABASE " + dbName;
                command.ExecuteNonQuery();
            }
            using(DbConnection connection = GetConnection())
            {
                connection.Open();
                DbCommand command = connection.CreateCommand();
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
        }

        public static void setConfiguration(string _host, string _dbName, string _port, string _user, string _password)
        {
            host = _host;
            dbName = _dbName;
            port = _port;
            user = _user;
            password = _password;
        }

        public static List<string> GetTables()
        {
            using(DbConnection connection = GetConnection())
            {
                List<string> tables = new List<string>();
                connection.Open();
                DbCommand command = connection.CreateCommand();
                command.CommandText = "SHOW TABLES";
                DbDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tables.Add(reader.GetString(0));
                    }
                }
                return tables;
            }
        }

        public static DbConnection GetConnection()
        {
            //string connectionString = "Database=exames;Data Source=localhost;User Id=root;Password=MySQL#Root";
            string connectionString = "Database="+dbName+";Data Source="+host+";User Id="+user+";Password="+password;
            return new MySqlConnection(connectionString);
        }
    }
}
