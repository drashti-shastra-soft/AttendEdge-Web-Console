namespace AttendEdgeWebConsole.Domain
{
    public class Credential
    {
        public string EmailAddress { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; } = true;

        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
