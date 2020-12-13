using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using MySql.Data.MySqlClient;

using Trip.IDAL;
using Trip.Model;
using Trip.Common;

namespace Trip.DAL
{
    public class  AccessTripRepository : ITripRepository
    {
        //string conn = System.Configuration.ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
        MySqlConnection m_oleConnection = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString);
        MySqlCommand m_oleCommand = new MySqlCommand();

        public string TripID;

        public void GetID()//SIGN：为了防止trip主键重合，添加记录时调用这个函数
        {
            try
            {
                if (m_oleConnection.State == ConnectionState.Closed)
                    m_oleConnection.Open();

                string strSQL = "select max(ID)+1 from trip";

                m_oleCommand.Connection = m_oleConnection;
                m_oleCommand.CommandText = strSQL;
                MySqlDataReader oleReader = m_oleCommand.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(oleReader);
                oleReader.Close();

                foreach (DataRow dr in dt.Rows) TripID = dr[0].ToString();

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
        public void Add(TripRecord trip)
        {
            StringBuilder strSQL = new StringBuilder("insert into Trip(ID, UserID, CityNo, ArriveTime, name, memo) values (");//SIGN:ID自动分配
            GetID();
            strSQL.Append(TripID + ", ");
            strSQL.Append(trip.userID + ", ");
            strSQL.Append(trip.cityNo + ", '");
            strSQL.Append(trip.arriveTime.ToString() + "','");
            strSQL.Append(trip.name + "','");
            strSQL.Append(trip.memo + "')");//SIGN:之前memo会出错
            //string x = strSQL.ToString();
//            strSQL.Append(trip.memo + "')");
//            StringBuilder strSQL = new StringBuilder("insert into Trip(UserID, CityNo, ArriveTime, Memo) values ('430102196806092413', '430101','" + trip.arriveTime.ToString() + "',' ')");


            try
            {
                if (m_oleConnection.State == ConnectionState.Closed)
                    m_oleConnection.Open();

                m_oleCommand.Connection = m_oleConnection;
                m_oleCommand.CommandText = strSQL.ToString();
                m_oleCommand.ExecuteNonQuery();
                m_oleConnection.Close();
            }
            catch (InvalidOperationException ex)
            {
                throw new Common.TripDBException(ex.Message + " " + "Failed to add trip: " + trip.userID + " " + trip.cityNo);
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw new Common.TripDBException(ex.Message + " " + "Failed to add trip: " + trip.userID + " " + trip.cityNo);
            }
            catch
            {
                throw new Common.TripDBException("Failed to add trip: " + trip.userID + " " + trip.cityNo);
            }
        }

        public void Delete(TripRecord trip)
        {
            string strSQL;
            try
            {
                if (m_oleConnection.State == ConnectionState.Closed)
                    m_oleConnection.Open();

                strSQL = "delete from Trip where userID = '" + trip.userID + "' and cityNo = '" + trip.cityNo + "'";

                m_oleCommand.Connection = m_oleConnection;
                m_oleCommand.CommandText = strSQL;
                m_oleCommand.ExecuteNonQuery();
            }
            catch (InvalidOperationException ex)
            {
                throw new Common.TripDBException(ex.Message + " " + "Failed to delete trip: " + trip.userID + " " + trip.cityNo);
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw new Common.TripDBException(ex.Message + " " + "Failed to delete trip: " + trip.userID + " " + trip.cityNo);
            }
            catch
            {
                throw new Common.TripDBException("Failed to delete trip: " + trip.userID + " " + trip.cityNo);
            }
        }

        public void Delete(string userID)
        {
            string strSQL;
            try
            {
                if (m_oleConnection.State == ConnectionState.Closed)
                    m_oleConnection.Open();


                strSQL = "delete from Trip where userID = '" + userID + "'";

                m_oleCommand.Connection = m_oleConnection;
                m_oleCommand.CommandText = strSQL;
                m_oleCommand.ExecuteNonQuery();
            }
            catch (InvalidOperationException ex)
            {
                throw new Common.TripDBException(ex.Message + " " + "Failed to delete trip: " + userID);
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw new Common.TripDBException(ex.Message + " " + "Failed to delete trip: " + userID);
            }
            catch
            {
                throw new Common.TripDBException("Failed to delete trip: " + userID);
            }
        }
        
        public void Update(TripRecord b_trip, TripRecord m_trip)//SIGN
        {
            StringBuilder strSQL = new StringBuilder("update trip set userID='");
            strSQL.Append(m_trip.userID + "', cityNo='");
            strSQL.Append(m_trip.cityNo + "', arriveTime='");
            strSQL.Append(m_trip.arriveTime.ToString() + /**/"', name='");
            strSQL.Append(m_trip.name.ToString() + /**/"', memo='");
            strSQL.Append(m_trip.memo + /**/"' where name = '" + b_trip.name + "' and UserID='" + b_trip.userID + "' and arriveTime='" + b_trip.arriveTime.ToString() + "'and memo='"+b_trip.memo+"'");
            string x = strSQL.ToString();

            //strSQL.Append(trip.memo + "' where userID = '" + trip.userID + "' and cityNo = '" + trip.cityNo + "'");

            try
            {
                if (m_oleConnection.State == ConnectionState.Closed)
                    m_oleConnection.Open();

                m_oleCommand.Connection = m_oleConnection;
                m_oleCommand.CommandText = strSQL.ToString();
                m_oleCommand.ExecuteNonQuery();
                m_oleConnection.Close();
            }
            catch (InvalidOperationException ex)
            {
                throw new Common.TripDBException(ex.Message + " " + "Failed to update trip: " + m_trip.userID + " " + m_trip.cityNo);
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw new Common.TripDBException(ex.Message + " " + "Failed to update trip: " + m_trip.userID + " " + m_trip.cityNo);
            }
            catch
            {
                throw new Common.TripDBException("Failed to update trip: " + m_trip.userID + " " + m_trip.cityNo);
            }
        }

        public TripRecord GetTrip(string userID, string cityNo)
        {
            string strSQL;
            try
            {
                if (m_oleConnection.State == ConnectionState.Closed)
                    m_oleConnection.Open();

                strSQL = "select userID, cityNo, arriveTime, memo, name from Trip where userID = '" + userID + "' and cityNo = '" + cityNo + "'";

                m_oleCommand.Connection = m_oleConnection;
                m_oleCommand.CommandText = strSQL;
                MySqlDataReader oleReader = m_oleCommand.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(oleReader);
                oleReader.Close();

                TripRecord tr = null;
                if (dt.Rows.Count > 0)
                {
                    tr = new TripRecord();
                    tr.userID = dt.Rows[0][0].ToString();
                    tr.cityNo = dt.Rows[0][1].ToString();
                    tr.arriveTime = Convert.ToDateTime(dt.Rows[0][2].ToString());
                    tr.memo = dt.Rows[0][3].ToString();
                    tr.memo = dt.Rows[0][4].ToString();
                }

                return tr;

            }
            catch (InvalidOperationException ex)
            {
                throw new Common.TripDBException(ex.Message + " " + "Failed to get trip: " + userID + " " + cityNo);
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw new Common.TripDBException(ex.Message + " " + "Failed to get trip: " + userID + " " + cityNo);
            }
            catch
            {
                throw new Common.TripDBException("Failed to get trip: " + userID + " " + cityNo);
            }

        }

        public List<TripRecord> GetTrips(string userID)
        {
            string strSQL;
            try
            {
                if (m_oleConnection.State == ConnectionState.Closed)
                    m_oleConnection.Open();

                strSQL = "select ID, userID, cityNo, arriveTime, memo, name from Trip where userID = '" + userID + "'";
                m_oleCommand.Connection = m_oleConnection;
                m_oleCommand.CommandText = strSQL;
                MySqlDataReader oleReader = m_oleCommand.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(oleReader);
                oleReader.Close();

                List<TripRecord> trs = null;
                if (dt.Rows.Count > 0)
                {
                    trs = new List<TripRecord>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        TripRecord tr = new TripRecord();
                        tr.ID = dr[0].ToString();
                        tr.userID = dr[1].ToString();
                        tr.cityNo = dr[2].ToString();
                        tr.arriveTime = Convert.ToDateTime(dr[3].ToString());
                        tr.memo = dr[4].ToString();
                        tr.memo = dr[5].ToString();

                        trs.Add(tr);
                    }
                }

                return trs;
            }
            catch (InvalidOperationException ex)
            {
                throw new Common.TripDBException(ex.Message + " " + "Failed to get trips: " + userID);
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw new Common.TripDBException(ex.Message + " " + "Failed to get trips: " + userID);
            }
            catch
            {
                throw new Common.TripDBException("Failed to get trip: " + userID + " " + userID);
            }
        }

        public List<TripRecord> GetTrips(string order, string sort, string search)
        {
            string strSQL;
            string strOrder = order;
            string strSort = sort;

            if (String.IsNullOrWhiteSpace(order))
                strOrder = "userID";
            if (String.IsNullOrWhiteSpace(sort) || sort != "asc" || sort != "desc") 
                strSort = "asc";

            try
            {
                if (m_oleConnection.State == ConnectionState.Closed)
                    m_oleConnection.Open();

                if (String.IsNullOrWhiteSpace(search))
                    strSQL = "select userID, cityNo, arriveTime, memo, name, ID from Trip where true order by " + strOrder + " " + strSort;
                else
                    strSQL = "select userID, cityNo, arriveTime, memo, name, ID from Trip where " + search + " order by " + strOrder + " " + strSort;

                m_oleCommand.Connection = m_oleConnection;
                m_oleCommand.CommandText = strSQL;
                MySqlDataReader oleReader = m_oleCommand.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(oleReader);
                oleReader.Close();

                List<TripRecord> trips = null; 
                if (dt.Rows.Count > 0)
                {
                    trips = new List<TripRecord>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        TripRecord trip = new TripRecord();
                        trip.userID = dr[0].ToString();
                        trip.cityNo = dr[1].ToString();
                        trip.arriveTime = Convert.ToDateTime(dr[2].ToString());
                        //if (string.IsNullOrWhiteSpace(dr[3].ToString())) trip.memo = ；
                        trip.memo = dr[3].ToString();
                        trip.name = dr[4].ToString();
                        trip.ID = dr[5].ToString();

                        trips.Add(trip);
                    }
                }

                return trips;
            }
            catch (InvalidOperationException ex)
            {
                throw new Common.UserDBException(ex.Message + " " + "Failed to get trips " + search);
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw new Common.UserDBException(ex.Message + " " + "Failed to get trips: " + search);
            }
            catch
            {
                throw new Common.UserDBException("Failed to get trips: " + search);
            }
        }
    }
}
