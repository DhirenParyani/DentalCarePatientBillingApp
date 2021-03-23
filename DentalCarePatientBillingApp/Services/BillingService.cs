using DentalCarePatientBillingApp.Data;
using DentalCarePatientBillingApp.Models;
using DentalCarePatientBillingApp.Utility;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DentalCarePatientBillingApp.Services
{
    public class BillingService : IBillingService
    {
        IDentalCareRepository dentalCareRepository;
        public BillingService(IDentalCareRepository repository)
        {
            dentalCareRepository = repository;
        }



        public IEnumerable<PatientSummary> GetPatientsBillingSummary()
        {
            List<PatientSummary> patientSummaries = new List<PatientSummary>();
            foreach (var accountNumber in dentalCareRepository.GetAccountNumberPatientBillDataMap().Keys)
            {

                patientSummaries.Add(dentalCareRepository.GetBillsSummaryForAPatient(accountNumber));
            }


            return patientSummaries;
        }
        public List<SystemGeneratedBill> GetBillsByAccountNumber(int accountNumber)
        {
            
            return dentalCareRepository.GetBillsForAPatient(accountNumber);
        }
        public bool RecordAPayment(int billNumber)
        {
            
            return dentalCareRepository.UpdateBillSettlement(billNumber);
        }


        public void GenerateBillsForTheMonth()
        {

            dentalCareRepository.InsertBillsForTheMonth();
        }

    }
    


}
