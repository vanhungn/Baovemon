using BUS;
using DTO;
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
    public partial class frmPhieuXuat : Form
    {
        PhieuXuatBUS bus = new PhieuXuatBUS();
        BUS.CTPhieuXuat busCTPX = new BUS.CTPhieuXuat();
        SqlConnection conn = new SqlConnection(
            @"Data Source = LAPTOP-NG3J2HSN\KTEAM; Initial Catalog = quanlykho; Integrated Security=True");
        public frmPhieuXuat()
        {
            InitializeComponent();
        }
        /* Hàm Load */
        void LoadData()
        {
            dgvPhieuXuat.DataSource = bus.GetAll();
            dgvPhieuXuat.AutoGenerateColumns = true;
            if (dgvPhieuXuat.Columns["TenKho"] != null)
                dgvPhieuXuat.Columns["TenKho"].Visible = false;
            if (dgvPhieuXuat.Columns["TenNV"] != null)
                dgvPhieuXuat.Columns["TenNV"].Visible = false;
        }
        void LoadMatHang()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT MaMh, TenMh FROM hang_hoa", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbMaMH.DataSource = dt;
            cbMaMH.DisplayMember = "MaMh";
            cbMaMH.ValueMember = "MaMh";
        }
        void LoadNhanVien()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT Manv, TenNV FROM nhan_vien", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbManv.DataSource = dt;
            cbManv.DisplayMember = "Manv";
            cbManv.ValueMember = "Manv";
            cbManv.SelectedIndex = -1;
        }
        void LoadKho()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT MaKho, TenKho FROM kho_hang", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbMakho.DataSource = dt;
            cbMakho.DisplayMember = "MaKho";
            cbMakho.ValueMember = "MaKho";
            cbMakho.SelectedIndex = -1;
        }
        void Clear()
        {
            txtSPX.Text = "";
            cbManv.SelectedIndex = -1;
            cbMakho.SelectedIndex = -1;
            txtSlx.Text = "";
            dtNgayxuatpx.Value = DateTime.Now;
        }
        public void LoadCTPX()
        {
            string sql = @"
        SELECT ct.MaPX, ct.MaMh, hh.TenMh, ct.SlXuat
        hh.DonGia,
                    (ct.SlXuat * hh.DonGia) AS ThanhTien
                FROM CT_Phieu_Xuat ct
                JOIN hang_hoa hh ON ct.MaMh = hh.MaMh
                WHERE ct.MaPX = @MaPX";

            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvCTPX.DataSource = dt;
        }

        private void frmPhieuXuat_Load(object sender, EventArgs e)
        {
            LoadNhanVien();
            LoadKho();
            LoadMatHang();
            LoadData();
        }

        private void dgvPhieuXuat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvPhieuXuat.Rows[e.RowIndex];

            if (row.Cells["MaPX"].Value == DBNull.Value) return;

            int maPN = Convert.ToInt32(row.Cells["MaPX"].Value);
            txtSPX.Text = maPN.ToString();

            // ===== FIX COMBOBOX =====
            if (row.Cells["Manv"].Value != DBNull.Value)
            {
                cbManv.SelectedValue = Convert.ToInt32(row.Cells["Manv"].Value);
            }

            if (row.Cells["MaKho"].Value != DBNull.Value)
            {
                cbMakho.SelectedValue = Convert.ToInt32(row.Cells["MaKho"].Value);
            }

            if (row.Cells["NgayXuat"].Value != DBNull.Value)
            {
                dtNgayxuatpx.Value = Convert.ToDateTime(row.Cells["NgayXuat"].Value);
            }
            int maPX = Convert.ToInt32(row.Cells["MaPX"].Value);
            dgvCTPX.DataSource = bus.GetCTPX(maPX);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cbManv.SelectedIndex == -1 || cbMakho.SelectedIndex == -1)
            {
                MessageBox.Show("Chọn nhân viên và kho!");
                return;
            }
            if (txtSlx.Text == "")
            {
                MessageBox.Show("Nhập số lượng!");
                return;
            }
            try
            {
                PhieuXuatDTO pn = new PhieuXuatDTO();
                pn.Manv = Convert.ToInt32(cbManv.SelectedValue);
                pn.MaKho = Convert.ToInt32(cbMakho.SelectedValue);
                pn.NgayXuat = dtNgayxuatpx.Value;
                int maPX = bus.InsertAndGetid(pn);
                ChiTietPhieuXuat ct = new ChiTietPhieuXuat();
                ct.MaPX = maPX;
                ct.MaMh = Convert.ToInt32(cbMaMH.SelectedValue);
                ct.SlXuat = int.Parse(txtSlx.Text);
                bus.InsertCTPX(ct);
                MessageBox.Show("Thêm thành công!");
                LoadData();
                dgvCTPX.DataSource = bus.GetCTPX(maPX);
                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtSPX.Text == "" || cbManv.SelectedValue == null || cbMakho.SelectedValue == null || cbMaMH.SelectedValue == null || txtSlx.Text == "")
            {
                MessageBox.Show("Chọn đầy đủ thông tin!");
                return;
            }
            try
            {
                // 1. Sửa bảng PhieuXuat
                PhieuXuatDTO pn = new PhieuXuatDTO();
                pn.MaPX = int.Parse(txtSPX.Text);
                pn.Manv = Convert.ToInt32(cbManv.SelectedValue);
                pn.MaKho = Convert.ToInt32(cbMakho.SelectedValue);
                pn.NgayXuat = dtNgayxuatpx.Value;

                string kq1 = bus.Update(pn);

                // 2. Sửa bảng CT_Phieu_XUat (Mã mặt hàng + Số lượng)
                ChiTietPhieuXuat ct = new ChiTietPhieuXuat();
                ct.MaPX = int.Parse(txtSPX.Text);
                ct.MaMh = Convert.ToInt32(cbMaMH.SelectedValue);
                ct.SlXuat = int.Parse(txtSlx.Text);

                string kq2 = busCTPX.Update(ct);

                // 3. Thông báo
                if (kq1 == "Sửa thành công!" && kq2 == "Sửa chi tiết thành công!")
                {
                    MessageBox.Show("Sửa thành công!");
                    LoadData();   // load phiếu xuất
                    LoadCTPX();   // load chi tiết phiếu xuất
                    Clear();
                }
                else
                {
                    MessageBox.Show("Sửa thất bại!\n" + kq1 + "\n" + kq2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sửa: " + ex.Message);
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            if (txtSPX.Text == "")
            {
                MessageBox.Show("Chọn dòng cần xóa!");
                return;
            }
            int ma = int.Parse(txtSPX.Text);
            DialogResult r = MessageBox.Show("Bạn chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
            {
                MessageBox.Show(bus.Delete(ma));
                LoadData();
                Clear();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult traloi;
            traloi = MessageBox.Show("Chắc không?", "Trả lời", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (traloi == DialogResult.OK) Application.Exit();
        }

        private void btn_reload_Click(object sender, EventArgs e)
        {
            LoadData();
            Clear();
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            string ma = txtLoc.Text.Trim();
            string ngay = "";
            if (dtLocngay.Checked)
            {
                ngay = dtLocngay.Value.ToString("yyyy-MM-dd");
            }
            dgvPhieuXuat.DataSource = bus.Search(ma, ngay);
        }

        private void btnKhongloc_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
