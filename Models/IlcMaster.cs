using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace employeeservice.Models
{
    public class IlcMaster
    {
        [Key]
        public string id { get; set; }
        public string _id { get; set; }
        public string _rev { get; set; }
        public string AccountId { get; set; }
        public string WorkItemId { get; set; }
        public string ActivityCd { get; set; }
        public string EmpDeptNumber { get; set; }
        public string EmpSerNum { get; set; }
        public string EmpName { get; set; }
        public string EmpLastName { get; set; }
        public string EmpInitials { get; set; }
        public string TotalHrsExpended { get; set; }
        public string BillingCode { get; set; }
        public string SentToCftDate { get; set; }
        public string CftErrorCode { get; set; }
        public string CftHoldInd { get; set; }
        public string SubmitterUserId { get; set; }
        public string CreatedTimestamp { get; set; }
        public string ApprovalStatus { get; set; }
        public string LabStatus { get; set; }
        public string ContactUid { get; set; }
        public string ContactNode { get; set; }
        public string ContractRevwReqd { get; set; }
        public string ContractRevwComp { get; set; }
        public string TotalHours { get; set; }
        public string AppendQuery { get; set; }
        public string Worknumber { get; set; }
        public string ReportGroup1 { get; set; }
        public string ReportGroupHeader1 { get; set; }
        public string ReportGroup2 { get; set; }
        public string ReportGroupHeader2 { get; set; }
        public string ReportTitle { get; set; }
        public string First { get; set; }
        public string Computed { get; set; }
        public string Computed2 { get; set; }
        public string WorkDesc { get; set; }
        public string ActivityDesc { get; set; }
        public string ActivityCdAndDesc { get; set; }
        public string Year { get; set; }
        public string OwningCountryCd { get; set; }
        public string OwningCompanyCd { get; set; }
        public string EmpCompanyCode { get; set; }
        public string CtryDesc { get; set; }
        public string Country { get; set; }
        public string WeekEndingDate { get; set; }
        public string WeekEndingDateMonth { get; set; }
        public string WeekEndingDateQuarter { get; set; }
        public string WeekEndingDateYear { get; set; }
        public string CurrentDate { get; set; }
        public string CurrentDateMonth { get; set; }
        public string CurrentDateYear { get; set; }
        public string CurrentDateQuarter { get; set; }
        public string CurrentMonthFlag { get; set; }
        public string CurrentQuarterFlag { get; set; }
        public string CurrentYearFlag { get; set; }
        public string CtrCd { get; set; }
        public string SatHoursExpended { get; set; }
        public string SunHoursExpended { get; set; }
        public string MonHoursExpended { get; set; }
        public string TueHoursExpended { get; set; }
        public string WedHoursExpended { get; set; }
        public string ThuHoursExpended { get; set; }
        public string FriHoursExpended { get; set; }
        public string WorkItemTitle { get; set; }
        public string BillingCdDesc { get; set; }
        public string BillingRate { get; set; }
        public string BillCurrencyCd { get; set; }
        public string SourceKey { get; set; }
        public string DateLimitType { get; set; }
        public string TotalDays { get; set; }
        public string BillCdTotalHours { get; set; }
        public string Authorized { get; set; }
        public string AcctTypCd { get; set; }
        public string SowTypCd { get; set; }
        public string PreAvailableHours { get; set; }
        public string PendingHours { get; set; }
        public string ApprovedHours { get; set; }
        public string RejectedHours { get; set; }
        public string DeferredHours { get; set; }
        public string ConcededHours { get; set; }
        public string WorkItemKey { get; set; }
        public string MonthNumber { get; set; }
        public string Year2 { get; set; }
        public string PreAustriaFlag { get; set; }
        public string CountryCode { get; set; }
        public string AcctgrpId { get; set; }
        public string BillStart { get; set; }
        public string BillEnd { get; set; }
        public string BillRate { get; set; }
        public string OvertimeInd { get; set; }
        public string GroupOvertimeInd { get; set; }
        public string StandbyHoursFlag { get; set; }
        public string TotalExpendedHrs { get; set; }
    }
}