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
    using System.ComponentModel.DataAnnotations;
    
    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            this.Cart = new HashSet<Cart>();
        }
        public Users(Users u)
        {
            this.Cart = new HashSet<Cart>();
            this.ID = u.ID;
            this.Name = u.Name;
            this.Address = u.Address;
            this.Email = u.Email;
            this.Phone = u.Phone;
            this.UserName = u.UserName;
            this.Password = u.Password;
            this.Role_id = 2;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        [Required]
        public string UserName { get; set; }
        public string Password { get; set; }
        public Nullable<int> Role_id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart> Cart { get; set; }
        public virtual User_Role User_Role { get; set; }
    }
}
