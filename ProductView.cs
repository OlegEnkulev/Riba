using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riba
{
    public class ProductView
    {
        public string Article { get; set; }
        public string Name { get; set; }
        public string Peace { get; set; }
        public int Cost { get; set; }
        public int MaxDiscount { get; set; }
        public string Manufacturer { get; set; }
        public string Deliverer { get; set; }
        public int Category { get; set; }
        public int DiscountAmount { get; set; }
        public int QuantityInStock { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public string BoxColor { get; set; }
        public string CategoryName { get; set; }
        public string PhotoPath { get; set; }
        public string ProductString { get; set; }

        public ProductView(string article, string name, string peace, int cost, int maxDiscount, string manufacturer, string deliverer, int category, int discountAmount, int quantityInStock, string description, string photo)
        {
            Article = article;
            Name = name;
            Peace = peace;
            Cost = cost;
            MaxDiscount = maxDiscount;
            Manufacturer = manufacturer;
            Deliverer = deliverer;
            Category = category;
            DiscountAmount = discountAmount;
            QuantityInStock = quantityInStock;
            Description = description;
            Photo = photo;
            if (quantityInStock == 0)
            {
                BoxColor = "Gray";
            }
            else
                BoxColor = "#FF76E383";
            CategoryName = Core.DB.Category.Where(c => c.CategoryID == category).FirstOrDefault().CategoryName;
            if(photo == null)
            {
                PhotoPath = "/Images/picture.png";
            }
            else
            {
                if (photo.Length <= 2)
                {
                    PhotoPath = "/Images/picture.png";
                }
                else
                    PhotoPath = "/Images/" + photo;
            }
            ProductString = article + " " + name + " " + manufacturer + " " + deliverer + " " + category + " " + description;
        }
    }
}
