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

namespace baovemon
{
    public partial class FormKhachHang : Form
    {
        string strConn = @"Data Source=DESKTOP-3INS5UR\MSSQLSERVER01;Initial Catalog=quanlykho;Integrated Security=True";
        public FormKhachHang()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(strConn))
                {
                    conn.Open();
                    string sql = @"SELECT 
                                    MaKH AS [Mã khách hàng], 
                                    TenKH AS [Tên khách hàng], 
                                    DiaChi AS [Địa chỉ], 
                                    NgaySinh AS [Ngày sinh], 
                                    DienThoai AS [Điện thoại], 
                                    ThanhVien AS [Thành viên] 
                                   FROM KhachHang";

                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvKhachHang.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnLoc_Click(object sender, EventArgs e)
        {
            if (dgvKhachHang.Rows.Count == 0) return;

            string keyword = "";
            string columnName = "";

            if (radTimMaKH.Checked)
            {
                keyword = txtTimMaKH.Text.Trim().ToLower(); 
                columnName = "Mã khách hàng";
                if (string.IsNullOrEmpty(keyword))
                {
                    MessageBox.Show("Vui lòng nhập Mã khách hàng cần tìm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTimMaKH.Focus();
                    return;
                }
            }
            else if (radTimTenKH.Checked)
            {
                keyword = txtTimTenKH.Text.Trim().ToLower();
                columnName = "Tên khách hàng";
                if (string.IsNullOrEmpty(keyword))
                {
                    MessageBox.Show("Vui lòng nhập Tên khách hàng cần tìm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTimTenKH.Focus();
                    return;
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn tiêu chí tìm (Mã hoặc Tên)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            dgvKhachHang.ClearSelection();

            bool isFound = false;
            foreach (DataGridViewRow row in dgvKhachHang.Rows)
            {
                if (row.IsNewRow) continue;
                row.DefaultCellStyle.BackColor = Color.White;

                if (row.Cells[columnName].Value != null)
                {
                    string cellValue = row.Cells[columnName].Value.ToString().ToLower();

                    if (cellValue.Contains(keyword))
                    {
                        row.DefaultCellStyle.BackColor = Color.LightBlue;

                        if (!isFound)
                        {
                            dgvKhachHang.FirstDisplayedScrollingRowIndex = row.Index;
                            isFound = true;
                        }
                    }
                }
            }
            if (!isFound)
            {
                MessageBox.Show("Không tìm thấy khách hàng nào khớp với từ khóa!", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnKhongLoc_Click(object sender, EventArgs e)
        {
            if (dgvKhachHang.DataSource != null)
            {
                DataTable dt = (DataTable)dgvKhachHang.DataSource;
                dt.DefaultView.RowFilter = "";
            }
            txtTimMaKH.Clear();
            txtTimTenKH.Clear();
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];

                txtMaKH.Text = row.Cells["Mã khách hàng"].Value?.ToString();
                txtTenKH.Text = row.Cells["Tên khách hàng"].Value?.ToString();
                txtDiaChi.Text = row.Cells["Địa chỉ"].Value?.ToString();
                txtDienThoai.Text = row.Cells["Điện thoại"].Value?.ToString();

                cboThanhVien.Text = row.Cells["Thành viên"].Value?.ToString();

                if (row.Cells["Ngày sinh"].Value != DBNull.Value && row.Cells["Ngày sinh"].Value != null)
                {
                    dtpNgaySinh.Value = Convert.ToDateTime(row.Cells["Ngày sinh"].Value);
                }
            }
        }

        private void FormKhachHang_Load(object sender, EventArgs e)
        {

        }
    }
}
