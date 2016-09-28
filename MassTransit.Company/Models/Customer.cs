using System;

namespace MassTransit.Company.Models
{
    public class Customer
    {
        private readonly Guid _id;
        private readonly string _name;
        private readonly string _address;

        public Guid Id { get { return _id; } }
        public string Address { get { return _address; } }
        public string Name { get { return _name; } }
        public DateTime RegisteredDate { get; set; }
        public int Type { get; set; }
        public bool Preferred { get; set; }
        public decimal DefaultDiscount { get; set; }


        public Customer(Guid id, string name, string address)
        {
            _id = id;
            _name = name;
            _address = address;
        }
    }
}
