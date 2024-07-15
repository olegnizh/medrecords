using System;
using System.Collections.Generic;
using System.Text;

namespace medrecords
{
    public class RootDirectionObject
    {
        public int error_code { get; set; }
        public string error_msg { get; set; }
        public DataDirection data { get; set; }
    }

    public class DataDirection
    {
        public string EvnDirection_id { get; set; }
        public string Evn_id { get; set; }
        public string EvnLabRequest_id { get; set; }
        public string EvnQueue_id { get; set; }
        public string EvnPrescr_id { get; set; }

    }



}





