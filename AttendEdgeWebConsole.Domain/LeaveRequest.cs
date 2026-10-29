using System;

namespace AttendEdgeWebConsole.Domain
{
    public class LeaveRequest : IAuditable, ICompanyScoped
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string LeaveType { get; set; }
        public string HalfType { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public string LeaveStatus { get; set; }
        public string AdminNote { get; set; }
        public int? ProcessedByAdminId { get; set; }
        public bool IsHalfDay { get; set; }
        public int CreatedBy { get; set; }
        public int? LastModifiedBy { get; set; }
        public int? DeletedBy { get; set; }
    }
}
