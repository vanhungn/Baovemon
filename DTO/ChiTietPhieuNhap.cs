using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ChiTietPhieuNhap
    {
		private int _MaMh; 
		private int _MaPN;
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
		public int MaPN
        {
			get { return _MaPN; }
			set { _MaPN = value; }
		}


	}
}
