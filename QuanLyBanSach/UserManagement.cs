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
    public partial class UserManagement : Form
    {
        bool temp;
        public UserManagement()
        {
            InitializeComponent();
            populate();
        }
        private SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\baolu\Desktop\KiemDinhPhanMem\PhanMemQuanLySach\TTNL\QuanLyBanSach\BookShopData.mdf;Integrated Security=True;Connect Timeout=30");
        private void populate()
        {
            Con.Open();
            string query = "select * from UserTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            UserDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Reset()
        {
            UNameCusTb.Text = "";
            UPhoneTb.Text = "";
            UAddressTb.Text = "";
            UAccountTb.Text = "";
            UPasswordTb.Text = "";
            temp = true;
        }
        private void label4_Click(object sender, EventArgs e)
        {

        }
        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void USaveBtn_Click(object sender, EventArgs e)
        {
           
        }
        int key = 0;
        private void UserDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void User_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'bookShopDataSet.Table' table. You can move, or remove it, as needed.
            this.tableTableAdapter.Fill(this.bookShopDataSet.Table);
            // TODO: This line of code loads data into the 'bookShopDataDataSet.UserTbl' table. You can move, or remove it, as needed.
            this.userTblTableAdapter1.Fill(this.bookShopDataDataSet.UserTbl);
            // TODO: This line of code loads data into the 'bookShopDataSet.UserTbl' table. You can move, or remove it, as needed.
            this.userTblTableAdapter.Fill(this.bookShopDataSet.UserTbl);

        }

        private void UserDGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = UserDGV.CurrentRow.Index;
            UNameCusTb.Text = UserDGV.Rows[i].Cells[1].Value.ToString();
            UPhoneTb.Text = UserDGV.Rows[i].Cells[2].Value.ToString();
            UAddressTb.Text = UserDGV.Rows[i].Cells[3].Value.ToString();
            UAccountTb.Text = UserDGV.Rows[i].Cells[4].Value.ToString();
            UPasswordTb.Text = UserDGV.Rows[i].Cells[5].Value.ToString();

            if (UNameCusTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(UserDGV.Rows[i].Cells[0].Value.ToString());
            }
        }

        private void UDeleteBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void UEditBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void UCancelBtn_Click(object sender, EventArgs e)
        {
            Reset();

        }
   
        void Check() // kiem tra user da ton tai
        {
            for (int i = 0; i < UserDGV.Rows.Count; i++)
            {
                if (UNameCusTb.Text.ToString() == UserDGV.Rows[i].Cells[1].Value.ToString() &&
                    UPhoneTb.Text.ToString() == UserDGV.Rows[i].Cells[2].Value.ToString() &&
                    UAddressTb.Text.ToString() == UserDGV.Rows[i].Cells[3].Value.ToString() &&
                    UAccountTb.Text.ToString() == UserDGV.Rows[i].Cells[4].Value.ToString() &&
                   UPasswordTb.Text.ToString() == UserDGV.Rows[i].Cells[5].Value.ToString() )
             
                {
                    temp = false;
                    break;
                }

            }

        }

        private void USaveBtn_Click_1(object sender, EventArgs e)
        {
             if (UNameCusTb.Text == "" || UPhoneTb.Text == "" || UAddressTb.Text == "" || UAccountTb.Text == "" || UPasswordTb.Text == "")
            {
                MessageBox.Show("Vui lòng nhập thông tin user");
            }
            else
            {
                Check();
                if (temp == false)
                {
                    MessageBox.Show("Thông tin nhân viên đã tồn tại");
                }
                else
                {
                    try
                    {
                        Con.Open();
                        string query = "insert into UserTbl values(N'" + UNameCusTb.Text + "',N'" + UPhoneTb.Text + "',N'" + UAddressTb.Text + "',N'" + UAccountTb.Text + "',N'" + UPasswordTb.Text + "')";
                        SqlCommand cmd = new SqlCommand(query, Con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Thông tin user đã thêm thành công");
                        Con.Close();
                        populate();
                        Reset();
                    }

                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }
                }
                

            }
        }

        private void UDeleteBtn_Click_1(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Vui lòng chọn thông tin user cần xóa");
                Reset();
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "delete from UserTbl where UserId=" + key + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thông tin user đã xóa thành công");
                    Con.Close();
                    populate();
                    Reset();
                }

                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void UEditBtn_Click_1(object sender, EventArgs e)
        {
            if (UNameCusTb.Text == "" || UPhoneTb.Text == "" || UAddressTb.Text == "" || UAccountTb.Text == "" || UPasswordTb.Text == "")
            {
                MessageBox.Show("Vui lòng chọn thông tin user cần sửa");
                Reset();
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "update UserTbl set UserName=N'" + UNameCusTb.Text + "',UserPhone=N'" + UPhoneTb.Text + "',UserAddress=N'" + UAddressTb.Text + "',UserAccount=N'" + UAccountTb.Text + "',UserPassword='" + UPasswordTb.Text + "' where UserId=" + key + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thông tin user đã sữa thành công");
                    Con.Close();
                    populate();
                    Reset();
                }

                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void UCancelBtn_Click_1(object sender, EventArgs e)
        {
            Reset();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();
            

        }

        private void btnUsers_Click_2(object sender, EventArgs e)
        {

        }

        private void btnViewSales_Click_2(object sender, EventArgs e)
        {
            StatisticManagement sm = new StatisticManagement();
            sm.Show();
            this.Hide();
        }

        private void btnPurchase_Click_2(object sender, EventArgs e)
        {
           
            BookManagement bm = new BookManagement();
            bm.Show();
            this.Hide();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            BillManagement bm = new BillManagement();
            bm.Show();
            this.Hide();
        }

        private void btnPurchase_Click(object sender, EventArgs e)
        {
            BookManagement bm = new BookManagement();
            bm.Show();
            this.Hide();
        }

        private void btnViewSales_Click(object sender, EventArgs e)
        {
            StatisticManagement st = new StatisticManagement();
            st.Show();
            this.Hide();
        }
    }
}
