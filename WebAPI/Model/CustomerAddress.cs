using System;

namespace WebAPI.Model
{
    public class CustomerAddress
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public string PostCode { get; set; }
        public string Town { get; set; }
        public DateTime DateTime { get { return DateTime.Now; } }
        public ShoppingCart Cart { get; set; }
    }
}