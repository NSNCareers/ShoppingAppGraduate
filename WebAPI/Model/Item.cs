namespace WebAPI.Model
{
    public class Item
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int Size { get; set; }
        public int Weight { get; set; }
        public string Seller { get; set; }
        public ShoppingCart Cart { get; set; }
    }
}
