using CsvHelper.Configuration;
using DentalCarePatientBillingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalCarePatientBillingApp.Mapper
{
    public class VisitMap: ClassMap<Visit>
    {
        public VisitMap()
        {
            Map(x => x.VisitNumber).Name("VisitNumber");
            Map(x => x.AccountNumber).Name("AccountNumber");
            Map(x => x.DateOfService).Name("DateOfService");
            Map(x => x.AmountCharged).Name("AmountCharged");


        }
    }
}
