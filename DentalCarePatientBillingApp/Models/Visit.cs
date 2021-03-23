using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalCarePatientBillingApp.Models
{
    public class Visit
    {
        public int VisitNumber { get; set; }
        public int AccountNumber { get; set; }
        public string DateOfService { get; set; }
        public double AmountCharged { get; set; }
      
    }
}
