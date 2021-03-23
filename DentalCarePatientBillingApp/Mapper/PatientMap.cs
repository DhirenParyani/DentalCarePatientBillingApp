using CsvHelper.Configuration;
using DentalCarePatientBillingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalCarePatientBillingApp.Mapper
{
    public class PatientMap : ClassMap<Patient>
    {
        public PatientMap()
        {
            Map(x => x.AccountNumber).Name("AccountNumber");
            Map(x => x.PatientName).Name("PatientName");
            Map(x => x.PatientAddress).Name("PatientAddress");
            //References<InsuranceMap>(m => m.Insurance);
            Map(x => x.IsInsured).Name("IsInsured");
            Map(x => x.InsuranceName).Name("InsuranceName");
            Map(x => x.InsuranceAddress).Name("InsuranceAddress");
        }
    }
}
