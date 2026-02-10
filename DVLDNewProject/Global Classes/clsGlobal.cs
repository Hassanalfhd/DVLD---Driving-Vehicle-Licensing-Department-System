using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using DVLD_Buisness;
using Microsoft.Win32;
using System.Security.Cryptography;

namespace DVLDNewProject.Classes
{
    class clsGlobal
    {
        public static clsUser CurrentUser;

        private static string KeyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD_UserLoginInfo";
        

        public static bool RememberUsernameAndPasswordToFile(string UserName, string Password)
        {
            try
            {
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();

                string filePath = currentDirectory + "\\data.txt";

                if (UserName == "" && File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }

                string dataToSave = UserName + "#//#" + Password;

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine(dataToSave);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"An error occurred: " + ex.Message);
                clsEventLog.WriteEntryExceptionToEventViewer(ex.Message, System.Diagnostics.EventLogEntryType.Error);
                return false;
            }

        }

        public static bool RememberUsernameAndPassword(string UserName, string Password)
        {


            try
            {
                string ValueName = "UserName";
                string ValueName1 = "Password";

                Registry.SetValue(KeyPath, ValueName, UserName);
                Registry.SetValue(KeyPath, ValueName1, Password);
                
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(@"An error occurred: " + ex.Message);
                clsEventLog.WriteEntryExceptionToEventViewer(ex.Message, System.Diagnostics.EventLogEntryType.Error);
                return false;
            }

        }



        public static bool GetStoredCredentialFromFile(ref string Username, ref string Password)
        {
            try
            {
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();

                string filePath = currentDirectory + "\\data.txt";

                if (File.Exists(filePath))
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {


                            string[] result = line.Split(new string[] { "#//#" }, StringSplitOptions.None);
                            Username = result[0];
                            Password = result[1];
                        }

                        return true;
                    }

                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: ", ex.Message);
                clsEventLog.WriteEntryExceptionToEventViewer(ex.Message, System.Diagnostics.EventLogEntryType.Error);
                return false;
            }
        }

        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            try
            {
                string ValueName = "UserName";
                string ValueName1 = "Password";

                Username = Registry.GetValue(KeyPath, ValueName , null) as string;
                Password = Registry.GetValue(KeyPath, ValueName1, null) as string;

                if (Username != null && Password != null)
                {

                    return true;
                }
                else
                {
                    return false;
                }
                
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: ", ex.Message);
                clsEventLog.WriteEntryExceptionToEventViewer(ex.Message, System.Diagnostics.EventLogEntryType.Error);
                return false;
            }
        }


    }
}
