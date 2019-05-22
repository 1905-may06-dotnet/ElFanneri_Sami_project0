using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaBox.Domain
{
    public class PizzaBuilder
    {
        public int CalcPrice(int pizzaID, int pizzaSizePrice, int pizzaCrustPrice)
        {
            int basePrice = 0;

            switch (pizzaID)
            {
                case 1:
                    basePrice = 8;
                    break;
                case 2:
                    basePrice = 9;
                    break;
                case 3:
                    basePrice = 6;
                    break;
                case 4:
                    basePrice = 5;
                    break;
                case 5:
                    basePrice = 3;
                    break;
                case 6:
                    basePrice = 3;
                    break;
                case 7:
                    basePrice = 4;
                    break;
            }
            return basePrice+ pizzaSizePrice+ pizzaCrustPrice;
        }

        public Boolean isPriceLimit(int totaldue)
        {
            if(totaldue > 5000)
            {
                Console.WriteLine("We are sorry, but your total cannot exceed 5000$");
                return false;
            }
            else
            {
                return true;
            }
        }

        public Boolean isCountLimit(int totalCount)
        {
            if(totalCount > 100)
            {
                Console.WriteLine("We are sorry, but you cannot order more than 100 pizzas per order");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
