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

namespace DVLDNewProject.Licenses.LocalLicenses
{
    public partial class frmIssueDriverLicenseFirstTime : Form
    {
        private int _LocalDrivingLicenseApplicationID = -1;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;

        public frmIssueDriverLicenseFirstTime(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();

            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
        }

        private void frmIssueDriverLicenseFirstTime_Load(object sender, EventArgs e)
        {
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Error: No Application With ID = " + _LocalDrivingLicenseApplicationID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            if (!_LocalDrivingLicenseApplication.PassedAllTests())
            {
                MessageBox.Show("Error: Person shoud Pass in All Tests Frist. ", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;            
            }

            int LicenseID = _LocalDrivingLicenseApplication.GetActiveLicense();
            if (LicenseID != -1)
            {
                MessageBox.Show("Error: Person already has License before with ID = " + LicenseID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ctrlDrivingLicenseApplicationInfo1.LoadLocalDrivingLicenseApplicationInfoByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);

        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            int LicenseID = _LocalDrivingLicenseApplication.IssueLicenseForTheFristTime(txtNotes.Text.Trim(), clsGlobal.CurrentUser.UserID);

            if (LicenseID != -1)
            {
                MessageBox.Show("License Issued Successfully with License ID = " + LicenseID.ToString(), "Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();

            }
            else
                MessageBox.Show("License Was not Issued ! ", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Information);
            

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }




    }
}
