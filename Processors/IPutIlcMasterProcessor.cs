using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using employeeservice.Models;
using employeeservice.Services;

namespace employeeservice.Processors
{
    public interface IPutIlcMasterProcessor
    {
        Task<UpdateIlcMasterResponse> PutExistingilcMasterRecord(IlcMaster ilcMasterUpdateRequest, ICloudantService cloudantService = null);
    }
}
