using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDNewProject.People;
using DVLDNewProject.Users;
using DVLDNewProject.Login;
using DVLDNewProject.Classes;
using DVLDNewProject.Applications.Application_Types;
using DVLDNewProject.Tests.Test_Types;
using DVLDNewProject.Applications.LcoalDrivingApplications;
using DVLDNewProject.Applications.Renew_Local_Licenses;
using DVLDNewProject.Applications.ReplaceLostOrDamagedLicense;
using DVLDNewProject.Drivers;
using DVLDNewProject.Licenses.Detaine_Licenses;
using DVLDNewProject.Applications.Rlease_Detained_License;
using DVLDNewProject.Applications.International_License;

namespace DVLDNewProject
{
    public partial class frmMain : Form
    {
        frmLogin _frmLogin;


        public frmMain(frmLogin frmLogin)
        {
            // TODO: Complete member initialization
            this._frmLogin = frmLogin;
            InitializeComponent();
        }


        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm1 = new People.frmListPeople();
            frm1.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmDriverList();
            frm.ShowDialog();
        }

        private void employeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm1 = new frmListUsers();
            frm1.ShowDialog();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm1 = new frmUserInfo(clsGlobal.CurrentUser.UserID) ;
            frm1.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm1 = new frmChangePassword(clsGlobal.CurrentUser.UserID);
            frm1.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;
            _frmLogin.Show();
            this.Close();

        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListApplicationTypes frm = new frmListApplicationTypes();
            frm.ShowDialog();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            clsGlobal.CurrentUser = null;
            _frmLogin.Show();
            this.Close();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListTestTypes frm = new frmListTestTypes();
            frm.ShowDialog();
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdateLocalDrinvingApplication();
            frm.ShowDialog();
        }

        private void manageLocalDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmListLocalDrivingLicesnseApplications();
            frm.ShowDialog();
        }

        private void retakeTestToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm = new frmListLocalDrivingLicesnseApplications();
            frm.ShowDialog();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmRenewLocalLicense();
            frm.ShowDialog();
        }

        private void ReplacementLostOrDamagedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmReplaceLostOrDamagedLicense();
            frm.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmDetainLicenseApplication();
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmReleaseDetainedLicenseApplication();
            frm.ShowDialog();

        }

        private void ManageDetainedLicensestoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm = new frmListDetainedLicenses();
            frm.ShowDialog();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();
        }

        private void ManageInternationaDrivingLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm = new frmListInternationalLicesnseApplications();
            frm.ShowDialog();
        }

        
    }
}
