//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FlowersShop.Repository
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order_Details
    {
        public int Order_Detail_ID { get; set; }
        public string Order_ID { get; set; }
        public Nullable<int> Product_ID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<double> Price { get; set; }
    
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
