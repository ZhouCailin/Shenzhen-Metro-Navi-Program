using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using MySql.Data.MySqlClient;
namespace Trip.Common
{
    public class Utility
    {
        public static string connString = ConfigurationManager.ConnectionStrings["Trip.Properties.Settings.PersonConnectionString"].ToString();
        //public static string connString = "server=localhost;user=root;password=2333; database=trip;";

        public static DataTable FillCity()
        {
            MySqlConnection SqlConnection = new MySqlConnection(connString);
            MySqlCommand SqlCommand = new MySqlCommand() ;
            DataTable dt = new DataTable();
            SqlCommand.CommandType = System.Data.CommandType.Text;
            SqlCommand.Connection = SqlConnection;
            String strSQL = "select CityNo, Name from City";

            try
            {
                if (SqlConnection.State == ConnectionState.Closed) SqlConnection.Open();
                SqlCommand.CommandText = strSQL;
                MySqlDataReader SqlReader = SqlCommand.ExecuteReader();

                dt.Load(SqlReader);
                SqlConnection.Close();
                return dt;
            }
            catch (InvalidOperationException e)
            {
                return null;
            }
            catch (System.Data.OleDb.OleDbException e)
            {
                return null;
            }
        }
    }
}
