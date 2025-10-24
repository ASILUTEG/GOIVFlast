namespace GOIVF
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.PictureBox picLogo;

        private void InitializeComponent()
        {
            this.panelMain = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.picLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.White;
            this.panelMain.Controls.Add(this.lblTitle);
            this.panelMain.Controls.Add(this.txtUsername);
            this.panelMain.Controls.Add(this.txtPassword);
            this.panelMain.Controls.Add(this.lblUsername);
            this.panelMain.Controls.Add(this.lblPassword);
            this.panelMain.Controls.Add(this.btnLogin);
            this.panelMain.Controls.Add(this.btnClose);
            this.panelMain.Controls.Add(this.picLogo);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(350, 420);
            this.panelMain.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.lblTitle.Location = new System.Drawing.Point(0, 120);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(350, 40);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Sign In";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picLogo
            // 
            this.picLogo.Location = new System.Drawing.Point(125, 30);
            this.picLogo.Size = new System.Drawing.Size(100, 80);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.Image = null; // Set your logo image here
            // 
            // lblUsername
            // 
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblUsername.Location = new System.Drawing.Point(40, 180);
            this.lblUsername.Size = new System.Drawing.Size(80, 25);
            this.lblUsername.Text = "Username";
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUsername.Location = new System.Drawing.Point(40, 210);
            this.txtUsername.Size = new System.Drawing.Size(270, 30);
            // 
            // lblPassword
            // 
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPassword.Location = new System.Drawing.Point(40, 250);
            this.lblPassword.Size = new System.Drawing.Size(80, 25);
            this.lblPassword.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPassword.Location = new System.Drawing.Point(40, 280);
            this.txtPassword.Size = new System.Drawing.Size(270, 30);
            this.txtPassword.PasswordChar = '?';
            // 
            // btnLogin
            // 
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.btnLogin.ForeColor = Color.White;
            this.btnLogin.Location = new System.Drawing.Point(40, 330);
            this.btnLogin.Size = new System.Drawing.Size(270, 40);
            this.btnLogin.Text = "Login";
            this.btnLogin.FlatStyle = FlatStyle.Flat;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClose.BackColor = Color.Transparent;
            this.btnClose.ForeColor = Color.Gray;
            this.btnClose.Location = new System.Drawing.Point(310, 10);
            this.btnClose.Size = new System.Drawing.Size(30, 30);
            this.btnClose.Text = "X";
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 420);
            this.Controls.Add(this.panelMain);
            this.Name = "LoginForm";
            this.Text = "Login";
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);
        }

        private void ApplyModernStyle()
        {
            // Rounded corners for panel
            this.panelMain.BackColor = Color.WhiteSmoke;
            this.panelMain.Region = System.Drawing.Region.FromHrgn(
                NativeMethods.CreateRoundRectRgn(0, 0, panelMain.Width, panelMain.Height, 20, 20));

            // Flat style for login button
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.BackColor = Color.FromArgb(0, 120, 215);
            btnLogin.ForeColor = Color.White;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnLogin.Cursor = Cursors.Hand;

            // Flat style for close button
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.BackColor = Color.Transparent;
            btnClose.ForeColor = Color.Gray;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Cursor = Cursors.Hand;

            // TextBoxes styling
            txtUsername.BorderStyle = BorderStyle.None;
            txtUsername.BackColor = Color.WhiteSmoke;
            txtUsername.Font = new Font("Segoe UI", 10F);
            txtUsername.Padding = new Padding(8, 6, 8, 6);

            txtPassword.BorderStyle = BorderStyle.None;
            txtPassword.BackColor = Color.WhiteSmoke;
            txtPassword.Font = new Font("Segoe UI", 10F);
            txtPassword.Padding = new Padding(8, 6, 8, 6);

            // Title styling
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(0, 120, 215);
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            // Optional: add shadow effect (requires custom paint)
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            ApplyModernStyle();
        }
    }
}
