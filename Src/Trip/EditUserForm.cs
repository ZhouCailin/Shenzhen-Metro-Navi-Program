using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Configuration;
using System.IO;

using Trip.IBLL;
using Trip.BLL;
using Trip.IDAL;
using Trip.DAL;
using Trip.Common;
using Trip.Model;

namespace Trip
{
    public partial class EditUserForm : Form
    {

        private User m_user = null;
        private bool m_bAdd = true;

        private IUserBLL m_userBLL = null;
        private ICityBLL m_cityBLL = null;

        public EditUserForm(User user, bool bAdd)
        {
            InitializeComponent();

            m_user = user;
            m_bAdd = bAdd;

            IUserRepository userRepository = TripApplication.Instance.DBFactory.CreateUserRepository();
            m_userBLL = new UserBLL(userRepository);

            ICityRepository cityRepository = TripApplication.Instance.DBFactory.CreateCityRepository();
            m_cityBLL = new CityBLL(cityRepository);

        }

        private void EditUserForm_Load(object sender, EventArgs e)
        {
            cboSex.Items.Add("男");
            cboSex.Items.Add("女");

            List<City> citys = m_cityBLL.GetAllCity();
            foreach (City city in citys)
                cboPlace.Items.Add(city.cityNo + " " + city.name);
            
/*            DataTable dt = Utility.FillCity();
            foreach (DataRow dr in dt.Rows)
                cboPlace.Items.Add(dr[0] + " " + dr[1].ToString().Trim());*/
            
            txtCode.Text = m_user.userID;
            txtName.Text = m_user.name;
            txtAge.Text = m_user.age.ToString();
            cboSex.Text = m_user.sex;
            cboPlace.Text = m_user.cityNo;
            txtSummary.Text = m_user.summary;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            m_user.userID = txtCode.Text;
            m_user.name = txtName.Text;
            m_user.age = Convert.ToInt32(txtAge.Text);
            m_user.sex = cboSex.Text;
            string[] arr = cboPlace.Text.Split(' ');
            m_user.cityNo = arr[0];
            m_user.summary = txtSummary.Text;

            try
            {
                if (m_bAdd)
                    m_userBLL.AddUser(m_user);
                else
                    m_userBLL.UpdateUser(m_user);

            }
            catch(UserDBException ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
