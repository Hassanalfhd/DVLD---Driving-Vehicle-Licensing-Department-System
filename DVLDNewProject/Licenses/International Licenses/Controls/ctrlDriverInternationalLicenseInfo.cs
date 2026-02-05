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
using DVLDNewProject.Properties;
using DVLDNewProject.Classes;
using System.IO;

namespace DVLDNewProject.Licenses.International_Licenses.Controls
{
    public partial class ctrlDriverInternationalLicenseInfo : UserControl
    {

        private clsInternationalLicense _InternationalLicense;
        private int _InternationalLicenseID = -1;

        public int InternationalLicenseID
        {
            get
            {
                return _InternationalLicenseID;
            }
        }



        public ctrlDriverInternationalLicenseInfo()
        {
            InitializeComponent();
        }

        private void _LoadPersonImage()
        {
            if (_InternationalLicense.DriverInfo.PersonInfo.Gendor == 0)
            {
                pbGendor.Image = Resources.Man_32;
                pbPersonImage.Image = Resources.Male_512;
                
            }
            else
            {
                pbGendor.Image = Resources.Woman_32;
                pbPersonImage.Image = Resources.Female_512;
            }

            string ImagePath = _InternationalLicense.DriverInfo.PersonInfo.ImagePath;

            if ( ImagePath != "")
            {
                if (File.Exists(ImagePath))
                    pbPersonImage.Load(ImagePath);
            }
            else
                MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);



        }

        public void LoadInfo(int InternationalLicenseID)
        {
            _InternationalLicenseID = InternationalLicenseID;

            _InternationalLicense = clsInternationalLicense.Find(_InternationalLicenseID);

          
            if (_InternationalLicense == null)
            {
                MessageBox.Show("Could not find Internationa License ID = " + _InternationalLicenseID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _InternationalLicenseID = -1;
                return;
            }

            lblInternationalLicenseID.Text = _InternationalLicense.InternationalLicenseID.ToString();
            lblApplicationID.Text = _InternationalLicense.ApplicationID.ToString();
            lblIsActive.Text = _InternationalLicense.IsActive ? "Yes" : "No";
            lblLocalLicenseID.Text = _InternationalLicense.IssuedUsingLocalLicenseID.ToString();
            lblFullName.Text = _InternationalLicense.DriverInfo.PersonInfo.FullName;
            lblNationalNo.Text = _InternationalLicense.DriverInfo.PersonInfo.NationalNo;
            lblGendor.Text = _InternationalLicense.DriverInfo.PersonInfo.Gendor == 0 ? "Male" : "Female";
            lblDateOfBirth.Text = clsFormat.DateToShort(_InternationalLicense.DriverInfo.PersonInfo.DateOfBirth);

            lblDriverID.Text = _InternationalLicense.DriverID.ToString();
            lblIssueDate.Text = clsFormat.DateToShort(_InternationalLicense.IssueDate);
            lblExpirationDate.Text = clsFormat.DateToShort(_InternationalLicense.ExpirationDate);

            _LoadPersonImage();


        }

    }
}
