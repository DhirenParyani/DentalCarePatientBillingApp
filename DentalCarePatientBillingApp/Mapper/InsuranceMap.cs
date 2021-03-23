using CsvHelper.Configuration;
using DentalCarePatientBillingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalCarePatientBillingApp.Mapper
{
    public class InsuranceMap : ClassMap<Insurance>
    {
        public InsuranceMap()
        {
            Map(x => x.InsuranceName).Name("InsuranceName");
            Map(x => x.InsuranceAddress).Name("InsuranceAddress");
            

        }
       
    }
}
