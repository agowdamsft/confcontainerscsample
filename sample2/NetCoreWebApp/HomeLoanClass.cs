using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApp
{
    public class HomeLoanClass
    {
        public string fname { get; set; }
        public string lname { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string ssn { get; set; }
        public string dob { get; set; }
        public string email { get; set; }
        public string contactphone { get; set; }
        public List<Employmentinfo> employmentinfo { get; set; }
        public int requestedloanamount { get; set; }
        public List<Assettsinfo> assettsinfo { get; set; }
    }

    public class Employmentinfo
    {
        public string ename { get; set; }
        public string datefrom { get; set; }
        public string dateto { get; set; }
    }

    public class Assettsinfo
    {
        public string type { get; set; }
        public int worth { get; set; }
        public string name { get; set; }
    }

    public class LoanSubmittedinfo
    {
        public string appid { get; set; }
        public string assignedloanofficer { get; set; }
        public DateTime datesubmitted { get; set; }
    }
}
