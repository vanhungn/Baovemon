using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PhieuXuatDTO
    {
		private int _MaPX;
        private DateTime _NgayXuat;
        private int _Manv;
        private int _MaKho;
        public int MaPX
        {
			get { return _MaPX; }
			set { _MaPX = value; }
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
		public DateTime NgayXuat
        {
			get { return _NgayXuat; }
			set { _NgayXuat = value; }
		}


	}
}
