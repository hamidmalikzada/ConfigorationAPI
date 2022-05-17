using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigAPI.Model
{
    public class Component
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public string ManufacturerPartId { get; set; }


    }
}
