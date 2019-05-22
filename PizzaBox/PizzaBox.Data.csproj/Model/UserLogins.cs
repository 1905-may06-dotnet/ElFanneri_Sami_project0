using System;
using System.Collections.Generic;

namespace PizzaBox.Data.Model
{
    public partial class UserLogins
    {
        public UserLogins()
        {
            UserDetails = new HashSet<UserDetails>();
        }

        public int UserLoginId { get; set; }
        public string UserName { get; set; }
        public string UserPwd { get; set; }

        public virtual ICollection<UserDetails> UserDetails { get; set; }
    }
}
