using System.Collections.Generic;

namespace AttendEdgeWebConsole.Domain
{
    public class HolidayLister
    {
        public List<Holiday> List { get; set; } = new List<Holiday>();

        public Holiday SearchCriteria { get; set; } = new Holiday();

        public Pagination Pagination { get; set; } = new Pagination();
    }
}
