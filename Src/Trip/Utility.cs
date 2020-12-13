using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Threading;
using System.Reflection;

namespace Trip
{
    class Utility
    {
        public static string connString = ConfigurationManager.ConnectionStrings["Trip.Properties.Settings.PersonConnectionString"].ToString();

        public static DataTable FillUser()
        {
            OleDbConnection oleConnection = new OleDbConnection(connString);
            OleDbCommand oleCommand = new OleDbCommand();
            DataTable dt = new DataTable();
            oleCommand.CommandType = System.Data.CommandType.Text;
            oleCommand.Connection = oleConnection;
            String strSQL = "select UserID, Name from User1";

            try
            {
                if (oleConnection.State == ConnectionState.Closed) oleConnection.Open();
                oleCommand.CommandText = strSQL;
                OleDbDataReader oleReader = oleCommand.ExecuteReader();

                dt.Load(oleReader);
                oleConnection.Close();
                return dt;
            }
            catch (InvalidOperationException e)
            {
                MessageBox.Show(e.GetType().FullName + Environment.NewLine + e.Message);
                return null;
            }
            catch (System.Data.OleDb.OleDbException e)
            {
                MessageBox.Show(e.GetType().FullName + Environment.NewLine + e.Message);
                return null;
            }
        }

        public static DataTable FillCity()
        {
            OleDbConnection oleConnection = new OleDbConnection(connString);
            OleDbCommand oleCommand = new OleDbCommand();
            DataTable dt = new DataTable();
            oleCommand.CommandType = System.Data.CommandType.Text;
            oleCommand.Connection = oleConnection;
            String strSQL = "select CityNo, Name from City";

            try
            {
                if (oleConnection.State == ConnectionState.Closed) oleConnection.Open();
                oleCommand.CommandText = strSQL;
                OleDbDataReader oleReader = oleCommand.ExecuteReader();

                dt.Load(oleReader);
                oleConnection.Close();
                return dt;
            }
            catch (InvalidOperationException e)
            {
                MessageBox.Show(e.GetType().FullName + Environment.NewLine + e.Message);
                return null;
            }
            catch (System.Data.OleDb.OleDbException e)
            {
                MessageBox.Show(e.GetType().FullName + Environment.NewLine + e.Message);
                return null;
            }
        }

        public static List<String> GetTrips(string userID)
        {
            OleDbConnection oleConnection = new OleDbConnection(connString);
            OleDbCommand oleCommand = new OleDbCommand();
            DataTable dt = new DataTable();
            oleCommand.CommandType = System.Data.CommandType.Text;
            oleCommand.Connection = oleConnection;
            String strSQL = "select CityNo from Trip where UserID='" + userID + "'";

            try
            {
                if (oleConnection.State == ConnectionState.Closed) oleConnection.Open();
                oleCommand.CommandText = strSQL;
                OleDbDataReader oleReader = oleCommand.ExecuteReader();

                dt.Load(oleReader);
                oleConnection.Close();

                List<String> trips = new List<String>();

                foreach(DataRow dr in dt.Rows)
                {
                    String trip = dr[0].ToString();
                    trips.Add(trip);
                }

                return trips;
            }
            catch (InvalidOperationException e)
            {
                MessageBox.Show(e.GetType().FullName + Environment.NewLine + e.Message);
                return null;
            }
            catch (System.Data.OleDb.OleDbException e)
            {
                MessageBox.Show(e.GetType().FullName + Environment.NewLine + e.Message);
                return null;
            }
        }
    }
}
