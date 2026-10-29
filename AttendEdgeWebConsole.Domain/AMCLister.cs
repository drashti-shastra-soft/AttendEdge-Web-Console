using System.Collections.Generic;

namespace AttendEdgeWebConsole.Domain
{
    public class AMCLister
    {
        public List<AMC> List { get; set; } = new List<AMC>();

        public Domain.AMC SearchCriteria { get; set; } = new Domain.AMC();

        public Pagination Pagination { get; set; } = new Pagination();
    }
}
