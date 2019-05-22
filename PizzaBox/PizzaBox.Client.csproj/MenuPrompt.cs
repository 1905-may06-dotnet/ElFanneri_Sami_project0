using PizzaBox.Client.csproj;
using PizzaBox.Data.Model;
using PizzaBox.Domain.csproj;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using PizzaBox.Domain;

namespace PizzaBox.Client
{
    public class MenuPrompt
    {
        String userName = null;
        String userPwd = null;

        List<UserDetails> currUser;
        List<RestaurantList> currLocation;
        List<int> pizzaOrderCost = new List<int>(capacity: 100);

        bool repromptUser = true;

        PizzaBuilder pizzaBuilder = new PizzaBuilder();

        public void MainMenu(PizzaBoxDbContext context)
        {
            int userChoice;
            do
            {
                Console.WriteLine("1 - Log in with existing account.");
                Console.WriteLine("2 - Create new account.");
                Console.WriteLine("3 - exit");

                userChoice = Convert.ToInt32(Console.ReadLine());
            } while (userChoice < 1 || userChoice > 3);

            switch (userChoice)
            {
                case 1:
                    Console.Clear();
                    LoginMenu(context);
                    break;
                case 2:
                    CreateNewAccount(context);
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
            }
        }

        public void LoginMenu(PizzaBoxDbContext context)
        {
            String userChoice;
            do
            {
                userName = UserPrompter.PromptUserLogin();
                userPwd = UserPrompter.PromptUserPwd();

                repromptUser = LoginAuth.IsValidUser(userName, userPwd, context);
                if (repromptUser)
                {
                    Console.WriteLine("User name or password incorrect. Try again?(y/n)");
                    userChoice = Console.ReadLine();
                    if(userChoice == "n")
                    {
                        Console.Clear();
                        MainMenu(context);
                    }
                    else
                    {
                        Console.Clear();
                    }
                }
            } while (repromptUser);

            this.currUser = context.UserDetails.Where(x => x.UserLoginId == LoginAuth.userID).ToList();

            LocationsMenu(context);
        }

        public void CreateNewAccount(PizzaBoxDbContext context)
        {
            Console.WriteLine("Please enter a desired username: ");
            var userName = Console.ReadLine();

            Console.WriteLine("Please enter a desired password: ");
            var passWord = Console.ReadLine();

            var newUser = context.Set<UserLogins>();
            newUser.Add(new UserLogins { UserName = userName, UserPwd = passWord});
            context.SaveChanges();
            Console.Clear();

            Console.WriteLine("Please enter your first name: ");
            var firstName = Console.ReadLine();

            Console.WriteLine("Please enter your last name: ");
            var lastName = Console.ReadLine();

            Console.WriteLine("Please enter your address: ");
            var userAddress = Console.ReadLine();

            Console.WriteLine("Please enter your phone number: ");
            var userPhone = Console.ReadLine();

            Console.WriteLine("Please enter your email: ");
            var userEmail = Console.ReadLine();

            var lastElement = context.UserLogins.OrderByDescending(p => p.UserLoginId).ToList();
            var newUserDetails = context.Set<UserDetails>();
            newUserDetails.Add(new UserDetails { FirstName = firstName, LastName = lastName, Address = userAddress,
                           Phone = userPhone, Email = userEmail, UserLoginId = lastElement[0].UserLoginId});
            context.SaveChanges();
            Console.Clear();
            MainMenu(context);
        }

        public void LocationsMenu(PizzaBoxDbContext context)
        {
            int userChoice = 0;
            do
            {
                Console.WriteLine("Please choose one of our locations from the following list: ");
                Console.WriteLine("");

                foreach (var x in context.RestaurantList)
                {
                    Console.WriteLine(x.LocationId + " " + x.LocationAddress + " " + x.LocationPhone);
                }

                userChoice = Convert.ToInt32(Console.ReadLine());
            } while (userChoice < 1 || userChoice > 4);

            Console.Clear();
            this.currLocation = context.RestaurantList.Where(x => x.LocationId == userChoice).ToList();

            OrderMenu(context);
        }

        public void OrderMenu(PizzaBoxDbContext context)
        {
            Console.Clear();
            Console.WriteLine("You have chosen " + currLocation[0].LocationAddress);
            int userChoice;
            do
            {
                Console.WriteLine("1 - Make new order.");
                Console.WriteLine("2 - View order history.");
                Console.WriteLine("3 - Log Out");
                userChoice = Convert.ToInt32(Console.ReadLine());
            } while (userChoice < 1 || userChoice > 3);

            switch(userChoice)
            {
                case 1:
                    Console.Clear();
                    OrderPizza(context);
                    break;
                case 2:
                    Console.Clear();
                    ViewOrderHistory(context);
                    break;
                case 3:
                    Console.Clear();
                    MainMenu(context);
                    break;
            }
        }

        public void OrderPizza(PizzaBoxDbContext context)
        {
            int userChoice;
            String answer;

            List<RestaurantList> userPizzaChoice;
            List<PizzaSize> userSizeChoice;
            List<PizzaCrust> userCrustChoice;

            do
            {
                Console.Clear();
                do
                {
                    Console.WriteLine("Choose among one of the finest pizzas in Texas:");
                    foreach (var x in context.PizzaDetails)
                    {
                        Console.WriteLine(x.PizzaId + " - " + x.PizzaName + "  - " + x.PizzaDesc);
                    }
                    userChoice = Convert.ToInt32(Console.ReadLine());
                } while (userChoice < 1 || userChoice > 4);

                userPizzaChoice = context.RestaurantList.Where(x => x.LocationId == userChoice).ToList();

                do
                {
                    Console.WriteLine("Choose your size:");
                    foreach (var x in context.PizzaSize)
                    {
                        Console.WriteLine(x.SizeId + " " + x.SizeName);
                    }
                    userChoice = Convert.ToInt32(Console.ReadLine());
                } while (userChoice < 1 || userChoice > 5);

                userSizeChoice = context.PizzaSize.Where(x => x.SizeId == userChoice).ToList();

                do
                {
                    Console.WriteLine("Choose your crust:");
                    foreach (var x in context.PizzaCrust)
                    {
                        Console.WriteLine(x.CrustId + " " + x.CrustName);
                    }
                    userChoice = Convert.ToInt32(Console.ReadLine());
                } while (userChoice < 1 || userChoice > 3);

                userCrustChoice = context.PizzaCrust.Where(x => x.CrustId == userChoice).ToList();

                pizzaOrderCost.Add(
                    pizzaBuilder.CalcPrice(userPizzaChoice[0].LocationId, userSizeChoice[0].SizePrice, userCrustChoice[0].CrustPrice)
                );

                Console.WriteLine("Would you like to add another Pizza to your order?(y/n)");
                answer = Console.ReadLine();
            } while (answer == "y");

            int totalDue = 0;
            for(var x = 0; x < pizzaOrderCost.Count; ++x)
            {
                totalDue = totalDue + pizzaOrderCost[x];
            }

            String userValidateOrder;
            Console.WriteLine("Your total due is : " + totalDue + "$. Would you like to confirm this order?(y/n)");
            userValidateOrder = Console.ReadLine();
            if (userValidateOrder == "y")
            {
                var isCountValid = pizzaBuilder.isCountLimit(pizzaOrderCost.Count);
                var isPriceValid = pizzaBuilder.isPriceLimit(totalDue);

                if (isCountValid == false || isPriceValid == false)
                {
                    OrderMenu(context);
                }
                else
                {
                    var newOrder = context.Set<Orders>();
                    newOrder.Add(new Orders { OrderAddress = currLocation[0].LocationAddress, OrderPrice = totalDue, UserId = currUser[0].UserId });
                    context.SaveChanges();
                    OrderMenu(context);
                }
            }
            else
            {
                OrderMenu(context);
            }
        }

        public void ViewOrderHistory(PizzaBoxDbContext context)
        {
            foreach(var x in context.Orders)
            {
                if(x.UserId == currUser[0].UserId)
                {
                    Console.WriteLine(x.OrderId + " " + x.OrderAddress + " " + x.OrderPrice + "$");
                }
            }

            String userChoice;
            do
            {
                Console.WriteLine("Press 'b' to go back to the previous menu: ");
                userChoice = Console.ReadLine();
            } while (userChoice != "b");

            OrderMenu(context);
        }
    }
}
