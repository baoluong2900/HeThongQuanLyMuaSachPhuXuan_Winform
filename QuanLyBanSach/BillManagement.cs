using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QuanLyBanSach
{
    public partial class BillManagement : Form
    {
        public BillManagement()
        {
            InitializeComponent();
            populate();
        }
        private SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\baolu\Desktop\KiemDinhPhanMem\PhanMemQuanLySach\TTNL\QuanLyBanSach\BookShopData.mdf;Integrated Security=True;Connect Timeout=30");
        private void populate()
        {
            Con.Open();
            string query = "select * from BillingTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BillDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void btnViewSales_Click(object sender, EventArgs e)
        {
            StatisticManagement re = new StatisticManagement();
            re.Show();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            UserManagement us = new UserManagement();
            us.Show();
        }

        private void btnPurchase_Click(object sender, EventArgs e)
        {
            BookManagement m = new BookManagement();
            m.Show();
        }

        private void Bill_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'bookShopDataDataSet3.BillingTbl' table. You can move, or remove it, as needed.
            this.billingTblTableAdapter.Fill(this.bookShopDataDataSet3.BillingTbl);
            // TODO: This line of code loads data into the 'bookShopDataDataSet2.BillTbl' table. You can move, or remove it, as needed.
            this.billTblTableAdapter.Fill(this.bookShopDataDataSet2.BillTbl);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            BillDGV.ReadOnly = true;
        }
   
        private void BDeleteBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa hết thông tin hóa đơn không ?", "Xóa hết thông tin hóa đơn", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    Con.Open();
                    string query = "delete from BillingTbl ";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thông tin hóa đơn đã xóa thành công");
       
                    Con.Close();
                    populate();
                }

                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();
        }

        private void btnViewSales_Click_1(object sender, EventArgs e)
        {
            StatisticManagement sm = new StatisticManagement();
            sm.Show();
            this.Hide();
        }

        private void btnUsers_Click_1(object sender, EventArgs e)
        {
            UserManagement um = new UserManagement();
            um.Show();
            this.Hide();
        }

        private void btnPurchase_Click_1(object sender, EventArgs e)
        {
            BookManagement bm = new BookManagement();
            bm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
