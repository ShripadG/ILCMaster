using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace employeeservice.Models
{
    public class BulkRow
    {
        public string id { get; set; }
        public string key { get; set; }
        public BulkRowRevision value { get; set; }
        public IlcMaster doc { get; set; }
    }
}
