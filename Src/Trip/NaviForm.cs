using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Trip.Model;
using Trip.BLL;


namespace Trip
{
    public partial class NaviForm : Form
    {
        private DistBLL m_distbll;
        private double paDis = 0;
        public NaviForm(DistBLL m_distBLL)
        {
            InitializeComponent();
            m_distbll = m_distBLL;
            //init();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void NaviForm_Load(object sender, EventArgs e)
        {
            //实现下拉
            string conn = System.Configuration.ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection m_oleConnection = new MySqlConnection(conn);
            MySqlCommand m_oleCommand = new MySqlCommand();
                if (m_oleConnection.State == ConnectionState.Closed)
                    m_oleConnection.Open();

                string strSQL = "select name from station";

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
                        this.comboBox1.Items.Add(dr[0].ToString());
                        this.comboBox2.Items.Add(dr[0].ToString());
                    }
                }
        }
        public void init()
        {
            string conn = System.Configuration.ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection m_oleConnection = new MySqlConnection(conn);
            MySqlCommand m_oleCommand = new MySqlCommand();
            try
            {
                m_oleConnection.Open();
                string sql = "select name,linename,is_trans,interest from city";
                MySqlCommand cmd = new MySqlCommand(sql, m_oleConnection);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int index = this.dataGridView1.Rows.Add(); 
                    this.dataGridView1.Rows[index].Cells[0].Value = reader.GetString("name");
                    this.dataGridView1.Rows[index].Cells[1].Value = reader.GetString("linename");
                    this.dataGridView1.Rows[index].Cells[2].Value = reader.GetInt32("is_trans");
                    this.dataGridView1.Rows[index].Cells[3].Value = reader.GetString("interest");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                m_oleConnection.Close();
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void fill(Stack opstk){
            int i = 1;
            string stkOut;
            string P_Str_ConnectionStr = System.Configuration.ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            string P_Str_SqlStr = "select name as 姓名,linename as 线路, (case when is_trans=1 then '换乘站' else '中间站' end) as 换乘提示,interest as 资讯 from city where pointuid in (";
            string Sqlstr2 = ") order by case pointuid when ";
            
            stkOut = opstk.Pop().ToString();
            P_Str_SqlStr = P_Str_SqlStr + stkOut;
            Sqlstr2 = Sqlstr2 + stkOut + " then " + Convert.ToString(i) + " ";
            while(opstk.Count!=0){
                i++;
                stkOut=opstk.Pop().ToString();
                Sqlstr2 = Sqlstr2 + " when " + stkOut + " then " + Convert.ToString(i);
                P_Str_SqlStr =P_Str_SqlStr+","+ stkOut;
            }
            Sqlstr2 = Sqlstr2 + " end";
            P_Str_SqlStr = P_Str_SqlStr + Sqlstr2;

            MySqlDataAdapter adapter = new MySqlDataAdapter(P_Str_SqlStr,P_Str_ConnectionStr);
            DataTable P_dt = new DataTable();
            adapter.Fill(P_dt);
            this.dataGridView1.DataSource = P_dt;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            var outputPath= new Stack();
            outputPath = m_distbll.getPath(this.comboBox1.Text, this.comboBox2.Text, ref paDis);
            var opPathCopy = new Stack(outputPath);
            fill(outputPath);

            //输出路径到MainForm画图
            var oneSta = m_distbll.id2string(Convert.ToInt32(opPathCopy.Pop().ToString()));
            MainForm.opPath.Add(oneSta);
            while (opPathCopy.Count != 0)
            {
                oneSta = m_distbll.id2string(Convert.ToInt32(opPathCopy.Pop().ToString()));
                if (!MainForm.opPath.Contains(oneSta)) MainForm.opPath.Add(oneSta);
            }
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
