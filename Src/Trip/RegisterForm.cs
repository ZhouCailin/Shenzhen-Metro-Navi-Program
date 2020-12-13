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
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void Btn_Confirm_Click(object sender, EventArgs e)
        {
            string userID = this.textUserID.Text;
            string userName = this.textUserName.Text;
            int userAge = Convert.ToInt32(this.textUserAge.Text);
            string userCity = this.textUserCity.Text;
            string userSumName = this.textUserSumName.Text;
            string userSex = Convert.ToString('男');
            if(radBtnWomen.Checked)
            {
                userSex = Convert.ToString('女');
            }
            string userPassword = this.textPassword.Text;
            string confirmPassword = this.textComfirmPassword.Text;

            try
            {
                if (!confirmPassword.Equals(userPassword))
                {
                    MessageBox.Show("两次输入的密码不相同，请重新输入!");
                }
                else
                {
                    string myConnString = System.Configuration.ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                    MySqlConnection conn = new MySqlConnection(myConnString);
                    conn.Open();

                    string sql = "Insert into trip.user1 VALUES(" + userID + ",'" + userName + "' ,'" + userSex + "'," + userAge
                        + ",(Select id from trip.station where name='" + userCity + "'),'" + userSumName + "','" + userPassword + "')";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("注册成功");

                    this.Close();
                    conn.Close();
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException)
            {// 验证是否身份证号重复
                MessageBox.Show(this, "用户名已存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void Btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
