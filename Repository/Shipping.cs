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
    
    public partial class Shipping
    {
        public int Shipping_ID { get; set; }
        public string Shipping_Method { get; set; }
        public Nullable<double> Shipping_Cost { get; set; }
        public Nullable<int> Order_ID { get; set; }
        public string Shipping_Status { get; set; }
    
        public virtual Order Order { get; set; }
    }
}
