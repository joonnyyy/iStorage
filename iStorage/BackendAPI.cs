using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace iStorage
{
    /// <summary>
    /// Contains all the functions connecting, writing, changing and reading the database
    /// </summary>
    class BackendAPI
    {
        //Define class wide variables.
        //Mainly those needed for connection to SQL.
        private string _SERVER = "(LocalDb)\\MSSQLLocalDB";
        private string _FILEPATH = "storage.mdf";
        private string _DATABASE = "storage";
        private string _DATAPATH;
        private string _CONNECTIONSTRING;
        private SqlConnection _CONNECTION;

        /// <summary>
        /// Calls class internal function that creates an instance of the BackendAPI and intializes the variablese for database connection.
        /// <para>Parameters with an empty string keep default values.</para>
        /// </summary>
        /// <param name="server">Used to define the SQL server. "(LocalDb)\\MSSQLLocalDB" if local file</param>
        /// <param name="filepath">Used to define the name of the database file contained in ..AppDir\Data\</param>
        /// <param name="table">Used to dfine the first used databse</param>
        /// <exception cref="BackendConnectionException"></exception>
        public BackendAPI(string server, string filepath, string table)
        {
            InternalConstructor(server, filepath, table);
        }

        /// <summary>
        /// Calls class internal function that creates an instance of the BackendAPI and intializes the variablese for database connection.
        /// <para>Parameters with an empty string keep default values.</para>
        /// </summary>
        /// <param name="server">Used to define the SQL server. "(LocalDb)\\MSSQLLocalDB" if local file</param>
        /// <param name="filepath">Used to define the name of the database file contained in ..AppDir\Data\</param>
        /// <exception cref="BackendConnectionException"></exception>
        public BackendAPI(string server, string filepath)
        {
            InternalConstructor(server, filepath,_DATABASE);
        }

        /// <summary>
        /// Calls class internal function that creates an instance of the BackendAPI and intializes the variablese for database connection.
        /// <para>Parameters with an empty string keep default values.</para>
        /// </summary>
        /// <param name="server">Used to define the SQL server. "(LocalDb)\\MSSQLLocalDB" if local file</param>
        /// <exception cref="BackendConnectionException"></exception>
        public BackendAPI(string server)
        {
            InternalConstructor(server, _FILEPATH, _DATABASE);
        }

        /// <summary>
        /// Calls class internal function that creates an instance of the BackendAPI and intializes the variablese for database connection.
        /// </summary>
        /// <exception cref="BackendConnectionException"></exception>
        public BackendAPI()
        {
            InternalConstructor(_SERVER, _FILEPATH, _DATABASE);
        }

        /// <summary>
        /// Creates an instance of the BackendAPI and intializes the variablese for database connection.
        /// </summary>
        /// <exception cref="BackendConnectionException"></exception>
        private void InternalConstructor(string server, string filepath, string table)
        {
            try
            {
                if(server != "") _SERVER = server;
                if(filepath != "" )_FILEPATH = filepath;
                if(table != "")_DATABASE = table;
                _DATAPATH = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Data\\" + _FILEPATH;
                _CONNECTIONSTRING = "Server=" + _SERVER + ";AttachDbFilename=" + _DATAPATH + ";Initial Catalog=" + _DATABASE + ";Integrated Security=SSPI;Trusted_Connection=yes;";
            }
            catch (Exception e)
            {
                throw new BackendConnectionException("Failed creating database connection: " + e.Message);
            }
        }

        /// <summary>
        /// Returns "loggeding" when paramteres match a user in the database and "wrong" if not.
        /// </summary>
        /// <exception cref="BackendConnectionException"></exception>
        public String Login(String user, String pass)
        {
            string Return;

            using (_CONNECTION = new SqlConnection(_CONNECTIONSTRING))
            {
                try
                {
                    _CONNECTION.Open();
                    using (SqlCommand _COMMAND = new SqlCommand("SELECT user, manager FROM users WHERE username='" + user + "' AND password='" + pass + "'", _CONNECTION))
                    {
                        using (SqlDataReader reader = _COMMAND.ExecuteReader())
                        {
                            if (reader.HasRows)
                                Return = "loggedin";
                            else
                                Return = "wrong";
                        }
                    }
                    return Return;
                }
                catch (Exception e)
                {
                    throw new BackendConnectionException("SQL Read failed: " + e.Message);
                }
            }
        }

        /// <summary>
        /// Returns all rows from articles, selecting columns id,amount,name,price_sell,category,material 
        /// </summary>
        /// <param name="rows">Returns the number of rows retrieved</param>
        /// <exception cref="BackendConnectionException"></exception>
        public List<List<String>> GetFromArticles(out int rows)
        {
            List<List<String>> Data = new List<List<String>>();
            rows = 0;
            return Data = GetFromArticles("SELECT id,amount,name,price_sell,category,material","",out rows);
        }

        /// <summary>
        /// Returns all rows from articles, selecting what SELECT field1,field2,field3 String you give in the selectstatement parameter
        /// </summary>
        /// <param name="rows">Returns the number of rows retrieved</param>
        /// <exception cref="BackendConnectionException"></exception>
        public List<List<String>> GetFromArticles(string select,out int rows)
        {
            List<List<String>> Data = new List<List<String>>();
            rows = 0;
            return Data = GetFromArticles(select,"", out rows);
        }

        /// <summary>
        /// Returns all rows from articles, selecting what field1,field2,field3 and condition String you give in the selectstatement and wherestatement parameter
        /// </summary>
        /// <param name="rows">Returns the number of rows retrieved</param>
        /// <exception cref="BackendConnectionException"></exception>
        public List<List<String>> GetFromArticles(string select,string condition, out int rows)
        {
            List<List<String>> Data = new List<List<String>>();
            rows = 0;
            if(condition != "")
            {
                string temp = condition;
                condition = "WHERE" + temp;
            }

            using (_CONNECTION = new SqlConnection(_CONNECTIONSTRING))
            {
                try
                {
                    _CONNECTION.Open();
                    using (SqlCommand _COMMAND = new SqlCommand("SELECT"+ select +"FROM articles"+ condition, _CONNECTION))
                    {
                        using (SqlDataReader reader = _COMMAND.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                List<String> temp = new List<String>();
                                for (int column = 0; column < reader.FieldCount; column++)
                                {
                                    temp.Add(reader.GetValue(column).ToString());
                                }
                                Data.Add(temp);
                                rows++;
                            }
                        }
                    }
                    return Data;
                }
                catch (Exception e)
                {
                    throw new BackendConnectionException("SQL Read failed: " + e.Message);
                }
            }
        }

        /// <summary>
        /// Returns rows from a table, selecting what SELECT field1,field2,field3 
        /// </summary>
        /// <param name="rows">Returns the number of rows retrieved</param>
        /// <exception cref="BackendConnectionException"></exception>
        public List<List<String>> GetFromDatabase(string select,string table, string condition, out int rows)
        {
            List<List<String>> Data = new List<List<String>>();
            rows = 0;
            if (condition != "")
            {
                string temp = condition;
                condition = "WHERE" + temp;
            }

            using (_CONNECTION = new SqlConnection(_CONNECTIONSTRING))
            {
                try
                {
                    _CONNECTION.Open();
                    using (SqlCommand _COMMAND = new SqlCommand("SELECT"+ select +"FROM"+ table + condition, _CONNECTION))
                    {
                        using (SqlDataReader reader = _COMMAND.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                List<String> temp = new List<String>();
                                for (int column = 0; column < reader.FieldCount; column++)
                                {
                                    temp.Add(reader.GetValue(column).ToString());
                                }
                                Data.Add(temp);
                                rows++;
                            }
                        }
                    }
                    return Data;
                }
                catch (Exception e)
                {
                    throw new BackendConnectionException("SQL Read failed: " + e.Message);
                }
            }
        }

        /// <summary>
        /// Is called when something goes wrong on connecting to the database.
        /// </summary>
        [System.Serializable]
        public class BackendConnectionException : Exception
        {
            public BackendConnectionException() { }
            public BackendConnectionException(string message) : base(message) { }
            public BackendConnectionException(string message, Exception inner) : base(message, inner) { }
            protected BackendConnectionException(
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        }

    }
}
