using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Models.RequestModel
{
    public class CompanyRequest
    {
        public string Business_Id { get; set; }
        public string Organization_Id { get; set; }
        public string Organization_Name_Ar { get; set; }
        public string Organization_Name_En { get; set; }
        public string Po_Box { get; set; }
        public string Phone_No { get; set; }
        public string Cr_Number { get; set; }
        public string Fax_No { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Membership_Date { get; set; }
        public string Membership_Renew_Date { get; set; }
        public string Membership_Next_RenewDdate { get; set; }
        public string Company_Type { get; set; }
        public string Contact_Person_Ar { get; set; }
        public string Contact_Person_En { get; set; }
        public string Address_En { get; set; }
        public string Address_Ar { get; set; }
        public string City_Name_Ar { get; set; }
        public string City_Name_En { get; set; }
        public string Area { get; set; }
    }
}
