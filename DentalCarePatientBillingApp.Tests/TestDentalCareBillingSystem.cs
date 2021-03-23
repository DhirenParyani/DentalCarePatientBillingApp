using DentalCarePatientBillingApp.Controllers;
using DentalCarePatientBillingApp.Data;
using DentalCarePatientBillingApp.Models;
using DentalCarePatientBillingApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DentalCarePatientBillingApp.Tests
{
    public class TestDentalCareBillingSystem
    {
        //In controller this implementation of IDentalCareRepository is passed using Dependency Injection.
        static IDentalCareRepository dentalCareRepository = new DentalCareRepository();
        static PatientBillingSystemController patientBillingController = new PatientBillingSystemController(dentalCareRepository);
        static PatientController patientController = new PatientController(dentalCareRepository);
        static PatientVisitController patientVisit = new PatientVisitController(dentalCareRepository);

        [Fact]
        public void TestRegister1Patient1VisitAndCheckGenerateBillsCountAndRecordPayment()
        {
            

            /*public static string PatientsCSVPath = "D:\\Patients.csv";
            public static string VisitsCSVPath = "D:\\Visits.csv";
            public static string BillsCSVPath = "D:\\Bills.csv";*/

            IBillingService billingService = new BillingService(dentalCareRepository);

            //1. Patient is registered in dental care
            Patient patient1 = new Patient();
            patient1.AccountNumber = 1;
            patient1.PatientName = "Dhiren Paryani";
            patient1.PatientAddress = "6560 Montezuma Road";
            patient1.IsInsured = true;
            patient1.InsuranceName = "JCB";
            patient1.InsuranceAddress = "Connecticut";


            var patientCreatedMessage = patientController.RegisterPatient(patient1);
            Console.WriteLine(patientCreatedMessage.Value);

            Assert.IsType<OkObjectResult>(patientCreatedMessage.Result);

            //2. Patient makes his visit to dental care
            Visit visit1 = new Visit();
            visit1.VisitNumber = 1;
            visit1.AccountNumber = 1;
            visit1.DateOfService = "03/22/2020";
            visit1.AmountCharged = 123.5;
            var visitMessage = patientVisit.RegisterVisit(visit1);
            Console.WriteLine(visitMessage.Value);
            Assert.IsType<OkObjectResult>(visitMessage.Result);


            //3. System generates a Bill (typically at the end of the month): This has been called as a cron job in BillGenerationBackgroundService.cs
            billingService.GenerateBillsForTheMonth();

            //4. Patient recieves a bill at the end of the month: This is used by the UI portal
            var billsResult = patientBillingController.GetBillsByAccountNumber(patient1.AccountNumber).Result as ObjectResult;
           
            List<SystemGeneratedBill> bills = (List<SystemGeneratedBill>) billsResult.Value;
            int billsCount = bills.ToList().Count;
            Assert.Equal(1, billsCount);



            //5. Patient pays the bill using the bill number
            var payTheBill =patientBillingController.RecordAPaymentAganistBillNumber(1);
            Assert.IsType<OkObjectResult>(payTheBill);

            //6. The bill should no longer be due since it has been payed by the Patient
            billsResult = patientBillingController.GetBillsByAccountNumber(patient1.AccountNumber).Result as ObjectResult;
            bills = (List<SystemGeneratedBill>)billsResult.Value;
            var billsCountAgain = bills.ToList().Count;
            Assert.Equal(0, billsCountAgain);
        }
      
       



    }   

       
 }
