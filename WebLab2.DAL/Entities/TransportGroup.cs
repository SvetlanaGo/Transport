using System;
using System.Collections.Generic;
using System.Text;

namespace WebLab2.DAL.Entities
{
    public class TransportGroup
    {
        public int TransportGroupId { get; set; }
        public string GroupName { get; set; }
        public List<Transport> Transports { get; set; }
    }
}
