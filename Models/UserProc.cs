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
    public class UserProc
    {
        //User Proc, Class này dùng để xử lý kiểm tra đăng nhập, đăng ký, xem có trùng dưới database hay không
        //TMI: User Proccesor, quá gà để xài từ nào khác :(

        QL_BanHoaEntities db = new QL_BanHoaEntities();
        public bool CheckUserName(string UserNameStr)
        {

            if (db.Users.Any(u => u.UserName.Equals(UserNameStr))) return true;
            return false;
        }
        public bool XacThucUser(Users users)
        {
            if (CheckUserName(users.UserName) && db.Users.Any(u => u.Password == users.Password))
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