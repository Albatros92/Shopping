using ShoppingApp.Models.Enums;

namespace ShoppingApp.Models
{
    public class Coupon
    {
        public double MinAmaount { get; set; }
        public double Discount { get; set; }
        public DiscountType DiscountType { get; set; }
        public Coupon(double minAmount, double discount, DiscountType discountType)
        {
            this.MinAmaount = minAmount;
            this.Discount = discount;
            this.DiscountType = discountType;
        }
    }
}
