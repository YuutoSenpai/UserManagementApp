using System;
using System.Windows.Forms;

namespace UserManagementApp
{
    public partial class MainForm : Form
    {
        private User currentUser;

        public MainForm(User user)
        {
            InitializeComponent();
            currentUser = user;
            lblWelcome.Text = $"Vítej, {currentUser.Username}!";
        }


        private void btnChangePassword_Click_1(object sender, EventArgs e)
        {
            string newPassword = txtNewPassword.Text;
            if (!string.IsNullOrEmpty(newPassword))
            {
                currentUser.PasswordHash = User.HashPassword(newPassword);
                UserManager.SaveUsers();
                MessageBox.Show("Heslo bylo změněno.", "Úspěch", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Zadejte nové heslo!", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
