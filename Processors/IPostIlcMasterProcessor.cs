using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using employeeservice.Models;
using employeeservice.Services;

namespace employeeservice.Processors
{
    public interface IPostIlcMasterProcessor
    {
        Task<UpdateIlcMasterResponse> PostNewIlcMasterRecord(IlcMasterAddRequest ilcMasterAddRequest, ICloudantService cloudantService);
    }
}
