using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace UserManagementApp
{
    [XmlInclude(typeof(Admin))] // Tady přidáme informaci o tom, že třída Admin může být serializována
    public class User
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }

        public User() { } // Nutný prázdný konstruktor pro serializaci

        public User(string username, string password)
        {
            Username = username;
            PasswordHash = HashPassword(password);
        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        public bool VerifyPassword(string password)
        {
            return PasswordHash == HashPassword(password);
        }
    }
}
