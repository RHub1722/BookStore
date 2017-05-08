using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Entities
{
    public class User : Entity
    {
        public string Role { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
