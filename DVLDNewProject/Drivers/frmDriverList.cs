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
using DVLDNewProject.People;
using DVLDNewProject.Licenses;

namespace DVLDNewProject.Drivers
{
    public partial class frmDriverList : Form
    {
        private DataTable _dtAllDrivers;

        public frmDriverList()
        {
            InitializeComponent();
        }

        private void frmDriverList_Load(object sender, EventArgs e)
        {
            _dtAllDrivers = clsDriver.GetAllDrivers();
            dgvDrivers.DataSource = _dtAllDrivers;
            lblRecordsCount.Text = dgvDrivers.Rows.Count.ToString();
            cbFilterBy.SelectedIndex = 0;

            if (dgvDrivers.Rows.Count > 0)
            {
                dgvDrivers.Columns[0].HeaderText = "Driver ID";
                dgvDrivers.Columns[0].Width = 120;


                dgvDrivers.Columns[1].HeaderText = "Person ID";
                dgvDrivers.Columns[1].Width = 120;


                dgvDrivers.Columns[2].HeaderText = "National No.";
                dgvDrivers.Columns[2].Width = 140;

                dgvDrivers.Columns[3].HeaderText = "Full Name";
                dgvDrivers.Columns[3].Width = 320;

                dgvDrivers.Columns[4].HeaderText = "Date";
                dgvDrivers.Columns[4].Width = 170;

                dgvDrivers.Columns[5].HeaderText = "Active Licenses";
                dgvDrivers.Columns[5].Width = 140;

            }


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo((int)dgvDrivers.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
        }


        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterName = "";

            switch (cbFilterBy.Text)
            { 
                case "Driver ID":
                    {
                    FilterName = "DriverID";
                      break;
                    }
            case "Person ID":
                {
                    FilterName = "PersonID";
                    break;
                }
            case "National No.":
                {
                    FilterName = "NationalNo";
                    break;
                }
            case "Full Name ":
               {
                   FilterName = "FullName";
                   break;
             
               }
                default:
               FilterName = "None";
               break;
            }


            if (FilterName == "None" || txtFilterValue.Text.Trim() == "")
            {
                _dtAllDrivers.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvDrivers.Rows.Count.ToString();
                return;
            }

            if (FilterName != "FullName" && FilterName != "NationalNo")
            {
                _dtAllDrivers.DefaultView.RowFilter = string.Format("[{0}] = {1}",FilterName, txtFilterValue.Text.Trim());
            }
            else
            {
                _dtAllDrivers.DefaultView.RowFilter = string.Format("[{0}] Like '{1}%'", FilterName, txtFilterValue.Text.Trim());
                
            }

            lblRecordsCount.Text = dgvDrivers.Rows.Count.ToString();

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtFilterValue.Visible = (cbFilterBy.Text != "None");

            if (cbFilterBy.Text == "None")
                txtFilterValue.Enabled = false;
            else
                txtFilterValue.Enabled = true;

            txtFilterValue.Clear();
            txtFilterValue.Focus();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {

            if(cbFilterBy.Text == "Driver ID" || cbFilterBy.Text == "Person ID")
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        private void dgvDrivers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo((int)dgvDrivers.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory((int)dgvDrivers.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
        }


    }
}
