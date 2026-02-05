using DVLDNewProject.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Buisness;
using DVLDNewProject.Licenses;
using DVLDNewProject.Licenses.International_Licenses;


namespace DVLDNewProject.Applications.International_License
{
    public partial class frmNewInternationalLicenseApplication : Form
    {
        private int _InternationalLicenseID =-1;
        public frmNewInternationalLicenseApplication()
        {
            InitializeComponent();
        }

        private void frmNewInternationalLicenseApplication_Load(object sender, EventArgs e)
        {

            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(1));
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            lblFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.NewInternationalLicense).Fees.ToString();


        }

        private void ctrDriverLicenseInfoWithFliter1_OnLicenseSeleted(int obj)
        {
            int _SelectedLicenseID = obj;

            llShowLicenseHistory.Enabled = (_SelectedLicenseID != -1);

            lblLocalLicenseID.Text = _SelectedLicenseID.ToString();

            if (_SelectedLicenseID == -1)
                return;

            if (ctrDriverLicenseInfoWithFliter1.SelectedLicenseInfo.LicenseClass != 3)
            {
                MessageBox.Show("Selected License should be Class 3, select another one.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int ActiveInternaionalLicenseID = clsInternationalLicense.GetActiveInternationalLicenseIDByDriverID(ctrDriverLicenseInfoWithFliter1.SelectedLicenseInfo.DriverID);

            if (ActiveInternaionalLicenseID != -1)
            {
                MessageBox.Show("Person already have an active international license with ID = " + ActiveInternaionalLicenseID.ToString(), "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                _InternationalLicenseID = ActiveInternaionalLicenseID;
                llShowLicenseInfo.Enabled = true;
                btnIssueLicense.Enabled = false;
                return;
            }

            btnIssueLicense.Enabled = true;



        }

        private void frmNewInternationalLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrDriverLicenseInfoWithFliter1.txtLicenseIDFocus();
        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


            clsInternationalLicense InternationalLicense = new clsInternationalLicense();

            InternationalLicense.ApplicantPersonID = ctrDriverLicenseInfoWithFliter1.SelectedLicenseInfo.DriverInfo.PersonID;
            InternationalLicense.ApplicationDate = DateTime.Now;
            InternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);
            InternationalLicense.DriverID = ctrDriverLicenseInfoWithFliter1.SelectedLicenseInfo.DriverID;
            InternationalLicense.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            InternationalLicense.ApplicationTypeID = clsApplicationType.Find((int)clsApplication.enApplicationType.NewInternationalLicense).ID;
            InternationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            InternationalLicense.IsActive = true;
            InternationalLicense.IssueDate = DateTime.Now;
            InternationalLicense.IssuedUsingLocalLicenseID = ctrDriverLicenseInfoWithFliter1.LicenseID;
            InternationalLicense.LastStatusDate = DateTime.Now;
            InternationalLicense.PaidFees = clsApplicationType.Find(InternationalLicense.ApplicationTypeID).Fees;

            if (!InternationalLicense.Save())
            {
                MessageBox.Show("Faild to Issue International License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _InternationalLicenseID = InternationalLicense.InternationalLicenseID;
            lblApplicationID.Text = InternationalLicense.ApplicationID.ToString();
            lblInternationalLicenseID.Text = _InternationalLicenseID.ToString();

            MessageBox.Show("International License Issued Successfully with ID=" + InternationalLicense.InternationalLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnIssueLicense.Enabled = false;
            llShowLicenseInfo.Enabled = true;

        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm =
           new frmShowPersonLicenseHistory(ctrDriverLicenseInfoWithFliter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(_InternationalLicenseID);
            frm.ShowDialog();
        }


    }
}
