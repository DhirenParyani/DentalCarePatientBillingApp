using DentalCarePatientBillingApp.Data;
using NCrontab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DentalCarePatientBillingApp.Services
{
    public class BillGenerationBackgroundService : Microsoft.Extensions.Hosting.BackgroundService
    {     
        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        private int lastDayOfTheMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
        private string Schedule => "0 0 12"+" "+lastDayOfTheMonth+" "+"1/1 *";
       
        public BillGenerationBackgroundService()
        {
            _schedule = CrontabSchedule.Parse(Schedule, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
            
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            BillingService billService = new BillingService(new DentalCareRepository());
            do
            {
                var now = DateTime.Now;
                var nextrun = _schedule.GetNextOccurrence(now);
                if (now >= _nextRun)
                {

                    billService.GenerateBillsForTheMonth();
                    DateTime nextDay = DateTime.Now.AddDays(1).Date;
                    int lastDayOfTheNextMonth = DateTime.DaysInMonth(nextDay.Year, nextDay.Month);
                   _schedule = CrontabSchedule.Parse("0 0 12" + " " + lastDayOfTheNextMonth + " " + "1/1 *", new CrontabSchedule.ParseOptions { IncludingSeconds = true });
                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                }
                await Task.Delay(5000, stoppingToken); //5 seconds delay
            }
            while (!stoppingToken.IsCancellationRequested);
        }
    }
}
