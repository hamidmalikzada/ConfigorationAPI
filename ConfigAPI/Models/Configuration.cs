using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigAPI.Model
{
    public class Configuration
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public  string VersionNumber { get; set; }

    }
}
