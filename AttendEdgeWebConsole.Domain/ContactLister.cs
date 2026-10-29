using System.Collections.Generic;

namespace AttendEdgeWebConsole.Domain
{
    public class ContactLister
    {
        public List<Contact> List { get; set; } = new List<Contact>();

        public Contact SearchCriteria { get; set; } = new Contact();

        public Pagination Pagination { get; set; } = new Pagination();
    }
}
