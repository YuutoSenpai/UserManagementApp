using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace UserManagementApp
{
    public static class UserManager
    {
        private static string filePath = "users.xml";
        public static List<User> Users { get; private set; } = new List<User>();

        public static void LoadUsers()
        {
            if (File.Exists(filePath))
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
                    using (FileStream fs = new FileStream(filePath, FileMode.Open))
                    {
                        // Pokud je soubor prázdný, načteme prázdný seznam
                        if (fs.Length == 0)
                        {
                            Users = new List<User>();
                        }
                        else
                        {
                            Users = (List<User>)serializer.Deserialize(fs);
                        }
                    }
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show($"Chyba při načítání uživatelů: {ex.Message}", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Zde můžete přidat logiku pro případ chyby deserializace (např. resetování souboru)
                }
            }
            else
            {
                // Pokud soubor neexistuje, vytvoříme prázdný seznam
                Users = new List<User>();
            }

            if (!Users.Any())
            {
                Users.Add(new User("user", "user123"));
                SaveUsers(); // Uložíme do souboru
            }

            // Přidáme defaultního admina, pokud neexistuje
            if (!Users.Any(u => u is Admin))
            {
                Users.Add(new Admin("admin", "admin123"));
                SaveUsers(); // Uložíme uživatele do souboru
            }
        }

        public static void SaveUsers()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(fs, Users);
            }
        }

        public static User Authenticate(string username, string password)
        {
            string hashedPassword = User.HashPassword(password);
            return Users.FirstOrDefault(u => u.Username == username && u.PasswordHash == hashedPassword);
        }
    }
}
