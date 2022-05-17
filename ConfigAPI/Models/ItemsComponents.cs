using ConfigAPI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigAPI.Models
{
    public class ItemsComponents
    {
        public int Id { get; set; }

        [ForeignKey("ItemId")]
        public int ItemId { get; set; }
        public Item Item { get; set; }

        [ForeignKey("ComponentId")]
        public int ComponentId { get; set; }
        public Component Component { get; set; }

        public int Quantity { get; set; }
    }
}
