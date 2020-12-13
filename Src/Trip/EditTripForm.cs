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
    public partial class EditTripForm : Form
    {

        private TripRecord m_trip = null;
        //private TripRecord b_trip = null;//SIGN:保留更改前的数据
        private bool m_bAdd = true;

        private IUserBLL m_userBLL = null;
        private ITripBLL m_tripBLL = null;
        private ICityBLL m_cityBLL = null;


        public EditTripForm(TripRecord trip, bool bAdd)
        {
            InitializeComponent();

            m_trip = trip;
            m_bAdd = bAdd;

            IUserRepository userRepository = TripApplication.Instance.DBFactory.CreateUserRepository();
            ITripRepository tripRepository = TripApplication.Instance.DBFactory.CreateTripRepository();
            ICityRepository cityRepository = TripApplication.Instance.DBFactory.CreateCityRepository();

            m_userBLL = new UserBLL(userRepository);
            m_tripBLL = new TripBLL(tripRepository);
            m_cityBLL = new CityBLL(cityRepository);

        }

        private void EditTripForm_Load(object sender, EventArgs e)
        {

            List<User> users = m_userBLL.GetAllUser();
            foreach (User user in users)
                cboUser.Items.Add(user.userID + " " + user.name);


            List<City> citys = m_cityBLL.GetAllCity();
            foreach (City city in citys)
                cboPlace.Items.Add(city.cityNo + " " + city.name);//SIGN:uid位数不定，逗号分割字符串，便于后面操作；
            

/*            DataTable dt = Utility.FillCity();
            foreach (DataRow dr in dt.Rows)
                cboPlace.Items.Add(dr[0] + " " + dr[1].ToString().Trim());*/

            //cboUser.Text = m_trip.userID;
            //cboPlace.Text = m_trip.cityNo;//SIGN：默认显示的数据，只有站点名没有编号，直接点确定会出错

            

            if (m_bAdd)
                txtArriveTime.Text = DateTime.Now.ToString();
            else
                txtArriveTime.Text = m_trip.arriveTime.ToString();

            txtMemo.Text = m_trip.memo;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
/*            m_user.userID = txtCode.Text;
            m_user.name = txtName.Text;
            m_user.age = Convert.ToInt32(m_user.age);
            m_user.sex = cboSex.Text;
            m_user.cityNo = cboPlace.Text.Substring(0, 6);
            m_user.summary = txtSummary.Text;*/

            string TuserID = m_trip.userID; string Tname = m_trip.name; DateTime TtxtArriveTime = m_trip.arriveTime; string Tmemo = m_trip.memo;
            TripRecord b_trip = new TripRecord();//SIGN：点进更新框时，用于记录原数据的对象
            b_trip.arriveTime = TtxtArriveTime; b_trip.userID = TuserID; b_trip.name = Tname; b_trip.memo = Tmemo;
            m_trip.ID = "0";
            m_trip.userID = cboUser.Text.Substring(0, 18);//SIGN：这里必须是个人ID相应的位数
            string[] arr = cboPlace.Text.Split(' ');//SIGN:逗号分割字符串
            m_trip.name = arr[1];
            m_trip.cityNo = arr[0];
            m_trip.arriveTime = Convert.ToDateTime(txtArriveTime.Text);
            m_trip.memo = txtMemo.Text;

            try
            {
                if (m_bAdd)
                    m_tripBLL.AddTrip(m_trip);
                else
                    m_tripBLL.UpdateTrip(b_trip, m_trip);

            }
            catch(TripDBException ex)
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

        private void cboUser_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
