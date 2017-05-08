using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.DTOS
{
    public class BookDto : DTOBase
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public int ChategoryId { get; set; }
        public decimal Price { get; set; }
    }
}
