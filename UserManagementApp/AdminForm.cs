using System;
using System.Linq;
using System.Windows.Forms;

namespace UserManagementApp
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            lstUsers.Items.Clear();
            foreach (var user in UserManager.Users.Where(u => !(u is Admin)))
            {
                lstUsers.Items.Add(user.Username);
            }
        }

        private void btnResetPassword_Click_1(object sender, EventArgs e)
        {
            if (lstUsers.SelectedItem != null)
            {
                string selectedUser = lstUsers.SelectedItem.ToString();
                var user = UserManager.Users.FirstOrDefault(u => u.Username == selectedUser);
                if (user != null)
                {
                    user.PasswordHash = User.HashPassword("default123"); // Reset na nové heslo
                    UserManager.SaveUsers();
                    MessageBox.Show($"Heslo pro {user.Username} bylo resetováno na 'default123'.", "Úspěch", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Vyberte uživatele!", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
