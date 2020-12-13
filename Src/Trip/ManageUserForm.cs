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
    public partial class ManageUserForm : Form
    {
        private const int ROWS = 10;
        private IUserBLL m_userBLL = null;
        private int m_curPage = 1;
        private int m_total = 0;


        public ManageUserForm()
        {
            InitializeComponent();
        }

        private void ManageUserForm_Load(object sender, EventArgs e)
        {
            IUserRepository userRepository = TripApplication.Instance.DBFactory.CreateUserRepository();
            m_userBLL = new UserBLL(userRepository);
            FillGrid();
        }

        private void FillGrid()
        {
            dataGridView1.Rows.Clear();

            List<User> users = m_userBLL.GetByParam("", (m_curPage-1)*ROWS, ROWS, "userID", "asc", "", ref m_total);
            foreach(User user in users)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = user.userID;
                dataGridView1.Rows[index].Cells[1].Value = user.name;
                dataGridView1.Rows[index].Cells[2].Value = user.sex;
                dataGridView1.Rows[index].Cells[3].Value = user.age;
                dataGridView1.Rows[index].Cells[4].Value = user.cityNo;
                dataGridView1.Rows[index].Cells[5].Value = user.summary;
            }

            txtPageNo.Text = m_curPage.ToString();
            txtPageSum.Text = (m_total / ROWS + 1).ToString();
            txtPageNum.Text = ROWS.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // if（dataGridView1.SelectedRows.c.get.SelectedRows）
            EditUserForm fm = new EditUserForm(new User(), true);
            if (fm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FillGrid();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            User user = new User();
            user.userID = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            user.name = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            user.sex = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            user.age = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
            user.cityNo = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            user.summary = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();

            EditUserForm fm = new EditUserForm(user, false);
            if (fm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FillGrid();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string userID = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            if (MessageBox.Show("确认删除这条记录?", "信息提示", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    m_userBLL.DeleteUser(userID);
                    FillGrid();
                }
                catch(UserDBException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (m_curPage < (m_total / ROWS + 1))
            {
                m_curPage ++;
                FillGrid();
            }


        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (m_curPage > 1)
            {
                m_curPage --;
                FillGrid();
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
