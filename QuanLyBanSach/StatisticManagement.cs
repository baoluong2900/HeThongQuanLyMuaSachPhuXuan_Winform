using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanSach
{
    public partial class StatisticManagement : Form
    {
        public StatisticManagement()
        {
            InitializeComponent();
           /* populate();*/
        }
        private SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\baolu\Desktop\KiemDinhPhanMem\PhanMemQuanLySach\TTNL\QuanLyBanSach\BookShopData.mdf;Integrated Security=True;Connect Timeout=30");
        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
/*        private void populate()
        {
            Con.Open();
            string query = "select * from BookTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
           testDGV.DataSource = ds.Tables[0];
            Con.Close();
        }*/

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnViewSales_Click(object sender, EventArgs e)
        {
            StatisticManagement dt = new StatisticManagement();
            dt.Show();
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

        private void label14_Click(object sender, EventArgs e)
        {
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DoanhThu_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'bookShopDataSet.BookTbl' table. You can move, or remove it, as needed.
            this.bookTblTableAdapter.Fill(this.bookShopDataSet.BookTbl);
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select sum (BookQuantity) from BookTbl", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            BookStockLbl.Text = dt.Rows[0][0].ToString();

            SqlDataAdapter sda1 = new SqlDataAdapter("select count(*) from UserTbl", Con);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            UserLbl.Text = dt1.Rows[0][0].ToString();
          


            SqlDataAdapter sda2 = new SqlDataAdapter("select sum (cast(BillAmount as int)) from BillingTbl", Con);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            TotalAmountLbl.Text = dt2.Rows[0][0].ToString();
            Con.Close();
        }
        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void guna2DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            BillManagement b = new BillManagement();
            b.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BookStockLbl_Click(object sender, EventArgs e)
        {

        }

        private void TotalAmountLbl_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();
        }

        private void btnPurchase_Click_1(object sender, EventArgs e)
        {
            BookManagement bm = new BookManagement();
            bm.Show();
            this.Hide();
        }

        private void btnUsers_Click_1(object sender, EventArgs e)
        {
            UserManagement um = new UserManagement();
            um.Show();
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            BillManagement bm = new BillManagement();
            bm.Show();
            this.Hide();
        }
    }
}
