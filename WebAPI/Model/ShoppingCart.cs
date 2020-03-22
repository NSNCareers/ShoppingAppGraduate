using System.Collections.Generic;

namespace WebAPI.Model
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public string Gender { get; set; }
        public ICollection<Item> Item { get; set; }
        public  string CustomerName { get; set; }
        public  string OrderQuantity { get; set; }
        public  decimal Price { get; set; }
        public CustomerAddress Address { get; set; }
    }
}
