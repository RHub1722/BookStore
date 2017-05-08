using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Entities
{
    public class Book : Entity
    {
        public string Name { get; set; } 
        public string Author { get; set; } 
        public int ChategoryId { get; set; }
        public Category Category { get; set; }
        public decimal Price { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
