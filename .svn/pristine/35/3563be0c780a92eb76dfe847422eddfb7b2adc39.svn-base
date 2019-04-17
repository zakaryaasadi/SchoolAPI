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
        private byte[] _image;

        public int id { get; set; }
        public string title { get; set; }
        public byte[] image { get { return _image; } set { _image = Compression.ImageCompression(value,ImageFormat.Png,100,100); } }
        public List<SubcategoryClass> subcategories { get; set; }

        public CategoryClass(NEWS_CATS category)
        {
            this.category = category;
            id = category.NEWS_CAT_ID;
            title = category.TITLE;
            image = category.PIC;
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