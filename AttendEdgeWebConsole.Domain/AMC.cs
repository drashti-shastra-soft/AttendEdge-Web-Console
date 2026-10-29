using System;

namespace AttendEdgeWebConsole.Domain
{
    public class AMC : IAuditable, ICompanyScoped
    {
       public int Id { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public string Company { get; set; }
        public string ContactPerson { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
        public string ServicesCovered { get; set; }
        public string ContractDocumentPath { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; } = true;
        public int CreatedBy { get; set; }
        public int? LastModifiedBy { get; set; }
    }
}
