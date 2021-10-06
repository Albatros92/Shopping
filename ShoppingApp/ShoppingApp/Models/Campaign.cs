using ShoppingApp.Interface;
using ShoppingApp.Models.Enums;

namespace ShoppingApp.Models
{
    public class Campaign : ICategory
    {
        public Category Category { get; set; }
        public double Discount { get; set; }
        public int ItemCount { get; set; }
        public DiscountType DiscountType { get; set; }

        public Campaign(Category category, double discount, int itemCount, DiscountType discountType)
        {
            this.Category = category;
            this.Discount = discount;
            this.ItemCount = itemCount;
            this.DiscountType = discountType;
        }
    }
}
