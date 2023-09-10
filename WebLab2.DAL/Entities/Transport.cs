using System;
using System.Collections.Generic;
using System.Text;

namespace WebLab2.DAL.Entities
{
    public class Transport
    {
        public int TransportId { get; set; } 
        public string TransportName { get; set; } 
        public string Description { get; set; } 
        public int Price { get; set; } 
        public string Image { get; set; } 
        public int TransportGroupId { get; set; }
        public TransportGroup Group { get; set; }
    }
}
