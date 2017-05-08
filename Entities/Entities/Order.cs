using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Entities
{
    public class Order : Entity
    {
        public Book Book { get; set; }
        public int BookId { get; set; }

        public int Qty { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
