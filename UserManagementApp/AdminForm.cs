using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace UserManagementApp
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
            ApplyStyles();
            LoadUsers();
        }

        private void ApplyStyles()
        {
            // Základní nastavení formuláře
            this.Text = "Administrátorský panel";
            this.ClientSize = new Size(600, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 10);

            // Panel záhlaví
            Panel headerPanel = new Panel();
            headerPanel.Dock = DockStyle.Top;
            headerPanel.BackColor = Color.FromArgb(0, 120, 215);
            headerPanel.Height = 60;

            Label titleLabel = new Label();
            titleLabel.Text = "Správa uživatelů";
            titleLabel.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            titleLabel.ForeColor = Color.White;
            titleLabel.Dock = DockStyle.Fill;
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;

            headerPanel.Controls.Add(titleLabel);
            this.Controls.Add(headerPanel);
            headerPanel.BringToFront();

            // Nastavení ovládacích prvků
            lstUsers.Location = new Point(30, 100);
            lstUsers.Size = new Size(300, 200);
            lstUsers.Font = new Font("Segoe UI", 10);
            lstUsers.BorderStyle = BorderStyle.FixedSingle;

            // Popisek nad seznamem uživatelů
            Label lblUserList = new Label();
            lblUserList.Text = "Seznam uživatelů:";
            lblUserList.Location = new Point(30, 80);
            lblUserList.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblUserList.AutoSize = true;
            this.Controls.Add(lblUserList);

            btnResetPassword.Location = new Point(350, 100);
            btnResetPassword.Size = new Size(200, 40);
            btnResetPassword.Text = "Resetovat heslo";
            btnResetPassword.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnResetPassword.BackColor = Color.FromArgb(0, 120, 215);
            btnResetPassword.ForeColor = Color.White;
            btnResetPassword.FlatStyle = FlatStyle.Flat;
            btnResetPassword.FlatAppearance.BorderSize = 0;
            btnResetPassword.Cursor = Cursors.Hand;

            // Nové tlačítko Zpět
            Button btnBack = new Button();
            btnBack.Location = new Point(350, 150);
            btnBack.Size = new Size(200, 40);
            btnBack.Text = "Odhlásit se";
            btnBack.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnBack.BackColor = Color.FromArgb(200, 80, 80);
            btnBack.ForeColor = Color.White;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Cursor = Cursors.Hand;
            btnBack.Click += BtnBack_Click;
            this.Controls.Add(btnBack);
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            // Vrátit se na přihlašovací formulář
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
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
                    user.PasswordHash = User.HashPassword("default123");
                    UserManager.SaveUsers();
                    MessageBox.Show($"Heslo pro {user.Username} bylo resetováno na 'default123'.",
                        "Úspěch", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Vyberte uživatele!", "Chyba",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}