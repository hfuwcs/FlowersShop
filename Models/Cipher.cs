using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace FlowersShop.Models
{
    public class Cipher
    {

        public static string EncryptSHA256(string plainText)
        {
            string res = "";
            using (SHA256 sha256 = SHA256.Create()) {
                //Chuyển plaintText thành mảng Byte
                //Convert plaint text to a bytes array

                byte[] sourceData=Encoding.UTF8.GetBytes(plainText);
                byte[] hashRes = sha256.ComputeHash(sourceData);

                res = BitConverter.ToString(hashRes, 0, hashRes.Length).Replace("-",string.Empty);

                return res;
            }
        }
    }
}