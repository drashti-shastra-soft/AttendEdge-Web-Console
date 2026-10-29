namespace AttendEdgeWebConsole.Domain
{
    public class Contact : IAuditable
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public string Message { get; set; }
        public bool IsActive { get; set; } = true;
        public int CreatedBy { get; set; }
        public int? LastModifiedBy { get; set; }
        public int? DeletedBy { get; set; }
        public string Status { get; set; }
        public string Token { get; set; }
    }
}
