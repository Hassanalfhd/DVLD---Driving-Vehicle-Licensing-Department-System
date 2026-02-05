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

namespace DVLDNewProject.Applications.LcoalDrivingApplications
{
    public partial class frmLocalDrivingLicenseApplicationInfo : Form
    {
        private int _ApplicationID = -1;
        public frmLocalDrivingLicenseApplicationInfo(int ApplicationID )
        {
            InitializeComponent();
            _ApplicationID = ApplicationID;
        }

        private void frmLocalDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {
            ctrlDrivingLicenseApplicationInfo1.LoadLocalDrivingLicenseApplicationInfoByLocalDrivingLicenseApplicationID(_ApplicationID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
