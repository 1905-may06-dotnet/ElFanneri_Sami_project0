using System;
using System.Collections.Generic;

namespace PizzaBox.Data.Model
{
    public partial class Orders
    {
        public Orders()
        {
            OrdersDetail = new HashSet<OrdersDetail>();
        }

        public int OrderId { get; set; }
        public string OrderAddress { get; set; }
        public int OrderPrice { get; set; }
        public int? UserId { get; set; }

        public virtual UserDetails User { get; set; }
        public virtual ICollection<OrdersDetail> OrdersDetail { get; set; }
    }
}
