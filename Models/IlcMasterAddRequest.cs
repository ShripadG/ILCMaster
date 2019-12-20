using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace employeeservice.Models
{
    public class IlcMasterAddRequest
    {
        [Key]
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("_id")]
        public string _id { get; set; }
        [JsonProperty("_rev")]
        public string _rev { get; set; }
        [JsonProperty("AccountId")]
        public string AccountId { get; set; }
        [JsonProperty("WorkItemId")]
        public string WorkItemId { get; set; }
        [JsonProperty("ActivityCd")]
        public string ActivityCd { get; set; }
        [JsonProperty("EmpDeptNumber")]
        public string EmpDeptNumber { get; set; }
        [JsonProperty("EmpSerNum")]
        public string EmpSerNum { get; set; }
        [JsonProperty("EmpName")]
        public string EmpName { get; set; }
        [JsonProperty("EmpLastName")]
        public string EmpLastName { get; set; }
        [JsonProperty("EmpInitials")]
        public string EmpInitials { get; set; }
        [JsonProperty("TotalHrsExpended")]
        public string TotalHrsExpended { get; set; }
        [JsonProperty("BillingCode")]
        public string BillingCode { get; set; }
        [JsonProperty("SentToCftDate")]
        public string SentToCftDate { get; set; }
        [JsonProperty("CftErrorCode")]
        public string CftErrorCode { get; set; }
        [JsonProperty("CftHoldInd")]
        public string CftHoldInd { get; set; }
        [JsonProperty("SubmitterUserId")]
        public string SubmitterUserId { get; set; }
        [JsonProperty("CreatedTimestamp")]
        public string CreatedTimestamp { get; set; }
        [JsonProperty("ApprovalStatus")]
        public string ApprovalStatus { get; set; }
        [JsonProperty("LabStatus")]
        public string LabStatus { get; set; }
        [JsonProperty("ContactUid")]
        public string ContactUid { get; set; }
        [JsonProperty("ContactNode")]
        public string ContactNode { get; set; }
        [JsonProperty("ContractRevwReqd")]
        public string ContractRevwReqd { get; set; }
        [JsonProperty("ContractRevwComp")]
        public string ContractRevwComp { get; set; }
        [JsonProperty("TotalHours")]
        public string TotalHours { get; set; }
        [JsonProperty("AppendQuery")]
        public string AppendQuery { get; set; }
        [JsonProperty("Worknumber")]
        public string Worknumber { get; set; }
        [JsonProperty("ReportGroup1")]
        public string ReportGroup1 { get; set; }
        [JsonProperty("ReportGroupHeader1")]
        public string ReportGroupHeader1 { get; set; }
        [JsonProperty("ReportGroup2")]
        public string ReportGroup2 { get; set; }
        [JsonProperty("ReportGroupHeader2")]
        public string ReportGroupHeader2 { get; set; }
        [JsonProperty("ReportTitle")]
        public string ReportTitle { get; set; }
        [JsonProperty("First")]
        public string First { get; set; }
        [JsonProperty("Computed")]
        public string Computed { get; set; }
        [JsonProperty("Computed2")]
        public string Computed2 { get; set; }
        [JsonProperty("WorkDesc")]
        public string WorkDesc { get; set; }
        [JsonProperty("ActivityDesc")]
        public string ActivityDesc { get; set; }
        [JsonProperty("ActivityCdAndDesc")]
        public string ActivityCdAndDesc { get; set; }
        [JsonProperty("Year")]
        public string Year { get; set; }
        [JsonProperty("OwningCountryCd")]
        public string OwningCountryCd { get; set; }
        [JsonProperty("OwningCompanyCd")]
        public string OwningCompanyCd { get; set; }
        [JsonProperty("EmpCompanyCode")]
        public string EmpCompanyCode { get; set; }
        [JsonProperty("CtryDesc")]
        public string CtryDesc { get; set; }
        [JsonProperty("Country")]
        public string Country { get; set; }
        [JsonProperty("WeekEndingDate")]
        public string WeekEndingDate { get; set; }
        [JsonProperty("WeekEndingDateMonth")]
        public string WeekEndingDateMonth { get; set; }
        [JsonProperty("WeekEndingDateQuarter")]
        public string WeekEndingDateQuarter { get; set; }
        [JsonProperty("WeekEndingDateYear")]
        public string WeekEndingDateYear { get; set; }
        [JsonProperty("CurrentDate")]
        public string CurrentDate { get; set; }
        [JsonProperty("CurrentDateMonth")]
        public string CurrentDateMonth { get; set; }
        [JsonProperty("CurrentDateYear")]
        public string CurrentDateYear { get; set; }
        [JsonProperty("CurrentDateQuarter")]
        public string CurrentDateQuarter { get; set; }
        [JsonProperty("CurrentMonthFlag")]
        public string CurrentMonthFlag { get; set; }
        [JsonProperty("CurrentQuarterFlag")]
        public string CurrentQuarterFlag { get; set; }
        [JsonProperty("CurrentYearFlag")]
        public string CurrentYearFlag { get; set; }
        [JsonProperty("CtrCd")]
        public string CtrCd { get; set; }
        [JsonProperty("SatHoursExpended")]
        public string SatHoursExpended { get; set; }
        [JsonProperty("SunHoursExpended")]
        public string SunHoursExpended { get; set; }
        [JsonProperty("MonHoursExpended")]
        public string MonHoursExpended { get; set; }
        [JsonProperty("TueHoursExpended")]
        public string TueHoursExpended { get; set; }
        [JsonProperty("WedHoursExpended")]
        public string WedHoursExpended { get; set; }
        [JsonProperty("ThuHoursExpended")]
        public string ThuHoursExpended { get; set; }
        [JsonProperty("FriHoursExpended")]
        public string FriHoursExpended { get; set; }
        [JsonProperty("WorkItemTitle")]
        public string WorkItemTitle { get; set; }
        [JsonProperty("BillingCdDesc")]
        public string BillingCdDesc { get; set; }
        [JsonProperty("BillingRate")]
        public string BillingRate { get; set; }
        [JsonProperty("BillCurrencyCd")]
        public string BillCurrencyCd { get; set; }
        [JsonProperty("SourceKey")]
        public string SourceKey { get; set; }
        [JsonProperty("DateLimitType")]
        public string DateLimitType { get; set; }
        [JsonProperty("TotalDays")]
        public string TotalDays { get; set; }
        [JsonProperty("BillCdTotalHours")]
        public string BillCdTotalHours { get; set; }
        [JsonProperty("Authorized")]
        public string Authorized { get; set; }
        [JsonProperty("AcctTypCd")]
        public string AcctTypCd { get; set; }
        [JsonProperty("SowTypCd")]
        public string SowTypCd { get; set; }
        [JsonProperty("PreAvailableHours")]
        public string PreAvailableHours { get; set; }
        [JsonProperty("PendingHours")]
        public string PendingHours { get; set; }
        [JsonProperty("ApprovedHours")]
        public string ApprovedHours { get; set; }
        [JsonProperty("RejectedHours")]
        public string RejectedHours { get; set; }
        [JsonProperty("DeferredHours")]
        public string DeferredHours { get; set; }
        [JsonProperty("ConcededHours")]
        public string ConcededHours { get; set; }
        [JsonProperty("WorkItemKey")]
        public string WorkItemKey { get; set; }
        [JsonProperty("MonthNumber")]
        public string MonthNumber { get; set; }
        [JsonProperty("Year2")]
        public string Year2 { get; set; }
        [JsonProperty("PreAustriaFlag")]
        public string PreAustriaFlag { get; set; }
        [JsonProperty("CountryCode")]
        public string CountryCode { get; set; }
        [JsonProperty("AcctgrpId")]
        public string AcctgrpId { get; set; }
        [JsonProperty("BillStart")]
        public string BillStart { get; set; }
        [JsonProperty("BillEnd")]
        public string BillEnd { get; set; }
        [JsonProperty("BillRate")]
        public string BillRate { get; set; }
        [JsonProperty("OvertimeInd")]
        public string OvertimeInd { get; set; }
        [JsonProperty("GroupOvertimeInd")]
        public string GroupOvertimeInd { get; set; }
        [JsonProperty("StandbyHoursFlag")]
        public string StandbyHoursFlag { get; set; }
        [JsonProperty("TotalExpendedHrs")]
        public string TotalExpendedHrs { get; set; }
    }
}