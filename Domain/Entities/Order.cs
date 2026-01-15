namespace Domain.Entities
{
    public class Order : Entity
    {
        public string Article { get; set; }
        public decimal TotalAmount { get; set; }
        public Customer Customer { get; set; }

        private Order() { }
        public Order(string article, decimal totalAmount, Customer customer)
        {
            Article = article;
            TotalAmount = totalAmount;
            Customer = customer;
        }

    }
}