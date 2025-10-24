using System;
using System.Windows.Forms;

namespace GOIVF
{
    public partial class MainDashboardForm : Form
    {
        public MainDashboardForm()
        {
          
            InitializeComponent();
            btnPatients.Click += (s, e) => LoadModule(new PatientControl());
            btnCycles.Click += (s, e) => LoadModule(new CycleControl());
            btnEmbryology.Click += (s, e) => LoadModule(new EmbryologyControl());
            btnTransfer.Click += (s, e) => LoadModule(new EmbryoTransferControl());
            btnAndrology.Click += (s, e) => LoadModule(new AndrologyControl());
            btnReports.Click += (s, e) => LoadModule(new ReportsControl());
            btnImages.Click += (s, e) => LoadModule(new ImagesControl());
            btnUsers.Click += (s, e) => LoadModule(new UserManagementControl());
            btnBackup.Click += (s, e) => LoadModule(new BackupRestoreControl());
            btnActivation.Click += (s, e) => LoadModule(new ActivationControl());
            btnLogout.Click += (s, e) => this.Close();

        }

        private void LoadModule(UserControl control)
        {
            panelContent.Controls.Clear();
            control.Dock = DockStyle.Fill;
            panelContent.Controls.Add(control);
        }

       
    }
}