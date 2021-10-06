using ShoppingApp.Models;
using ShoppingApp.Models.Enums;
using System;
using System.Collections.Generic;

namespace ShoppingApp
{
    public class ShoppingProgram
    {
        public static void Main(String[] args)
        {
            //Adding categories which you want to add
            Category food = new Category("Food");
            Category fruit = new Category("Fruit");
            Category drink = new Category("Drink");
            fruit.MainCategory = food;
            //Adding products with price and categories
            Product apple = new Product("Apple", 10.0, fruit);
            Product banana = new Product("Banana", 15.0, food);
            Product coke = new Product("Coke", 5.0, drink);
            Product lemonate = new Product("Lemonate", 7.5, drink);
            Campaign campaign1 = new Campaign(food, 50.0, 5, DiscountType.Rate);
            Campaign campaign2 = new Campaign(drink, 50.0, 5, DiscountType.Rate);
            Campaign campaign3 = new Campaign(fruit, 5, 3, DiscountType.Amount);
            List<Campaign> campaigns = new List<Campaign>() { campaign1, campaign2, campaign3 };
            Coupon coupon = new Coupon(75, 15, DiscountType.Amount);
            ShoppingCart cart = new ShoppingCart();
            cart.Products.Add(apple, 5);
            cart.Products.Add(banana, 4);
            cart.Products.Add(coke, 3);
            cart.Products.Add(lemonate, 2);
            cart.applyDiscount(campaigns);
            cart.applyCoupon(coupon);
            DeliveryCostCalculator costCalculator = new DeliveryCostCalculator(1.5, 0.25);
            List<string> detailedInvoice = cart.getDetailInvoice();
            Console.WriteLine("Invoice details:");
            detailedInvoice.ForEach(i => Console.WriteLine("{0}\t", i));
            Console.WriteLine($"Total delivery cost amount : {costCalculator.calculateFor(cart)}");
            Console.WriteLine($"Total invoice amount : {cart.getTotalAmountAfterDiscounts()}"); 
            Console.ReadLine();
        }
    }
}
