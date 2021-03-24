using DentalCarePatientBillingApp.Data;
using DentalCarePatientBillingApp.Models;
using DentalCarePatientBillingApp.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//CSV Paths can be edited under Constants: currently set to Project's root folder

namespace DentalCarePatientBillingApp.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/dentalcare/visit")]
    [ApiController]
    public class PatientVisitController: ControllerBase
    {
        private readonly IDentalCareRepository dentalCareRepository;
        private IVisitService visitService;
        public PatientVisitController(IDentalCareRepository patientBillRepository)
        {
            dentalCareRepository = patientBillRepository;
            visitService = new VisitService(dentalCareRepository);
        }

        [HttpPost]
        public ActionResult<Visit> RegisterVisit(Visit visit)
        {
            if (visitService.RegisterVisit(visit))
                return Ok("Your Visit was unsuccessful");

            return BadRequest("Your Dentalcare Visit was successfully");
        }
    }
}
