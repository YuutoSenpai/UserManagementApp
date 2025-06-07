using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace UserManagementApp
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            ApplyStyles();
            UserManager.LoadUsers();
        }

        private void ApplyStyles()
        {
            // Základní nastavení formuláře
            this.Text = "Přihlášení do systému";
            this.ClientSize = new Size(450, 400); // Zvětšeno kvůli novému tlačítku
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 10);

            // Centrování ovládacích prvků
            int centerX = this.ClientSize.Width / 2;

            lblUsername.Text = "Uživatelské jméno:";
            lblUsername.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(centerX - lblUsername.Width / 2, 60);

            txtUsername.Location = new Point(centerX - 125, 90);
            txtUsername.Size = new Size(250, 30);
            txtUsername.Font = new Font("Segoe UI", 10);
            txtUsername.BorderStyle = BorderStyle.FixedSingle;

            lblPassword.Text = "Heslo:";
            lblPassword.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(centerX - lblPassword.Width / 2, 140);

            txtPassword.Location = new Point(centerX - 125, 170);
            txtPassword.Size = new Size(250, 30);
            txtPassword.Font = new Font("Segoe UI", 10);
            txtPassword.BorderStyle = BorderStyle.FixedSingle;

            btnLogin.Text = "Přihlásit";
            btnLogin.Location = new Point(centerX - 65, 220);
            btnLogin.Size = new Size(130, 40);
            btnLogin.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnLogin.BackColor = Color.FromArgb(0, 120, 215);
            btnLogin.ForeColor = Color.White;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Cursor = Cursors.Hand;

            // Nové tlačítko pro registraci
            Button btnRegister = new Button();
            btnRegister.Text = "Zaregistrovat se";
            btnRegister.Location = new Point(centerX - 65, 280);
            btnRegister.Size = new Size(130, 40);
            btnRegister.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnRegister.BackColor = Color.FromArgb(46, 204, 113); // Zelená barva
            btnRegister.ForeColor = Color.White;
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.Cursor = Cursors.Hand;
            btnRegister.Click += BtnRegister_Click;
            this.Controls.Add(btnRegister);
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vyplňte uživatelské jméno i heslo!", "Chyba",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (UserManager.Users.Any(u => u.Username == username))
            {
                MessageBox.Show("Uživatel s tímto jménem již existuje!", "Chyba",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Vytvoření nového běžného uživatele (ne admina)
            User newUser = new User(username, password);
            UserManager.Users.Add(newUser);
            UserManager.SaveUsers();

            MessageBox.Show("Uživatel byl úspěšně zaregistrován!", "Úspěch",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            User user = UserManager.Authenticate(username, password);

            if (user != null)
            {
                MessageBox.Show($"Přihlášen jako: {user.Username}", "Úspěch",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                MessageBox.Show("Neplatné přihlašovací údaje.", "Chyba",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}