using SchoolAPI.Models.MD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolAPI.Models
{
    public class SubcategoryClass
    {
        public int id { get; set; }
        public string title { get; set; }
        public int? categoryId { get; set; }

        public SubcategoryClass() { }
        public SubcategoryClass(int categoryId, NEWS_SUB_CATS subCats)
        {
            if (subCats == null)
                return;

            id = subCats.NEWS_SUB_CAT_ID;
            title = subCats.TITLE;
            this.categoryId = categoryId;
        }

        public SubcategoryClass(int categoryId, int? subCatsId)
        {
            if (subCatsId == null)
                return;

            Entities e = new Entities();
            NEWS_SUB_CATS subCats = e.NEWS_SUB_CATS.FirstOrDefault(n => n.NEWS_SUB_CAT_ID == subCatsId);
            id = subCats.NEWS_SUB_CAT_ID;
            title = subCats.TITLE;
            this.categoryId = categoryId;
        }

        public SubcategoryClass(NEWS_SUB_CATS subCats)
        {
            if (subCats == null)
                return;

            id = subCats.NEWS_SUB_CAT_ID;
            title = subCats.TITLE;
        }
    }
}