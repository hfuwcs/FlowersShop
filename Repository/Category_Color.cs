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
    
    public partial class Category_Color
    {
        public int Category_Color_ID { get; set; }
        public Nullable<int> Category_ID { get; set; }
        public Nullable<int> Color_ID { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Color Color { get; set; }
    }
}
