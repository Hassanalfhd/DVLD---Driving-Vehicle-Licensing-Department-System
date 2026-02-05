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
using DVLDNewProject.Properties;

namespace DVLDNewProject.Tests
{
    public partial class frmListTestAppointments : Form
    {
        private DataTable _dtLicenseTestAppointments;
        private int _LocalDrivingLicenseApplicationID = -1;
        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;


        public frmListTestAppointments(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            InitializeComponent();

            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestTypeID = TestTypeID;
        }

        private void _LoadTestTypeImageAndTitle()
        {

            switch (_TestTypeID)
            {

                case clsTestType.enTestType.VisionTest:
                    {
                        lblTitle.Text = "Vision Test";
                        this.Text = "Vision Test";
                        pbTestTypeImage.Image = Resources.Vision_512;
                        break;
                    }
                case clsTestType.enTestType.WrittenTest:
                    {
                        lblTitle.Text = "Written Test";
                        this.Text = "Written Test";
                        pbTestTypeImage.Image = Resources.Written_Test_512;
                        break;
                    }

                case clsTestType.enTestType.StreetTest:
                    {
                        lblTitle.Text = "Street Test";
                        this.Text = "Street Test";
                        pbTestTypeImage.Image = Resources.driving_test_512;
                        break;
                    }
            
            }
        }

        private void frmListTestAppointments_Load(object sender, EventArgs e)
        {
            _LoadTestTypeImageAndTitle();
            ctrlDrivingLicenseApplicationInfo1.LoadLocalDrivingLicenseApplicationInfoByLocalDrivingLicenseApplicationID(_LocalDrivingLicenseApplicationID);
            _dtLicenseTestAppointments = clsTestAppointment.GetApplicationTestAppointmentsPerTestType(_LocalDrivingLicenseApplicationID,_TestTypeID);


            dgvLicenseTestAppointments.DataSource = _dtLicenseTestAppointments;

            lblRecordsCount.Text = dgvLicenseTestAppointments.Rows.Count.ToString();

            if (dgvLicenseTestAppointments.Rows.Count > 0)
            {
                dgvLicenseTestAppointments.Columns[0].HeaderText = "Appointment ID";
                dgvLicenseTestAppointments.Columns[0].Width = 150;

                dgvLicenseTestAppointments.Columns[1].HeaderText = "Appointment Date";
                dgvLicenseTestAppointments.Columns[1].Width = 250;

                dgvLicenseTestAppointments.Columns[2].HeaderText = "Paid Fees";
                dgvLicenseTestAppointments.Columns[2].Width = 150;

                dgvLicenseTestAppointments.Columns[3].HeaderText = "Is Locked";
                dgvLicenseTestAppointments.Columns[3].Width = 100;
            }
        }

        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);

            if (localDrivingLicenseApplication.IsThereAnActiveScheduledTest(_TestTypeID))
            {
                MessageBox.Show("Person already have an active appointment for this test, You cannot add new appointment", "Not allowed",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsTest LastTest = localDrivingLicenseApplication.GetLastTestPerTestType(_TestTypeID);

            if (LastTest == null)
            {
                frmScheduleTest frm1 = new frmScheduleTest(_LocalDrivingLicenseApplicationID, _TestTypeID);
                frm1.ShowDialog();
                frmListTestAppointments_Load(null, null);
                return;
            }

            // if Test Result is true
            if (LastTest.TestResult)
            {
                MessageBox.Show("This person already passed this test before, you can only retake faild test", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmScheduleTest frm2 = new frmScheduleTest(LastTest.TestAppointmentInfo.LocalDrivingLicenseApplicationID, _TestTypeID);
            frm2.ShowDialog();
            frmListTestAppointments_Load(null, null);
            return;
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int AppointmentID = (int)dgvLicenseTestAppointments.CurrentRow.Cells[0].Value;
            frmScheduleTest frm1 = new frmScheduleTest(_LocalDrivingLicenseApplicationID, _TestTypeID, AppointmentID);
            frm1.ShowDialog();
            frmListTestAppointments_Load(null, null);

        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int AppointmentID = (int)dgvLicenseTestAppointments.CurrentRow.Cells[0].Value;
            frmTakeTest frm = new frmTakeTest(AppointmentID, _TestTypeID);
            frm.ShowDialog();
            frmListTestAppointments_Load(null,null);
        }


    }
}
