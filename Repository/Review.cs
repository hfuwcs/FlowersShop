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
    
    public partial class Review
    {
        public int Review_ID { get; set; }
        public Nullable<int> Product_ID { get; set; }
        public Nullable<int> Customer_ID { get; set; }
        public Nullable<int> Rating { get; set; }
        public string Comment { get; set; }
        public Nullable<System.DateTime> Review_Date { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }
}
