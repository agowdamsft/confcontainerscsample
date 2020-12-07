using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalPortal.Models
{
    public class PatientInfoModel
    {
            public string fname { get; set; }
            public string lname { get; set; }
            public string address { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string ssn { get; set; }
            public string email { get; set; }
            public string dob { get; set; }
            public string contactphone { get; set; }
            public string drugallergies { get; set; }
            public string preexistingconditions { get; set; }
            public string dateadmitted { get; set; }
            public string insurancedetails { get; set; }
        

    }
}
