using ShoppingApp.Interface;
using ShoppingApp.Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingApp.Models
{
    public class ShoppingCart : IShoppingCart
    {
        public static double totalAmount = 0;
        public static bool CampaignApplied = false;
        public static bool CouponApplied = false;
        public static double CampaignAmount = 0.0;
        public static double CouponAmount = 0.0;
        public static Campaign bestCampaign = null;

        public Dictionary<Product, int> Products { get; set; } // Products and quantity list
        public ShoppingCart()
        {
            this.Products = new Dictionary<Product, int>();
        }

        #region detail shopping info with discounts
        public List<string> getDetailInvoice()
        {
            List<string> detailedInvoice = new List<string>() { "Category Name Product Name Quantity Unit Price Total Price Total Discount(campaign + coupon) Total Price After Discount" };
            double discountAmountForProduct = 0.0;
            double couponDiscountForEachProduct = CouponAmount > 0.0 ? CouponAmount / Products.Keys.Count() : 0.0;
            foreach (KeyValuePair<Product, int> product in Products)
            {
                discountAmountForProduct = couponDiscountForEachProduct;
                if (bestCampaign != null && (product.Key.Category.Title == bestCampaign.Category.Title || product.Key.Category?.MainCategory?.Title == bestCampaign.Category.Title))
                {
                    var selectedProductTotalAmount = product.Key.Price * product.Value;
                    var selectedProductTotalQuantity = Products.Where(p => (p.Key.Category.Title == bestCampaign.Category.Title) || (p.Key.Category?.MainCategory?.Title == bestCampaign.Category.Title)).Select(x => x.Value).Sum();
                    discountAmountForProduct = CalculateDiscountAmount(selectedProductTotalAmount, selectedProductTotalQuantity, bestCampaign) + couponDiscountForEachProduct;
                }
                detailedInvoice.Add($"{product.Key.Category.Title} / {product.Key.Title} / {product.Key.Price} / {product.Key.Price * product.Value} / {discountAmountForProduct} / {product.Key.Price * product.Value - discountAmountForProduct}");
            }

            detailedInvoice.Add($"Coupon discount Amount totally : {CouponAmount}");

            return detailedInvoice;

        }
        #endregion

        #region applyDiscount after calculation
        public void applyDiscount(List<Campaign> campaigns)
        {
            totalAmount = Products.Select(p => p.Key.Price * p.Value).Sum();
            CampaignAmount = getCampaignDiscount(campaigns);
            totalAmount = totalAmount - CampaignAmount;
            CampaignApplied = true;
        }
        #endregion

        #region get  campaign with Max discount amount
        public double getCampaignDiscount(List<Campaign> campaigns)
        {
            Dictionary<Campaign, double> discountAmounts = new Dictionary<Campaign, double>();

            foreach (Campaign campaign in campaigns)
            {
                var FilteredProducts = Products.Where(p => (p.Key.Category.Title == campaign.Category.Title) || (p.Key.Category?.MainCategory?.Title == campaign.Category.Title)).ToList();
                var FilterProductsTotalAmount = FilteredProducts.Select(x => x.Key.Price * x.Value).Sum();
                var FilterProductsTotalQuantity = FilteredProducts.Select(x => x.Value).Sum();
                discountAmounts.Add(campaign, CalculateDiscountAmount(FilterProductsTotalAmount, FilterProductsTotalQuantity, campaign));
            }
            var bestCampaignPair = discountAmounts.OrderByDescending(x => x.Value).FirstOrDefault();
            bestCampaign = bestCampaignPair.Key;
            return bestCampaignPair.Value;
        }
        #endregion

        #region apply coupon sale after calculation
        public void applyCoupon(Coupon coupon)
        {
            if (CampaignApplied)
            {
                CouponAmount = getCouponDiscount(coupon);
                totalAmount = totalAmount - CouponAmount;
                CouponApplied = true;
            }
        }
        #endregion

        #region calculate coupon discount 
        //calculate coupon price sale in after campaign applied
        public double getCouponDiscount(Coupon coupon)
        {
            double couponDiscount = 0;
            if (totalAmount >= coupon.MinAmaount)
                couponDiscount = coupon.DiscountType == DiscountType.Amount ? coupon.Discount : (totalAmount * coupon.Discount / 100);

            return couponDiscount;
        }
        #endregion

        #region get total amount after calculation 
        public double getTotalAmountAfterDiscounts() => totalAmount != 0 ? totalAmount : Products.Select(p => p.Key.Price * p.Value).Sum();
        #endregion

        #region calculte discount amount for each campaign
        private double CalculateDiscountAmount(double totalAmount, int totalQuantity, Campaign campaign)
        {
            if (totalQuantity < campaign.ItemCount)
                return 0;

            if (campaign.DiscountType == DiscountType.Amount)
                return campaign.Discount;

            else
                return (campaign.Discount * totalAmount) / 100;
        }
        #endregion
    }

}
