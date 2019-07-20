using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Certificate_generator.DBCON
{
    public class DB_connection
    {

        public SqlConnection con = null;
        public SqlCommand com = null;
        public SqlDataAdapter adapter;
        public DataSet ds;
        public DataTable dt;
        private static string ConString = @"Data Source=PC-13;Initial Catalog=Certificate_Generating_System;User ID=sa;Password=abc123;";


        public DB_connection()
        {
                connection_Sql(); 
        }

        public void connection_Sql()
        {
            con = new SqlConnection(ConString);
            
        }

        public void OpenCon()
        {
            con.Open();
        }

        public void CloseCon()
        {
            con.Close();
        }

        public int insert_del_update(string query)
        {

            OpenCon();
            int line = 0;
            com = new SqlCommand(query, con);
            line = com.ExecuteNonQuery();
            com.Dispose();
            CloseCon();
            return line;
        }

    }
}
