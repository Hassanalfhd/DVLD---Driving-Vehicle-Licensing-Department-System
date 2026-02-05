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

namespace DVLDNewProject.Licenses.LocalLicenses.Controls
{
    public partial class ctrDriverLicenseInfo : UserControl
    {
        private int _LicneseID = -1;

        private clsLicense _Licnese;

        public int LicenseID
        {
            get
            {
                return _LicneseID;
            }
        }
        public clsLicense SelectedLicenseInfo
        {
            get
            {
                return _Licnese;
            }
        }
        

        public ctrDriverLicenseInfo()
        {
            InitializeComponent();
        }


        private void _LoadPersonImage()
        {

            if (_Licnese.DriverInfo.PersonInfo.Gendor == 0)
            {
                pbPersonImage.Image = Resources.Male_512;
                pbGendor.Image = Resources.Man_32;
            }
            else
            {
                pbPersonImage.Image = Resources.Female_512;
                pbGendor.Image = Resources.Woman_32;
            }

            string ImagePath = _Licnese.DriverInfo.PersonInfo.ImagePath;

            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPersonImage.Load(ImagePath);
                else
                    MessageBox.Show("Could not find this image: = "+ ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


        
        }

        public void LoadLicenseInfo(int LicenseID)
        {
            _LicneseID = LicenseID;
            _Licnese = clsLicense.Find(_LicneseID);

            if (_Licnese == null)
            {
                MessageBox.Show("Could not find License with ID = "+ LicenseID.ToString(),"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                _LicneseID = -1;
                return;
            }

            lblClass.Text = _Licnese.LicenseClassIfo.ClassName;
            lblFullName.Text = _Licnese.DriverInfo.PersonInfo.FullName;
            lblLicenseID.Text = _Licnese.LicenseID.ToString();
            lblNationalNo.Text = _Licnese.DriverInfo.PersonInfo.NationalNo;
            lblGendor.Text = _Licnese.DriverInfo.PersonInfo.Gendor == 0 ? "Male" : "Female";
            lblIssueDate.Text = clsFormat.DateToShort(_Licnese.IssueDate);
            lblIssueReason.Text = _Licnese.IssueReason.ToString();
            lblNotes.Text = (_Licnese.Notes == "" )?"No Notes":_Licnese.Notes;
            lblIsActive.Text = _Licnese.IsActive ? "Yes" : "No";
            lblDateOfBirth.Text = clsFormat.DateToShort(_Licnese.DriverInfo.PersonInfo.DateOfBirth);
            lblDriverID.Text = _Licnese.DriverID.ToString();
            lblExpirationDate.Text = clsFormat.DateToShort(_Licnese.ExpirationDate);
            /// We Do this Later on 
            lblIsDetained.Text = _Licnese.IsDetained ? "Yes":"No";
            _LoadPersonImage();
        }

    }
}
