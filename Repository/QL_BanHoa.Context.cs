﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QL_BanHoaEntities : DbContext
    {
        public QL_BanHoaEntities()
            : base("name=QL_BanHoaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<Color> Color { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Discount> Discount { get; set; }
        public virtual DbSet<Object> Object { get; set; }
        public virtual DbSet<Occasion> Occasion { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Order_Details> Order_Details { get; set; }
        public virtual DbSet<Order_Discount> Order_Discount { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<Presentation> Presentation { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Review> Review { get; set; }
        public virtual DbSet<Shipping> Shipping { get; set; }
        public virtual DbSet<User_Role> User_Role { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Flower_Type> Flower_Type { get; set; }
    }
}
