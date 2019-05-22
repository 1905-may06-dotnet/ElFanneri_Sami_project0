using PizzaBox.Data.Model;
using System;

namespace PizzaBox.Client.csproj
{
    public static class UserPrompter
    {
        public static String PromptUserLogin()
        {
            String tempValue;

            Console.Write("Username: ");
            tempValue = Console.ReadLine();
            Console.WriteLine("");

            return tempValue;
        }

        public static String PromptUserPwd()
        {
            String tempValue;

            Console.Write("Password: ");
            tempValue = Console.ReadLine();
            Console.WriteLine("");

            return tempValue;
        }

    }
}
