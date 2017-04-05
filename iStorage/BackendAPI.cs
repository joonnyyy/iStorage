using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace iStorage
{
    class BackendAPI
    {
        string _DATAPATH;
        SqlConnection _CONNECTION;
        SqlCommand _COMMAND;

        public BackendAPI()
        {
            try
            {
                _DATAPATH = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Data\\storage.mdf";
                _CONNECTION = new SqlConnection("Server=(LocalDb)\\MSSQLLocalDB;AttachDbFilename="+ _DATAPATH +";Initial Catalog=storage;Integrated Security=SSPI;Trusted_Connection=yes;");
                _COMMAND = new SqlCommand();
                _COMMAND.Connection = _CONNECTION;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public String Login(String user, String pass)
        {
            _COMMAND.CommandText = "SELECT user, manager FROM users WHERE username='"+ user +"' AND password='"+ pass +"'";
            _CONNECTION.Open();
            using (SqlDataReader reader = _COMMAND.ExecuteReader())
            {
                try
                {
                    if (reader.HasRows)
                    {
                        _CONNECTION.Close();
                        return "loggedin";
                    }
                    else
                    {
                        _CONNECTION.Close();
                        return "wrong";
                    }
                }
                catch(Exception e)
                {
                    _CONNECTION.Close();
                    return "<Error: "+ e.Message +">";
                }
                
            }
        }


    }
}
