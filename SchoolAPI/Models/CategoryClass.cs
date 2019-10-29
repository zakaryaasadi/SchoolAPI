using SchoolAPI.Models.MD;
using SchoolAPI.ModelView;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace SchoolAPI.Models
{
    public class CategoryClass
    {
        private NEWS_CATS category;

        public int id { get; set; }
        public string title { get; set; }
        public byte[] image { get; set; }
        public List<SubcategoryClass> subcategories { get; set; }

        public CategoryClass(NEWS_CATS category)
        {
            this.category = category;
            id = category.NEWS_CAT_ID;
            title = category.TITLE;
            image = Compression.ImageCompression(category.PIC, ImageFormat.Png, 128);
            subcategories = getSubcategories();
        }

        private List<SubcategoryClass> getSubcategories() {
            List<SubcategoryClass> subCatsList = new List<SubcategoryClass>();

            foreach (var item in category.NEWS_SUB_CATS.ToList())
                subCatsList.Add(new SubcategoryClass(id, item.NEWS_SUB_CAT_ID));

            return subCatsList;
        }
    }
}