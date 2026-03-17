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
    public partial class NhanVien : Form
    {
        BUS.NhanVien nhanvien = new BUS.NhanVien();
        public NhanVien()
        {
            InitializeComponent();
            this.AutoScroll = true;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void NhanVien_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = nhanvien.GetDataNhanVien();
            txt_MaNV.Enabled = false;
            txt_DiaChi.Enabled = false;
            txt_MK.Visible = false;
            txt_SDT.Enabled = false;
            txt_TenNV.Enabled = false;
            txt_TK.Enabled = false;
            combGoiTinh.Enabled = false;
            comb_VaiTro.Enabled = false;
            dateTimeP_NgaySinh.Enabled = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            if (r >= 0) { 
                txt_MaNV.Text = dataGridView1.Rows[r].Cells["Manv"].Value.ToString();
                txt_DiaChi.Text = dataGridView1.Rows[r].Cells["DiaChi"].Value.ToString();
                txt_SDT.Text = dataGridView1.Rows[r].Cells["DienThoai"].Value.ToString();
                txt_TenNV.Text = dataGridView1.Rows[r].Cells["TenNV"].Value.ToString();
                txt_TK.Text = dataGridView1.Rows[r].Cells["TaiKhoan"].Value.ToString();
                combGoiTinh.Text = dataGridView1.Rows[r].Cells["GioiTinh"].Value.ToString();
                comb_VaiTro.Text = dataGridView1.Rows[r].Cells["VaiTro"].Value.ToString() ;
                dateTimeP_NgaySinh.Value = Convert.ToDateTime(dataGridView1.Rows[r].Cells["NgaySinh"].Value);
            }
        }
    }
}
