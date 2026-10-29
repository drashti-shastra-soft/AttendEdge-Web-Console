using System.Collections.Generic;

namespace AttendEdgeWebConsole.Domain
{
    public class AttendanceLister
    {
        public List<Attendance> List { get; set; } = new List<Attendance>();

        public Domain.Attendance SearchCriteria { get; set; } = new Domain.Attendance();

        public Pagination Pagination { get; set; } = new Pagination();
    }
}
