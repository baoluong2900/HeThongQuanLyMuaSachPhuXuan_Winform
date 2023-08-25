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
    public partial class Login : Form
    {
        private SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\baolu\Documents\DataBook.mdf;Integrated Security=True;Connect Timeout=30");
        public Login()
        {
            InitializeComponent();
        }
        public static string UserName = "";
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            Con.Open();
            SqlDataAdapter sta = new SqlDataAdapter("select count(*) from UserTbl where  UserAccount=N'" + UNameTb.Text + "' and UserPassword =N'" + UPasswordTb.Text + "'", Con);
            DataTable dt = new DataTable();
            sta.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {

                SqlDataAdapter sta1 = new SqlDataAdapter("select UserName from UserTbl where  UserAccount=N'" + UNameTb.Text + "' and UserPassword =N'" + UPasswordTb.Text + "'", Con);
                DataTable dt1 = new DataTable();
                sta1.Fill(dt1);
                foreach (DataRow dr in dt1.Rows)
                {
                    UserName = dr["UserName"].ToString();

                }

                Billing b = new Billing();
                b.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không đúng ! ");
            }

    

            Con.Close();

        }
        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click_2(object sender, EventArgs e)
        {
            Admin ad = new Admin();
            ad.Show();
            this.Hide();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
