using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medrecords
{
    class RootRefbookObject
    {
        public string error_code { get; set; }
        public string error_msg { get; set; }
        public DatumRefbook[] data { get; set; }
    }

    class DatumRefbook
    {
        public string Refbook_Code { get; set; }
        public string Refbook_Name { get; set; }
        public string RefbookType_id { get; set; }
        public string Refbook_TableName { get; set; }
        
    }

}
