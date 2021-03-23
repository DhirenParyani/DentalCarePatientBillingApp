using DentalCarePatientBillingApp.Data;
using DentalCarePatientBillingApp.Models;
using DentalCarePatientBillingApp.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalCarePatientBillingApp.Services
{
    public class PatientService : IPatientService
    {
        private IDentalCareRepository dentalCareRepository;
        public PatientService(IDentalCareRepository repository)
        {
            dentalCareRepository = repository;
        }

        public bool RegisterPatient(Patient patient)
        {
            if (dentalCareRepository.GetAccountNumberPatientMap().ContainsKey(patient.AccountNumber))
                return false;
            dentalCareRepository.InsertPatient(patient);
            return true;
        }
    }
}
