using System.Collections.Generic;

namespace AttendEdgeWebConsole.Domain
{
    public class LeaveRequestLister
    {
        public List<LeaveRequest> List { get; set; } = new List<LeaveRequest>();

        public LeaveRequest SearchCriteria { get; set; } = new LeaveRequest();

        public Pagination Pagination { get; set; } = new Pagination();
    }
}
