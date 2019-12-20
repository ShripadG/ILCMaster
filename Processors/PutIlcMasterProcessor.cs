using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using employeeservice.Models;
using employeeservice.Services;
using Newtonsoft.Json;
using employeeservice.Common;

namespace employeeservice.Processors
{
    public class PutIlcMasterProcessor : IPutIlcMasterProcessor
    {
        public async Task<UpdateIlcMasterResponse> PutExistingilcMasterRecord(IlcMaster ilcMasterUpdateRequest, ICloudantService cloudantService = null)
        {
            //AuditData auditData = new AuditData();
            //auditData.eventname = "edit";
            //auditData.loginid = employeeUpdateRequest.LoginID;
            //auditData.datetime = System.DateTime.UtcNow.ToString();
            //auditData.empid = employeeUpdateRequest.IBMID;

            if (cloudantService != null)
            {
                var response = await cloudantService.UpdateAsync(ilcMasterUpdateRequest, DBNames.ilcmaster.ToString());
               // var audit = await cloudantService.CreateAsync(auditData, DBNames.auditdata.ToString());
                return JsonConvert.DeserializeObject<UpdateIlcMasterResponse>(response);
            }
            else
            {
                return new UpdateIlcMasterResponse();
            }
        }
    }
}
