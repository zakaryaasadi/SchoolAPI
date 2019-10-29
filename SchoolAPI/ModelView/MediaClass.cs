using SchoolAPI.Models.MD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolAPI.ModelView
{
    public class MediaClass
    {
        public int id { get; set; }
        public short? type { get; set; }
        public MediaClass(NEWS_MEDIAS m)
        {
            id = m.NEWS_MEDIA_ID;
            type = m.TYPE;
        }
    }
}