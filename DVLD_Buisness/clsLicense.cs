using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;
using System.Data;

namespace DVLD_Buisness
{
    public class clsLicense
    {

        public enum enMode {AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public enum enIssueReason { FirstTime = 1, Renew = 2, DamagedReplacement = 3, LostReplacement = 4 };

        public clsDriver DriverInfo;
        public int LicenseID { set; get; }
        public int ApplicationID { set; get; }
        public int DriverID { set; get; }
        public int LicenseClass { set; get; }
        public clsLicenseClass LicenseClassIfo;
        public DateTime IssueDate { set; get; }
        public DateTime ExpirationDate { set; get; }
        public string Notes { set; get; }
        public float PaidFees { set; get; }
        public bool IsActive { set; get; }
        public enIssueReason IssueReason { set; get; }
        public int CreatedByUserID { set; get; }

        public string IssueReasonText
        {
            get
            {
                return GetIssueReasonText(this.IssueReason);
            }
        }

        private string GetIssueReasonText(enIssueReason IssueReason)
        {
            switch (IssueReason)
            { 
                case enIssueReason.FirstTime:
                    return "Frist Time";
                case enIssueReason.Renew:
                    return "Renew";
                case enIssueReason.DamagedReplacement:
                    return "Damaged Replacement";

                case enIssueReason.LostReplacement:
                    return "Lost Replacement";
                default:
                    return "Frist Time";

            }
        }

        public clsDetainedLicense DetainedInfo {set;get;}

        public bool IsDetained
        {
            get
            {
                return clsDetainedLicense.IsLicenseDetained(this.LicenseID);
            }
        }

        public clsLicense()
        {
            LicenseID = -1;
            ApplicationID = -1;
            DriverID = -1;
            LicenseClass = -1;
            IssueDate = DateTime.Now;
            ExpirationDate = DateTime.Now;
            Notes = "";
            PaidFees = 0;
            IsActive = false;
            IssueReason = enIssueReason.FirstTime;
            CreatedByUserID = -1;
            Mode = enMode.AddNew;
        }


        public clsLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClass,
            DateTime IssueDate, DateTime ExpirationDate, string Notes,
            float PaidFees, bool IsActive, enIssueReason IssueReason, int CreatedByUserID)
        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClass = LicenseClass;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;

            this.DriverInfo = clsDriver.FindByDriverID(this.DriverID);
            this.LicenseClassIfo = clsLicenseClass.Find(this.LicenseClass);
            this.DetainedInfo = clsDetainedLicense.FindByLicenseID(this.LicenseID);

            Mode = enMode.Update;
        }


        private bool _AddNewLicense()
        {
            this.LicenseID = clsLicenseData.AddNewLicense(this.ApplicationID, this.DriverID, this.LicenseClass,
               this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees,
               this.IsActive, (byte)this.IssueReason, this.CreatedByUserID);

            return (this.LicenseID != -1);
        }

        private bool _UpdateLicense()
        {

            return clsLicenseData.UpdateLicense(this.ApplicationID, this.LicenseID, this.DriverID, this.LicenseClass,
               this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees,
               this.IsActive, (byte)this.IssueReason, this.CreatedByUserID);
        }

        public static clsLicense Find(int LicenseID)
        {
            int ApplicationID = -1; int DriverID = -1; int LicenseClass = -1;
            DateTime IssueDate = DateTime.Now; DateTime ExpirationDate = DateTime.Now;
            string Notes = "";
            float PaidFees = 0; bool IsActive = true; int CreatedByUserID = 1;
            byte IssueReason = 1;
            if (clsLicenseData.GetLicenseInfoByLicenseID(LicenseID, ref ApplicationID, ref DriverID, ref LicenseClass,
            ref IssueDate, ref ExpirationDate, ref Notes,
            ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))

                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass,
                                     IssueDate, ExpirationDate, Notes,
                                     PaidFees, IsActive, (enIssueReason)IssueReason, CreatedByUserID);
            else
                return null;

        }

        public static DataTable GetAllLicenses()
        {
            return clsLicenseData.GetAllLicenses();

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicense())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateLicense();

            }

            return false;
        }

        public static bool IsLicenseExistByPersonID(int PersonID, int LicenseClassID)
        {
            return GetActiveLicenseByPersonID(PersonID, LicenseClassID) != -1;
        }
        
        public static int GetActiveLicenseByPersonID(int PersonID, int LicenseClassID)
        {
            return clsLicenseData.GetActiveLicenseIDByPersonID(PersonID, LicenseClassID);
        }

        public bool DeactivateCurrentLicense()
        {
            return clsLicenseData.DeactiveLicense(this.LicenseID);
        }

        public Boolean IsLicenseExpired()
        {
            return (this.ExpirationDate < DateTime.Now);
        }


        public static DataTable GetDriverLicenses(int DriverID)
        {
            return clsLicenseData.GetDriverLicenses(DriverID);
        }

        public clsLicense RenewLicense(string Notes, int CreatedByUserID)
        {

            clsApplication Application = new clsApplication();
            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            Application.ApplicationTypeID = (int)clsApplication.enApplicationType.RenewDrivingLicense;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).Fees;
            Application.CreatedByUserID = CreatedByUserID;

            if (!Application.Save())
                return null;

            clsLicense NewLicense = new clsLicense();

            NewLicense.ApplicationID = Application.ApplicationID;
            NewLicense.CreatedByUserID = CreatedByUserID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.LicenseClass = this.LicenseClass;
            NewLicense.IssueReason = enIssueReason.Renew;
            NewLicense.Notes = Notes;
            NewLicense.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassIfo.DefaultValidityLength);
            NewLicense.IsActive = true;
            NewLicense.PaidFees = this.LicenseClassIfo.ClassFees;

            if (!NewLicense.Save())
                return null;

            DeactivateCurrentLicense();

            return NewLicense;
        }


        public clsLicense Replace(enIssueReason IssueReason, int CreatedByUserID)
        {

            clsApplication Application = new clsApplication();
            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            Application.ApplicationTypeID = (IssueReason == enIssueReason.DamagedReplacement)?
                (int)enIssueReason.DamagedReplacement: (int)enIssueReason.LostReplacement;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationType.Find(Application.ApplicationTypeID).Fees;
            Application.CreatedByUserID = CreatedByUserID;

            if (!Application.Save())
                return null;

            clsLicense NewLicense = new clsLicense();

            NewLicense.ApplicationID = Application.ApplicationID;
            NewLicense.CreatedByUserID = CreatedByUserID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.LicenseClass = this.LicenseClass;
            NewLicense.IssueReason = IssueReason;
            NewLicense.IsActive = true;
            NewLicense.ExpirationDate = this.ExpirationDate;
            NewLicense.PaidFees = 0;// No Fees For this Lciense because it's Replace 

            if (!NewLicense.Save())
                return null;

            DeactivateCurrentLicense();

            return NewLicense;
        }

        public int Detain(float FineFees, int CreatedByUserID)
        {
            clsDetainedLicense detainedLicense = new clsDetainedLicense();

            detainedLicense.LicenseID = this.LicenseID;
            detainedLicense.FineFees = Convert.ToSingle(FineFees);
            detainedLicense.CreatedByUserID = CreatedByUserID;
            detainedLicense.DetainDate = DateTime.Now;

            if (!detainedLicense.Save())
                return -1;

            return detainedLicense.DetainID;
        }

        public bool ReleaseDetainedLicense(int ReleasedByUserID, ref int ApplicationID)
        {
            clsApplication Application = new clsApplication();

            Application.ApplicantPersonID = DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = (int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense;
            Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            Application.CreatedByUserID = ReleasedByUserID;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationType.Find((int)Application.ApplicationTypeID).Fees;
            
            if (!Application.Save())
            {
                ApplicationID = -1;
                return false;
            }
                ApplicationID = Application.ApplicationID;


                return DetainedInfo.ReleaseDetainedLicense(ReleasedByUserID, Application.ApplicationID);
        }


    }
}
