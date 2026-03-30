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
    public partial class TrangChu : Form
    {
        string _role;
        public TrangChu( string role)
        {
            InitializeComponent();
            _role = role;
            Console.WriteLine(role);
        }

        private void btn_NhanVien_Click(object sender, EventArgs e)
        {
            NhanVien f = new NhanVien();
            f.ShowDialog();
        }

        private void btn_NhaCungCap_Click(object sender, EventArgs e)
        {
            NhaCungCap f = new NhaCungCap();
            f.ShowDialog();
        }

        private void TrangChu_Load(object sender, EventArgs e)
        {
            if (_role != "Admin")
            {
                btn_KhachHang.Visible= false;
                btn_NhaCungCap.Visible= false;
                btn_NhanVien.Visible= false;
                btn_SanPham.Visible= false;
                button1.Visible= false;
            }
           
            this.WindowState = FormWindowState.Maximized;
            panelContent.Dock = DockStyle.Fill;
            panelMenu.Dock = DockStyle.Left;
            groupBox1.Dock = DockStyle.Top;
        }

        private void btn_ChangePassword_Click(object sender, EventArgs e)
        {
            DoiMatKhau f = new DoiMatKhau();
            f.ShowDialog();
        }

        private void panelContent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_Resize(object sender, EventArgs e)
        {
            label1.Left = (groupBox1.Width - label1.Width) / 2;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát không?","Thông báo",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void btn_SanPham_Click(object sender, EventArgs e)
        {

        }

        private void btn_PhieuXuat_Click(object sender, EventArgs e)
        {
            frmPhieuXuat f = new frmPhieuXuat();
            f.ShowDialog();
        }

        private void btn_PhieuNhap_Click(object sender, EventArgs e)
        {
            frmPhieuNhap f = new frmPhieuNhap();    
            f.ShowDialog();
        }
    }
}
