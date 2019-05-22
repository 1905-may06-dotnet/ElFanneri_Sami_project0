using System;
using System.Collections.Generic;

namespace PizzaBox.Data.Model
{
    public partial class UserDetails
    {
        public UserDetails()
        {
            Orders = new HashSet<Orders>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int? UserLoginId { get; set; }

        public virtual UserLogins UserLogin { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
