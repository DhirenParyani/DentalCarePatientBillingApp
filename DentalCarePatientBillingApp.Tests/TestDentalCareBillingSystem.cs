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
        static IDentalCareRepository dentalCareRepository = new DentalCareRepository();
        static PatientBillingSystemController patientBillingController = new PatientBillingSystemController(dentalCareRepository);
        static PatientController patientController = new PatientController(dentalCareRepository);
        static PatientVisitController patientVisit = new PatientVisitController(dentalCareRepository);

        [Fact]
        public void TestRegister1Patient1VisitAndCheckGenerateBillsCount()
        {
            //await Task.Delay(TimeSpan.FromSeconds(5));
            //Test Code

            /*public static string PatientsCSVPath = "D:\\Patients.csv";
            public static string VisitsCSVPath = "D:\\Visits.csv";
            public static string BillsCSVPath = "D:\\Bills.csv";*/

            IBillingService billingService = new BillingService(dentalCareRepository);
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


            Visit visit1 = new Visit();
            visit1.VisitNumber = 1;
            visit1.AccountNumber = 1;
            visit1.DateOfService = "03/22/2020";
            visit1.AmountCharged = 123.5;


            var visitMessage = patientVisit.RegisterVisit(visit1);
            Console.WriteLine(visitMessage.Value);

            billingService.GenerateBillsForTheMonth();

            Assert.IsType<OkObjectResult>(visitMessage.Result);
            var billsResult = patientBillingController.GetBillsByAccountNumber(patient1.AccountNumber).Result as ObjectResult;
            //response.Value.ToList();
            List<SystemGeneratedBill> bills = (List<SystemGeneratedBill>) billsResult.Value;
            int billsCount = bills.ToList().Count;
            Assert.Equal(1, billsCount);

           


            var payTheBill=patientBillingController.RecordAPaymentAganistBillNumber(1);
            Assert.IsType<OkObjectResult>(payTheBill);
            billsResult = patientBillingController.GetBillsByAccountNumber(patient1.AccountNumber).Result as ObjectResult;
            bills = (List<SystemGeneratedBill>)billsResult.Value;
            var billsCountAgain = bills.ToList().Count;
            Assert.Equal(0, billsCountAgain);
        }
      
       



    }   

       
 }
