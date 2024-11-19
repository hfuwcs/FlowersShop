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
    
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.Order_Details = new HashSet<Order_Details>();
            this.Order_Discount = new HashSet<Order_Discount>();
            this.Payment = new HashSet<Payment>();
            this.Shipping = new HashSet<Shipping>();
        }
    
        public int Order_ID { get; set; }
        public Nullable<System.DateTime> Order_Date { get; set; }
        public string Status { get; set; }
        public Nullable<double> Total_Amount { get; set; }
        public Nullable<int> Customer_ID { get; set; }
        public string Shipping_Address { get; set; }
        public string Payment_Method { get; set; }
        public Nullable<int> User_ID { get; set; }
        public string ThoiGianNhanHang { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Users Users { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Details> Order_Details { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Discount> Order_Discount { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payment> Payment { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Shipping> Shipping { get; set; }
    }
}
