using System;
using System.Windows.Forms;

namespace UserManagementApp
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            UserManager.LoadUsers();
        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            User user = UserManager.Authenticate(username, password);

            if (user != null)
            {
                MessageBox.Show($"Přihlášen jako: {user.Username}", "Úspěch", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (user is Admin)
                {
                    new AdminForm().Show();
                }
                else
                {
                    new MainForm(user).Show();
                }
                this.Hide();
            }
            else
            {
                MessageBox.Show("Neplatné přihlašovací údaje.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
