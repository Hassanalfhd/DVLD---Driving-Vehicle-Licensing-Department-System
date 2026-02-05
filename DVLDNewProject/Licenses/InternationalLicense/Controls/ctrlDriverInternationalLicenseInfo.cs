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
using DVLDNewProject.Classes;
using DVLDNewProject.Properties;

namespace DVLDNewProject.Licenses.InternationalLicense.Controls
{
    public partial class ctrlDriverInternationalLicenseInfo : UserControl
    {
        private clsInternationalLicense _InternationalLicense;
        private int _InternationalLicenseID;
        
        public ctrlDriverInternationalLicenseInfo()
        {
            InitializeComponent();
        }

        public int InternationlLicenseID
        {
            get
            {
                return _InternationalLicenseID;
            }
        }

        public clsInternationalLicense SelectedInternationalLicense
        {
            get
            {
                return _InternationalLicense;
            }
        }

        private void _LoadPersonImage()
        {
            if (_InternationalLicense.DriverInfo.PersonInfo.Gendor == 0)
            {
                pbGendor.Image = Resources.Man_32;
                pbPersonImage.Image = Resources.Male_512;
            }
            
        }
        public void LoadInfo(int InternationalLicenseID)
        {
            _InternationalLicenseID = InternationalLicenseID;

            _InternationalLicense = clsInternationalLicense.Find(InternationalLicenseID);

            if (_InternationalLicense == null)
            {
                MessageBox.Show("Could not find Internationa License ID = " + _InternationalLicenseID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _InternationalLicenseID = -1;
                return;
            }

            lblFullName.Text = _InternationalLicense.DriverInfo.PersonInfo.FullName;
            lblInternationalLicenseID.Text = _InternationalLicense.InternationalLicenseID.ToString();
            lblLocalLicenseID.Text = _InternationalLicense.IssuedUsingLocalLicenseID.ToString();
            lblNationalNo.Text = _InternationalLicense.DriverInfo.PersonInfo.NationalNo;

            lblGendor.Text = (_InternationalLicense.DriverInfo.PersonInfo.Gendor == 0 ? "Male" : "Female");
            
            lblIssueDate.Text = clsFormat.DateToShort(_InternationalLicense.IssueDate);
            lblExpirationDate.Text = clsFormat.DateToShort(_InternationalLicense.ExpirationDate);
            lblDateOfBirth.Text = clsFormat.DateToShort(_InternationalLicense.DriverInfo.PersonInfo.DateOfBirth);

            lblApplicationID.Text = _InternationalLicense.ApplicationID.ToString();

            lblIsActive.Text = _InternationalLicense.IsActive? "Yes" : "NO";
            lblDriverID.Text = _InternationalLicense.DriverID.ToString();


            
        }
    }
}
