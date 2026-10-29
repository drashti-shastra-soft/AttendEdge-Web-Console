using System;

namespace AttendEdgeWebConsole.Domain
{
    public class Holiday : IAuditable, ICompanyScoped
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsHalfDay { get; set; }
        public bool IsActive { get; set; } = true;
        public int CreatedBy { get; set; }
        public int? LastModifiedBy { get; set; }
        public string Status { get; set; }
        public string Company { get; set; }
        public string Token { get; set; }
    }
}
