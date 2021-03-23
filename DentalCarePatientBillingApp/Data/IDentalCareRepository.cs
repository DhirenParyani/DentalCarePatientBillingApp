using DentalCarePatientBillingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalCarePatientBillingApp.Data
{
    public interface IDentalCareRepository
    {
        void InitializeRepository();
        void InsertBillsData(int accountNumber, PatientBillData patientBillData);
        void InsertVisit(Visit visit);
        void InsertPatient(Patient patient);

        Dictionary<int, Patient> GetAccountNumberPatientMap();
        Dictionary<int, Visit> GetVisitNumberVisitMap();
        Dictionary<int, PatientBillData> GetBills();
        int GetBillsCount();
        int GetVisitsCount();
        int GetPatientsCount();
        bool UpdateBillSettlement(int billNumber);
        PatientSummary GetBillsSummaryForAPatient(int accountNumber);
        Dictionary<int, List<PatientBillData>> GetAccountNumberPatientBillDataMap();

        List<PatientBillData> GetBillDataForNewVisits();
        List<PatientBillData> GetBillDataForUnpaidBills();
        List<SystemGeneratedBill> GetBillsForAPatient(int accountNumber);
        bool InsertBillsForTheMonth();
    }
}
