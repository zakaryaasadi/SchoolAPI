using Newtonsoft.Json;
using SchoolAPI.ModelView;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace SchoolAPI.Models
{

    public class NewsClass
    {
        private byte[] _profileImage;
        private byte[] _newsImage;
        public int id { get; set; }
        public string personName { get; set; }

        public int userId { get; set; }
        public string userName { get; set; }
        public byte[] profileImage { get { return _profileImage; } set { _profileImage = Compression.ImageCompression(value, ImageFormat.Jpeg, 100, 100); } }
        public short? type { get; set; }
        public PrivateNewsType privateNewsType { get; set; }
        public string title { get; set; }
        public DateTime? creationDate { get; set; }
        public SubcategoryClass subcategory { get; set; }
        public string headLine { get; set; }
        public string body { get; set; }
        public byte[] newsImage { get { return _newsImage; } set { _newsImage = Compression.ImageCompression(value, ImageFormat.Jpeg); } }
        public bool sharable { get; set; }
        public DateTime? eventDate { get; set; }



    }

    public enum PrivateNewsType
    {
        Public,Group, Class, Subject
    }

}