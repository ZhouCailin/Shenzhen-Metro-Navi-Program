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
    public partial class ManageTripForm : Form
    {
        private const int ROWS = 10;
        private int m_curPage = 1;
        private int m_total = 0;

        private ITripBLL m_tripBLL = null;


        public ManageTripForm()
        {
            InitializeComponent();
        }

        private void ManageUserForm_Load(object sender, EventArgs e)
        {
            ITripRepository tripRepository = TripApplication.Instance.DBFactory.CreateTripRepository();
            m_tripBLL = new TripBLL(tripRepository);

            FillGrid();
         
        }

        private void FillGrid()
        {
            dataGridView1.Rows.Clear();

            List<TripRecord> trips = m_tripBLL.GetByParam("", (m_curPage-1)*ROWS, ROWS, "userID", "asc", "", ref m_total);
            foreach (TripRecord trip in trips)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = trip.userID;
                dataGridView1.Rows[index].Cells[1].Value = trip.name;//SIGN:插入地铁站点名称
                dataGridView1.Rows[index].Cells[2].Value = trip.arriveTime.ToString();
                dataGridView1.Rows[index].Cells[3].Value = trip.memo;
            }

            txtPageNo.Text = m_curPage.ToString();
            txtPageSum.Text = (m_total / ROWS + 1).ToString();
            txtPageNum.Text = ROWS.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            EditTripForm fm = new EditTripForm(new TripRecord(), true);
            if (fm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FillGrid();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            TripRecord trip = new TripRecord();
            trip.userID = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            trip.name = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            trip.arriveTime = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells[2].Value.ToString());
            trip.memo= dataGridView1.SelectedRows[0].Cells[3].Value.ToString();

            EditTripForm fm = new EditTripForm(trip, false);
            if (fm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FillGrid();
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string userID = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            string cityNo = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            TripRecord trip = new TripRecord();
            trip.userID = userID;
            trip.cityNo = cityNo;

            if (MessageBox.Show("确认删除这条记录?", "信息提示", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    m_tripBLL.DeleteTrip(trip);
                    FillGrid();
                }
                catch (TripDBException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (m_curPage < (m_total / ROWS + 1))
            {
                m_curPage++;
                FillGrid();
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (m_curPage > 1)
            {
                m_curPage--;
                FillGrid();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
