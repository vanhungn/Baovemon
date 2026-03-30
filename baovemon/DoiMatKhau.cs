using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace baovemon
{
    public partial class DoiMatKhau : Form
    {
        public DoiMatKhau()
        {
            InitializeComponent();
        }
        BUS.NhanVien nhanvien = new BUS.NhanVien();

        private void DoiMatKhau_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DTO.NhanVien nv = new DTO.NhanVien() { 
                TaiKhoan = textBox1.Text,
                MatKhau = textBox2.Text,
                newPassword = textBox3.Text,
            };
            try
            {
                if (nhanvien.DoiMatKhau(nv))
                {
                    MessageBox.Show("Đổi mật khẩu thành công!");
                }
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
