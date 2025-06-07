using System;
using System.Drawing;
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
            ApplyStyles();
        }

        private void ApplyStyles()
        {
            // Základní nastavení formuláře
            this.Text = "Hlavní panel";
            this.ClientSize = new Size(600, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 10);

            // Panel záhlaví
            Panel headerPanel = new Panel();
            headerPanel.Dock = DockStyle.Top;
            headerPanel.BackColor = Color.FromArgb(0, 120, 215);
            headerPanel.Height = 80;

            lblWelcome.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblWelcome.ForeColor = Color.White;
            lblWelcome.Dock = DockStyle.Fill;
            lblWelcome.TextAlign = ContentAlignment.MiddleCenter;
            lblWelcome.Text = $"Vítej, {currentUser.Username}!";

            headerPanel.Controls.Add(lblWelcome);
            this.Controls.Add(headerPanel);

            // Hlavní obsah
            lblNewPassword.Text = "Nové heslo:";
            lblNewPassword.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblNewPassword.Location = new Point(50, 120);
            lblNewPassword.AutoSize = true;

            txtNewPassword.Location = new Point(50, 150);
            txtNewPassword.Size = new Size(300, 30);
            txtNewPassword.Font = new Font("Segoe UI", 10);
            txtNewPassword.BorderStyle = BorderStyle.FixedSingle;

            btnChangePassword.Location = new Point(50, 200);
            btnChangePassword.Size = new Size(150, 40);
            btnChangePassword.Text = "Změnit heslo";
            btnChangePassword.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnChangePassword.BackColor = Color.FromArgb(0, 120, 215);
            btnChangePassword.ForeColor = Color.White;
            btnChangePassword.FlatStyle = FlatStyle.Flat;
            btnChangePassword.FlatAppearance.BorderSize = 0;
            btnChangePassword.Cursor = Cursors.Hand;

            // Tlačítko pro odhlášení
            Button btnLogout = new Button();
            btnLogout.Location = new Point(400, 200);
            btnLogout.Size = new Size(150, 40);
            btnLogout.Text = "Odhlásit se";
            btnLogout.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnLogout.BackColor = Color.FromArgb(200, 80, 80);
            btnLogout.ForeColor = Color.White;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.Click += BtnLogout_Click;
            this.Controls.Add(btnLogout);
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }

        private void btnChangePassword_Click_1(object sender, EventArgs e)
        {
            string newPassword = txtNewPassword.Text;
            if (!string.IsNullOrEmpty(newPassword))
            {
                currentUser.PasswordHash = User.HashPassword(newPassword);
                UserManager.SaveUsers();
                MessageBox.Show("Heslo bylo změněno.", "Úspěch",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Zadejte nové heslo!", "Chyba",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}