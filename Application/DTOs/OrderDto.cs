namespace Application.DTOs
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string Article { get; set; }
        public decimal TotalAmount { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhoneNumber { get; set; }
    }
}