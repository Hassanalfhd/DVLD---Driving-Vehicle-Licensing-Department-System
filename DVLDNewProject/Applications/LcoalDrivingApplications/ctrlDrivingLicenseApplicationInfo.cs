using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Buisness;
using DVLDNewProject.Licenses.LocalLicenses;

namespace DVLDNewProject.Applications.LcoalDrivingApplications
{
    public partial class ctrlDrivingLicenseApplicationInfo : UserControl
    {


        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;

        private int _LocalDrivingLicenseApplicationID;
        private int _LicenseID;
        public int LocalDrivingLicenseApplicationID
        {
            get
            {
                return _LocalDrivingLicenseApplicationID;
            }
        }
            
        public ctrlDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }

        public void _ResteLocalDrivingLicenseApplicationInfo()
        {
            _LocalDrivingLicenseApplicationID = -1;
            lblAppliedFor.Text = "[???]";
            lblLocalDrivingLicenseApplicationID.Text = "[???]";
            ctrlApplicationBasicInfo1.ResetApplicationInfo();
        }

        private void _FillLocalDrivingLicenseApplicationInfo()
        {
            _LicenseID = _LocalDrivingLicenseApplication.GetActiveLicense();
          
            llShowLicenceInfo.Enabled = (_LicenseID != -1);
            
            
            ctrlApplicationBasicInfo1.LoadApplicationInfo(_LocalDrivingLicenseApplication.ApplicationID);
            _LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID;
            lblLocalDrivingLicenseApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblAppliedFor.Text = "Not Implmaention yet!";
            lblPassedTests.Text = "Not Implmaention yet!";
        }

        public void LoadLocalDrivingLicenseApplicationInfoByApplicationID(int ApplicationID)
        {

            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByApplicationID(ApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                _ResteLocalDrivingLicenseApplicationInfo();
                MessageBox.Show("No Local Driving License Application with Application ID = " + ApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
                _FillLocalDrivingLicenseApplicationInfo();    

        }

        public void LoadLocalDrivingLicenseApplicationInfoByLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID)
        {

            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                _ResteLocalDrivingLicenseApplicationInfo();
                MessageBox.Show("No Local Driving License Application with Local Application ID = " + LocalDrivingLicenseApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
                _FillLocalDrivingLicenseApplicationInfo();

        }

        private void llShowLicenceInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            Form frm = new frmShowLicenseInfo(_LocalDrivingLicenseApplication.GetActiveLicense());
             frm.ShowDialog();

        }

     


    }
}
