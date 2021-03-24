using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DentalCarePatientBillingApp.Data;
using DentalCarePatientBillingApp.Models;
using Microsoft.AspNetCore.Cors;
using DentalCarePatientBillingApp.Services;

//CSV Paths can be edited under Constants: currently set to Project's root folder

namespace DentalCarePatientBillingApp.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/dentalcare/patientbills/")]
    [ApiController]
    public class PatientBillingSystemController : ControllerBase
    {
       
        private readonly IDentalCareRepository dentalCareRepository;
        private IBillingService billingService;
        public PatientBillingSystemController(IDentalCareRepository repository)
        {
            dentalCareRepository = repository;
            billingService = new BillingService(dentalCareRepository); 
        }

        [HttpGet]
        public IEnumerable<PatientSummary> GetPatientsBillingSummary()
        {
            var patientSummaries = billingService.GetPatientsBillingSummary();

            return patientSummaries;
        }

        
        [HttpGet("{accountNumber}")]
        public ActionResult<IEnumerable<List<SystemGeneratedBill>>> GetBillsByAccountNumber(int accountNumber)
        {
            if (!dentalCareRepository.GetPatientsData().ContainsKey(accountNumber))
                return BadRequest("Account Number doesn't exist");
              
            List<SystemGeneratedBill> bills = billingService.GetBillsByAccountNumber(accountNumber);
              return Ok(bills);
        

        }

        [HttpPut("{billNumber}")]
        public ActionResult RecordAPaymentAganistBillNumber(int billNumber)
        {
            if (dentalCareRepository.GetBills().ContainsKey(billNumber))
            {
                billingService.RecordAPayment(billNumber);
                return Ok("Payment recorded aganist the given bill number");
            }
                

            return BadRequest("Bill with given BillNumber doesn't exist");
        }
    }
}
