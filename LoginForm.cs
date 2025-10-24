using System;
using System.Drawing;
using System.Windows.Forms;

namespace GOIVF
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.Padding = new Padding(20);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // TODO: Add authentication logic here
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter username and password.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Example: Validate against database
            // bool isValid = ...
            // if (isValid)
            // {
            //     this.Hide();
            //     var mainForm = new MainDashboardForm();
            //     mainForm.ShowDialog();
            //     this.Close();
            // }
            // else
            // {
            //     MessageBox.Show("Invalid credentials.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
            // }

            // For demonstration, always show main form after login:
            this.Hide();
            var mainForm = new MainDashboardForm();
            mainForm.ShowDialog();
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
