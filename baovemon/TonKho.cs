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
    public partial class TonKho : Form
    {
        string connectionString = @"Data Source=DESKTOP-3INS5UR\MSSQLSERVER01;Initial Catalog=quanlykho;Integrated Security=True";

        public TonKho()
        {
            InitializeComponent();
        }

        private void TonKho_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT MaKho AS [Mã Kho], MaMh AS [Mã Mặt Hàng], SoLuong AS [Số Lượng] FROM ton_kho";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvTonKho.DataSource = dt;
                    ResetColor();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối CSDL: " + ex.Message);
            }
        }
        private void ResetColor()
        {
            foreach (DataGridViewRow row in dgvTonKho.Rows)
            {
                if (!row.IsNewRow)
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }
            }
        }
        private void btnLoc_Click(object sender, EventArgs e)
        {
            ResetColor();

            string keyword = "";
            string columnName = "";


            if (radTimMaKho.Checked)
            {
                keyword = txtTimMaKho.Text.Trim();
                columnName = "Mã Kho";
            }
            else if (radTimMaMH.Checked)
            {
                keyword = txtTimMaMH.Text.Trim();
                columnName = "Mã Mặt Hàng";
            }

            if (string.IsNullOrEmpty(keyword))
            {
                return;
            }


            bool found = false;
            foreach (DataGridViewRow row in dgvTonKho.Rows)
            {
                if (!row.IsNewRow)
                {
                    var cellValue = row.Cells[columnName].Value;

                    if (cellValue != null && cellValue.ToString().Contains(keyword))
                    {
                        row.DefaultCellStyle.BackColor = Color.LightBlue;
                        found = true;
                    }
                }
            }

            if (!found)
            {
                MessageBox.Show("Không tìm thấy dữ liệu khớp với từ khóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnKhongLoc_Click(object sender, EventArgs e)
        {
            txtTimMaKho.Clear();
            txtTimMaMH.Clear();
            if (radTimMaKho.Checked) txtTimMaKho.Focus();
            else txtTimMaMH.Focus();

            ResetColor();
        }

        private void dgvTonKho_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvTonKho.Rows[e.RowIndex];
                txtMaKho.Text = row.Cells["Mã Kho"].Value.ToString();
                txtMaMH.Text = row.Cells["Mã Mặt Hàng"].Value.ToString();
                txtSoLuong.Text = row.Cells["Số Lượng"].Value.ToString();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKho.Text) || string.IsNullOrWhiteSpace(txtMaMH.Text) || string.IsNullOrWhiteSpace(txtSoLuong.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO ton_kho (MaKho, MaMh, SoLuong) VALUES (@MaKho, @MaMh, @SoLuong)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKho", int.Parse(txtMaKho.Text));
                        cmd.Parameters.AddWithValue("@MaMh", int.Parse(txtMaMH.Text));
                        cmd.Parameters.AddWithValue("@SoLuong", int.Parse(txtSoLuong.Text));

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Cập nhật lại DataGridView và làm sạch form
                        LoadData();
                        btnLamMoi_Click(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm (Có thể mã kho/mã MH không tồn tại hoặc đã bị trùng): " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKho.Text) || string.IsNullOrWhiteSpace(txtMaMH.Text))
            {
                MessageBox.Show("Vui lòng chọn bản ghi cần sửa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE ton_kho SET SoLuong = @SoLuong WHERE MaKho = @MaKho AND MaMh = @MaMh";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaKho", int.Parse(txtMaKho.Text));
                        cmd.Parameters.AddWithValue("@MaMh", int.Parse(txtMaMH.Text));
                        cmd.Parameters.AddWithValue("@SoLuong", int.Parse(txtSoLuong.Text));

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                            btnLamMoi_Click(sender, e);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy bản ghi để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKho.Text) || string.IsNullOrWhiteSpace(txtMaMH.Text))
            {
                MessageBox.Show("Vui lòng chọn bản ghi cần xóa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa tồn kho này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "DELETE FROM ton_kho WHERE MaKho = @MaKho AND MaMh = @MaMh";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaKho", int.Parse(txtMaKho.Text));
                            cmd.Parameters.AddWithValue("@MaMh", int.Parse(txtMaMH.Text));

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            LoadData();
                            btnLamMoi_Click(sender, e);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaKho.Clear();
            txtMaMH.Clear();
            txtSoLuong.Clear();

            txtMaKho.Enabled = true;
            txtMaMH.Enabled = true;
            LoadData();
            txtMaKho.Focus();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn thoát chương trình?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
            }
        }
        private void HienThi(int index)
        {
            if (index >= 0 && index < dgvTonKho.Rows.Count && !dgvTonKho.Rows[index].IsNewRow)
            {
               
                dgvTonKho.ClearSelection();

                dgvTonKho.Rows[index].Selected = true;
                dgvTonKho.CurrentCell = dgvTonKho.Rows[index].Cells[0];

                txtMaKho.Text = dgvTonKho.Rows[index].Cells["Mã Kho"].Value.ToString();
                txtMaMH.Text = dgvTonKho.Rows[index].Cells["Mã Mặt Hàng"].Value.ToString();
                txtSoLuong.Text = dgvTonKho.Rows[index].Cells["Số Lượng"].Value.ToString();

                txtMaKho.Enabled = false;
                txtMaMH.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HienThi(0);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dgvTonKho.CurrentRow != null)
            {
                int currentIndex = dgvTonKho.CurrentRow.Index;
                HienThi(currentIndex - 1);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dgvTonKho.CurrentRow != null)
            {
                int currentIndex = dgvTonKho.CurrentRow.Index;
                HienThi(currentIndex + 1);  
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int lastIndex = dgvTonKho.Rows.Count - 1;
            if (dgvTonKho.AllowUserToAddRows)
            {
                lastIndex -= 1;
            }

            HienThi(lastIndex);
        }
    }
}
