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
using DVLDNewProject.Classes;

namespace DVLDNewProject.Login
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string Username = "", Password = "";

            if (clsGlobal.GetStoredCredential(ref  Username, ref  Password))
            {
                txtUserName.Text = Username;
                txtPassword.Text = Password;
                chkRememberMe.Checked = true;


            }
            else
                chkRememberMe.Checked = false;
 

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //string Password = clsGlobal.ComputeHash(txtPassword.Text);
            //string Password = txtPassword.Text;

            clsUser user = clsUser.FindByUsernameAndPassword(txtUserName.Text, txtPassword.Text);

           

            if (user != null)
            {
                if (chkRememberMe.Checked)
                {
                    clsGlobal.RememberUsernameAndPassword(txtUserName.Text, txtPassword.Text);

                }
                else
                {
                    clsGlobal.RememberUsernameAndPassword("", "");
                }


                if (!user.IsActive)
                {
                    txtUserName.Focus();
                    MessageBox.Show("Your Account is not Active, Contact Admin.", "In Active Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                clsGlobal.CurrentUser = user;
                this.Hide();
                frmMain frm = new frmMain(this);
                frm.ShowDialog();
            }else
            {
                txtUserName.Focus();
                MessageBox.Show("Invalid Username/Password.","Wrong Credintials",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
