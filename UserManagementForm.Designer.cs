namespace GOIVF
{
    partial class UserManagementForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelMain;
        private Label lblTitle;
        private void InitializeComponent()
        {
            this.panelMain = new Panel();
            this.lblTitle = new Label();
            // panelMain
            this.panelMain.Dock = DockStyle.Fill;
            this.panelMain.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelMain.Controls.Add(this.lblTitle);
            // lblTitle
            this.lblTitle.Text = "User Management";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Size = new System.Drawing.Size(400, 40);
            // UserManagementForm
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.panelMain);
            this.Name = "UserManagementForm";
            this.Text = "User Management";
        }
    }
}