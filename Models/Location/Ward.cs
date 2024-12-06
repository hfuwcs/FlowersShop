using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlowersShop.Models.Adress
{
    public class Ward
    {
        public string name { get; set; }
        public int code { get; set; }
        public string division_type { get; set; }
        public string codename { get; set; }
        public int district_code { get; set; }
    }
}