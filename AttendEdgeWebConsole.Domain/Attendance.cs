using System;

namespace AttendEdgeWebConsole.Domain
{
    public class Attendance : IAuditable, ICompanyScoped
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Company { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public DateTime? BreakOutTime { get; set; }
        public DateTime? BreakInTime { get; set; }
        public decimal? LatitudeCheckIn { get; set; }
        public decimal? LatitudeCheckOut { get; set; }
        public decimal? LongitudeCheckIn { get; set; }
        public decimal? LongitudeCheckOut { get; set; }
        public decimal? LongitudeBreakIn { get; set; }
        public decimal? LongitudeBreakOut { get; set; }
        public decimal? LatitudeBreakOut { get; set; }
        public decimal? LatitudeBreakIn { get; set; }
        public string CheckInImagePath { get; set; }
        public string CheckOutImagePath { get; set; }
        public string BreakInImagePath { get; set; }
        public string BreakOutImagePath { get; set; }
        public string LocationDescription { get; set; }
        public string DeviceInfo { get; set; }
        public string IPAddress { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
