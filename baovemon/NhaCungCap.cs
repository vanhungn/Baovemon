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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace baovemon
{
    public partial class NhaCungCap : Form
    {
        BUS.NhaCungCap nhaCungCap = new BUS.NhaCungCap();
        int skip = 1;
        int limit = 10;
        bool them = false;
        public NhaCungCap()
        {
            InitializeComponent();
        }

        private void NhaCungCap_Load(object sender, EventArgs e)
        {
            int maNV = 0;
            int countNhanVine = nhaCungCap.GetCountNhaCungCap().Rows.Count;
            txt_page.Text = skip.ToString();
            lbl_totalPage.Text ="of " + ((int)Math.Ceiling((double) countNhanVine / limit));
            if (!string.IsNullOrWhiteSpace(txt_searchTenNCC.Text))
            {

                maNV = Convert.ToInt32(txt_searchMaNCC.Text);
            }

            DTO.NhaCungCap ncc = new DTO.NhaCungCap()
            {
                MaNCC = maNV,
                TenNCC = txt_searchMaNCC.Text,
                skip = (skip - 1) * limit,
                limit = limit
            };
            dataGridView1.DataSource = nhaCungCap.GetNhaCungCap(ncc);
            dataGridView1.AllowUserToAddRows = false;
            LoadData();
            ResetData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

            int countNhanVine = nhaCungCap.GetCountNhaCungCap().Rows.Count;
            int maNV = 0;

            if (!string.IsNullOrWhiteSpace(txt_searchMaNCC.Text))
            {

                maNV = Convert.ToInt32(txt_searchMaNCC.Text);
            }
            skip =(int)Math.Ceiling((double)countNhanVine / limit);

            txt_page.Text = skip.ToString();
            DTO.NhaCungCap ncc = new DTO.NhaCungCap()
            {
                MaNCC = maNV,
                TenNCC = txt_searchTenNCC.Text,
                skip = (skip - 1) * limit,
                limit = limit
            };
            dataGridView1.DataSource = nhaCungCap.GetNhaCungCap(ncc);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int countNhanVine = nhaCungCap.GetCountNhaCungCap().Rows.Count;
            int maNV = 0;

            if (!string.IsNullOrWhiteSpace(txt_searchMaNCC.Text))
            {

                maNV = Convert.ToInt32(txt_searchMaNCC.Text);
            }
            skip = skip + 1;
            txt_page.Text = skip.ToString();
            if (skip > ((int)Math.Ceiling((double)countNhanVine / limit))) return;
            DTO.NhaCungCap ncc = new DTO.NhaCungCap()
            {
                MaNCC = maNV,
                TenNCC = txt_searchTenNCC.Text,
                skip = (skip - 1) * limit,
                limit = limit
            };
            dataGridView1.DataSource = nhaCungCap.GetNhaCungCap(ncc);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int maNV = 0;

            if (!string.IsNullOrWhiteSpace(txt_searchMaNCC.Text))
            {

                maNV = Convert.ToInt32(txt_searchMaNCC.Text);
            }
            skip = 0;
            txt_page.Text = skip.ToString();
            DTO.NhaCungCap nv = new DTO.NhaCungCap()
            {
                MaNCC = maNV,
                TenNCC = txt_searchTenNCC.Text,
                skip = (skip - 1) * limit,
                limit = limit
            };
            dataGridView1.DataSource = nhaCungCap.GetNhaCungCap(nv);
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int maNV = 0;

            if (!string.IsNullOrWhiteSpace(txt_searchMaNCC.Text))
            {

                maNV = Convert.ToInt32(txt_searchMaNCC.Text);
            }
            skip = skip - 1;
            txt_page.Text = skip.ToString();
            if (skip < 1) return;
            DTO.NhaCungCap nv = new DTO.NhaCungCap()
            {
                MaNCC = maNV,
                TenNCC = txt_searchTenNCC.Text,
                skip = (skip - 1) * limit,
                limit = limit
            };
            dataGridView1.DataSource = nhaCungCap.GetNhaCungCap(nv);
        }

        private void lbl_totalPage_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void btn_huy_Click(object sender, EventArgs e)
        {
            LoadData();
            ResetData();
        }

        private void btn_reload_Click(object sender, EventArgs e)
        {
            txt_DiaChi.ResetText();
            txt_TenNCC.ResetText();
            txt_SDT.ResetText();
            txt_Email.ResetText();
        }

        private void btn_Luu_Click(object sender, EventArgs e)
        {
            if (
                txt_DiaChi.Text == "" ||
                txt_SDT.Text == "" ||
                txt_TenNCC.Text == "" ||
                txt_Email.Text == "" 
                )
            {
                MessageBox.Show("Bạn vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
         
            DTO.NhaCungCap ncc = new DTO.NhaCungCap()
            {
                DiaChi = txt_DiaChi.Text,
                Email = txt_Email.Text,
                DienThoaiCC = txt_SDT.Text,
                TenNCC = txt_TenNCC.Text,
                
            };
           
            int maNV = 0;

            if (!string.IsNullOrWhiteSpace(txt_searchMaNCC.Text))
            {

                maNV = Convert.ToInt32(txt_searchMaNCC.Text);
            }

            DTO.NhaCungCap nvd = new DTO.NhaCungCap()
            {
                MaNCC = maNV,
                TenNCC = txt_searchTenNCC.Text,
                skip = skip,
                limit = limit
            };
            if (them == true)
            {

                try
                {
                    if (txt_SDT.Text.Length > 10 || txt_SDT.Text.Length < 10)
                    {

                        MessageBox.Show("Số điện thoại phải đủ 10 số!", "Thông báo", MessageBoxButtons.OK);
                        return;
                    }
                    if (nhaCungCap.ThemNhaCungCap(ncc))
                    {
                        MessageBox.Show("Thêm mới nhân viên thành công!", "Thông báo", MessageBoxButtons.OK);

                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = nhaCungCap.GetNhaCungCap(nvd);
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
                        MessageBox.Show(ex.Message);
                    }
                    else
                    {
                        Console.WriteLine(ex.Message);
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }

            }
            else
            {
                DTO.NhaCungCap nccs = new DTO.NhaCungCap()
                {
                    DiaChi = txt_DiaChi.Text,
                    Email = txt_Email.Text,
                    DienThoaiCC = txt_SDT.Text,
                    TenNCC = txt_TenNCC.Text,

                    MaNCC = Convert.ToInt32(txt_MaNCC.Text)
                };
                try
                {
                    if (txt_SDT.Text.Length > 10 || txt_SDT.Text.Length < 10)
                    {

                        MessageBox.Show("Số điện thoại phải đủ 10 số!", "Thông báo", MessageBoxButtons.OK);
                        return;
                    }
                    if (nhaCungCap.SuaNhaCungCap(nccs))
                    {

                        MessageBox.Show("Cập nhật nhân viên thành công!", "Thông báo", MessageBoxButtons.OK);
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = nhaCungCap.GetNhaCungCap(nvd);
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

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            DTO.NhaCungCap ncc = new DTO.NhaCungCap()
            {
                MaNCC = Convert.ToInt32(txt_MaNCC.Text),
                

            };
            int maNV = 0;

            if (!string.IsNullOrWhiteSpace(txt_searchTenNCC.Text))
            {

                maNV = Convert.ToInt32(txt_searchMaNCC.Text);
            }

            DTO.NhaCungCap nccc = new DTO.NhaCungCap()
            {
                MaNCC = maNV,
                TenNCC = txt_searchMaNCC.Text,
                skip = (skip - 1) * limit,
                limit = limit
            };
            if (nhaCungCap.XoaNhaCungCap(ncc))
            {
                MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = nhaCungCap.GetNhaCungCap(nccc);
                dataGridView1.Refresh();
                LoadData();
                ResetData();
            }
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            txt_DiaChi.Enabled = true;
            txt_SDT.Enabled = true;
            txt_Email.Enabled = true;
            txt_TenNCC.Enabled = true;
            btn_Luu.Enabled = true;
            btn_huy.Enabled = true;
            btn_reload.Enabled = true;
            btn_xoa.Enabled = false;
            btn_sua.Enabled = false;
            btn_them.Enabled = false;
        }
        public void ResetData()
        {
            txt_DiaChi.ResetText();
            txt_MaNCC.ResetText();
            txt_TenNCC.ResetText();
            txt_SDT.ResetText();
            txt_Email.ResetText();
        }
        public void LoadData()
        {
            lbl_MaNV.Visible = true;
            txt_MaNCC.Visible = true;
            txt_DiaChi.Enabled = false;
            txt_SDT.Enabled = false;
            txt_Email.Enabled = false;
            txt_TenNCC.Enabled = false;
            btn_Luu.Enabled = false;
            btn_huy.Enabled = false;
            btn_reload.Enabled = false;
            btn_xoa.Enabled = false;
            btn_sua.Enabled = false;
            btn_them.Enabled = true;


        }
        public void ClickThemSua()
        {
            txt_DiaChi.Enabled = true;
            txt_MaNCC.Visible = false;
            txt_SDT.Enabled = true;
            txt_TenNCC.Enabled = true;
            txt_Email.Enabled = true;
            btn_Luu.Enabled = true;
            btn_huy.Enabled = true;
            btn_reload.Enabled = true;
            btn_sua.Enabled = false;
            btn_xoa.Enabled = false;
            btn_them.Enabled = false;
            txt_MaNCC.Visible = false;
            lbl_MaNV.Visible = false;

        }
        private void btn_them_Click(object sender, EventArgs e)
        {
            them = true;
            ClickThemSua();
        }
      

        private void combGoiTinh_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimeP_NgaySinh_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txt_DiaChi_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_MK_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_TK_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            int maNV = 0;

            if (!string.IsNullOrWhiteSpace(txt_searchMaNCC.Text))
            {

                maNV = Convert.ToInt32(txt_searchMaNCC.Text);
            }

            DTO.NhaCungCap ncc = new DTO.NhaCungCap()
            {
                MaNCC = maNV,
                TenNCC = txt_searchTenNCC.Text,
                skip = skip,
                limit = limit
            };
            dataGridView1.DataSource = nhaCungCap.GetNhaCungCap(ncc);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comb_VaiTro_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txt_SDT_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_TenNV_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_MaNV_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void lbl_MatKhau_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void lbl_MaNV_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = e.RowIndex;

            if (r >= 0)
            {
                btn_xoa.Enabled = true;
                btn_sua.Enabled = true;
                lbl_MaNV.Visible = true;
                txt_MaNCC.Visible = true;
                txt_MaNCC.Enabled = false;
                txt_DiaChi.Enabled = false;
                txt_Email.Enabled = false;
                txt_SDT.Enabled = false;
                txt_TenNCC.Visible = true;
                btn_Luu.Enabled = false;
                btn_huy.Enabled = false;
                btn_reload.Enabled = false;
                txt_MaNCC.Text = dataGridView1.Rows[r].Cells["MaNCC"].Value.ToString();
                txt_DiaChi.Text = dataGridView1.Rows[r].Cells["DiaChi"].Value.ToString();
                txt_SDT.Text = dataGridView1.Rows[r].Cells["DienThoaiCC"].Value.ToString();
                txt_TenNCC.Text = dataGridView1.Rows[r].Cells["TenNCC"].Value.ToString();
                txt_Email.Text = dataGridView1.Rows[r].Cells["Email"].Value.ToString();
            }
        }
    }
}
