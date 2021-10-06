using ShoppingApp.Interface;
using System.Linq;

namespace ShoppingApp.Models
{
    public class DeliveryCostCalculator : ICalculator
    {
        public double CostPerDelivery { get; set; }
        public double CostPerProduct { get; set; }
        public double FixedCost { get; set; }
        public DeliveryCostCalculator(double costPerDelivery, double costPerProduct, double fixedCost = 2.99)
        {
            this.CostPerDelivery = costPerDelivery;
            this.CostPerProduct = costPerProduct;
            this.FixedCost = fixedCost;
        }

        public double calculateFor(ShoppingCart cart)
        {
            int numberOfDeliveries = cart.Products.Keys.Select(c => c.Category).Distinct().Count();
            int numberOfProducts = cart.Products.Keys.Select(p => p.Title).Distinct().Count();
            double totalDeliveryCost = (CostPerDelivery * numberOfDeliveries) + (CostPerProduct * numberOfProducts) + FixedCost;
            return totalDeliveryCost;
        }
    }
}
