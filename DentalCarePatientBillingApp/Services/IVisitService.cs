using DentalCarePatientBillingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalCarePatientBillingApp.Services
{
    interface IVisitService
    {
        bool RegisterVisit(Visit visit);
    }
}
