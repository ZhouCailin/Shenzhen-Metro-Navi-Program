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
    public class  AccessUserRepository : IUserRepository
    {
        //string conn = System.Configuration.ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
        MySqlConnection m_oleConnection = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString);
        MySqlCommand m_oleCommand = new MySqlCommand();

        public void Add(User user)
        {
            StringBuilder strSQL = new StringBuilder("insert into User1(UserID, Name, Sex, Age, CityNo, summary) values (");
            strSQL.Append("'" + user.userID + "','");
            strSQL.Append(user.name + "','");
            strSQL.Append(user.sex + "',");
            strSQL.Append(user.age + ",'");
            strSQL.Append(user.cityNo + "','");
            strSQL.Append(user.summary + "')");

            try
            {
                if (m_oleConnection.State == ConnectionState.Closed)
                    m_oleConnection.Open();

                m_oleCommand.Connection = m_oleConnection;
                m_oleCommand.CommandText = strSQL.ToString();
                m_oleCommand.ExecuteNonQuery();
                m_oleConnection.Close();
                return;
            }
            catch (InvalidOperationException ex)
            {
                throw new Common.UserDBException(ex.Message + " " + "Failed to add user: " + user.userID);
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw new Common.UserDBException(ex.Message + " " + "Failed to add user: " + user.userID);
            }
            catch
            {
                throw new Common.UserDBException("Failed to add user: " + user.userID);
            }
        }

        public void Delete(string userID)
        {
            string strSQL;
            try
            {
                if (m_oleConnection.State == ConnectionState.Closed)
                    m_oleConnection.Open();

                m_oleCommand.Connection = m_oleConnection;
                strSQL = "delete from User1 where userID = '" + userID + "'";
                m_oleCommand.CommandText = strSQL;
                m_oleCommand.ExecuteNonQuery();
            }
            catch (InvalidOperationException ex)
            {
                throw new Common.UserDBException(ex.Message + " " + "Failed to delete user: " + userID);
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw new Common.UserDBException(ex.Message + " " + "Failed to delete user: " + userID);
            }
            catch
            {
                throw new Common.UserDBException("Failed to delete trip: " + userID);
            }
        }

        public void Update(User user)
        {
            StringBuilder strSQL = new StringBuilder("update User1 set userID='");
            strSQL.Append(user.userID + "', name='");
            strSQL.Append(user.name + "', sex='");
            strSQL.Append(user.sex + "', age=");
            strSQL.Append(user.age.ToString() + ", cityNo='");
            strSQL.Append(user.cityNo + "', summary='");
            strSQL.Append(user.summary + "' where userID = '" + user.userID + "'");

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
                throw new Common.UserDBException(ex.Message + " " + "Failed to update user: " + user.userID);
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw new Common.UserDBException(ex.Message + " " + "Failed to update user: " + user.userID);
            }
            catch
            {
                throw new Common.UserDBException("Failed to update user: " + user.userID);
            }
        }

        public User GetUser(string userID)
        {
            string strSQL;
            try
            {
                if (m_oleConnection.State == ConnectionState.Closed)
                    m_oleConnection.Open();

                m_oleCommand.Connection = m_oleConnection;
                strSQL = "select userID, name, sex, age, cityNo, summary from User1 where userID = '" + userID + "'";
                m_oleCommand.CommandText = strSQL;
                MySqlDataReader oleReader = m_oleCommand.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(oleReader);
                oleReader.Close();

                User user = null;
                if (dt.Rows.Count > 0)
                {
                    user = new User();
                    user.userID = dt.Rows[0][0].ToString();
                    user.name = dt.Rows[0][1].ToString();
                    user.sex = dt.Rows[0][2].ToString();
                    user.age = Convert.ToInt32(dt.Rows[0][3].ToString());
                    user.cityNo = dt.Rows[0][4].ToString();
                    user.summary = dt.Rows[0][5].ToString();
                }

                return user;
            }
            catch (InvalidOperationException ex)
            {
                throw new Common.UserDBException(ex.Message + " " + "Failed to get user: " + userID);
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw new Common.UserDBException(ex.Message + " " + "Failed to get user: " + userID);
            }
            catch
            {
                throw new Common.UserDBException("Failed to get user: " + userID);
            }

        }

        public List<User> GetUsers(string order, string sort, string search)
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
                    strSQL = "select userID, name, sex, age, cityNo, summary from User1 order by " + strOrder + " " + strSort;
                else
                    strSQL = "select userID, name, sex, age, cityNo, summary from User1 where " + search + " order by " + strOrder + " " + strSort;

                m_oleCommand.Connection = m_oleConnection;
                m_oleCommand.CommandText = strSQL;
                MySqlDataReader oleReader = m_oleCommand.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(oleReader);
                oleReader.Close();

                List<User> users = null; 
                if (dt.Rows.Count > 0)
                {
                    users = new List<User>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        User user = new User();
                        user.userID = dr[0].ToString();
                        user.name = dr[1].ToString();
                        user.sex = dr[2].ToString();
                        user.age = Convert.ToInt32(dr[3].ToString());
                        user.cityNo = dr[4].ToString();
                        user.summary = dr[5].ToString();

                        users.Add(user);
                    }
                }

                return users;
            }
            catch (InvalidOperationException ex)
            {
                throw new Common.UserDBException(ex.Message + " " + "Failed to get users " + search);
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw new Common.UserDBException(ex.Message + " " + "Failed to get user: " + search);
            }
            catch
            {
                throw new Common.UserDBException("Failed to get user: " + search);
            }
        }

    }
}
