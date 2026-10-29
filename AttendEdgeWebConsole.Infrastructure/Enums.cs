namespace AttendEdgeWebConsole.Infrastructure
{
    public class Enums
    {
        public enum Operation
        {
            Save = 1,
            Delete = 2,
        }

        public enum MenuItem
        {
            Company = 1,
            User = 2,
            Holiday = 3,
            Contact = 4,
            LeaveRequest = 5,
            AMC = 6,
            Attendance = 7,
            ChangePassword = 8
        }

        public enum Result
        {
            Success = 1,
            Error = 2
        }

        public enum Role
        {
            System_Owner = 1,
            Super_Admin = 2,
            Admin = 3,
            Employee = 4
        }

        public enum LeaveType 
        {
            Paid = 1,
            Sick = 2,
            Casual = 3
        }

        public enum StatusType
        {
            Pending = 1,
            Approved = 2,
            Rejected = 3
        }

        public enum HalfType
        {
            First_Half = 1,
            Second_Half = 2
        }

    }
}
