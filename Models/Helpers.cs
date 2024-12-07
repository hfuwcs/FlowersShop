using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlowersShop.Models
{
    public static class Helpers
    {
         public static List<string> MonthName()
        {
            return new List<string>
            {
                "January",
                "February",
                "March",
                "April",
                "May",
                "June",
                "July",
                "August",
                "September",
                "October",
                "November",
                "December"
            };
        }
        public static List<int> MonthNumber()
        {
            return new List<int>
            {
                1,2,3,4,5,6,7,8,9,10,11,12
            };
        }
    }
}