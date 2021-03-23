using DentalCarePatientBillingApp.Models;
using DentalCarePatientBillingApp.Services;
using DentalCarePatientBillingApp.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalCarePatientBillingApp.Data
{
    public class DentalCareRepository : IDentalCareRepository
    {
        private Dictionary<int, List<PatientBillData>> accountNumberPatientBillDataMap;
        private Dictionary<int, Patient> accountNumberPatientMap;
        private Dictionary<int, Visit> visitNumberVisitMap;
        private Dictionary<int, PatientBillData> billNumberBillDataMap;
        private Dictionary<int, List<PatientBillData>> visitNumberBillDataMap;
       
       
        public DentalCareRepository()
        {
           
            InitializeRepository();
        }

        public void InsertBillsData(int accountNumber, PatientBillData patientBillData)
        {
            if(accountNumberPatientMap.ContainsKey(accountNumber) && !accountNumberPatientBillDataMap.ContainsKey(accountNumber))
               accountNumberPatientBillDataMap.Add(accountNumber,new List<PatientBillData>());
            if (visitNumberVisitMap.ContainsKey(patientBillData.VisitNumber) && !visitNumberBillDataMap.ContainsKey(patientBillData.VisitNumber))
                visitNumberBillDataMap.Add(patientBillData.VisitNumber, new List<PatientBillData>());

             accountNumberPatientBillDataMap[accountNumber].Add(patientBillData);
            billNumberBillDataMap.Add(patientBillData.BillNumber, patientBillData);
            visitNumberBillDataMap[patientBillData.VisitNumber].Add(patientBillData);
        }

        public void InsertVisit(Visit visit)
        {
            if (accountNumberPatientMap.ContainsKey(visit.AccountNumber) && !visitNumberVisitMap.ContainsKey(visit.VisitNumber))
            {
                new CSVService().AppendVisitsDataToCSV(visit);
                visitNumberVisitMap.Add(visit.VisitNumber,visit);

            }
          
        }
        public void InsertPatient(Patient patient)
        {
            if (!accountNumberPatientMap.ContainsKey(patient.AccountNumber))
            {
                new CSVService().AppendPatientsDataToCSV(patient);
                accountNumberPatientMap.Add(patient.AccountNumber, patient);

            }

        }
        public Dictionary<int, List<PatientBillData>> GetAccountNumberPatientBillDataMap()
        {
            return accountNumberPatientBillDataMap;

        }

        public Dictionary<int, Patient> GetPatientsData()
        {
            return accountNumberPatientMap;
        }
        public Dictionary<int, Visit> GetVisitsData()
        {
            return visitNumberVisitMap;
        }
       

        public int GetBillsCount()
        {
            return billNumberBillDataMap.Count;
        }
        public int GetVisitsCount()
        {
            return visitNumberVisitMap.Count;
        }
        public int GetPatientsCount()
        {
            return accountNumberPatientBillDataMap.Count;
        }

        public Dictionary<int, PatientBillData> GetBills()
        {
            return billNumberBillDataMap;
        }

        public PatientSummary GetBillsSummaryForAPatient(int accountNumber)
        {
            Patient patient = GetPatientsData()[accountNumber];
            List<PatientBillData> patientBills = GetAccountNumberPatientBillDataMap()[accountNumber];
            PatientSummary patientSummary = new PatientSummary();
            patientSummary.AccountNumber = patient.AccountNumber;
            patientSummary.PatientName = patient.PatientName;
            double TotalBalanceDue = 0.0;
            List<int> distinctVisits = new List<int>();
            foreach (PatientBillData billData in patientBills)
            {
                //This Calculation should be for distinct visits
                DateTime BillDueDate = DateTime.Parse(billData.DueDate);
                int DateDifference = (System.DateTime.Now.Date - BillDueDate.Date).Days;
               
                if (!distinctVisits.Contains(billData.VisitNumber) && !billData.IsSettled)
                {
                    distinctVisits.Add(billData.VisitNumber);
                    TotalBalanceDue += billData.AmountDue;
                }
                   

            }
            patientSummary.BalanceDue = TotalBalanceDue;

            return patientSummary;
        }


        public List<PatientBillData> GetBillDataForNewVisits()
        {
            List<PatientBillData> patientBillsDataList = new List<PatientBillData>();
           
            int billNumberCount = GetBillsCount();
            foreach (int visitNumber in GetVisitsData().Keys)
            {
                Visit visit = GetVisitsData()[visitNumber];
                DateTime dateOfService = DateTime.Parse(visit.DateOfService);
                int daysDifference = (System.DateTime.Now.Date - dateOfService.Date).Days;
                int numberOfDaysInCurrentMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                if(!visitNumberBillDataMap.ContainsKey(visit.VisitNumber))
                {
                    //||  daysDifference <= numberOfDaysInCurrentMonth
                    billNumberCount++;
                    PatientBillData patientBillData = new PatientBillData();
                    patientBillData.VisitNumber = visit.VisitNumber;
                    patientBillData.BillNumber = billNumberCount;
                    patientBillData.AccountNumber = visit.AccountNumber;
                    patientBillData.AmountDue = visit.AmountCharged;
                    DateTime invoiceDate = System.DateTime.Now.Date;
                    patientBillData.InvoiceDate = invoiceDate.ToString();
                    patientBillData.DueDate = invoiceDate.AddDays(15).Date.ToString();
                    patientBillsDataList.Add(patientBillData);
                }
            }

            return patientBillsDataList;
        }

        public List<PatientBillData> GetBillDataForUnpaidBills()
        {
            List<PatientBillData> patientBillsDataList = new List<PatientBillData>();
            HashSet<int> distinctVisits = new HashSet<int>();
            int billNumberCount = GetBillsCount();
            foreach (PatientBillData billData in GetBills().Values)
            {
                if (!distinctVisits.Contains(billData.VisitNumber) && !billData.IsSettled)
                {
                    billNumberCount++;
                    PatientBillData patientBillData = new PatientBillData();
                    patientBillData.BillNumber = billNumberCount;
                    patientBillData.AccountNumber = billData.AccountNumber;
                    patientBillData.AmountDue = billData.AmountDue;
                    patientBillData.IsSettled = false;
                    patientBillData.VisitNumber = billData.VisitNumber;
                    patientBillData.InvoiceDate = billData.InvoiceDate;
                    DateTime newDueDate = System.DateTime.Now.Date.AddDays(15).Date;
                    patientBillData.DueDate = newDueDate.ToString();
                    patientBillsDataList.Add(patientBillData);
                    distinctVisits.Add(billData.VisitNumber);
                    
                }

            }

            return patientBillsDataList;
        }

        public List<SystemGeneratedBill> GetBillsForAPatient(int accountNumber)
        {
            Patient patient = GetPatientsData()[accountNumber];
            List<PatientBillData> patientBills = GetAccountNumberPatientBillDataMap()[accountNumber];
            List<SystemGeneratedBill> systemGeneratedBillsList = new List<SystemGeneratedBill>();
           
            foreach (PatientBillData billData in patientBills)
            {


                DateTime BillDueDate = DateTime.Parse(billData.DueDate);
                int DateDifference = (System.DateTime.Now.Date - BillDueDate.Date).Days;
                if (!billData.IsSettled)
                {
                    SystemGeneratedBill systemGeneratedBill = new SystemGeneratedBill();
                    systemGeneratedBill.PatientName = patient.PatientName;
                    if (patient.IsInsured)
                    {
                        systemGeneratedBill.BillingName = patient.InsuranceName;
                        systemGeneratedBill.BillingAddress = patient.InsuranceAddress;
                    }
                    else
                    {
                        systemGeneratedBill.BillingName = patient.PatientName;
                        systemGeneratedBill.BillingAddress = patient.PatientAddress;
                    }
                    systemGeneratedBill.DaysPastDue = DateDifference;
                    systemGeneratedBill.InvoiceDate = DateTime.Parse(billData.InvoiceDate).ToString("MMM dd yyyy");
                    systemGeneratedBill.BillAmount = billData.AmountDue;

                    systemGeneratedBillsList.Add(systemGeneratedBill);
                }


            }
            systemGeneratedBillsList.Sort((a, b) => DateTime.Parse(a.InvoiceDate).CompareTo(DateTime.Parse(b.InvoiceDate)));
            return systemGeneratedBillsList;

        }
        public bool UpdateBillSettlement(int billNumber)
        {
            PatientBillData billData = GetBills()[billNumber];
            new CSVService().UpdateBillSettlementInCSV(billData);
            InitializeRepository();
            return true;
        }
        public bool InsertBillsForTheMonth()
        {
            List<PatientBillData> patientBillDataForNewVisits = GetBillDataForNewVisits();
            List<PatientBillData> patientBillDataForUnpaidBills = GetBillDataForUnpaidBills();
            foreach (PatientBillData billData in patientBillDataForNewVisits)
            {
                new CSVService().AppendBillsDataToCSV(billData);
                InsertBillsData(billData.AccountNumber, billData);
            }
           
            foreach (PatientBillData billData in patientBillDataForUnpaidBills)
            {
                new CSVService().AppendBillsDataToCSV(billData);
                InsertBillsData(billData.AccountNumber, billData);
            }
            InitializeRepository();
            return true;
        }

        public void InitializeRepository()
        {
            accountNumberPatientBillDataMap = new Dictionary<int, List<PatientBillData>>();
            accountNumberPatientMap = new Dictionary<int, Patient>();
            visitNumberVisitMap = new Dictionary<int, Visit>();
            billNumberBillDataMap = new Dictionary<int, PatientBillData>();
            visitNumberBillDataMap = new Dictionary<int, List<PatientBillData>>();
            new CSVService().CreateFilesIfTheyDontExist();
            new CSVService().MapCSVFileToPatientBillModel(accountNumberPatientBillDataMap, billNumberBillDataMap, visitNumberBillDataMap);
            //billNumberBillDataMap= new CSVService().MapCSVFileToPatientBillModelWithBillNumber();
            //visitNumberBillDataMap= new CSVService().MapCSVFileToPatientBillModelWithVisitNumber();
            accountNumberPatientMap = new CSVService().MapCSVFileToPatientModelWithAccountNumber();
            visitNumberVisitMap= new CSVService().MapCSVFileToVisitModelWithVisitNumber();
        }

        
    }
}
