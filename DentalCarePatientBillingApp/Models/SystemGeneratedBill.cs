using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalCarePatientBillingApp.Models
{
    public class SystemGeneratedBill
    {
        public string PatientName { get; set; }
        public string BillingName { get; set; }
        public string BillingAddress { get; set; }
        public double BillAmount { get; set; }
        public int DaysPastDue { get; set; }

        public string InvoiceDate { get; set; }

    }
}
