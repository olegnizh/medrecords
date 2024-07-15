using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medrecords
{
    public class RootPersonObject
    {
        public int error_code { get; set; }
        public string error_msg { get; set; }
        public DatumPerson[] data { get; set; }
    }

    public class DatumPerson
    {
        public string Person_id { get; set; }
        public string PersonSurName_SurName { get; set; }
        public string PersonFirName_FirNam { get; set; }
        public string PersonSecName_SecName { get; set; }
        public string PersonBirthDay_BirthDay { get; set; }
        public string Person_Sex_id { get; set; }
        public string PersonPhone_Phone { get; set; }
        public string PersonSnils_Snils { get; set; }
        public string SocStatus_id { get; set; }
        public string DeputyOrg_id { get; set; }
        public string UAddress_id { get; set; }
        public string PAddress_id { get; set; }
        public string Org_id { get; set; }
        public string Post_id { get; set; }
        public string Lpu_id { get; set; }
        public string KLCountry_id { get; set; }

    }


}
