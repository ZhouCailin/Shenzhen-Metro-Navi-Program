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
    public partial class LoginForm : Form
    {
        public bool closefig = false;
        public static string loginUser;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void Btn_login_Click(object sender, EventArgs e)
        {
            string userName = this.textUserName.Text;
            string userPassword = this.textPassword.Text;

            if (userName.Equals("") || userPassword.Equals(""))
            {//验证用户输入是否为空值
                MessageBox.Show("用户名或密码不能为空！");
            }
            else
            {
                string myConnString = System.Configuration.ConfigurationManager.ConnectionStrings["MysqlConnection"].ConnectionString;
                MySqlConnection conn = new MySqlConnection(myConnString);
                conn.Open();


                string sql = "select Name,password from trip.user1 where Name='" + userName + "' and password='" + userPassword + "'";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader sqlDataReader = cmd.ExecuteReader();

                if (sqlDataReader.HasRows) //查询结果存在
                {
                    loginUser = userName;
                    this.Hide();//关闭登录的界面
                    MainForm mainForm = new MainForm();
                    mainForm.ShowDialog();
                    Application.ExitThread();
                }
                else
                {
                    MessageBox.Show("用户名或密码错误！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                conn.Close();
            }

        }

        private void Btn_register_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
        }

        private void Btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
