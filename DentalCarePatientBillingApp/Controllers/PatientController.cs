using DentalCarePatientBillingApp.Data;
using DentalCarePatientBillingApp.Models;
using DentalCarePatientBillingApp.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalCarePatientBillingApp.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/dentalcare/patient")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IDentalCareRepository dentalCareRepository;
        private IPatientService patientService;
        public PatientController(IDentalCareRepository patientBillRepository)
        {
            dentalCareRepository = patientBillRepository;
            patientService = new PatientService(dentalCareRepository);
        }
        
        [HttpPost]
        public ActionResult<string> RegisterPatient(Patient patient)
        {   if(patientService.RegisterPatient(patient))
            return Ok("Patient Registered successfully");

            return BadRequest("Patient Registration unsuccessful");
        }
    }
}
