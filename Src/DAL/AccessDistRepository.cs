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
    public class AccessDistRepository : IDistRepository
    {
        //string conn = System.Configuration.ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
        MySqlConnection m_oleConnection = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString);
        MySqlCommand m_oleCommand = new MySqlCommand();

        public List<DistRecord> GetAllWeight()
        {
            try
            {
                if (m_oleConnection.State == ConnectionState.Closed)
                    m_oleConnection.Open();

                string strSQL = "select pointuid, Name, neighbor1fid, neighbor1, dist1, is_end, is_trans, neighbor2fid, neighbor2 ,dist2 from city ORDER BY pointuid";
                m_oleCommand.Connection = m_oleConnection;
                m_oleCommand.CommandText = strSQL;
                MySqlDataReader oleReader = m_oleCommand.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(oleReader);
                oleReader.Close();

                List<DistRecord> trs = null;
                if (dt.Rows.Count > 0)
                {
                    trs = new List<DistRecord>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        if(int.Parse(dr[5].ToString())!=1)//is_end=1的情况下，多记录一个邻接关系
                        {
                            DistRecord tra = new DistRecord();
                            tra.fid1 = dr[0].ToString();
                            tra.fid2 = dr[7].ToString();
                            tra.dist = double.Parse(dr[9].ToString());
                            tra.name1 = dr[1].ToString();
                            tra.name2 = dr[8].ToString();
                            trs.Add(tra);
                        }
                        DistRecord tr = new DistRecord();
                        tr.fid1 = dr[0].ToString();
                        tr.fid2 = dr[2].ToString();
                        tr.dist = double.Parse(dr[4].ToString());
                        tr.name1 = dr[1].ToString();
                        tr.name2 = dr[3].ToString();
                        trs.Add(tr);
                    }
                }

                return trs;

            }
            catch (InvalidOperationException ex)
            {
                throw new Common.UserDBException(ex.Message + " " + "Failed to get weight ");
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw new Common.UserDBException(ex.Message + " " + "Failed to get weight");
            }
            catch
            {
                throw new Common.UserDBException("Failed to get weight");
            }
        }
    }
}
