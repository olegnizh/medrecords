using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medrecords
{
    class RootLpuObject
    {
        public string error_code { get; set; }
        public string error_msg { get; set; }
        public DatumLpu[] data { get; set; }

    }

    class DatumLpu
    {
        public string Lpu_id { get; set; }
        public string Lpu_Name { get; set; }
        public string Lpu_f003mcod { get; set; }
        public string LPU_OID { get; set; }

    }


}
