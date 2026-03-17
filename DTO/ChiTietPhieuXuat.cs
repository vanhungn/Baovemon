using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ChiTietPhieuXuat
    {
		private int _MaMh;

		public int MaMh
        {
			get { return _MaMh; }
			set { _MaMh = value; }
		}
		private int _SlXuat;

		public int SlXuat
        {
			get { return _SlXuat; }
			set { _SlXuat = value; }
		}
		private int _MaPX;

		public int MaPX
        {
			get { return _MaPX; }
			set { _MaPX = value; }
		}



	}
}
