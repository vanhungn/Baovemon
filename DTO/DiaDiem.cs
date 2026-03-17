using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DiaDiem
    {
		private int _MaDD;
		private string _TenDD;
		private string _DiaChi;
		private string _DienThoaiDD;
        private int _Manv;
        public int MaDD
        {
			get { return _MaDD; }
			set { _MaDD = value; }
		}
		public string TenDD
        {
			get { return _TenDD; }
			set { _TenDD = value; }
		}
		public string DiaChi
        {
			get { return _DiaChi; }
			set { _DiaChi = value; }
		}
		public string DienThoaiDD
        {
			get { return _DienThoaiDD; }
			set { _DienThoaiDD = value; }
		}
		public int Manv
        {
			get { return _Manv; }
			set { _Manv = value; }
		}
	}
}
