using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanSach
{
    
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }
        public static string Password = "admin123";
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (UPasswordTb.Text == Password)
            {
                BookManagement m = new BookManagement();
                m.Show();
                this.Hide();
            }
            else if (UPasswordTb.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu !");
            }
            else
            {
                MessageBox.Show("Mật khẩu admin không đúng !");
            }

        }

        private void Admin_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Hide();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            ChangePassword cp = new ChangePassword();
            cp.Show();
        }
    }
}
