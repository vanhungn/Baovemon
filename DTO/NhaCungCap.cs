using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NhaCungCap
    {
		private int _MaNCC;
		private string _TenNCC;
        private string _DiaChi;
        private string _DienThoaiCC;
        private string _Email;
        private int _skip;

        public int skip
        {
            get { return _skip; }
            set { _skip = value; }
        }
        private int _limit;

        public int limit
        {
            get { return _limit; }
            set { _limit = value; }
        }
        public int MaNCC
        {
			get { return _MaNCC; }
			set { _MaNCC = value; }
		}
		
		public string TenNCC
        {
			get { return _TenNCC; }
			set { _TenNCC = value; }
		}
		
		public string DiaChi
        {
			get { return _DiaChi; }
			set { _DiaChi = value; }
		}
		
		public string DienThoaiCC
        {
			get { return _DienThoaiCC; }
			set { _DienThoaiCC = value; }
		}
		
		public string Email
        {
			get { return _Email; }
			set { _Email = value; }
		}
	}
}
