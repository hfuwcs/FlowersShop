using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlowersShop.Models
{
    public static class Helpers
    {
         public static List<string> Months()
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
    }
}