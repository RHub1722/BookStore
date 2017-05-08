namespace Shared.DTOS
{
    public class OrderDto : DTOBase
    {
        public int BookId { get; set; }

        public int Qty { get; set; }
       
        public int UserId { get; set; }

        public decimal TotalPrice { get; set; }
    }
}