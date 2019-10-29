using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SchoolAPI.Models.MD;

namespace SchoolAPI.Models
{
    public class HolidayClass
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime? holiday_date { get; set; }
        public string description { get; set; }

        public HolidayClass()
        {

        }

        public HolidayClass(HOLYDAYS holiday)
        {
            id = holiday.HOLYDAY_ID;
            name = holiday.NAME;
            holiday_date = holiday.HOLYDAY_DATE;
            description = holiday.DESCRIPTION;
        }
    }
}