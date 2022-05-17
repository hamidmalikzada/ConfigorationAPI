using ConfigAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigAPI.Models.ParameterModels
{
    public class CreateItemModel
    {
        public Item Item { get; set; }
        public List<ComponentTemp> Components { get; set; }


        public class ComponentTemp
        {
            public int Id { get; set; }
            public int Quantity { get; set; }
            public double Price { get; set; }
        }
    }
}
