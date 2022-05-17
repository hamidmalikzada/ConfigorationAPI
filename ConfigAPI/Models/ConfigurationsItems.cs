using ConfigAPI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigAPI.Models
{
    public class ConfigurationsItems
    {
        public int Id { get; set; }
        [ForeignKey("ConfigurationId")]
        public int ConfigurationId { get; set; }
        public Configuration Configuration { get; set; }
        [ForeignKey("ItemId")]
        public int ItemId { get; set; }
        public Item Item { get; set; }
    
    }
}
