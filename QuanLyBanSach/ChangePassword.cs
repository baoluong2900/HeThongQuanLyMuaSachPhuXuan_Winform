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
    public partial class ChangePassword : Form
    {
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void USaveBtn_Click(object sender, EventArgs e)
        {
            if(UPasswordNewTb.Text=="" || UPasswordOldTb.Text=="" || UConfirmPasswordNewTb.Text=="")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu ");
            }
            else
            {
                if (UPasswordOldTb.Text == Admin.Password)
                {
                    if (UPasswordNewTb.Text == UConfirmPasswordNewTb.Text)
                    {
                        Admin.Password = UPasswordNewTb.Text;
                        MessageBox.Show("Thay đổi mật khẩu thành công");
                        Admin d = new Admin();
                        d.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Mật khẩu thay đổi không trùng khớp ");
                    }
                }
                else
                {
                    MessageBox.Show("Mật khẩu admin không đúng ");
                }

            }
           
        }

        private void CPCanelBtn_Click(object sender, EventArgs e)
        {
            Admin d = new Admin();
            d.Show();
            this.Hide();
        }

        private void ChangePassword_Load(object sender, EventArgs e)
        {

        }
    }
}
