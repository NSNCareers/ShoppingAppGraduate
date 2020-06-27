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

        // So that entity framework will populate address when getting shoppingcart from DB
        public virtual CustomerAddress Address { get; set; }
    }
}
