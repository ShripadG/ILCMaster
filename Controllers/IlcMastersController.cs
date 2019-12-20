using Microsoft.AspNetCore.Mvc;
using employeeservice.Models;
using ilcmasterservice.Models;
using Microsoft.AspNetCore.Http;
using employeeservice.Services;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using System;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.IO;
using ExcelDataReader;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.PlatformAbstractions;
using employeeservice.Processors;
using employeeservice.Common;

namespace employeeservice.Controllers
{
    /// <summary>
    /// This class contains methods for CRUD operations
    /// </summary>
    [Route("api/[controller]")]
    public class IlcMastersController : Controller
    {
        private readonly HtmlEncoder _htmlEncoder;
        private readonly ICloudantService _cloudantService;
        private readonly IHelper _helper;
        private readonly IPostIlcMasterProcessor _postIlcMasterProcessor;
        private readonly IPutIlcMasterProcessor _putIlcMasterProcessor;

        /// <summary>
        /// The default constructor 
        /// </summary>
        /// <param name="htmlEncoder"></param>
        /// <param name="postIlcMasterProcessor"></param>
        /// <param name="putIlcMasterProcessor"></param>
        /// <param name="helper"></param>
        /// <param name="cloudantService"></param>
        public IlcMastersController(HtmlEncoder htmlEncoder, IPostIlcMasterProcessor postIlcMasterProcessor, IPutIlcMasterProcessor putIlcMasterProcessor, IHelper helper,ICloudantService cloudantService = null)
        {
            _cloudantService = cloudantService;
            _helper = helper;
            _postIlcMasterProcessor = postIlcMasterProcessor;
            _putIlcMasterProcessor = putIlcMasterProcessor;
            _htmlEncoder = htmlEncoder;
        }

        /// <summary>
        /// Get all the records
        /// </summary>
        /// <returns>returns all records from database</returns>
        [HttpGet]
        public async Task<dynamic> Get()
        {
            if (_cloudantService == null)
            {
                return new string[] { "No database connection" };
            }
            else
            {
                return await _cloudantService.GetAllAsync(DBNames.ilcmaster.ToString());
            }
        }
             
         /// <summary>
        /// Get record by ID
        /// </summary>
        /// <param name="id">ID to be selected</param>
        /// <returns>record for the given id</returns>
        [HttpGet("id")]
        public async Task<dynamic> GetByID(string id)
        {
            if (_cloudantService == null)
            {
                return new string[] { "No database connection" };
            }
            else
            {
                var response = await _cloudantService.GetByIdAsync(id, DBNames.ilcmaster.ToString());
                return JsonConvert.DeserializeObject<IlcMaster>(response);
            }
        }

        /// <summary>
        /// Create a new record
        /// </summary>
        /// <param name="ilcmaster">New record to be created</param>
        /// <returns>status of the newly added record</returns>
        [HttpPost]
        public async Task<UpdateIlcMasterResponse> Post([FromBody]IlcMasterAddRequest ilcmaster)
        {
            if (_postIlcMasterProcessor != null)
            {                
                return await _postIlcMasterProcessor.PostNewIlcMasterRecord(ilcmaster, _cloudantService);
            }
            else
            {
                return new UpdateIlcMasterResponse();
            }
        }

        /// <summary>
        /// Update an existing record by giving _id and _rev values
        /// </summary>
        /// <param name="ilcmaster">record to be updated for given _id and _rev</param>
        /// <returns>status of the record updated</returns>
        [HttpPut]
        public async Task<dynamic> Update([FromBody]IlcMaster ilcmaster)
        {
            if (_postIlcMasterProcessor != null)
            {
                return await _putIlcMasterProcessor.PutExistingilcMasterRecord(ilcmaster, _cloudantService);
            }
            else
            {
                return new string[] { "No database connection" };
            }
        }


        /// <summary>
        /// Delete the all the records from table
        /// </summary>
        /// <returns>status of the record deleted</returns>
        //[HttpDelete("All")]
        //public async Task<dynamic> Delete()
        //{
        //    if (_cloudantService != null)
        //    {
        //        return await _cloudantService.DeleteAsync(DBNames.ilcmaster.ToString());
        //        //Console.WriteLine("Update RESULT " + response);
        //        //return new string[] { employee.IBMEmailID, employee._id, employee._rev };
        //        //return JsonConvert.DeserializeObject<UpdateEmployeeResponse>(response.Result);
        //    }
        //    else
        //    {
        //        return new string[] { "No database connection" };
        //    }
        //}
        ///// <summary>
        /// Delete the record for the given id
        /// </summary>
        /// <param name="id">record id to be deleted</param>
        /// <param name="rev">revision number of the record to be deleted</param>
        /// <returns>status of the record deleted</returns>
        [HttpDelete]
        public async Task<dynamic> Delete(string id, string rev)
        {
            if (_cloudantService != null)
            {
                return await _cloudantService.DeleteAsync(id, rev, DBNames.ilcmaster.ToString());
                //Console.WriteLine("Update RESULT " + response);
                //return new string[] { employee.IBMEmailID, employee._id, employee._rev };
                //return JsonConvert.DeserializeObject<UpdateEmployeeResponse>(response.Result);
            }
            else
            {
                return new string[] { "No database connection" };
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("FILE")]
        public async Task<dynamic> Upload(FileUploadViewModel model)
        {
            var file = model.File;
            string[] filePaths = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"bulkdata\"));
            string spath = Path.Combine(Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "bulkdata"));
            if (Directory.Exists(spath))
            {
                foreach (string filePath in filePaths)
                System.IO.File.Delete(filePath);
            }
                      
            if (file.Length > 0)
            {
                string path = Path.Combine(Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "bulkdata"));
                using (var fs = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                {
                    await file.CopyToAsync(fs);
                }
                var responseconvert = ConvertFromExcelToJson(file);
                if (_cloudantService != null)
                {
                    string dbname = "ilcmaster";
                    var outFilePath= Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"bulkdata\ilcextract.json");
                    // var outFilePath = @"C:\gith\pushed\employeeservice\src\employeeservice\bulkdata\ilcextract.json";
                    var responsetype = await _cloudantService.GetAllAsync(DBNames.ilcmaster.ToString());
                    BulkData ilcrecords = JsonConvert.DeserializeObject<BulkData>(responsetype);
                    if (ilcrecords.rows.Count() > 0)
                    {
                        var deleteresponse = await _cloudantService.DeleteAsync(DBNames.ilcmaster.ToString());
                        //var fileresp=ConvertFromExcelToJson(file);

                    }
                    var response= await _cloudantService.BulkUpload(dbname, outFilePath);
                    return new { Success = true };
                }
                else
                {
                    return new string[] { "No database connection" };
                }
            }
            else
            {
                return new string[] { "File not valid" };
            }
        }
            /// <summary>
            /// This method will convert the given excel into JSON
            /// TO DO: add {"docs": at the start of the generated JSON.
            /// </summary>
            /// <returns>returns status of conversion</returns>
        [HttpGet("exceltojson")]
        public async Task<dynamic> ConvertFromExcelToJson(IFormFile file )
        {
            if (_cloudantService != null)
            {
                //savefile(file);
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                var inFilePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath,@"bulkdata\ILC extract.xlsx");
                var outFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"bulkdata\ilcextract.json");
                //var inFilePath = @"C:\gith\pushed\employeeservice\src\employeeservice\bulkdata\master2.xlsx";
                //var outFilePath = @"C:\gith\pushed\employeeservice\src\employeeservice\bulkdata\ilcextchract.json";


                using (var inFile = System.IO.File.Open(inFilePath, FileMode.Open, FileAccess.Read))
                using (var outFile = System.IO.File.CreateText(outFilePath))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(inFile, new ExcelReaderConfiguration()
                    { FallbackEncoding = Encoding.GetEncoding(1252) }))
                    using (var writer = new JsonTextWriter(outFile))
                    {
                        writer.Formatting = Formatting.Indented; //I likes it tidy
                        writer.WriteStartArray();
                        reader.Read(); //SKIP FIRST ROW, it's TITLES.

                        int count = reader.FieldCount;
                        //writer.WriteValue(@"{ //"docs//":");
                        do
                        {
                            while (reader.Read())
                            {
                                count = count + 1;
                                int i = 0;
                                var AccountId = "";
                                //peek ahead? Bail before we start anything so we don't get an empty object
                                try
                                {
                                    AccountId = Convert.ToString(reader.GetValue(i));
                                }
                                catch
                                {

                                }
                                if (string.IsNullOrWhiteSpace(AccountId)) break;

                                writer.WriteStartObject();
                                writer.WritePropertyName("AccountId");
                                //writer.WriteValue(UnformattedEmployeeId); writer.WritePropertyName("AccountId"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("WorkItemId"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("ActivityCd"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("EmpDeptNumber"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("EmpSerNum"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("EmpName"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("EmpLastName"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("EmpInitials"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("TotalHrsExpended"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("BillingCode"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("SentToCftDate"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("CftErrorCode"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("CftHoldInd"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("SubmitterUserId"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("CreatedTimestamp"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("ApprovalStatus"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("LabStatus"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("ContactUid"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("ContactNode"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("ContractRevwReqd"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("ContractRevwComp"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("TotalHours"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("AppendQuery"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("Worknumber"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("ReportGroup1"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("ReportGroupHeader1"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("ReportGroup2"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("ReportGroupHeader2"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("ReportTitle"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("First"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("Computed"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("Computed2"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("WorkDesc"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("ActivityDesc"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("ActivityCdAndDesc"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("Year"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("OwningCountryCd"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("OwningCompanyCd"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("EmpCompanyCode"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("CtryDesc"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("Country"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("WeekEndingDate"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("WeekEndingDateMonth"); i = i + 1;
                                //writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("EmployeeType");
                               // i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("WeekEndingDateQuarter"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("WeekEndingDateYear"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("Current_Date"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("CurrentDateMonth"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("CurrentDateYear"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("CurrentDateQuarter"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("CurrentMonthFlag"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("CurrentQuarterFlag"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("CurrentYearFlag"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("CtrCd"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("SatHoursExpended"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("SunHoursExpended"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("MonHoursExpended"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("TueHoursExpended"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("WedHoursExpended"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("ThuHoursExpended"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("FriHoursExpended"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("WorkItemTitle"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("BillingCdDesc"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("BillingRate"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("BillCurrencyCd"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("SourceKey"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("DateLimitType"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("TotalDays"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("BillCdTotalHours"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("Authorized"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("AcctTypCd"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("SowTypCd"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("PreAvailableHours"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("PendingHours"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("ApprovedHours"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("RejectedHours"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("DeferredHours"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("ConcededHours"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("WorkItemKey"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("MonthNumber"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("Year2"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("PreAustriaFlag"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("CountryCode"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("AcctgrpId"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("BillStart"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("BillEnd"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("BillRate"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("OvertimeInd"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("GroupOvertimeInd"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("StandbyHoursFlag"); i = i + 1;
                                writer.WriteValue(Convert.ToString(reader.GetValue(i))); writer.WritePropertyName("TotalExpendedHrs"); i = i + 1;

                                writer.WriteEndObject();

                            }
                        } while (reader.NextResult());
                        writer.WriteEndArray();
                    }
                }
                return new string[] { "done" };
            }
            else
            {
                return new string[] { "No database connection" };
            }
        }

        /// <summary>
        /// This method bulk uploads the given data from json file into cloudant
        /// </summary>
        /// <returns>status of the bulk upload</returns>
        /// 
        //[HttpGet("Upload")]
        //private async Task<dynamic> BulkUpload()
        //{
        //    if (_cloudantService != null)
        //    {
        //        string dbname = "ilcmaster";               
        //        var outFilePath = @"C:\gith\pushed\employeeservice\src\employeeservice\bulkdata\ilcextract.json";
        //        ////var responsetype = await _cloudantService.GetAllAsync(DBNames.ilcmaster.ToString());
        //        //BulkData ilcrecords = JsonConvert.DeserializeObject<BulkData>(responsetype);
        //        //if (ilcrecords.rows.Count() > 0)
        //        //{
        //        //    var deleteresponse = await _cloudantService.DeleteAsync(DBNames.ilcmaster.ToString());
        //        //    //var fileresp=ConvertFromExcelToJson(file);
                   
        //        //}
        //        return await _cloudantService.BulkUpload(dbname, outFilePath);
        //     }
        //    else
        //    {
        //        return new string[] { "No database connection" };
        //    }
        //}    
    }
}
