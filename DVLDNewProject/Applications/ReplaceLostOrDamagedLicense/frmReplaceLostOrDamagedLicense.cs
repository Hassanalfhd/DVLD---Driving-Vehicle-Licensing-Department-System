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
using DVLDNewProject.Classes;
using DVLDNewProject.Licenses.LocalLicenses;
using DVLDNewProject.Licenses;

namespace DVLDNewProject.Applications.ReplaceLostOrDamagedLicense
{
    public partial class frmReplaceLostOrDamagedLicense : Form
    {
        int _NewLicenseID = -1;
        public frmReplaceLostOrDamagedLicense()
        {
            InitializeComponent();
        }

        private void frmReplaceLostOrDamagedLicense_Load(object sender, EventArgs e)
        {

            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            rbDamagedLicense.Checked = true;
        }

        private clsLicense.enIssueReason _GetIssueReason()
        {
            if (rbDamagedLicense.Checked)
                return clsLicense.enIssueReason.DamagedReplacement;
            else
                return clsLicense.enIssueReason.LostReplacement;
        }
        private int _GetApplicationType()
        {
            if (rbDamagedLicense.Checked)
                return (int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense;
            else
                return (int)clsApplication.enApplicationType.ReplaceLostDrivingLicense;

        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            this.Text = "Replacement for Damaged License";
            lblTitle.Text = "Replacement for Damaged License";
            lblApplicationFees.Text = clsApplicationType.Find(_GetApplicationType()).Fees.ToString();
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            this.Text = "Replacement for Lost License";
            lblTitle.Text = "Replacement for Lost License";
            lblApplicationFees.Text = clsApplicationType.Find(_GetApplicationType()).Fees.ToString();
        }

        private void ctrDriverLicenseInfoWithFliter1_OnLicenseSeleted(int obj)
        {

            int SelectedLicenseID = obj;

            lblOldLicenseID.Text = SelectedLicenseID.ToString();
            llShowLicenseHistory.Enabled = (SelectedLicenseID != -1);

            if (SelectedLicenseID == -1)
                return;


            //dont allow a replacement if is Active .
            if (!ctrDriverLicenseInfoWithFliter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License is not Active, choose an active license.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssueReplacement.Enabled = false;
                return;
            }

            btnIssueReplacement.Enabled = true;

        }

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Issue a Replacement for the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            clsLicense NewLicense = ctrDriverLicenseInfoWithFliter1.SelectedLicenseInfo.Replace(_GetIssueReason(), clsGlobal.CurrentUser.UserID);

            if (NewLicense == null)
            {
                MessageBox.Show("Faild to Issue a replacemnet for this  License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                return;
            }

            _NewLicenseID = NewLicense.LicenseID;
            lblRreplacedLicenseID.Text = _NewLicenseID.ToString();

            lblApplicationID.Text = NewLicense.ApplicationID.ToString();
            MessageBox.Show("Licensed Replaced Successfully with ID=" + _NewLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
            llShowLicenseInfo.Enabled = true;
            btnIssueReplacement.Enabled = false;
            ctrDriverLicenseInfoWithFliter1.FilterEnable = false;
            gbReplacementFor.Enabled = false;


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

        private void frmReplaceLostOrDamagedLicense_Activated(object sender, EventArgs e)
        {
            ctrDriverLicenseInfoWithFliter1.txtLicenseIDFocus();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ctrDriverLicenseInfoWithFliter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }



    }
}
