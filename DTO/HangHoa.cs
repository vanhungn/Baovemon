using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class HangHoa
    {
		private int _MaMh;
        private string _TenMh;
        private string _LoaiMh;
        private int _MaKho;
        private int _MaNCC;
        public int MaMh
        {
			get { return _MaMh; }
			set { _MaMh = value; }
		}
		

		public string TenMh
        {
			get { return _TenMh; }
			set { _TenMh = value; }
		}
		

		public string LoaiMh
        {
			get { return _LoaiMh; }
			set { _LoaiMh = value; }
		}
		

		public int MaKho
        {
			get { return _MaKho; }
			set { _MaKho = value; }
		}
		

		public int MaNCC
        {
			get { return _MaNCC; }
			set { _MaNCC = value; }
		}
	}
}
