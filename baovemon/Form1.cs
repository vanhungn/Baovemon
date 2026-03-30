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

namespace baovemon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        BUS.NhanVien nhanvien = new BUS.NhanVien(); 
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
           
                try {
                    DTO.NhanVien nv = new DTO.NhanVien()
                    {
                        TaiKhoan = textBox1.Text,
                        MatKhau = textBox2.Text,
                    };
                     if (nhanvien.DangNhap(nv) !=null)
                     {
                    TrangChu form1 = new TrangChu(nhanvien.DangNhap(nv));
                    form1.Show();
                    this.Hide();
                    }
                    else
                    {
                    MessageBox.Show("Tài khoản mật khẩu không chính xác", "Thông báo", MessageBoxButtons.OK);
                }

            }
                catch (SqlException ex) {
                    MessageBox.Show("Lỗi: " + ex.Message);
                   
                }
                
           
        }
    }
}
