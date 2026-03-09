using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    internal class ChiTietPhieuNhap
    {
		private int _MaMh;
		private int _MaPX;
        private int _SlNhap;
        public int MaMh
        {
			get { return _MaMh; }
			set { _MaMh = value; }
		}
		public int SlNhap
        {
			get { return _SlNhap; }
			set { _SlNhap = value; }
		}
		public int MaPX
        {
			get { return _MaPX; }
			set { _MaPX = value; }
		}


	}
}
