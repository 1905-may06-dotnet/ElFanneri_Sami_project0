using System;
using System.Collections.Generic;

namespace PizzaBox.Data.Model
{
    public partial class RestaurantList
    {
        public int LocationId { get; set; }
        public string LocationAddress { get; set; }
        public string LocationPhone { get; set; }
    }
}
