namespace GOIVF
{
    partial class PatientForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblPatientID, lblRegDate, lblFirstName, lblMiddleName, lblLastName, lblDOB, lblAge, lblGender, lblNationalID, lblMaritalStatus, lblReligion, lblNationality;
        private System.Windows.Forms.Label lblAddress, lblCity, lblState, lblCountry, lblPhone, lblEmail, lblEmergencyContactName, lblEmergencyContactPhone;
        private System.Windows.Forms.TextBox txtPatientID, txtFirstName, txtMiddleName, txtLastName, txtAge, txtNationalID, txtReligion, txtNationality;
        private System.Windows.Forms.TextBox txtAddress, txtCity, txtState, txtCountry, txtPhone, txtEmail, txtEmergencyContactName, txtEmergencyContactPhone;
        private System.Windows.Forms.DateTimePicker dtpRegDate, dtpDOB;
        private System.Windows.Forms.ComboBox cmbGender, cmbMaritalStatus;
        private System.Windows.Forms.Button btnSave, btnUpdate, btnDelete, btnNew;

        private void InitializeComponent()
        {
            panelMain = new Panel();
            lblTitle = new Label();
            lblPatientID = new Label();
            txtPatientID = new TextBox();
            lblRegDate = new Label();
            dtpRegDate = new DateTimePicker();
            lblFirstName = new Label();
            txtFirstName = new TextBox();
            lblMiddleName = new Label();
            txtMiddleName = new TextBox();
            lblLastName = new Label();
            txtLastName = new TextBox();
            lblDOB = new Label();
            dtpDOB = new DateTimePicker();
            lblAge = new Label();
            txtAge = new TextBox();
            lblGender = new Label();
            cmbGender = new ComboBox();
            lblNationalID = new Label();
            txtNationalID = new TextBox();
            lblMaritalStatus = new Label();
            cmbMaritalStatus = new ComboBox();
            lblReligion = new Label();
            txtReligion = new TextBox();
            lblNationality = new Label();
            txtNationality = new TextBox();
            lblAddress = new Label();
            txtAddress = new TextBox();
            lblCity = new Label();
            txtCity = new TextBox();
            lblState = new Label();
            txtState = new TextBox();
            lblCountry = new Label();
            txtCountry = new TextBox();
            lblPhone = new Label();
            txtPhone = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblEmergencyContactName = new Label();
            txtEmergencyContactName = new TextBox();
            lblEmergencyContactPhone = new Label();
            txtEmergencyContactPhone = new TextBox();
            btnSave = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnNew = new Button();
            panelMain.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.AutoScroll = true;
            panelMain.BackColor = Color.WhiteSmoke;
            panelMain.Controls.Add(lblTitle);
            panelMain.Controls.Add(lblPatientID);
            panelMain.Controls.Add(txtPatientID);
            panelMain.Controls.Add(lblRegDate);
            panelMain.Controls.Add(dtpRegDate);
            panelMain.Controls.Add(lblFirstName);
            panelMain.Controls.Add(txtFirstName);
            panelMain.Controls.Add(lblMiddleName);
            panelMain.Controls.Add(txtMiddleName);
            panelMain.Controls.Add(lblLastName);
            panelMain.Controls.Add(txtLastName);
            panelMain.Controls.Add(lblDOB);
            panelMain.Controls.Add(dtpDOB);
            panelMain.Controls.Add(lblAge);
            panelMain.Controls.Add(txtAge);
            panelMain.Controls.Add(lblGender);
            panelMain.Controls.Add(cmbGender);
            panelMain.Controls.Add(lblNationalID);
            panelMain.Controls.Add(txtNationalID);
            panelMain.Controls.Add(lblMaritalStatus);
            panelMain.Controls.Add(cmbMaritalStatus);
            panelMain.Controls.Add(lblReligion);
            panelMain.Controls.Add(txtReligion);
            panelMain.Controls.Add(lblNationality);
            panelMain.Controls.Add(txtNationality);
            panelMain.Controls.Add(lblAddress);
            panelMain.Controls.Add(txtAddress);
            panelMain.Controls.Add(lblCity);
            panelMain.Controls.Add(txtCity);
            panelMain.Controls.Add(lblState);
            panelMain.Controls.Add(txtState);
            panelMain.Controls.Add(lblCountry);
            panelMain.Controls.Add(txtCountry);
            panelMain.Controls.Add(lblPhone);
            panelMain.Controls.Add(txtPhone);
            panelMain.Controls.Add(lblEmail);
            panelMain.Controls.Add(txtEmail);
            panelMain.Controls.Add(lblEmergencyContactName);
            panelMain.Controls.Add(txtEmergencyContactName);
            panelMain.Controls.Add(lblEmergencyContactPhone);
            panelMain.Controls.Add(txtEmergencyContactPhone);
            panelMain.Controls.Add(btnSave);
            panelMain.Controls.Add(btnUpdate);
            panelMain.Controls.Add(btnDelete);
            panelMain.Controls.Add(btnNew);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 0);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(600, 900);
            panelMain.TabIndex = 0;
            panelMain.Paint += panelMain_Paint;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(0, 120, 215);
            lblTitle.Location = new Point(20, 10);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(400, 40);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Patient Information";
            // 
            // lblPatientID
            // 
            lblPatientID.Location = new Point(30, 60);
            lblPatientID.Name = "lblPatientID";
            lblPatientID.Size = new Size(140, 28);
            lblPatientID.TabIndex = 1;
            lblPatientID.Text = "Patient ID";
            // 
            // txtPatientID
            // 
            txtPatientID.Location = new Point(180, 60);
            txtPatientID.Name = "txtPatientID";
            txtPatientID.ReadOnly = true;
            txtPatientID.Size = new Size(220, 23);
            txtPatientID.TabIndex = 2;
            // 
            // lblRegDate
            // 
            lblRegDate.Location = new Point(30, 98);
            lblRegDate.Name = "lblRegDate";
            lblRegDate.Size = new Size(140, 28);
            lblRegDate.TabIndex = 3;
            lblRegDate.Text = "Registration Date";
            // 
            // dtpRegDate
            // 
            dtpRegDate.Format = DateTimePickerFormat.Short;
            dtpRegDate.Location = new Point(180, 98);
            dtpRegDate.Name = "dtpRegDate";
            dtpRegDate.Size = new Size(220, 23);
            dtpRegDate.TabIndex = 4;
            // 
            // lblFirstName
            // 
            lblFirstName.Location = new Point(30, 136);
            lblFirstName.Name = "lblFirstName";
            lblFirstName.Size = new Size(140, 28);
            lblFirstName.TabIndex = 5;
            lblFirstName.Text = "First Name*";
            // 
            // txtFirstName
            // 
            txtFirstName.Location = new Point(180, 136);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(220, 23);
            txtFirstName.TabIndex = 6;
            // 
            // lblMiddleName
            // 
            lblMiddleName.Location = new Point(30, 174);
            lblMiddleName.Name = "lblMiddleName";
            lblMiddleName.Size = new Size(140, 28);
            lblMiddleName.TabIndex = 7;
            lblMiddleName.Text = "Middle Name";
            // 
            // txtMiddleName
            // 
            txtMiddleName.Location = new Point(180, 174);
            txtMiddleName.Name = "txtMiddleName";
            txtMiddleName.Size = new Size(220, 23);
            txtMiddleName.TabIndex = 8;
            // 
            // lblLastName
            // 
            lblLastName.Location = new Point(30, 212);
            lblLastName.Name = "lblLastName";
            lblLastName.Size = new Size(140, 28);
            lblLastName.TabIndex = 9;
            lblLastName.Text = "Last Name*";
            // 
            // txtLastName
            // 
            txtLastName.Location = new Point(180, 212);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(220, 23);
            txtLastName.TabIndex = 10;
            // 
            // lblDOB
            // 
            lblDOB.Location = new Point(30, 250);
            lblDOB.Name = "lblDOB";
            lblDOB.Size = new Size(140, 28);
            lblDOB.TabIndex = 11;
            lblDOB.Text = "Date of Birth*";
            // 
            // dtpDOB
            // 
            dtpDOB.Format = DateTimePickerFormat.Short;
            dtpDOB.Location = new Point(180, 250);
            dtpDOB.Name = "dtpDOB";
            dtpDOB.Size = new Size(220, 23);
            dtpDOB.TabIndex = 12;
            // 
            // lblAge
            // 
            lblAge.Location = new Point(30, 288);
            lblAge.Name = "lblAge";
            lblAge.Size = new Size(140, 28);
            lblAge.TabIndex = 13;
            lblAge.Text = "Age";
            // 
            // txtAge
            // 
            txtAge.Location = new Point(180, 288);
            txtAge.Name = "txtAge";
            txtAge.ReadOnly = true;
            txtAge.Size = new Size(220, 23);
            txtAge.TabIndex = 14;
            // 
            // lblGender
            // 
            lblGender.Location = new Point(30, 326);
            lblGender.Name = "lblGender";
            lblGender.Size = new Size(140, 28);
            lblGender.TabIndex = 15;
            lblGender.Text = "Gender";
            // 
            // cmbGender
            // 
            cmbGender.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGender.Items.AddRange(new object[] { "Male", "Female", "Other" });
            cmbGender.Location = new Point(180, 326);
            cmbGender.Name = "cmbGender";
            cmbGender.Size = new Size(220, 23);
            cmbGender.TabIndex = 16;
            // 
            // lblNationalID
            // 
            lblNationalID.Location = new Point(30, 364);
            lblNationalID.Name = "lblNationalID";
            lblNationalID.Size = new Size(140, 28);
            lblNationalID.TabIndex = 17;
            lblNationalID.Text = "National ID / Passport No.";
            // 
            // txtNationalID
            // 
            txtNationalID.Location = new Point(180, 364);
            txtNationalID.Name = "txtNationalID";
            txtNationalID.Size = new Size(220, 23);
            txtNationalID.TabIndex = 18;
            // 
            // lblMaritalStatus
            // 
            lblMaritalStatus.Location = new Point(30, 402);
            lblMaritalStatus.Name = "lblMaritalStatus";
            lblMaritalStatus.Size = new Size(140, 28);
            lblMaritalStatus.TabIndex = 19;
            lblMaritalStatus.Text = "Marital Status";
            // 
            // cmbMaritalStatus
            // 
            cmbMaritalStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMaritalStatus.Items.AddRange(new object[] { "Single", "Married", "Divorced", "Widowed" });
            cmbMaritalStatus.Location = new Point(180, 402);
            cmbMaritalStatus.Name = "cmbMaritalStatus";
            cmbMaritalStatus.Size = new Size(220, 23);
            cmbMaritalStatus.TabIndex = 20;
            // 
            // lblReligion
            // 
            lblReligion.Location = new Point(30, 440);
            lblReligion.Name = "lblReligion";
            lblReligion.Size = new Size(140, 28);
            lblReligion.TabIndex = 21;
            lblReligion.Text = "Religion";
            // 
            // txtReligion
            // 
            txtReligion.Location = new Point(180, 440);
            txtReligion.Name = "txtReligion";
            txtReligion.Size = new Size(220, 23);
            txtReligion.TabIndex = 22;
            // 
            // lblNationality
            // 
            lblNationality.Location = new Point(30, 478);
            lblNationality.Name = "lblNationality";
            lblNationality.Size = new Size(140, 28);
            lblNationality.TabIndex = 23;
            lblNationality.Text = "Nationality";
            // 
            // txtNationality
            // 
            txtNationality.Location = new Point(180, 478);
            txtNationality.Name = "txtNationality";
            txtNationality.Size = new Size(220, 23);
            txtNationality.TabIndex = 24;
            // 
            // lblAddress
            // 
            lblAddress.Location = new Point(30, 516);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(140, 28);
            lblAddress.TabIndex = 25;
            lblAddress.Text = "Address";
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(180, 516);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(220, 23);
            txtAddress.TabIndex = 26;
            // 
            // lblCity
            // 
            lblCity.Location = new Point(30, 554);
            lblCity.Name = "lblCity";
            lblCity.Size = new Size(140, 28);
            lblCity.TabIndex = 27;
            lblCity.Text = "City";
            // 
            // txtCity
            // 
            txtCity.Location = new Point(180, 554);
            txtCity.Name = "txtCity";
            txtCity.Size = new Size(220, 23);
            txtCity.TabIndex = 28;
            // 
            // lblState
            // 
            lblState.Location = new Point(30, 592);
            lblState.Name = "lblState";
            lblState.Size = new Size(140, 28);
            lblState.TabIndex = 29;
            lblState.Text = "State";
            // 
            // txtState
            // 
            txtState.Location = new Point(180, 592);
            txtState.Name = "txtState";
            txtState.Size = new Size(220, 23);
            txtState.TabIndex = 30;
            // 
            // lblCountry
            // 
            lblCountry.Location = new Point(30, 630);
            lblCountry.Name = "lblCountry";
            lblCountry.Size = new Size(140, 28);
            lblCountry.TabIndex = 31;
            lblCountry.Text = "Country";
            // 
            // txtCountry
            // 
            txtCountry.Location = new Point(180, 630);
            txtCountry.Name = "txtCountry";
            txtCountry.Size = new Size(220, 23);
            txtCountry.TabIndex = 32;
            // 
            // lblPhone
            // 
            lblPhone.Location = new Point(30, 668);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(140, 28);
            lblPhone.TabIndex = 33;
            lblPhone.Text = "Phone";
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(180, 668);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(220, 23);
            txtPhone.TabIndex = 34;
            // 
            // lblEmail
            // 
            lblEmail.Location = new Point(30, 706);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(140, 28);
            lblEmail.TabIndex = 35;
            lblEmail.Text = "Email";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(180, 706);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(220, 23);
            txtEmail.TabIndex = 36;
            // 
            // lblEmergencyContactName
            // 
            lblEmergencyContactName.Location = new Point(30, 744);
            lblEmergencyContactName.Name = "lblEmergencyContactName";
            lblEmergencyContactName.Size = new Size(140, 28);
            lblEmergencyContactName.TabIndex = 37;
            lblEmergencyContactName.Text = "Emergency Contact Name";
            // 
            // txtEmergencyContactName
            // 
            txtEmergencyContactName.Location = new Point(180, 744);
            txtEmergencyContactName.Name = "txtEmergencyContactName";
            txtEmergencyContactName.Size = new Size(220, 23);
            txtEmergencyContactName.TabIndex = 38;
            // 
            // lblEmergencyContactPhone
            // 
            lblEmergencyContactPhone.Location = new Point(30, 782);
            lblEmergencyContactPhone.Name = "lblEmergencyContactPhone";
            lblEmergencyContactPhone.Size = new Size(140, 28);
            lblEmergencyContactPhone.TabIndex = 39;
            lblEmergencyContactPhone.Text = "Emergency Contact Phone";
            // 
            // txtEmergencyContactPhone
            // 
            txtEmergencyContactPhone.Location = new Point(180, 782);
            txtEmergencyContactPhone.Name = "txtEmergencyContactPhone";
            txtEmergencyContactPhone.Size = new Size(220, 23);
            txtEmergencyContactPhone.TabIndex = 40;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(0, 120, 215);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(180, 830);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(90, 36);
            btnSave.TabIndex = 41;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.FromArgb(0, 120, 215);
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.ForeColor = Color.White;
            btnUpdate.Location = new Point(280, 830);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(90, 36);
            btnUpdate.TabIndex = 42;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(220, 53, 69);
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(380, 830);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(90, 36);
            btnDelete.TabIndex = 43;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnNew
            // 
            btnNew.BackColor = Color.FromArgb(40, 167, 69);
            btnNew.FlatStyle = FlatStyle.Flat;
            btnNew.ForeColor = Color.White;
            btnNew.Location = new Point(480, 830);
            btnNew.Name = "btnNew";
            btnNew.Size = new Size(90, 36);
            btnNew.TabIndex = 44;
            btnNew.Text = "New";
            btnNew.UseVisualStyleBackColor = false;
            // 
            // PatientForm
            // 
            ClientSize = new Size(600, 900);
            Controls.Add(panelMain);
            Name = "PatientForm";
            Text = "Patient Management";
            panelMain.ResumeLayout(false);
            panelMain.PerformLayout();
            ResumeLayout(false);
        }
    }
}