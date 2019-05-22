using System;
using System.Collections.Generic;

namespace PizzaBox.Data.Model
{
    public partial class OrdersDetail
    {
        public int OrderDetailId { get; set; }
        public string OrderPizzaName { get; set; }
        public int OrderPizzaCount { get; set; }
        public int? OrderId { get; set; }

        public virtual Orders Order { get; set; }
    }
}
