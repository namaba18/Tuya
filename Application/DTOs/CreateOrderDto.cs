namespace Application.DTOs
{
    public class CreateOrderDto
    {
        public string Article { get; set; }
        public decimal TotalAmount { get; set; }
        public Guid CustomerId { get; set; }
    }
}