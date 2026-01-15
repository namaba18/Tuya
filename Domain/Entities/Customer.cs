using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Customer : Entity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        private Customer() { }
        public Customer(string name, string email, string phone)
        {
            Name=name;
            Email=email;
            PhoneNumber=phone;
        }
    }
}