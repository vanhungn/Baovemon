using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TonKho
    {
		private int _MaKho;

		public int MaKho
        {
			get { return _MaKho; }
			set { _MaKho = value; }
		}
		private int _MaMh;

		public int MaMh
        {
			get { return _MaMh; }
			set { _MaMh = value; }
		}
		private int _soluong;

		public int soluong
        {
			get { return _soluong; }
			set { _soluong = value; }
		}



	}
}
