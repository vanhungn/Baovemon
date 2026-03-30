using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using DTO;
using BUS;



namespace baovemon
{
    public partial class FormQuanLySanPham : Form
    {
        string strConn = @"Data Source=DESKTOP-3INS5UR\MSSQLSERVER01;Initial Catalog=quanlykho;Integrated Security=True";
        SqlConnection conn;
        public FormQuanLySanPham()
        {
            InitializeComponent();
        }

        private void FormQuanLySanPham_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(strConn);
            LoadData();
            LoadComboBox();
        }
        void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    string sql = @"SELECT h.MaMh, h.TenMh, h.LoaiMh, n.TenNCC 
                           FROM hang_hoa h 
                           JOIN nha_cung_cap n ON h.MaNCC = n.MaNCC";

                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvSanPham.DataSource = dt;
                    dgvSanPham.Columns["MaMh"].HeaderText = "Mã MH";
                    dgvSanPham.Columns["TenMh"].HeaderText = "Tên mặt hàng";
                    dgvSanPham.Columns["LoaiMh"].HeaderText = "Loại hàng";
                    dgvSanPham.Columns["TenNCC"].HeaderText = "Nhà cung cấp";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }
        void LoadComboBox()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    conn.Open();

                    string sqlNCC = "SELECT MaNCC, TenNCC FROM nha_cung_cap ORDER BY TenNCC ASC";
                    SqlDataAdapter daNCC = new SqlDataAdapter(sqlNCC, conn);
                    DataTable dtNCC = new DataTable();
                    daNCC.Fill(dtNCC);

                    cboNCC.DataSource = dtNCC;
                    cboNCC.DisplayMember = "TenNCC";
                    cboNCC.ValueMember = "MaNCC";
                    cboNCC.SelectedIndex = -1;
                    string sqlLoai = "SELECT DISTINCT LoaiMh FROM hang_hoa WHERE LoaiMh IS NOT NULL";
                    SqlDataAdapter daLoai = new SqlDataAdapter(sqlLoai, conn);
                    DataTable dtLoai = new DataTable();
                    daLoai.Fill(dtLoai);

                    cboLoaiSP.DataSource = dtLoai;
                    cboLoaiSP.DisplayMember = "LoaiMh";
                    cboLoaiSP.ValueMember = "LoaiMh";
                    cboLoaiSP.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục ComboBox: " + ex.Message, "Thông báo");
            }
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            string searchKeyword = txtTimKiem.Text.Trim();

            if (string.IsNullOrEmpty(searchKeyword))
            {
                MessageBox.Show("Vui lòng nhập nội dung tìm kiếm!");
                return;
            }

            bool found = false;
            Color highlightColor = Color.LightBlue;

            foreach (DataGridViewRow row in dgvSanPham.Rows)
            {
                if (row.IsNewRow) continue;

                bool isMatch = false;

                if (rdoMaSP.Checked)
                {
                    if (row.Cells["MaMh"].Value != null &&
                        row.Cells["MaMh"].Value.ToString().Equals(searchKeyword))
                    {
                        isMatch = true;
                    }
                }
                else
                {
                    if (row.Cells["TenMh"].Value != null &&
                        row.Cells["TenMh"].Value.ToString().ToLower().Contains(searchKeyword.ToLower()))
                    {
                        isMatch = true;
                    }
                }

                if (isMatch)
                {
                    row.DefaultCellStyle.BackColor = highlightColor;
                    if (!found)
                    {
                        dgvSanPham.FirstDisplayedScrollingRowIndex = row.Index;
                        found = true;
                    }
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }
            }

            if (!found)
            {
                MessageBox.Show("Không tìm thấy kết quả nào khớp!");
            }
        }

        private void btnKhongLoc_Click(object sender, EventArgs e)
        {
            if (dgvSanPham.DataSource == null) return;

            DataTable dt = (DataTable)dgvSanPham.DataSource;
            string filter = "";

            if (string.IsNullOrEmpty(txtTimKiem.Text))
            {
                MessageBox.Show("Vui lòng nhập nội dung tìm kiếm!");
                return;
            }

            try
            {
                if (rdoMaSP.Checked)
                {
                    if (int.TryParse(txtTimKiem.Text, out int ma))
                    {
                        filter = string.Format("MaMh = {0}", ma);
                    }
                    else
                    {
                        MessageBox.Show("Mã sản phẩm phải là số!");
                        return;
                    }
                }
                else
                {
                    filter = string.Format("TenMh LIKE '%{0}%'", txtTimKiem.Text);
                }

                dt.DefaultView.RowFilter = filter;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc: " + ex.Message);
            }
        }

        private void dgvSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSanPham.Rows[e.RowIndex];

                txtMaSP.Text = row.Cells["MaMh"].Value?.ToString();
                txtTenSP.Text = row.Cells["TenMh"].Value?.ToString();
                cboLoaiSP.Text = row.Cells["LoaiMh"].Value?.ToString();
                cboNCC.Text = row.Cells["TenNCC"].Value?.ToString();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    conn.Open();
                    string sql = "INSERT INTO hang_hoa (TenMh, LoaiMh, MaNCC) VALUES (@ten, @loai, @ncc)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ten", txtTenSP.Text);
                    cmd.Parameters.AddWithValue("@loai", cboLoaiSP.Text);
                    cmd.Parameters.AddWithValue("@ncc", cboNCC.SelectedValue);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm mới thành công!");
                    LoadData();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    conn.Open();
                    string sql = "UPDATE hang_hoa SET TenMh=@ten, LoaiMh=@loai, MaNCC=@ncc WHERE MaMh=@ma";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ma", txtMaSP.Text);
                    cmd.Parameters.AddWithValue("@ten", txtTenSP.Text);
                    cmd.Parameters.AddWithValue("@loai", cboLoaiSP.Text);
                    cmd.Parameters.AddWithValue("@ncc", cboNCC.SelectedValue);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cập nhật thành công!");
                    LoadData();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(strConn))
                    {
                        conn.Open();
                        string sql = "DELETE FROM hang_hoa WHERE MaMh=@ma";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@ma", txtMaSP.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Xóa thành công!");
                        LoadData();
                        btnLamMoi_Click(null, null);
                    }
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 547) MessageBox.Show("Sản phẩm này đang có trong kho, không thể xóa!");
                    else MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaSP.Clear();
            txtTenSP.Clear();
            txtTimKiem.Clear();
            cboLoaiSP.SelectedIndex = -1;
            cboNCC.SelectedIndex = -1;
            btnKhongLoc_Click(null, null);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "Thoát", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
        }
        private void HienThiDong(int index)
        {
            if (index >= 0 && index < dgvSanPham.Rows.Count && !dgvSanPham.Rows[index].IsNewRow)
            {

                dgvSanPham.ClearSelection();
                dgvSanPham.Rows[index].Selected = true;
                dgvSanPham.FirstDisplayedScrollingRowIndex = index;

                DataGridViewRow row = dgvSanPham.Rows[index];
                txtMaSP.Text = row.Cells["MaMh"].Value?.ToString();
                txtTenSP.Text = row.Cells["TenMh"].Value?.ToString();
                cboLoaiSP.Text = row.Cells["LoaiMh"].Value?.ToString();
                cboNCC.Text = row.Cells["TenNCC"].Value?.ToString();

                txtViTri.Text = string.Format("Dòng {0} / {1}", index + 1, dgvSanPham.Rows.Count - (dgvSanPham.AllowUserToAddRows ? 1 : 0));
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dgvSanPham.Rows.Count > 0)
            {
                HienThiDong(0);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int index = dgvSanPham.CurrentRow.Index;
            if (index > 0)
            {
                HienThiDong(index - 1);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int index = dgvSanPham.CurrentRow.Index;
            int maxIndex = dgvSanPham.Rows.Count - (dgvSanPham.AllowUserToAddRows ? 2 : 1);
            if (index <= maxIndex)
            {
                HienThiDong(index + 1);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dgvSanPham.Rows.Count > 0)
            {
                int lastIndex = dgvSanPham.Rows.Count - (dgvSanPham.AllowUserToAddRows ? 2 : 1);
                HienThiDong(lastIndex);
            }
        }
    }
}
