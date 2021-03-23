using DentalCarePatientBillingApp.Data;
using DentalCarePatientBillingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalCarePatientBillingApp.Services
{
    public interface IBillingService
    {
        IEnumerable<PatientSummary> GetPatientsBillingSummary();
        List<SystemGeneratedBill> GetBillsByAccountNumber(int accountNumber);
        void GenerateBillsForTheMonth();

        bool RecordAPayment(int billNumber);

    }
}
