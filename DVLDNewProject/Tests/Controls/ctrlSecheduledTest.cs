using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDNewProject.Properties;
using DVLD_Buisness;
using DVLDNewProject.Classes;

namespace DVLDNewProject.Tests.Controls
{
    public partial class ctrlSecheduledTest : UserControl
    {
        private int _TestID = -1;
        private clsTestType.enTestType _TestTypeID =  clsTestType.enTestType.VisionTest;

        public clsTestType.enTestType TestTypeID
        {
            get
            {
                return _TestTypeID;
            }
            set
            {
                switch (_TestTypeID)
                {
                    case clsTestType.enTestType.VisionTest:
                        {
                            pbTestTypeImage.Image = Resources.Vision_512;
                            gbTestType.Text = "Vision Test";
                            break;
                        }
                    case clsTestType.enTestType.WrittenTest:
                        {
                            pbTestTypeImage.Image = Resources.Written_Test_512;
                            gbTestType.Text = "Written Test";
                            break;
                        }
                    case clsTestType.enTestType.StreetTest:
                        {
                            pbTestTypeImage.Image = Resources.driving_test_512;
                            gbTestType.Text = "Street Test";
                            break;
                        }
                }
            
            }
        }
        private int _TestAppointmentID = -1;

        public int TestAppointmentID
        {
            get
            {
                return _TestAppointmentID;
            }
        }

        public int TestID
        {
            get
            {
                return _TestID;
            }
        }

        private int _LocalDrivingLicenseApplicationID = -1;
     
        private clsTestAppointment _TestAppointment;
     
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
     
        public void LoadInfo(int TestAppointmentID)
        {
            _TestAppointmentID = TestAppointmentID;
            _TestAppointment = clsTestAppointment.Find(TestAppointmentID);
            if (_TestAppointment == null)
            {
                MessageBox.Show("Error:No Appointment ID = " + TestAppointmentID.ToString(), "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                _TestAppointmentID = -1;
                return;
            }


            _TestID = _TestAppointment.TestID;

            _LocalDrivingLicenseApplicationID = _TestAppointment.LocalDrivingLicenseApplicationID;

            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Error:No Local Driving License Application ID = " + _LocalDrivingLicenseApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblLocalDrivingLicenseAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = _LocalDrivingLicenseApplication.LicenseClassID.ToString();

            lblFullName.Text = _LocalDrivingLicenseApplication.PersonFullName;

            lblTrial.Text = _LocalDrivingLicenseApplication.TotalTrialsPerTest(_TestTypeID).ToString();

            lblDate.Text = clsFormat.DateToShort(_TestAppointment.AppointmentDate);
            lblFees.Text = _TestAppointment.PaidFees.ToString();
            lblTestID.Text = (_TestAppointment.TestID == -1) ? "Not Taken Yet" : _TestAppointment.TestID.ToString();

        }

        public ctrlSecheduledTest()
        {
            InitializeComponent();
        }
    }
}
