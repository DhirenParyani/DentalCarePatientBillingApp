using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalCarePatientBillingApp.Models
{
    public class PatientBillData
    {
        public int BillNumber { get; set; }
        public int VisitNumber { get; set; }
        public int AccountNumber { get; set; }
        public string InvoiceDate { get; set; }
        public string DueDate { get; set; }
        public double AmountDue { get; set; }
        public bool IsSettled { get; set; }

    }
}
