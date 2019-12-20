using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace employeeservice.Models
{
    public class Dashboard
    {
        public int TotalActive { get; set; }
        public int TotalOnShore { get; set; }
        public int TotalOffShore { get; set; }

        public int OnShoreMale { get; set; }
        public int OnShoreFemale { get; set; }

        public int OffShoreMale { get; set; }
        public int OffShoreFemale { get; set; }

        public int Billable { get; set; }
        public int NonBillable { get; set; }

        public BandWise[] BandData { get; set; }

        public BulkRow[] onboard { get; set; }
        public BulkRow[] offboard { get; set; }

        public CalenderWise[] calender_onboard { get; set; }
        public CalenderWise[] calender_offboard { get; set; }

        public SquadWise[] SquadWiseGenderOnShore { get; set; }
        public SquadWise[] SquadWiseGenderOffShore { get; set; }
    }
}
