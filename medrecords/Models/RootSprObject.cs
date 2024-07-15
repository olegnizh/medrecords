using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medrecords
{
    class RootSprObject
    {
        public string error_code { get; set; }
        public string error_msg { get; set; }
        public DatumSpr[] data { get; set; }

    }

    class DatumSpr
    {
        public string id { get; set; }
        public string OrgType_id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string KLRgn_id { get; set; }
        public string begDate { get; set; }
        public string endDate { get; set; }
    }


}
