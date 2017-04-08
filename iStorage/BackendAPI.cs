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
        private SqlConnection _CONNECTION;
        private SqlCommand _COMMAND;


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
                _CONNECTION = new SqlConnection("Server=" + _SERVER + ";AttachDbFilename=" + _DATAPATH + ";Initial Catalog=" + _DATABASE + ";Integrated Security=SSPI;Trusted_Connection=yes;");
                _COMMAND = new SqlCommand();
                _COMMAND.Connection = _CONNECTION;
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
            _COMMAND.CommandText = "SELECT user, manager FROM users WHERE username='"+ user +"' AND password='"+ pass +"'";
            using (_CONNECTION)
            {
                try
                {
                    _CONNECTION.Open();
                    using (SqlDataReader reader = _COMMAND.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {
                            return "loggedin";
                        }
                        else
                        {
                            return "wrong";
                        }


                    }
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
        public List<List<String>> AllArticlesLimitedInfo(out int rows)
        {
            _COMMAND.CommandText = "SELECT id,amount,name,price_sell,category,material FROM articles";


            using (_CONNECTION)
            {
                try
                {
                    _CONNECTION.Open();
                    List<List<String>> data = new List<List<String>>();

                    using (SqlDataReader reader = _COMMAND.ExecuteReader())
                    {
                        rows = 0;
                        while (reader.Read())
                        {
                           List<String> temp = new List<String>();
                           for(int column = 0; column < 6; column++)
                            {
                                temp.Add(reader.GetValue(column).ToString());
                            }
                            data.Add(temp);
                            rows++;
                        }
                    }
                    return data;
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
