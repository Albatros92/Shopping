using ShoppingApp.Interface;

namespace ShoppingApp.Models
{
    public class Product : ICategory
    {
        public string Title { get; set; }
        public double Price { get; set; }
        public Category Category { get; set; }

        public Product(string title, double price, Category category)
        {
            this.Title = title;
            this.Price = price;
            this.Category = category;
        }
    }
     
   
}
