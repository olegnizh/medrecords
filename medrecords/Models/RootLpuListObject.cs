using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medrecords
{
    class RootLpuListObject
    {
        public string error_code { get; set; }
        public string error_msg { get; set; }
        public DatumLpuList[] data { get; set; }

    }

    class DatumLpuList
    {
        public string Lpu_id { get; set; }
        public string Org_Name { get; set; }
        public string Org_Nick { get; set; }


    }


}
