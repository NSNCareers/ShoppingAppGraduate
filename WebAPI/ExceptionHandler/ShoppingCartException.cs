using System;
using System.Collections.Generic;
using WebAPI.Model;

namespace WebAPI.ExceptionHandler
{
    public class ShoppingCartException
    {
        public string Message { get; set; }
        public string Source { get; set; }
        public string StackTrace { get; set; }
        public Exception InnerException { get; set; }
        public bool BoolResults { get; set; }
        public ICollection<ShoppingCart> Collection { get; set; }
        public ShoppingCart ShoppingCarts { get; set; }
    }
}
