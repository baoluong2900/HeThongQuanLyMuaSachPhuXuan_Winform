using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyBanSach
{
    public partial class Billing : Form
    {
        public Billing()
        {
            InitializeComponent();
            populate();
        }

        private SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\baolu\Documents\DataBook.mdf;Integrated Security=True;Connect Timeout=30");

        private void populate()
        {
            Con.Open();
            string query = "select * from BookTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BookDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private int key = 0, stock = 0, stock1 = 0,temp=0, key1=0;

        private void BookDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = BookDGV.CurrentRow.Index;

            BBNameTb.Text = BookDGV.Rows[i].Cells[1].Value.ToString();
            BBQuantityTb.Text = "";
            BBPriceTb.Text = BookDGV.Rows[i].Cells[6].Value.ToString();
            if (BBNameTb.Text == "")
            {
                key = 0;
                stock = 0;
            }
            else
            {
                key = Convert.ToInt32(BookDGV.Rows[i].Cells[0].Value.ToString());
                stock = Convert.ToInt32(BookDGV.Rows[i].Cells[5].Value.ToString());
            }
            BBQuantityTb.ReadOnly =false;
        }

        private void Billing_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'bookShopDataDataSet2.BillTbl' table. You can move, or remove it, as needed.
            this.billTblTableAdapter.Fill(this.bookShopDataDataSet2.BillTbl);
            btnUsers.Text = Login.UserName;
            TotalLbl1.Visible = false;
            QuantityLbl.Visible = false;
            Saleslbl.Visible = false;

        }

        private void Reset()
        {
            BBNameTb.Text = "";
            BBQuantityTb.Text = "";
            BBPriceTb.Text = "";
            txtCustomer.Text = "";
            txtSales.Text = "";
            BillDGV.Rows.Clear();
            n = 0;
            Quantity = 0;
            TotalLbl1.Text = "";
            TotalLbl1.Visible = false;
            Saleslbl.Text = "";
        }
        private void ResetBill()
        {
            BBNameTb.Text = "";
            BBQuantityTb.Text = "";
            BBPriceTb.Text = "";
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
       
        }

        private void UpdateBook()
        {
            int newQty = stock - Convert.ToInt32(BBQuantityTb.Text);
            try
            {
                Con.Open();
                string query = "update BookTbl set BookQuantity=" + newQty + "  where BookID=" + key + ";";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                Con.Close();
                populate();
                ResetBill();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        public static bool IsNumeric(string s)
        {
            double retNum;
            bool isNum = Double.TryParse(Convert.ToString(s), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (BBNameTb.Text == "" || BBQuantityTb.Text == "" || BBQuantityTb.ReadOnly == true)
            {
                MessageBox.Show("Vui lòng chọn thông tin sách");
            }
            else if (BBQuantityTb.Text == "")
            {
                MessageBox.Show("Vui lòng nhập số lượng");
            }
            else if(IsNumeric(BBQuantityTb.Text.ToString())==false)
            {         
                    MessageBox.Show("Số lượng nhập vào hợp lệ");            
            }
            else if (BBQuantityTb.Text == "" || Convert.ToInt32(BBQuantityTb.Text) > stock)
            {
                MessageBox.Show("Không đủ số lượng");
            }
            else
            {
                bool temp = false;
                foreach (DataGridViewRow row in this.BillDGV.Rows)
                {
                    if (row.Cells[0] != null && row.Cells[1].Value.ToString() == BBNameTb.Text)
                    {
                        temp = true;

                        break;
                    }
                }
                if (temp)
                {
                    for (int i = 0; i < BillDGV.Rows.Count; i++)
                    {
                        if (BBNameTb.Text == BillDGV.Rows[i].Cells[1].Value.ToString() & BBQuantityTb.Text != BillDGV.CurrentRow.Cells[1].Value.ToString())
                        {
                            BillDGV.Rows[i].Cells[2].Value = Convert.ToInt32(BillDGV.Rows[i].Cells[2].Value) + Convert.ToInt32(BBQuantityTb.Text);
                            BillDGV.Rows[i].Cells[4].Value = Convert.ToInt32(BillDGV.Rows[i].Cells[3].Value) * Convert.ToInt32(BillDGV.Rows[i].Cells[2].Value);
                            Quantity = Quantity + Convert.ToInt32(BBQuantityTb.Text);
                            QuantityLbl.Text = "" + Quantity;
                            UpdateBook();
                            break;
                        }
                    }
                }
                else
                {
                    int total = Convert.ToInt32(BBQuantityTb.Text) * Convert.ToInt32(BBPriceTb.Text);

                    DataGridViewRow newRow = new DataGridViewRow();
                    newRow.CreateCells(BillDGV);
                    //* Thêm vào dữ liệu *//*
                    newRow.Cells[0].Value = n + 1;
                    newRow.Cells[1].Value = BBNameTb.Text;
                    newRow.Cells[2].Value = BBQuantityTb.Text;
                    newRow.Cells[3].Value = BBPriceTb.Text;
                    newRow.Cells[4].Value = total;
                    BillDGV.Rows.Add(newRow);
                    n++;
                    // GrdTotal = GrdTotal + total;
                    Quantity = Quantity + Convert.ToInt32(BBQuantityTb.Text);
                    QuantityLbl.Text = "" + Quantity;
                    UpdateBook();
                }
            }
        }
        private int n = 0, Quantity = 0;

        private void TotalLbl_Click(object sender, EventArgs e)
        {
        }

        private void UpdatedBook()
        {
            int newQty1=temp+stock1;
 
            try
            {
                Con.Open();
                string query1 = "update BookTbl set BookQuantity=" + newQty1 + "  where BookID=" + key1 + ";";
                SqlCommand cmd = new SqlCommand(query1, Con);
                cmd.ExecuteNonQuery();
                Con.Close();
                populate();
                ResetBill();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
    

        private void BillDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = BillDGV.CurrentRow.Index;
            BBNameTb.Text = BillDGV.Rows[i].Cells[1].Value.ToString();
            BBQuantityTb.Text = BillDGV.Rows[i].Cells[2].Value.ToString();
            BBPriceTb.Text = BillDGV.Rows[i].Cells[3].Value.ToString();
            stock1 = Convert.ToInt32(BillDGV.Rows[i].Cells[2].Value.ToString());
            
            for(int j=0; j < BookDGV.Rows.Count; j++)
            {
                if (BBNameTb.Text == BookDGV.Rows[j].Cells[1].Value.ToString())
                {
                    temp = int.Parse(BookDGV.Rows[j].Cells[5].Value.ToString());
                    key1 = int.Parse(BookDGV.Rows[j].Cells[0].Value.ToString());

                }
            }
              BBQuantityTb.ReadOnly = true;
        
        }
        int sum = 0;
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (txtCustomer.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng !");
            }
            else if (BillDGV.Rows[0].Cells[0].Value == null)
            {
                MessageBox.Show("Vui lòng thêm sách vào hóa đơn !");
            }
            else
            {
                if (txtSales.Text == "")
                {
                   

                    for (int i = 0; i < BillDGV.Rows.Count; i++)
                    {
                        sum += int.Parse(BillDGV.Rows[i].Cells[4].Value.ToString());
                    }

                    TotalLbl1.Text =  sum + " VNĐ";
                    Saleslbl.Text ="0";
                    printPreviewDialog1.Document = printDocument1;
                    printPreviewDialog1.ShowDialog();
                }
                else
                {
                 

                    for (int i = 0; i < BillDGV.Rows.Count; i++)
                    {
                        sum += int.Parse(BillDGV.Rows[i].Cells[4].Value.ToString());
                    }

                    sum = (sum - (sum * Convert.ToInt32(txtSales.Text)) / 100);
                    TotalLbl1.Text =sum + " VNĐ";
                    Saleslbl.Text = txtSales.Text.ToString();
                    printPreviewDialog1.Document = printDocument1;
                    printPreviewDialog1.ShowDialog();
                }
                try
                {
                    Con.Open();
                    string query = "insert into BillingTbl values(N'" + btnUsers.Text + "'" +
                       ",N'" + txtCustomer.Text + "'" +
                        ",N'" + sum.ToString() + "'" +
                        ",N'" + txtSales.Text.ToString() + "'" +
                        ",N'" + QuantityLbl.Text.ToString() + "'" +
                        ",GETDATE())";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thanh toán hóa đơn thành công");
              /*      TotalLbl1.Visible = true;*/
                    sum = 0;
                    Con.Close();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
               
     

            }

         
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtSales_TextChanged(object sender, EventArgs e)
        {
        }

        private void label6_Click(object sender, EventArgs e)
        {
        }

        private void TotalLbl1_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void idstock1_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage_1(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("NHÀ SÁCH PHÚ XUÂN", new Font("Century Gothic", 30, FontStyle.Bold), Brushes.Blue, 210, 10);
            e.Graphics.DrawString("2 Lê lợi, thành phố Huế", new Font("Century Gothic", 15), Brushes.Blue, 290, 80);
            e.Graphics.DrawString("PHIẾU THANH TOÁN", new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Blue, 280, 130);
            e.Graphics.DrawString(" ***********************************", new Font("Century Gothic", 15), Brushes.Blue, 250, 200);

            Bitmap objmap = new Bitmap(this.BillDGV.Width, this.BillDGV.Height);
            BillDGV.DrawToBitmap(objmap, new Rectangle(30, 30, this.BillDGV.Width, this.BillDGV.Height));
            e.Graphics.DrawImage(objmap, 100, 220);

                e.Graphics.DrawString(" ***********************************", new Font("Century Gothic", 15), Brushes.Blue, 250, 450);
                e.Graphics.DrawString("Giảm giá", new Font("Century Gothic", 15, FontStyle.Bold), Brushes.Blue, 130, 500);
                e.Graphics.DrawString(Saleslbl.Text+" %", new Font("Century Gothic", 15, FontStyle.Bold), Brushes.Blue, 620, 500);

                e.Graphics.DrawString(" ***********************************", new Font("Century Gothic", 15), Brushes.Blue, 250, 550);
                e.Graphics.DrawString("Tổng cộng", new Font("Century Gothic", 15, FontStyle.Bold), Brushes.Blue, 130, 600);
                e.Graphics.DrawString(TotalLbl1.Text, new Font("Century Gothic", 15, FontStyle.Bold), Brushes.Blue, 620, 600);


                e.Graphics.DrawString(" ***********************************", new Font("Century Gothic", 15), Brushes.Blue, 250, 650);
                e.Graphics.DrawString("Cảm ơn quý khách và hẹn gặp lại ", new Font("Century Gothic", 15, FontStyle.Bold), Brushes.Blue, 230, 700);






        }

            private void Saleslbl_Click(object sender, EventArgs e)
        {

        }

        private void test3_Click(object sender, EventArgs e)
        {
          
        }

        private void printPreviewDialog1_Load_1(object sender, EventArgs e)
        {

        }

        private void NameTotal_Click(object sender, EventArgs e)
        {
        }

        private void delete()
        {
        }

        private void BillDete_Click(object sender, EventArgs e)
        {
            if (BillDGV.Rows.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm thông tin sách vào hóa đơn ");
            }
            else if(BillDGV.Rows.Count != 0 && BBQuantityTb.ReadOnly == false)
            {
                MessageBox.Show("Vui lòng chọn thông tin sách tại hóa đơn cần xóa ");
            }
            /*else if (BBNameTb.Text!="" && BBQuantityTb.Text!="" && BBPriceTb.Text!="") */
            else 
            {
                n = 0;
                int rowIndex = BillDGV.CurrentCell.RowIndex;
                BillDGV.Rows.RemoveAt(rowIndex);

                for (int i = 0; i < BillDGV.Rows.Count; i++)
                {
                    BillDGV.Rows[i].Cells[0].Value = n;
                }
                for (int i = 0; i < BillDGV.Rows.Count; i++)
                {
                    BillDGV.Rows[i].Cells[0].Value = n + 1;
                    n++;
                }
                UpdatedBook();

            }
            ResetBill();
            BillDGV.Refresh();

        }

      
    }
}