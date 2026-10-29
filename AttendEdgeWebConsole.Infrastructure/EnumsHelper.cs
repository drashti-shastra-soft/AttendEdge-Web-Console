using System;

namespace AttendEdgeWebConsole.Infrastructure
{
    public class EnumsHelper
    {
        public static string GetEnumString(Enums.Operation operation)
        {

            switch (operation)
            {
                case Enums.Operation.Save:
                    return nameof(Enums.Operation.Save);
                case Enums.Operation.Delete:
                    return nameof(Enums.Operation.Delete);
                default:
                    throw new ArgumentOutOfRangeException(nameof(operation), operation, null);
            }
        }

        public static string GetEnumString(Enums.MenuItem menuItem)
        {

            switch (menuItem)
            {
                case Enums.MenuItem.Company:
                    return nameof(Enums.MenuItem.Company);
                case Enums.MenuItem.User:
                    return nameof(Enums.MenuItem.User);
                case Enums.MenuItem.Holiday:
                    return nameof(Enums.MenuItem.Holiday);
                case Enums.MenuItem.Contact:
                    return nameof(Enums.MenuItem.Contact);
                case Enums.MenuItem.LeaveRequest:
                    return nameof(Enums.MenuItem.LeaveRequest);
                case Enums.MenuItem.AMC:
                    return nameof(Enums.MenuItem.AMC);
                case Enums.MenuItem.Attendance:
                    return nameof(Enums.MenuItem.Attendance);
                case Enums.MenuItem.ChangePassword:
                    return nameof(Enums.MenuItem.ChangePassword);
                default:
                    throw new ArgumentOutOfRangeException(nameof(menuItem), menuItem, null);
            }
        }

        public static string GetEnumString(Enums.Result result)
        {
            switch (result)
            {
                case Enums.Result.Success:
                    return nameof(Enums.Result.Success);
                case Enums.Result.Error:
                    return nameof(Enums.Result.Error);
                default:
                    throw new ArgumentOutOfRangeException(nameof(result), result, null);
            }
        }

        public static int GetEnumIndex(Enums.Role role)
        {
            switch (role)
            {
                case Enums.Role.System_Owner:
                    return (int)Enums.Role.System_Owner;
                case Enums.Role.Super_Admin:
                    return (int)Enums.Role.Super_Admin;
                case Enums.Role.Admin:
                    return (int)Enums.Role.Admin;
                case Enums.Role.Employee:
                    return (int)Enums.Role.Employee;
                default:
                    throw new ArgumentOutOfRangeException(nameof(role), role, null);
            }
        }

    }
}
