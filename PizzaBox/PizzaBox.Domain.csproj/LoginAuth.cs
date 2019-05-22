using PizzaBox.Data.Model;
using System;

namespace PizzaBox.Domain.csproj
{
    public static class LoginAuth
    {
        public static int userID;
        public static Boolean IsValidUser(String userName, String userPwd, PizzaBoxDbContext context)
        {
            foreach (var x in context.UserLogins)
            {
                if (x.UserName == userName && x.UserPwd == userPwd)
                {
                    userID = x.UserLoginId;
                    Console.WriteLine("Record Found!");
                    Console.WriteLine("");
                    return false;
                }
            }

            return true;
        }
    }
}
