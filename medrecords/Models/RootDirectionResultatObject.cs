using System;
using System.Collections.Generic;
using System.Text;

namespace medrecords
{

    public class RootDirectionResultatObject
    {
        public int error_code { get; set; }
        public DataDirectionResult data { get; set; }
    }

    public class DataDirectionResult
    {
        public string EvnLabSample_id { get; set; }
        public string Lpu_id { get; set; }
        public string MedStaffFact_id { get; set; }
        public string PayType_id { get; set; }
        public Testlist[] TestList { get; set; }
    }

    public class Testlist
    {
        public string UslugaTest_id { get; set; }
        public string UslugaComplex_id { get; set; }
        public object UslugaTest_ResultValue { get; set; }
        public string Unit_id { get; set; }
        public string UslugaTest_ResultUnit { get; set; }
        public object UslugaTest_Comment { get; set; }
        public object UslugaTest_setDT { get; set; }
        public string UslugaTest_ResultLower { get; set; }
        public string UslugaTest_ResultUpper { get; set; }
        public object UslugaTest_ResultLowerCrit { get; set; }
        public object UslugaTest_ResultUpperCrit { get; set; }
        public object UslugaTest_ResultApproved { get; set; }
        public int UslugaTest_deleted { get; set; }
        public object UslugaTest_delDT { get; set; }
        public object UslugaTest_ResultCancelReason { get; set; }
        public object UslugaTest_ResultAppDate { get; set; }
        public object LabTest_id { get; set; }
        public object LabTest_Code { get; set; }
        public string UslugaTest_pid { get; set; }
        public string UslugaComplex_Code { get; set; }
    }



}
