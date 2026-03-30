using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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
        bool them = false;
        int skip = 1;
        int limit = 10;
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
            int maNV = 0;

            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {

                maNV = Convert.ToInt32(textBox1.Text);
            }
            skip = skip - 1;
           
            if (skip < 1) return;
            txt_page.Text = skip.ToString();
            DTO.NhanVien nv = new DTO.NhanVien()
            {
                Manv = maNV,
                TenNV = textBox2.Text,
                skip = (skip - 1 ) * limit,
                limit = limit
            };
            dataGridView1.DataSource = nhanvien.GetDataNhanVien(nv);
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void NhanVien_Load(object sender, EventArgs e)
        {
            int countNhanVine = nhanvien.GetCountNhanVien().Rows.Count;
            txt_page.Text = skip.ToString();
            lbl_totalPage.Text = "of " + ((int)Math.Ceiling((double)countNhanVine / limit));
            int maNV = 0;

            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {

                maNV = Convert.ToInt32(textBox1.Text);
            }
            DTO.NhanVien nv = new DTO.NhanVien()
            {
                Manv = maNV,
                TenNV = textBox2.Text,
                skip = (skip - 1) * limit,
                limit = limit
            };
            dataGridView1.DataSource = nhanvien.GetDataNhanVien(nv);
            dataGridView1.AllowUserToAddRows = false;
            LoadData();
            ResetData();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;
            
            if (r >= 0 ) {
                btn_xoa.Enabled = true;
                btn_sua.Enabled = true;
                lbl_MaNV.Visible = true;
                txt_MaNV.Visible = true;
                txt_DiaChi.Enabled = false;
                txt_MK.Visible = false;
                txt_SDT.Enabled = false;
                txt_TenNV.Enabled = false;
                txt_TK.Enabled = false;
                combGoiTinh.Enabled = false;
                comb_VaiTro.Enabled = false;
                dateTimeP_NgaySinh.Enabled = false;
                lbl_MatKhau.Visible = false;
                btn_Luu.Enabled = false;
                btn_huy.Enabled = false;
                btn_reload.Enabled = false;
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
        public void ClickThemSua()
        {
            txt_DiaChi.Enabled = true;
            txt_MK.Visible = true;
            txt_SDT.Enabled = true;
            txt_TenNV.Enabled = true;
            txt_TK.Enabled = true;
            combGoiTinh.Enabled = true;
            comb_VaiTro.Enabled = true;
            dateTimeP_NgaySinh.Enabled = true;
            lbl_MatKhau.Visible = true;
            btn_Luu.Enabled = true;
            btn_huy.Enabled = true;
            btn_reload.Enabled = true;
            btn_sua.Enabled = false;
            btn_xoa.Enabled = false;
            btn_them.Enabled = false;
            txt_MaNV.Visible = false;
            lbl_MaNV.Visible = false;

        }
        private void btn_them_Click(object sender, EventArgs e)
        {
            them = true;
           ClickThemSua();
        }
        public void ResetData()
        {
            txt_DiaChi.ResetText();
            txt_MaNV.ResetText();
            txt_MK.ResetText();
            txt_TenNV.ResetText();
            txt_SDT.ResetText();
            txt_TK.ResetText();
            combGoiTinh.ResetText();
            comb_VaiTro.ResetText();
            dateTimeP_NgaySinh.ResetText();
        }
        public void LoadData()
        {
            lbl_MaNV.Visible = true;
            txt_MaNV.Visible = true;
            txt_MaNV.Enabled = false;
            txt_DiaChi.Enabled = false;
            txt_MK.Visible = false;
            txt_SDT.Enabled = false;
            txt_TenNV.Enabled = false;
            txt_TK.Enabled = false;
            combGoiTinh.Enabled = false;
            comb_VaiTro.Enabled = false;
            dateTimeP_NgaySinh.Enabled = false;
            lbl_MatKhau.Visible = false;
            btn_Luu.Enabled = false;
            btn_huy.Enabled = false;
            btn_reload.Enabled = false;
            btn_xoa.Enabled = false;
            btn_sua.Enabled = false;
            btn_them.Enabled = true;
            

        }
        private void btn_Luu_Click(object sender, EventArgs e)
        {
            DateTime minDate = DateTime.Now.AddYears(-16);
            if (
                txt_DiaChi.Text == "" ||
               
                txt_SDT.Text == "" ||
                txt_TenNV.Text == "" ||
                txt_TK.Text == "" ||
                combGoiTinh.Text == "" ||
                comb_VaiTro.Text == ""

                )
            {
                MessageBox.Show("Bạn vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            if (dateTimeP_NgaySinh.Value > minDate)
            {
                MessageBox.Show("Nhân viên chưa đủ 16 tuổi!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            
            DTO.NhanVien nv = new DTO.NhanVien()
            {
                DiaChi = txt_DiaChi.Text,
                TaiKhoan = txt_TK.Text,
                DienThoai = txt_SDT.Text,
                TenNV = txt_TenNV.Text,
                MatKhau = txt_MK.Text,
                GioiTinh = combGoiTinh.Text,
                VaiTro = comb_VaiTro.Text,
                NgaySinh = dateTimeP_NgaySinh.Value.ToString(),
            };
          
            int maNV = 0;

            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {

                maNV = Convert.ToInt32(textBox1.Text);
            }

            DTO.NhanVien nvd = new DTO.NhanVien()
            {
                Manv = maNV,
                TenNV = textBox2.Text,
                skip = skip,
                limit = limit
            };
            if (them == true)
            {
                if (txt_MK.Text == "")
                {
                    MessageBox.Show("Bạn vui lòng điền mật khẩu!","Thông báo", MessageBoxButtons.OK);
                    return;
                }
                try {
                    if (txt_SDT.Text.Length > 10 || txt_SDT.Text.Length < 10)
                    {

                        MessageBox.Show("Số điện thoại phải đủ 10 số!", "Thông báo", MessageBoxButtons.OK);
                        return;
                    }
                    if (nhanvien.ThemNhanVien(nv))
                    {
                        MessageBox.Show("Thêm mới nhân viên thành công!", "Thông báo", MessageBoxButtons.OK);

                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = nhanvien.GetDataNhanVien(nvd);
                        dataGridView1.Refresh();
                        LoadData();
                        ResetData();
                    }
                }
                catch(SqlException ex)
                {

                    if (ex.Number == 2627 || ex.Number == 2601)
                    {
                        MessageBox.Show("Số điện thoại hoặc tài khoản đã tồn tại!");
                    }
                    else if (ex.Number == 2628)
                    {
                        MessageBox.Show("Số điện thoại quá dài! (tối đa 10 số)");
                    }
                    else if (ex.Number == 50000)
                    {
                        MessageBox.Show("Bạn vui lòng nhập đúng số định dạng!");
                    }
                    else
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }
               
            }
            else
            {
                DTO.NhanVien nvs = new DTO.NhanVien()
                {
                    DiaChi = txt_DiaChi.Text,
                    TaiKhoan = txt_TK.Text,
                    DienThoai = txt_SDT.Text,
                    TenNV = txt_TenNV.Text,
                    MatKhau = txt_MK.Text,
                    GioiTinh = combGoiTinh.Text,
                    VaiTro = comb_VaiTro.Text,
                    NgaySinh = dateTimeP_NgaySinh.Value.ToString(),
                    Manv = Convert.ToInt32(txt_MaNV.Text)
                };
                try {
                    if (txt_SDT.Text.Length >10 || txt_SDT.Text.Length < 10)
                    {

                        MessageBox.Show("Số điện thoại phải đủ 10 số!", "Thông báo", MessageBoxButtons.OK);
                        return;
                    }
                    if (nhanvien.SuaNhanVien(nvs))
                    {
                       
                        MessageBox.Show("Cập nhật nhân viên thành công!", "Thông báo", MessageBoxButtons.OK);
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = nhanvien.GetDataNhanVien(nvd);
                        dataGridView1.Refresh();
                        LoadData();
                        ResetData();
                    }
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627 || ex.Number == 2601)
                    {
                        MessageBox.Show("Số điện thoại hoặc tài khoản đã tồn tại!");
                    }
                    else if (ex.Number == 2628)
                    {
                        MessageBox.Show("Số điện thoại quá dài! (tối đa 10 số)");
                    }
                    else if (ex.Number == 50000)
                    {
                        MessageBox.Show("Bạn vui lòng nhập đúng số định dạng!");
                    }
                    else
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }
            }
        }

        private void btn_huy_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btn_reload_Click(object sender, EventArgs e)
        {
            txt_DiaChi.ResetText();
            txt_MK.ResetText();
            txt_TenNV.ResetText();
            txt_SDT.ResetText();
            txt_TK.ResetText();
            combGoiTinh.ResetText();
            comb_VaiTro.ResetText();
            dateTimeP_NgaySinh.ResetText();
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            them = false;
            ClickThemSua();

        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            DTO.NhanVien nv = new DTO.NhanVien()
            {
                Manv = Convert.ToInt32(txt_MaNV.Text),
                TenNV = txt_TenNV.Text
            };
            int maNV = 0;

            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {

                maNV = Convert.ToInt32(textBox1.Text);
            }

            DTO.NhanVien nvd = new DTO.NhanVien()
            {
                Manv = maNV,
                TenNV = textBox2.Text,
                skip = skip,
                limit = limit
            };
            try
            {
                if (nhanvien.XoaNhanvien(nv))
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = nhanvien.GetDataNhanVien(nvd);
                    dataGridView1.Refresh();
                    ResetData();
                    LoadData();
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message,"Thông báo",MessageBoxButtons.OK);
            }
          
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int maNV = 0;

            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {
                
                maNV = Convert.ToInt32(textBox1.Text);
            }
           
            DTO.NhanVien nv = new DTO.NhanVien()
            {
                Manv= maNV,
                TenNV= textBox2.Text,
                skip = skip,
                limit = limit
            };
            dataGridView1.DataSource =nhanvien.GetDataNhanVien(nv);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int maNV = 0;

            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {

                maNV = Convert.ToInt32(textBox1.Text);
            }
            skip = 1;
            txt_page.Text = skip.ToString();
            DTO.NhanVien nv = new DTO.NhanVien()
            {
                Manv = maNV,
                TenNV = textBox2.Text,
                skip = skip - 1,
                limit = limit
            };
            dataGridView1.DataSource = nhanvien.GetDataNhanVien(nv);
        }

        private void button3_Click(object sender, EventArgs e)
        {
             int countNhanVine =  nhanvien.GetCountNhanVien().Rows.Count;
            int maNV = 0;

            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {

                maNV = Convert.ToInt32(textBox1.Text);
            }
            int newSkip = skip + 1;
            if (newSkip > ((int)Math.Ceiling((double)countNhanVine / limit))) return;
            skip = newSkip;
           
            txt_page.Text = skip.ToString();
            DTO.NhanVien nv = new DTO.NhanVien()
            {
                Manv = maNV,
                TenNV = textBox2.Text,
                skip = (skip -1) *limit,
                limit = limit
            };
            dataGridView1.DataSource = nhanvien.GetDataNhanVien(nv);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int countNhanVine = nhanvien.GetCountNhanVien().Rows.Count;
            int maNV = 0;

            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {

                maNV = Convert.ToInt32(textBox1.Text);
            }
            skip = (int)Math.Ceiling((double)countNhanVine / limit);
            txt_page.Text = skip.ToString();
            DTO.NhanVien nv = new DTO.NhanVien()
            {
                Manv = maNV,
                TenNV = textBox2.Text,
                skip = (skip - 1) * limit,
                limit = limit
            };
            dataGridView1.DataSource = nhanvien.GetDataNhanVien(nv);
        }
    }
}
