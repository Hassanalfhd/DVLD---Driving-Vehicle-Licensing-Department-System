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

namespace DVLDNewProject.Licenses.LocalLicenses.Controls
{
    public partial class ctrDriverLicenseInfoWithFliter : UserControl
    {

        public event Action<int> OnLicenseSeleted;

        public virtual void LicenseSelected(int LicenseID)
        {
            Action<int> handler = OnLicenseSeleted;
            if (handler != null)
            {
                handler(LicenseID);
            }
        }

        private bool _FilterEnable = true;

        public bool FilterEnable
        {
            get 
            {
                return _FilterEnable;
            }

            set
            {
                _FilterEnable = value;
                gbFilters.Enabled = _FilterEnable;
            }
        }

        private int _LicenseID = -1;

        public int LicenseID
        {
            get
            {
                return ctrDriverLicenseInfo1.LicenseID;
            }
        }

        public clsLicense SelectedLicenseInfo
        {
            get
            {
                return ctrDriverLicenseInfo1.SelectedLicenseInfo;
            }
        }
     
        public ctrDriverLicenseInfoWithFliter()
        {
            InitializeComponent();
        }

        public void LoadLicenseInfo(int LicenseID)
        {

            txtLicenseID.Text = LicenseID.ToString();
            ctrDriverLicenseInfo1.LoadLicenseInfo(LicenseID);
            _LicenseID = ctrDriverLicenseInfo1.LicenseID;

            if (OnLicenseSeleted != null && FilterEnable)
                OnLicenseSeleted(_LicenseID);

        }

        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            if (e.KeyChar == (int)13)
            {
                btnFind.PerformClick();
            }
        }

        private void txtLicenseID_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtLicenseID.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtLicenseID, "This field is required!");
            }
            else
            {
                errorProvider1.SetError(txtLicenseID, null);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLicenseID.Focus();
                return;

            }

            _LicenseID = int.Parse(txtLicenseID.Text.Trim());
            LoadLicenseInfo(_LicenseID);

        }

        public void txtLicenseIDFocus()
        {
            txtLicenseID.Focus();
        }

    }
}
