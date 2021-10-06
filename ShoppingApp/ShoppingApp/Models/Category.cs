using ShoppingApp.Interface;

namespace ShoppingApp.Models
{
    public class Category : ICategory
    {
        public string Title { get; set; }
        public Category MainCategory { get; set; }

        public Category(string title)
        {
            this.Title = title;
        }
        public Category(string title, Category mainCategory)
        {
            this.Title = title;
            this.MainCategory = mainCategory;
        }
    }
}
