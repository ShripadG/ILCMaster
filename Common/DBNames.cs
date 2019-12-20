using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace employeeservice.Common
{
    public enum DBNames
    {
        /// <summary>
        /// Name of the table/doc to store employee master data
        /// </summary>
        [Description("empmaster")]
        empmaster,

        /// <summary>
        /// Name of the table/doc to store audit data
        /// </summary>
        [Description("auditdata")]
        auditdata,
              /// <summary>
              /// Name of the table/doc to store audit data
              /// </summary>
        [Description("ilcmaster")]
        ilcmaster

    }
}
