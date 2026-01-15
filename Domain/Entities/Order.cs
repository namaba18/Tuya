namespace Domain.Entities
{
    public class Order : Entity
    {
        public string Article { get; set; }
        public decimal TotalAmount { get; set; }
        public Customer Customer { get; set; }

    }
}