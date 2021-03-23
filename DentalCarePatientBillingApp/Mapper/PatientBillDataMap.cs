using CsvHelper.Configuration;
using DentalCarePatientBillingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalCarePatientBillingApp.Mapper
{
    public class PatientBillDataMap: ClassMap<PatientBillData>
    {
        public PatientBillDataMap()
        {
            Map(x => x.BillNumber).Name("BillNumber");
            Map(x => x.VisitNumber).Name("VisitNumber");
            Map(x => x.AccountNumber).Name("AccountNumber");
            Map(x => x.InvoiceDate).Name("InvoiceDate");
            Map(x => x.DueDate).Name("DueDate");
            Map(x => x.AmountDue).Name("AmountDue");
            Map(x => x.IsSettled).Name("IsSettled");
        }
    }
}
