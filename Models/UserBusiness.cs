using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlowersShop.Models;
using System.Data.Entity;
using Microsoft.Ajax.Utilities;
using FlowersShop.Repository;//Các context, class được lưu ở Repository

namespace FlowersShop.Models
{
    public class UserBusiness
    {

        QL_BanHoaEntities db = new QL_BanHoaEntities();
        public bool CheckUserName(string UserNameStr)
        {

            if (db.Users.Any(u => u.UserName.Equals(UserNameStr))) return true;
            return false;
        }
        public bool IsAdmin(Users users)
        {
            string users_Password = Cipher.EncryptSHA256(users.Password);
            Users User = db.Users
                .Where(u => u.UserName.Equals(users.UserName) && u.Password.Equals(users_Password))
                .FirstOrDefault<Users>();
            bool IsAdmin = User != null && CheckUserName(User.UserName) && db.Users.Any(u => u.Password.Equals(User.Password)) && User.Role_ID == 1;
            if (IsAdmin)
            {
                return true;
            }
            return false;
        }
        public bool IsUser(Users users)
        {
            string users_Password = Cipher.EncryptSHA256(users.Password);
            Users User = db.Users
                .Where(u => u.UserName.Equals(users.UserName) && u.Password.Equals(users_Password))
                .FirstOrDefault<Users>();
            bool IsUsers = User!=null && CheckUserName(User.UserName) && db.Users.Any(u => u.Password.Equals(User.Password)) && User.Role_ID == 2;
            if (IsUsers)
            {
                //Dùng Linq Any để check xem có đúng UserName với Password hay không
                return true;
            }
            return false;
        }
        public List<Users> GetUsers() {
            return db.Users.ToList();
        }
    }
}