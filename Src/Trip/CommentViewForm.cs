using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Trip
{
    public partial class CommentViewForm : Form
    {
        public CommentViewForm()
        {
            InitializeComponent();
        }

        private void Btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_CommentSearch_Click(object sender, EventArgs e)
        {
            string stationName = textStationName.Text;

            string myConnString = System.Configuration.ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(myConnString);
            conn.Open();

            string sql = "select username,id from trip.share where sitename ='" + stationName + "'";
            MySqlCommand sqlCommand = new MySqlCommand(sql, conn);//初始化SQL命令对象
            MySqlDataAdapter sda = new MySqlDataAdapter(sqlCommand);
            DataSet ds = new DataSet();
            sda.Fill(ds, "User_Comment");
            dGV_commentView.DataSource = ds;
            dGV_commentView.DataMember = "User_Comment";
            dGV_commentView.Columns[0].HeaderText = "用户";

            conn.Close();
        }

        private void DGV_CommentView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowindex = e.RowIndex;
            string getUsername = Convert.ToString(dGV_commentView.Rows[rowindex].Cells["username"].Value);
            int getID = Convert.ToInt32(dGV_commentView.Rows[rowindex].Cells["id"].Value);

            string myConnString = System.Configuration.ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(myConnString);
            conn.Open();

            string sql = "select sharetext from trip.share where username ='" + getUsername + "'and id=" + getID;
            MySqlCommand sqlCommand = new MySqlCommand(sql, conn);//初始化SQL命令对象
            MySqlDataReader sqlDataReader = sqlCommand.ExecuteReader(); //执行命令并取出结果

            while (sqlDataReader.Read())
            {
                textComment.ReadOnly = true;
                string userComment = sqlDataReader[0].ToString();
                textComment.Text = userComment;
            }

            conn.Close();
        }
    }
}
