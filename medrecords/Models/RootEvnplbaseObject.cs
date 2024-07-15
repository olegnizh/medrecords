using System;
using System.Collections.Generic;
using System.Text;

namespace medrecords
{
    public class RootEvnplbaseObject
    {
        public int error_code { get; set; }
        public string error_msg { get; set; }
        public DataEvnplbase data { get; set; }

    }

    public class DataEvnplbase
    {
        public string EvnPLBase_id { get; set; }
        public string EvnVizitPL_id { get; set; }
        //public object EvnVizitPL_NumGroup { get; set; }
    }


}





