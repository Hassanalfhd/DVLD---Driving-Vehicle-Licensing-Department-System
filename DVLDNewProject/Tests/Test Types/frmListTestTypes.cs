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

namespace DVLDNewProject.Tests.Test_Types
{
    public partial class frmListTestTypes : Form
    {
        DataTable _dtTestTypes;

        public frmListTestTypes()
        {
            InitializeComponent();
        }

        private void frmListTestTypes_Load(object sender, EventArgs e)
        {
            _dtTestTypes = clsTestType.GetAllTestTypes();
            dgvTestTypes.DataSource = _dtTestTypes;
            lblRecordsCount.Text = dgvTestTypes.Rows.Count.ToString();

            if (dgvTestTypes.Rows.Count > 0)
            {
                dgvTestTypes.Columns[0].HeaderText = "ID";
                dgvTestTypes.Columns[0].Width = 110;


                dgvTestTypes.Columns[1].HeaderText = "Title";
                dgvTestTypes.Columns[1].Width = 200;


                dgvTestTypes.Columns[2].HeaderText = "Description";
                dgvTestTypes.Columns[2].Width = 400;


                dgvTestTypes.Columns[3].HeaderText = "Fees";
                dgvTestTypes.Columns[3].Width = 100;
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm1 = new frmEditTestType((clsTestType.enTestType)dgvTestTypes.CurrentRow.Cells[0].Value);
            frm1.ShowDialog();
            frmListTestTypes_Load(null,null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      

       

    }
}
