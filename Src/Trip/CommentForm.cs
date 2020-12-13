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
    public partial class CommentForm : Form
    {
        public CommentForm()
        {
            InitializeComponent();
        }

        private void Btn_comment_Click(object sender, EventArgs e)
        {
            string userName = LoginForm.loginUser;
            string stationName = textStationName.Text;
            string lineName = textLineName.Text;
            string commentContent = textCommentContent.Text;

            string myConnString = System.Configuration.ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
            MySqlConnection conn = new MySqlConnection(myConnString);
            conn.Open();

            string sql = "Insert Into trip.share Value(NULL,(SELECT UserID from trip.user1 where Name='" + userName + "'),'" + userName
                + "',(SELECT CityNo from trip.city where Name='" + stationName
                + "'and linename='" + lineName + "'),'" + stationName + "','" + commentContent + "')";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            MessageBox.Show("评论已发表！", "Tips", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            this.Close();
            conn.Close();
        }

        private void Btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
