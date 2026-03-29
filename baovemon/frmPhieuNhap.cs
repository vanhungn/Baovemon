using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using BUS;
using DTO;

namespace baovemon
{
    public partial class frmPhieuNhap : Form
    {
        PhieuNhapBUS bus = new PhieuNhapBUS();
        BUS.CTPhieuNhap busCTPN = new BUS.CTPhieuNhap();
        SqlConnection conn = new SqlConnection(
            @"Data Source=MEDIA\SQLEXPRESS;Initial Catalog=quanlykho;Integrated Security=True");
        public frmPhieuNhap()
        {
            InitializeComponent();
        }
        /* Hàm Load */
        void LoadData()
        {
            dgvPhieuNhap.DataSource = bus.GetAll();
            dgvPhieuNhap.AutoGenerateColumns = true;
            if (dgvPhieuNhap.Columns["TenKho"] != null)
                dgvPhieuNhap.Columns["TenKho"].Visible = false;
            if (dgvPhieuNhap.Columns["TenNV"] != null)
                dgvPhieuNhap.Columns["TenNV"].Visible = false;
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
            txtSPN.Text = "";
            cbManv.SelectedIndex = -1;
            cbMakho.SelectedIndex = -1;
            txtSln.Text = "";
            dtNgaynhappn.Value = DateTime.Now;
        }
        public void LoadCTPN()
        {
            string sql = @"
        SELECT ct.MaPN, ct.MaMh, hh.TenMh, ct.SlNhap
        hh.DonGia,
                    (ct.SlNhap * hh.DonGia) AS ThanhTien
                FROM CT_Phieu_Nhap ct
                JOIN hang_hoa hh ON ct.MaMh = hh.MaMh
                WHERE ct.MaPN = @MaPN";

            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvCTPN.DataSource = dt;
        }
        private void frmPhieuNhap_Load(object sender, EventArgs e)
        {
            LoadNhanVien();
            LoadKho();
            LoadMatHang();
            LoadData();
        }
        
        private void dgvPhieuNhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvPhieuNhap.Rows[e.RowIndex];

            if (row.Cells["MaPN"].Value == DBNull.Value) return;

            int maPN = Convert.ToInt32(row.Cells["MaPN"].Value);
            txtSPN.Text = maPN.ToString();

            // ===== FIX COMBOBOX =====
            if (row.Cells["Manv"].Value != DBNull.Value)
            {
                cbManv.SelectedValue = Convert.ToInt32(row.Cells["Manv"].Value);
            }

            if (row.Cells["MaKho"].Value != DBNull.Value)
            {
                cbMakho.SelectedValue = Convert.ToInt32(row.Cells["MaKho"].Value);
            }

            if (row.Cells["NgayNhap"].Value != DBNull.Value)
            {
                dtNgaynhappn.Value = Convert.ToDateTime(row.Cells["NgayNhap"].Value);
            }

            dgvCTPN.DataSource = bus.GetCTPN(maPN);
        }
        /* Thêm dữ liệu */
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cbManv.SelectedIndex == -1 || cbMakho.SelectedIndex == -1)
            {
                MessageBox.Show("Chọn nhân viên và kho!");
                return;
            }
            if (txtSln.Text == "")
            {
                MessageBox.Show("Nhập số lượng!");
                return;
            }
            try
            {
                PhieuNhapDTO pn = new PhieuNhapDTO();
                pn.Manv = Convert.ToInt32(cbManv.SelectedValue);
                pn.MaKho = Convert.ToInt32(cbMakho.SelectedValue);
                pn.NgayNhap = dtNgaynhappn.Value;
                int maPN = bus.InsertAndGetID(pn); 
                ChiTietPhieuNhap ct = new ChiTietPhieuNhap();
                ct.MaPN = maPN;
                ct.MaMh = Convert.ToInt32(cbMaMH.SelectedValue); 
                ct.SlNhap = int.Parse(txtSln.Text);
                bus.InsertCTPN(ct);
                MessageBox.Show("Thêm thành công!");
                LoadData();
                dgvCTPN.DataSource = bus.GetCTPN(maPN);
                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /* Sửa dữ liệu */
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtSPN.Text == "" || cbManv.SelectedValue == null || cbMakho.SelectedValue == null || cbMaMH.SelectedValue == null || txtSln.Text == "")
            {
                MessageBox.Show("Chọn đầy đủ thông tin!");
                return;
            }
            try
            {
                // 1. Sửa bảng PhieuNhap
                PhieuNhapDTO pn = new PhieuNhapDTO();
                pn.MaPN = int.Parse(txtSPN.Text);
                pn.Manv = Convert.ToInt32(cbManv.SelectedValue);
                pn.MaKho = Convert.ToInt32(cbMakho.SelectedValue);
                pn.NgayNhap = dtNgaynhappn.Value;

                string kq1 = bus.Update(pn);

                // 2. Sửa bảng CT_Phieu_Nhap (Mã mặt hàng + Số lượng)
                ChiTietPhieuNhap ct = new ChiTietPhieuNhap();
                ct.MaPN = int.Parse(txtSPN.Text);
                ct.MaMh = Convert.ToInt32(cbMaMH.SelectedValue);
                ct.SlNhap = int.Parse(txtSln.Text);

                string kq2 = busCTPN.Update(ct);

                // 3. Thông báo
                if (kq1 == "Sửa thành công!" && kq2 == "Sửa chi tiết thành công!")
                {
                    MessageBox.Show("Sửa thành công!");
                    LoadData();   // load phiếu nhập
                    LoadCTPN();   // load chi tiết phiếu nhập
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
       /* Lọc dữ liệu */
        private void btnLoc_Click_1(object sender, EventArgs e)
        {
            string ma = txtLoc.Text.Trim();
            string ngay = "";
            if (dtLocngay.Checked) 
            {
                ngay = dtLocngay.Value.ToString("yyyy-MM-dd");
            }
            dgvPhieuNhap.DataSource = bus.Search(ma, ngay);
        }
        private void btnKhongloc_Click_1(object sender, EventArgs e)
        {
            LoadData();
        }
        private void btn_reload_Click_1(object sender, EventArgs e)
        {
            LoadData();
            Clear();
        }
        private void btn_xoa_Click_1(object sender, EventArgs e)
        {
            if (txtSPN.Text == "")
            {
                MessageBox.Show("Chọn dòng cần xóa!");
                return;
            }
            int ma = int.Parse(txtSPN.Text);
            DialogResult r = MessageBox.Show("Bạn chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo);
            if (r == DialogResult.Yes)
            {
                MessageBox.Show(bus.Delete(ma));
                LoadData();
                Clear();
            }
        }
        private void btnThoat_Click_1(object sender, EventArgs e)
        {
            DialogResult traloi;
            traloi = MessageBox.Show("Chắc không?", "Trả lời", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (traloi == DialogResult.OK) Application.Exit();
        }
    }
}