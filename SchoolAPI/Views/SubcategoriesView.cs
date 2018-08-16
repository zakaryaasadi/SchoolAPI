using SchoolAPI.Models;
using SchoolAPI.Models.MD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolAPI.Views
{
    public class SubcategoriesView
    {
        public static List<SubcategoryClass> getSubcategories(int categoryId, List<NEWS_SUB_CATS> newsSubCatsList)
        {

            List<SubcategoryClass> subCatsList = new List<SubcategoryClass>();

            foreach (var item in newsSubCatsList)
                subCatsList.Add(new SubcategoryClass()
                {
                    id = item.NEWS_SUB_CAT_ID,
                    title = item.TITLE,
                    categoryId = categoryId
                });

            return subCatsList;
        }
    }
}