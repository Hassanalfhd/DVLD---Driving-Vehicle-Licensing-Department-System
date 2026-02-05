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
using DVLDNewProject.Licenses.LocalLicenses;

namespace DVLDNewProject.Applications.Rlease_Detained_License
{
    public partial class frmReleaseDetainedLicenseApplication : Form
    {
        private int _SelectedLicenseID = -1;

        public frmReleaseDetainedLicenseApplication()
        {
            InitializeComponent();
        }

        public frmReleaseDetainedLicenseApplication(int LicenseID)
        {
            InitializeComponent();
            ctrlDriverLicenseInfoWithFliter1.LoadLicenseInfo(LicenseID);
            ctrlDriverLicenseInfoWithFliter1.FilterEnable = false;
        }
    
        private void ctrDriverLicenseInfoWithFliter1_OnLicenseSeleted(int obj)
        {
            _SelectedLicenseID = obj;

            llShowLicenseHistory.Enabled = (_SelectedLicenseID != -1);

            lblLicenseID.Text = _SelectedLicenseID.ToString();

            if (_SelectedLicenseID == -1)
            {
                return;
            }

            if (!ctrlDriverLicenseInfoWithFliter1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License  is not detained, choose another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRelease.Enabled = false;

                return;
            }

            lblDetainID.Text = ctrlDriverLicenseInfoWithFliter1.SelectedLicenseInfo.DetainedInfo.DetainID.ToString();
            lblDetainDate.Text = clsFormat.DateToShort(ctrlDriverLicenseInfoWithFliter1.SelectedLicenseInfo.DetainedInfo.DetainDate);
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            lblFineFees.Text = ctrlDriverLicenseInfoWithFliter1.SelectedLicenseInfo.DetainedInfo.FineFees.ToString();
            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).Fees.ToString();

            lblTotalFees.Text = (Convert.ToSingle(lblFineFees.Text) + lblApplicationFees.Text).ToString();

            btnRelease.Enabled = true;

        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure, you want to Release this detained License", "Confierm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                return;

            int ApplicationID = -1;

            bool isReleased = ctrlDriverLicenseInfoWithFliter1.SelectedLicenseInfo.ReleaseDetainedLicense(clsGlobal.CurrentUser.UserID, ref ApplicationID);

            lblApplicationID.Text = ApplicationID.ToString();

            if (!isReleased)
            {
                MessageBox.Show("Faild to to release the Detain License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Detained License released Successfully ", "Detained License Released", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnRelease.Enabled = false;
            llShowLicenseInfo.Enabled = true;
            ctrlDriverLicenseInfoWithFliter1.FilterEnable = false;
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm =
             new frmShowPersonLicenseHistory(ctrlDriverLicenseInfoWithFliter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_SelectedLicenseID);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmReleaseDetainedLicenseApplication_Activated(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfoWithFliter1.txtLicenseIDFocus();
        }
    }
}
