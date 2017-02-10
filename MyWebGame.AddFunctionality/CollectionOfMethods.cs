using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text.RegularExpressions; 

namespace MyWebGam.AddFunctionality
{
    public class CollectionOfMethods
    { 
            private static Regex reGuid =   
        new Regex(  
        @"^[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}$",  
        RegexOptions.Compiled  
                    );  
  
        public static Guid StringToGuid(string id)  
        {  
            if (id == null || id.Length != 36 ) return Guid.Empty;  
            if (reGuid.IsMatch(id))  
                return new Guid(id);  
            else  
                return Guid.Empty;  
        }  
  
        public static string GetHashString(string s)  
            {  
              //переводим строку в байт-массим  
              byte[] bytes = Encoding.Unicode.GetBytes(s);  
  
              //создаем объект для получения средст шифрования  
              MD5CryptoServiceProvider CSP =  
                  new MD5CryptoServiceProvider();  
          
              //вычисляем хеш-представление в байтах  
              byte[] byteHash = CSP.ComputeHash(bytes);  
  
              string hash = string.Empty;  
  
              //формируем одну цельную строку из массива  
              foreach (byte b in byteHash)  
                  hash += string.Format("{0:x2}", b);  
  
              return hash;  
           }  
    }
}
