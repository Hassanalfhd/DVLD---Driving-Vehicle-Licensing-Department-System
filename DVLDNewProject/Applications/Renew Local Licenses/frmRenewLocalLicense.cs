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
using DVLDNewProject.Licenses.LocalLicenses;
using DVLDNewProject.Licenses;

namespace DVLDNewProject.Applications.Renew_Local_Licenses
{
    public partial class frmRenewLocalLicense : Form
    {
        private int _NewLicenseID;
        public frmRenewLocalLicense()
        {
            InitializeComponent();
        }

        private void frmRenewLocalLicense_Load(object sender, EventArgs e)
        {
            ctrDriverLicenseInfoWithFliter1.txtLicenseIDFocus();

            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblExpirationDate.Text = "???";
            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).Fees.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
        }

        private void ctrDriverLicenseInfoWithFliter1_OnLicenseSeleted(int obj)
        {
            int SelectedLicenseID = obj;
            lblOldLicenseID.Text = SelectedLicenseID.ToString();
            llShowLicenseHistory.Enabled = (SelectedLicenseID != -1);

            if (SelectedLicenseID == -1)
            {
                return;
            }

            int DefaultValidityLength = ctrDriverLicenseInfoWithFliter1.SelectedLicenseInfo.LicenseClassIfo.DefaultValidityLength;
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now.AddYears(DefaultValidityLength));
            lblLicenseFees.Text = ctrDriverLicenseInfoWithFliter1.SelectedLicenseInfo.LicenseClassIfo.ClassFees.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblLicenseFees.Text) + Convert.ToSingle(lblApplicationFees.Text)).ToString();
            txtNotes.Text = ctrDriverLicenseInfoWithFliter1.SelectedLicenseInfo.Notes;

            if (!ctrDriverLicenseInfoWithFliter1.SelectedLicenseInfo.IsLicenseExpired())
            {
                MessageBox.Show("Selected License is not yet expiared, it will expire on:"+ctrDriverLicenseInfoWithFliter1.SelectedLicenseInfo.ExpirationDate ,"Not Allowed",MessageBoxButtons.OK,MessageBoxIcon.Error);
                btnRenewLicense.Enabled = false;
                return;
            }

            if (!ctrDriverLicenseInfoWithFliter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License is not Not Active, choose an active license", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenewLicense.Enabled = false;
                return;            
            }

            btnRenewLicense.Enabled = true;
        }

        private void btnRenewLicense_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Renew the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            clsLicense NewLicense = ctrDriverLicenseInfoWithFliter1.SelectedLicenseInfo.RenewLicense(txtNotes.Text.Trim(), clsGlobal.CurrentUser.UserID);

            if (NewLicense == null)
            {
                MessageBox.Show("Faild to Renew the License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblApplicationID.Text = NewLicense.ApplicationID.ToString();
            lblRenewedLicenseID.Text = NewLicense.LicenseID.ToString();
            _NewLicenseID = NewLicense.LicenseID;
            MessageBox.Show("Licensed Renewed Successfully with ID=" + _NewLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnRenewLicense.Enabled = false;
            ctrDriverLicenseInfoWithFliter1.FilterEnable = false;
            llShowLicenseInfo.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_NewLicenseID);
            frm.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ctrDriverLicenseInfoWithFliter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }


    }
}
