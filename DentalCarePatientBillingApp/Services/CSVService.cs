using CsvHelper;
using CsvHelper.Configuration;
using DentalCarePatientBillingApp.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DentalCarePatientBillingApp.Constants;

namespace DentalCarePatientBillingApp.Utility
{
    public class CSVService
    {
        
        public Dictionary<int, List<PatientBillData>> MapCSVFileToPatientBillModelWithAccountNumber()
        {
            var AccountNumberPatientBillDataMap = new Dictionary<int, List<PatientBillData>>(); 
            try
            {
                using (var reader = new StreamReader(Constants.Constants.BillsCSVPath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {


                    csv.Read();
                    csv.ReadHeader();
                    while (csv.Read())
                    {
                        int accountNumber = Int32.Parse(csv.GetField("AccountNumber"));
                        if (!AccountNumberPatientBillDataMap.ContainsKey(accountNumber))
                            AccountNumberPatientBillDataMap.Add(accountNumber, new List<PatientBillData>());
                        AccountNumberPatientBillDataMap[accountNumber].Add(csv.GetRecord<PatientBillData>());

                    }

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return AccountNumberPatientBillDataMap;
        }
        public Dictionary<int, PatientBillData> MapCSVFileToPatientBillModelWithBillNumber()
        {
            var BillNumberPatientBillDataMap = new Dictionary<int, PatientBillData>();
           try
            {
                using (var reader = new StreamReader(Constants.Constants.BillsCSVPath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {


                    csv.Read();
                    csv.ReadHeader();
                    while (csv.Read())
                    {
                        int billNumber = Int32.Parse(csv.GetField("BillNumber"));
                        /*if (!BillNumberPatientBillDataMap.ContainsKey(billNumber))
                            BillNumberPatientBillDataMap.Add(accountNumber, new List<PatientBillData>());*/

                        BillNumberPatientBillDataMap.Add(billNumber,csv.GetRecord<PatientBillData>());

                    }

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return BillNumberPatientBillDataMap;
        }

        public Dictionary<int, List<PatientBillData>> MapCSVFileToPatientBillModelWithVisitNumber()
        {
            var VisitNumberPatientBillDataMap = new Dictionary<int, List<PatientBillData>>();
            try
            {
                using (var reader = new StreamReader(Constants.Constants.BillsCSVPath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {


                    csv.Read();
                    csv.ReadHeader();
                    while (csv.Read())
                    {
                        int visitNumber = Int32.Parse(csv.GetField("VisitNumber"));
                        if (!VisitNumberPatientBillDataMap.ContainsKey(visitNumber))
                            VisitNumberPatientBillDataMap.Add(visitNumber, new List<PatientBillData>());
                        VisitNumberPatientBillDataMap[visitNumber].Add(csv.GetRecord<PatientBillData>());

                    }

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return VisitNumberPatientBillDataMap;
        }
        public Dictionary<int, Patient> MapCSVFileToPatientModelWithAccountNumber()
        {
            var AccountNumberPatientMap = new Dictionary<int, Patient>();
            try
            {
                using (var reader = new StreamReader(Constants.Constants.PatientsCSVPath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader();
                    while (csv.Read())
                    {
                        int accountNumber = Int32.Parse(csv.GetField("AccountNumber"));
                        AccountNumberPatientMap.Add(accountNumber, csv.GetRecord<Patient>());
                        
                    }
                   
                    
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return AccountNumberPatientMap;
        }

        public Dictionary<int, Visit> MapCSVFileToVisitModelWithVisitNumber()
        {
            //var AccountNumberVisitMap = new Dictionary<int, List<Visit>>();
            var VisitNumberVisitMap = new Dictionary<int, Visit>();

            try
            {
                using (var reader = new StreamReader(Constants.Constants.VisitsCSVPath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader();
                    while (csv.Read())
                    {
                        int VisitNumber = Int32.Parse(csv.GetField("VisitNumber"));
                        VisitNumberVisitMap.Add(VisitNumber, csv.GetRecord<Visit>());
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return VisitNumberVisitMap;
        }

        public void CreateFilesIfTheyDontExist()
        {
            if (!File.Exists(Constants.Constants.BillsCSVPath))
            {

                string header = "BillNumber,VisitNumber,AccountNumber,InvoiceDate,DueDate,AmountDue,IsSettled" + Environment.NewLine;

                File.WriteAllText(Constants.Constants.BillsCSVPath, header);
            }

            if (!File.Exists(Constants.Constants.PatientsCSVPath))
            {

                string header = "AccountNumber,PatientName,PatientAddress,IsInsured,InsuranceName,InsuranceAddress" + Environment.NewLine;

                File.WriteAllText(Constants.Constants.PatientsCSVPath, header);
            }
            if (!File.Exists(Constants.Constants.VisitsCSVPath))
            {

                string header = "VisitNumber,AccountNumber,DateOfService,AmountCharged" + Environment.NewLine;

                File.WriteAllText(Constants.Constants.VisitsCSVPath, header);
            }
        }

        public void AppendBillsDataToCSV(PatientBillData patientBillData)
        {
            CreateFilesIfTheyDontExist();
            string patientBillDataString = patientBillData.BillNumber + "," + patientBillData.VisitNumber + "," + patientBillData.AccountNumber + "," + patientBillData.InvoiceDate + "," + patientBillData.DueDate + "," + patientBillData.AmountDue + "," + patientBillData.IsSettled+ Environment.NewLine;
            File.AppendAllText(Constants.Constants.BillsCSVPath, patientBillDataString);
        }
        public void AppendPatientsDataToCSV(Patient patient)
        {
            CreateFilesIfTheyDontExist();
            string patientDataString = patient.AccountNumber + "," + patient.PatientName + "," + patient.PatientAddress + "," + patient.IsInsured + "," + patient.InsuranceName + "," + patient.InsuranceAddress+Environment.NewLine;
            File.AppendAllText(Constants.Constants.PatientsCSVPath, patientDataString);
        }

        public void AppendVisitsDataToCSV(Visit visit)
        {
            CreateFilesIfTheyDontExist();
            string visitDataString = visit.VisitNumber + "," + visit.AccountNumber + "," + visit.DateOfService + "," + visit.AmountCharged+ Environment.NewLine;
            File.AppendAllText(Constants.Constants.VisitsCSVPath, visitDataString);
        }
        
        public void UpdateBillSettlementInCSV(PatientBillData billData)
        {
            int visitNumber = billData.VisitNumber;
            List<String> lines = new List<String>();
            if (File.Exists(Constants.Constants.BillsCSVPath)) ;
            {
                using (StreamReader reader = new StreamReader(Constants.Constants.BillsCSVPath))
                {
                    String line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Contains(","))
                        {
                            String[] split = line.Split(',');

                            if (split[1].Equals(visitNumber.ToString()))
                            {
                                line = split[0] + "," + split[1] + "," + split[2] + "," + split[3] + "," + split[4] + "," + split[5] + "," + "TRUE";
                            }
                        }

                        lines.Add(line);
                    }
                }

                using (StreamWriter writer = new StreamWriter(Constants.Constants.BillsCSVPath, false))
                {
                    foreach (String line in lines)
                        writer.WriteLine(line);
                }
            }
        }

    }
}
