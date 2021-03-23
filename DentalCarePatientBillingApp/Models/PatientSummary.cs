using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalCarePatientBillingApp.Models
{
    public class PatientSummary
    {
        public int AccountNumber { get; set; }
        public string PatientName { get; set; }
        public double BalanceDue { get; set; }
    }
}
