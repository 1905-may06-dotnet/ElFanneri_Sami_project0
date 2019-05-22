using PizzaBox.Client;
using PizzaBox.Client.csproj;
using PizzaBox.Data.Model;
using PizzaBox.Domain;
using PizzaBox.Domain.csproj;
using System;
using System.Linq;

namespace PizzaBox
{
    class Program
    {
        static void Main(string[] args)
        {
            MenuPrompt menuPrompt = new MenuPrompt();

            using (var context = new PizzaBoxDbContext())
            {
                menuPrompt.MainMenu(context);
            }
        }
    }
}
