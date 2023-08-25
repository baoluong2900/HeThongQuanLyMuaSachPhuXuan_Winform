using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyBanSach
{
    public partial class BookManagement : Form
    {
        bool temp;
        public BookManagement()
        {
            InitializeComponent();
            populate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\SupportCustomer\SourceWinform\QuanLyBanSach\DataBook.mdf;Integrated Security=True;Connect Timeout=30");
        private void populate() // Kết nối cơ sở dữ liệu
        {
            Con.Open();
            string query = "select * from BookTbl"; // Lấy hết dữ liệu trong bảng Book
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);  // Chuyển dữ liệu về  
            SqlCommandBuilder builder = new SqlCommandBuilder(sda); 
            var ds = new DataSet();  // Tạo bộ nhớ để lưu cơ sở dữ liệu
            sda.Fill(ds); // Thêm cơ sở dữu liệu vào bộ nhớ
            BookDGV.DataSource = ds.Tables[0]; // Hiển thị cơ sở dữ liệu lên DataGridView
            Con.Close(); 
        }

        private void Filter()
        {
            Con.Open();
            string query = "select * from BookTbl where BookCat=N'" + BListCate.SelectedItem.ToString() + "' ";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BookDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void BCateCb_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void BookDVG_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void Main_Load(object sender, EventArgs e)
        {
           // this.bookTblTableAdapter.Fill(this.dataBookDataSet.BookTbl);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void BListCatCb_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btnPurchase_Click(object sender, EventArgs e)
        {
            BookManagement m = new BookManagement();
            m.Show();
        }

  

        private void BookDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /*
                        int index = e.RowIndex;
                        if (!(index < 0))
                        {
                            DataGridViewRow rows = BookDGV.Rows[index];
                            BNameTb.Text = rows.Cells["BookTitle"].Value.ToString();
                            BAuTb.Text = rows.Cells["BookAuthor"].Value.ToString();
                            BCateCb.SelectedItem = rows.Cells["BookCat"].Value.ToString();
                            BPublisherTb.Text = rows.Cells["BookPublisher"].Value.ToString();
                            BQuantityCb.Text = rows.Cells["BookQuantity"].Value.ToString();
                            BPriceTb.Text = rows.Cells["BookPrice"].Value.ToString();

                            BookDGV.Focus();
                        }
                        BookDGV.Focus();
            */

            int i;
            i = bookTblDataGridView.CurrentRow.Index;
            BNameTb.Text = bookTblDataGridView.Rows[i].Cells[1].Value.ToString();
            BAuTb.Text = bookTblDataGridView.Rows[i].Cells[2].Value.ToString();
            BCateCb.SelectedItem = bookTblDataGridView.Rows[i].Cells[3].Value.ToString();
            BPublisherTb.Text = bookTblDataGridView.Rows[i].Cells[4].Value.ToString();
            BQuantityCb.Text = bookTblDataGridView.Rows[i].Cells[5].Value.ToString();
            BPriceTb.Text = bookTblDataGridView.Rows[i].Cells[6].Value.ToString();


            if (BNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(bookTblDataGridView.Rows[i].Cells[0].Value.ToString());
            }
        }

        private void BDeleteBtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Vui lòng chọn thông tin cần xóa");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "delete from BookTbl where BookID=" + key + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thông tin sách đã xóa thành công");
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

        private void BListCate_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Filter();
        }

        private void BResetBtn_Click(object sender, EventArgs e)
        {
            populate();
            BListCate.SelectedIndex = -1;
        }

        private void Reset()
        {
            BNameTb.Text = "";
            BAuTb.Text = "";
            BCateCb.SelectedIndex = -1;
            BPublisherTb.Text = "";
            BQuantityCb.Text = "";
            BPriceTb.Text = "";
            temp = true;
            //temp1 = true;
    
        }

        private void BCanelBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void BNameTb_TextChanged(object sender, EventArgs e)
        {
        }

        private void BListCate_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void BookDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void bookTblBindingSource_CurrentChanged(object sender, EventArgs e)
        {
        }

        private void BEditBtn_Click(object sender, EventArgs e)
        {
            if (BNameTb.Text == "" || BAuTb.Text == "" || BCateCb.Text == "" || BPublisherTb.Text == "" || BQuantityCb.Text == "" || BPriceTb.Text == "" || BCateCb.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn thông tin cần sửa");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "update BookTbl set BookTitle=N'" + BNameTb.Text + "',BookAuthor=N'" + BAuTb.Text + "',BookCat=N'" + BCateCb.SelectedItem.ToString() + "',BookPublisher=N'" + BPublisherTb.Text + "',BookQuantity='" + BQuantityCb.Text + "',BookPrice=" + BPriceTb.Text + " where BookID=" + key + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thông tin sách đã sữa thành công");
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


        void Check() // kiem tra thong tin sach da ton tai
        {
            
      
        }
       private void BSaveBtn_Click_1(object sender, EventArgs e)
        {
    

            if (BNameTb.Text == "" || 
                BAuTb.Text == "" || 
                BCateCb.Text == "" || 
                BPublisherTb.Text == "" || 
                BQuantityCb.Text == "" || 
                BPriceTb.Text == "" || 
                BCateCb.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng điền thông tin");
            }
            else
            {
                for (int i = 0; i < bookTblDataGridView.Rows.Count; i++)
                {
                    if (BNameTb.Text.ToString() == bookTblDataGridView.Rows[i].Cells[1].Value.ToString() &&
                        BAuTb.Text.ToString() == bookTblDataGridView.Rows[i].Cells[2].Value.ToString() &&
                        BCateCb.Text.ToString() == bookTblDataGridView.Rows[i].Cells[3].Value.ToString() &&
                        BPublisherTb.Text.ToString() == bookTblDataGridView.Rows[i].Cells[4].Value.ToString() &&
                        BPriceTb.Text.ToString() == bookTblDataGridView.Rows[i].Cells[6].Value.ToString())
                    {
                        temp = false;
                        break;
                    }

                }
                if (temp == false)
                {
                    MessageBox.Show("Thông tin sách đã tồn tại ");
                    Reset();
                }
                else
                {
                    try
                    {
                        Con.Open();
                        string query = "insert into BookTbl values(" + "N'" + BNameTb.Text + "'," +
                            "N'" + BAuTb.Text + "'," +
                            "N'" + BCateCb.SelectedItem.ToString() + "'," +
                            "N'" + BPublisherTb.Text + "'," +
                            "'" + BQuantityCb.Text + "'," +
                            "'" + BPriceTb.Text + "')";
                        SqlCommand cmd = new SqlCommand(query, Con);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Thông tin sách đã lưu thành công");
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
        private int key = 0;
        private void BDeleteBtn_Click_1(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Vui lòng chọn thông tin cần xóa");
                Reset();

            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "delete from BookTbl where BookID=" + key + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thông tin sách đã xóa thành công");
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

        private void BEditBtn_Click_1(object sender, EventArgs e)
        {
            if (BNameTb.Text == "" || BAuTb.Text == "" || BCateCb.Text == "" || BPublisherTb.Text == "" || BQuantityCb.Text == "" || BPriceTb.Text == "" || BCateCb.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn thông tin cần sửa");
                Reset();
            
                
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "update BookTbl set BookTitle=N'" + BNameTb.Text + "',BookAuthor=N'" + BAuTb.Text + "',BookCat=N'" + BCateCb.SelectedItem.ToString() + "',BookPublisher=N'" + BPublisherTb.Text + "',BookQuantity='" + BQuantityCb.Text + "',BookPrice=" + BPriceTb.Text + " where BookID=" + key + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thông tin sách đã sữa thành công");
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

        private void BCancelBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private UserManagement us = new UserManagement();
        private StatisticManagement tk = new StatisticManagement();
        private BillManagement b = new BillManagement();

        private void btnPurchase_Click_2(object sender, EventArgs e)
        {
        }

        private void btnUsers_Click_2(object sender, EventArgs e)
        {
            UserManagement um = new UserManagement();
            um.Show();
            this.Hide();
        }

        private void btnViewSales_Click_2(object sender, EventArgs e)
        {
            StatisticManagement st = new StatisticManagement();
            st.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BillManagement bm = new BillManagement();
            bm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.bookTblTableAdapter1.FillBy(this.dataBookDataSet.BookTbl);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void fillBy1ToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.bookTblTableAdapter1.FillBy1(this.dataBookDataSet.BookTbl);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void fillBy2ToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.bookTblTableAdapter1.FillBy2(this.dataBookDataSet.BookTbl);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }
    }
}