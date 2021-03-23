using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalCarePatientBillingApp.Models
{
    public class Patient
    {
        public int AccountNumber { get; set; }
        public string PatientName { get; set; }
        public string PatientAddress { get; set; }

        //public Insurance Insurance;
        public bool  IsInsured { get; set; }

        public string InsuranceName { get; set; }
        public string InsuranceAddress { get; set; }

    }
}
