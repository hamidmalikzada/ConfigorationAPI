using ConfigAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigAPI.Models.ParameterModels
{
    public class CreateConfigurationModel
    {
        public Configuration Configuration { get; set; }

        public List<Item> Items  { get; set; }
    }
}
