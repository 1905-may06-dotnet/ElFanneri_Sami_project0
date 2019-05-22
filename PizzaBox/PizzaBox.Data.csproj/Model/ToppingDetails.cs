using System;
using System.Collections.Generic;

namespace PizzaBox.Data.Model
{
    public partial class ToppingDetails
    {
        public int ToppingId { get; set; }
        public string ToppingName { get; set; }
        public int ToppingPrice { get; set; }
    }
}
