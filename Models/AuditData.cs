using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace employeeservice.Models
{
    public class AuditData
    {        
        public string eventname { get; set; }
        public string empid { get; set; }
        public string loginid { get; set; }
        public string datetime { get; set; }
    }
}
