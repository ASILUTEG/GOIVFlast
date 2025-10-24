namespace GOIVF
{
    partial class ReportsForm
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
            this.lblTitle.Text = "Reports & Statistics";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Size = new System.Drawing.Size(400, 40);
            // ReportsForm
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.panelMain);
            this.Name = "ReportsForm";
            this.Text = "Reports & Statistics";
        }
    }
}