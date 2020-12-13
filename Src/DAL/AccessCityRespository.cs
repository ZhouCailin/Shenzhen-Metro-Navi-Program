using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using MySql.Data.MySqlClient;
using System.ComponentModel;

using Trip.IDAL;
using Trip.Model;
using Trip.Common;

namespace Trip.DAL
{
    public class AccessCityRepository : ICityRepository
    {
        //string conn = System.Configuration.ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
        MySqlConnection m_oleConnection = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString);
        MySqlCommand m_oleCommand = new MySqlCommand();

        public List<City> GetAllCity()
        {
            try
            {
                if (m_oleConnection.State == ConnectionState.Closed)
                    m_oleConnection.Open();

                string strSQL = "select cityNo, name from City";

                m_oleCommand.Connection = m_oleConnection;
                m_oleCommand.CommandText = strSQL;
                MySqlDataReader oleReader = m_oleCommand.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(oleReader);
                oleReader.Close();

                List<City> citys = null; 
                if (dt.Rows.Count > 0)
                {
                    citys = new List<City>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        City city = new City();
                        city.cityNo = dr[0].ToString();
                        city.name = dr[1].ToString();
                        citys.Add(city);
                    }
                }

                return citys;
            }
            catch (InvalidOperationException ex)
            {
                throw new Common.UserDBException(ex.Message + " " + "Failed to get citys ");
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw new Common.UserDBException(ex.Message + " " + "Failed to get citys" );
            }
            catch
            {
                throw new Common.UserDBException("Failed to get citys");
            }
        }

        public List<Station>GetStation()
        {
            try
            {
                if (m_oleConnection.State == ConnectionState.Closed)
                    m_oleConnection.Open();

                string strSQL = "select Name,linename,is_trans,interest from City";

                m_oleCommand.Connection = m_oleConnection;
                m_oleCommand.CommandText = strSQL;
                MySqlDataReader oleReader = m_oleCommand.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(oleReader);
                oleReader.Close();

                List<Station> stations = null;
                if (dt.Rows.Count > 0)
                {
                    stations= new List<Station>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Station sta= new Station();
                        sta.name= dr[0].ToString();
                        sta.linename= dr[1].ToString();
                        sta.is_trans = dr[2].ToString();
                        sta.interest = dr[3].ToString();
                        stations.Add(sta);
                    }
                }
                return stations;
            }
            catch (InvalidOperationException ex)
            {
                throw new Common.UserDBException(ex.Message + " " + "Failed to get citys ");
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw new Common.UserDBException(ex.Message + " " + "Failed to get citys");
            }
            catch
            {
                throw new Common.UserDBException("Failed to get citys");
            }

        }
    }
}
