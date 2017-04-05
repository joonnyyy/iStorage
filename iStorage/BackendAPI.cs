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
        SqlConnection SQLConnection = new SqlConnection("Server=(LocalDb)\\v11.0;Initial Catalog=storage;Integrated Security=SSPI;Trusted_Connection=yes;");

        public BackendAPI()
        {
            try
            {
                SQLConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        //public user_connect()
        //{

        //}


        public user_select()
        {

        }


        public user_update()
        {

        }


        public GF_connect()
        {

        }

        public GF_selelect()
        {

        }


        public GF_change()
        {

        }


        public GF_order()
        {

        }


        public String Login(String user, String pass)
        {
            SqlCommand compareLogin = new SqlCommand("SELECT Manager, Username FROM users WHERE Username="+user+" AND Password="+pass);
            if(compareLogin.ExecuteNonQuery() == 1)
            {
                return "true";
            }else if (compareLogin.ExecuteNonQuery() == 0)
            {
                return "false";
            }
            return "Error";
        }

    }
}
