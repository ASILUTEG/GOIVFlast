using System;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Windows.Forms;

namespace GOIVF
{
    public partial class PatientForm : Form
    {
        private Button btnLangSwitch;
        private ResourceManager resMan;
        private CultureInfo currentCulture;

        public PatientForm()
        {
            InitializeComponent();
            resMan = new ResourceManager("GOIVF.PatientForm", typeof(PatientForm).Assembly);
            currentCulture = Thread.CurrentThread.CurrentUICulture;
            AddLanguageSwitchButton();
            ApplyLocalization();
            dtpRegDate.Value = DateTime.Today;
            dtpDOB.ValueChanged += (s, e) => txtAge.Text = CalculateAge(dtpDOB.Value).ToString();
            btnSave.Click += BtnSave_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;
            btnNew.Click += BtnNew_Click;
        }

        private void AddLanguageSwitchButton()
        {
            btnLangSwitch = new Button();
            btnLangSwitch.Size = new System.Drawing.Size(60, 32);
            btnLangSwitch.Location = new System.Drawing.Point(this.ClientSize.Width - 80, 10);
            btnLangSwitch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLangSwitch.Click += BtnLangSwitch_Click;
            this.Controls.Add(btnLangSwitch);
        }

        private void BtnLangSwitch_Click(object sender, EventArgs e)
        {
            if (currentCulture.TwoLetterISOLanguageName == "ar")
                SetLanguage("en");
            else
                SetLanguage("ar");
        }

        private void SetLanguage(string lang)
        {
            currentCulture = new CultureInfo(lang);
            Thread.CurrentThread.CurrentUICulture = currentCulture;
            ApplyLocalization();
        }

        private void ApplyLocalization()
        {
            lblTitle.Text = resMan.GetString("FormTitle", currentCulture);
            lblPatientID.Text = resMan.GetString("PatientID", currentCulture);
            lblRegDate.Text = resMan.GetString("RegistrationDate", currentCulture);
            lblFirstName.Text = resMan.GetString("FirstName", currentCulture);
            lblMiddleName.Text = resMan.GetString("MiddleName", currentCulture);
            lblLastName.Text = resMan.GetString("LastName", currentCulture);
            lblDOB.Text = resMan.GetString("DateOfBirth", currentCulture);
            lblAge.Text = resMan.GetString("Age", currentCulture);
            lblGender.Text = resMan.GetString("Gender", currentCulture);
            lblNationalID.Text = resMan.GetString("NationalID", currentCulture);
            lblMaritalStatus.Text = resMan.GetString("MaritalStatus", currentCulture);
            lblReligion.Text = resMan.GetString("Religion", currentCulture);
            lblNationality.Text = resMan.GetString("Nationality", currentCulture);
            lblAddress.Text = resMan.GetString("Address", currentCulture);
            lblCity.Text = resMan.GetString("City", currentCulture);
            lblState.Text = resMan.GetString("State", currentCulture);
            lblCountry.Text = resMan.GetString("Country", currentCulture);
            lblPhone.Text = resMan.GetString("Phone", currentCulture);
            lblEmail.Text = resMan.GetString("Email", currentCulture);
            lblEmergencyContactName.Text = resMan.GetString("EmergencyContactName", currentCulture);
            lblEmergencyContactPhone.Text = resMan.GetString("EmergencyContactPhone", currentCulture);
            btnSave.Text = resMan.GetString("Save", currentCulture);
            btnUpdate.Text = resMan.GetString("Update", currentCulture);
            btnDelete.Text = resMan.GetString("Delete", currentCulture);
            btnNew.Text = resMan.GetString("New", currentCulture);
            btnLangSwitch.Text = resMan.GetString("LangSwitch", currentCulture);

            if (currentCulture.TwoLetterISOLanguageName == "ar")
            {
                this.RightToLeft = RightToLeft.Yes;
                this.RightToLeftLayout = true;
            }
            else
            {
                this.RightToLeft = RightToLeft.No;
                this.RightToLeftLayout = false;
            }
        }

        private int CalculateAge(DateTime dob)
        {
            var today = DateTime.Today;
            int age = today.Year - dob.Year;
            if (dob > today.AddYears(-age)) age--;
            return age;
        }

        private void BtnSave_Click(object sender, EventArgs e) { /* ... */ }
        private void BtnUpdate_Click(object sender, EventArgs e) { /* ... */ }
        private void BtnDelete_Click(object sender, EventArgs e) { /* ... */ }
        private void BtnNew_Click(object sender, EventArgs e) { /* ... */ }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}