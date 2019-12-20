using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using employeeservice.Models;
using Newtonsoft.Json;
using System.Globalization;

namespace employeeservice.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class Helper : IHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="employees"></param>
        /// <returns></returns>
        //public Dashboard GetDashBoardData(BulkData employees)
        //{
        //    Dashboard dashboard = new Dashboard();
        //    dashboard.TotalActive = employees.rows.Count(a => a.doc.IsDeleted == "false" && a.doc.Status == "Onboarded");
        //    dashboard.TotalOnShore = employees.rows.Count(a => a.doc.IsDeleted == "false" && a.doc.Status == "Onboarded" && a.doc.LocationStatus == "Onshore");
        //    dashboard.TotalOffShore = employees.rows.Count(a => a.doc.IsDeleted == "false" && a.doc.Status == "Onboarded" && a.doc.LocationStatus == "Offshore");

        //    dashboard.OnShoreFemale = employees.rows.Count(a => a.doc.IsDeleted == "false" && a.doc.Status == "Onboarded" && a.doc.LocationStatus == "Onshore" && a.doc.Gender == "Female");
        //    dashboard.OnShoreMale = employees.rows.Count(a => a.doc.IsDeleted == "false" && a.doc.Status == "Onboarded" && a.doc.LocationStatus == "Onshore" && a.doc.Gender == "Male");

        //    dashboard.OffShoreFemale = employees.rows.Count(a => a.doc.IsDeleted == "false" && a.doc.Status == "Onboarded" && a.doc.LocationStatus == "Offshore" && a.doc.Gender == "Female");
        //    dashboard.OffShoreMale = employees.rows.Count(a => a.doc.IsDeleted == "false" && a.doc.Status == "Onboarded" && a.doc.LocationStatus == "Offshore" && a.doc.Gender == "Male");

        //    dashboard.Billable = employees.rows.Count(a => a.doc.IsDeleted == "false" && (a.doc.Status == "Onboarded") && (a.doc.Billable == "Y" || a.doc.Billable == "P"));
        //    dashboard.NonBillable = dashboard.TotalActive - dashboard.Billable;

        //    // band wise grouping and count
        //    var bandwise = (from e in employees.rows
        //                    where !string.IsNullOrWhiteSpace(e.doc.CurrentBand) && e.doc.CurrentBand != "string"
        //                    group e by e.doc.CurrentBand into g
        //                    select new BandWise { band = g.Key, count = g.Count() });
        //    dashboard.BandData = bandwise.ToArray();


        //    //Latest 5 onboarding
        //    var Latestonboarding = (from e in employees.rows
        //                            where e.doc.IsDeleted == "false"
        //                            && !string.IsNullOrWhiteSpace(e.doc.AccountOnboardDate)
        //                            && e.doc.AccountOnboardDate != "string" && e.doc.AccountOnboardDate != "TBC"
        //                            orderby e.doc.AccountOnboardDate descending
        //                            select e).Take(5);
        //    dashboard.onboard = Latestonboarding.ToArray();

        //    //latest 5 offboarding
        //    var Latestoffboarding = (from e in employees.rows
        //                             where e.doc.IsDeleted == "false"
        //                             && !string.IsNullOrWhiteSpace(e.doc.AccountOffboardingOffboardedDate)
        //                             && e.doc.AccountOffboardingOffboardedDate != "string" && e.doc.AccountOffboardingOffboardedDate != "TBC"
        //                             orderby e.doc.AccountOffboardingOffboardedDate descending 
        //                             select e).Take(5);
        //    dashboard.offboard = Latestoffboarding.ToArray();
        //    try
        //    {
        //        // Monthly Onboarding
        //        var MonthlyOnBoards = (from e in employees.rows
        //                               where
        //                               e.doc.IsDeleted == "false" &&
        //                               e.doc.Status == "Onboarded" //&&
        //                                                           //!string.IsNullOrWhiteSpace(e.doc.CurrentBand) && e.doc.CurrentBand != "string"
        //                               && !string.IsNullOrWhiteSpace(e.doc.AccountOnboardDate)
        //                               && e.doc.AccountOnboardDate != "string" && e.doc.AccountOnboardDate != "Other" && e.doc.AccountOnboardDate != "TBC"
        //                               //&& DateTime.TryParse(e.doc.AccountOnboardDate, new CultureInfo("en-GB"),DateTimeStyles.None, out resultdate)
        //                               let dateValue = tryToGetDate(e.doc.AccountOnboardDate)
        //                               where dateValue != null
        //                               group e by new
        //                               {
        //                                   Convert.ToDateTime(e.doc.AccountOnboardDate).Month,
        //                                   Convert.ToDateTime(e.doc.AccountOnboardDate).Year
        //                               } into g
        //                               select new CalenderWise()
        //                               {
        //                                   month = g.Key.Month.ToString(),
        //                                   year = g.Key.Year.ToString(),
        //                                   count = g.Count()
        //                               });
        //        dashboard.calender_onboard = MonthlyOnBoards.ToArray();
        //    }
        //    catch
        //    {

        //    }

        //    //// Monthly Offboarding
        //    //var MonthlyOffBoards = (from e in employees.rows
        //    //                        where
        //    //                        e.doc.IsDeleted == "false" &&
        //    //                        !string.IsNullOrWhiteSpace(e.doc.CurrentBand) && e.doc.CurrentBand != "string"
        //    //                        && !string.IsNullOrWhiteSpace(e.doc.AccountOffboardingOffboardedDate)
        //    //                        && e.doc.AccountOffboardingOffboardedDate != "string" && e.doc.AccountOffboardingOffboardedDate != "Other" && e.doc.AccountOffboardingOffboardedDate != "TBC"
        //    //                        && DateTime.ParseExact(e.doc.AccountOffboardingOffboardedDate, "dd/MM/yyyy", culture) != null
        //    //                        group e by new
        //    //                        {
        //    //                            DateTime.ParseExact(e.doc.AccountOffboardingOffboardedDate, "dd/MM/yyyy", culture).Month,
        //    //                            DateTime.ParseExact(e.doc.AccountOffboardingOffboardedDate, "dd/MM/yyyy", culture).Year
        //    //                        } into g
        //    //                        select new CalenderWise()
        //    //                        {
        //    //                            month = g.Key.Month.ToString(),
        //    //                            year = g.Key.Year.ToString(),
        //    //                            count = g.Count()
        //    //                        });
        //    //dashboard.calender_offboard = MonthlyOffBoards.ToArray();

        //    var SquadWiseGenderOnShore = (from e in employees.rows
        //                                  where
        //                                  e.doc.IsDeleted == "false" &&
        //                                  (e.doc.Status == "Onboarded")
        //                                  group e by new
        //                                  {
        //                                      e.doc.DGDCSquad,
        //                                      e.doc.Gender
        //                                  } into g
        //                                  select new SquadWise()
        //                                  {
        //                                      DGDCSquad = g.Key.DGDCSquad.ToString(),
        //                                      Gender = g.Key.Gender.ToString(),
        //                                      Count = g.Count()
        //                                  });
        //    dashboard.SquadWiseGenderOnShore = SquadWiseGenderOnShore.ToArray();
        //    var SquadWiseGenderOffShore = (from e in employees.rows
        //                                   where
        //                                   e.doc.IsDeleted == "false" &&
        //                                   (e.doc.Status == "Offboarded")
        //                                   group e by new
        //                                   {
        //                                       e.doc.DGDCSquad,
        //                                       e.doc.Gender
        //                                   } into g
        //                                   select new SquadWise()
        //                                   {
        //                                       DGDCSquad = g.Key.DGDCSquad.ToString(),
        //                                       Gender = g.Key.Gender.ToString(),
        //                                       Count = g.Count()
        //                                   });

        //    dashboard.SquadWiseGenderOffShore = SquadWiseGenderOffShore.ToArray();

        //    return dashboard;

        //}

        private Func<string, DateTime?> tryToGetDate =
        value =>
        {
            DateTime dateValue;
            return DateTime.TryParse(value,new CultureInfo("en-GB"), DateTimeStyles.None, out dateValue) ? (DateTime?)dateValue : null;
        };
    }
}
