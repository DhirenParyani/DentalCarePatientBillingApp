using DentalCarePatientBillingApp.Data;
using DentalCarePatientBillingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalCarePatientBillingApp.Services
{
    public class VisitService : IVisitService
    {
        private IDentalCareRepository dentalCareRepository;
        public VisitService(IDentalCareRepository repository)
        {
            dentalCareRepository = repository;
        }

        public bool RegisterVisit(Visit visit)
        {
            if (dentalCareRepository.GetVisitNumberVisitMap().ContainsKey(visit.VisitNumber))
                return false;
            dentalCareRepository.InsertVisit(visit);
            return true;
        }
    }
}
