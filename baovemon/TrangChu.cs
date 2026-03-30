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
        public TrangChu()
        {
            InitializeComponent();
        }

        private void btn_NhanVien_Click(object sender, EventArgs e)
        {
            NhanVien f = new NhanVien();
            f.ShowDialog();
        }

        private void btn_SanPham_Click(object sender, EventArgs e)
        {
            FormQuanLySanPham f = new FormQuanLySanPham();
            f.ShowDialog();
        }

        private void btnTonKho_Click(object sender, EventArgs e)
        {
            TonKho f = new TonKho();
            f.ShowDialog();
        }

        private void TrangChu_Load(object sender, EventArgs e)
        {

        }

        private void btn_KhachHang_Click(object sender, EventArgs e)
        {
            FormKhachHang f = new FormKhachHang();
            f.ShowDialog();
        }
    }
}
