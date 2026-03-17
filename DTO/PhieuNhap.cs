using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PhieuNhap
    {
		private int _MaPN;
        private string _NgayNhap;
        private int _Manv;
        private int _MaKho;
        public int MaPN
        {
			get { return _MaPN; }
			set { _MaPN = value; }
		}
		public int Manv
        {
			get { return _Manv; }
			set { _Manv = value; }
		}
		public int MaKho
        {
			get { return _MaKho; }
			set { _MaKho = value; }
		}
		public string NgayNhap
        {
			get { return _NgayNhap; }
			set { _NgayNhap = value; }
		}
	}
}
