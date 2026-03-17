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
    }
}
