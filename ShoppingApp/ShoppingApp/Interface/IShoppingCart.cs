using ShoppingApp.Models;
using System.Collections.Generic;

namespace ShoppingApp.Interface
{
    interface IShoppingCart
    {
        List<string> getDetailInvoice();
        void applyDiscount(List<Campaign> campaigns);
        double getCampaignDiscount(List<Campaign> campaigns);
        void applyCoupon(Coupon coupon);
        double getCouponDiscount(Coupon coupon);
        double getTotalAmountAfterDiscounts();
    }
}
