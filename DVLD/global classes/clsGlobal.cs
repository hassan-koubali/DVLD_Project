using DVLD_Buisness;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Cryptography;




namespace DVLD.Classes
{
    internal static  class clsGlobal
    {
        public static clsUser CurrentUser;

        public static bool RememberUsernameAndPassword(string Username, string Password)
        {
            string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD_Project";
            string userName = "UserName";
            string UserNameValue = Username;
            string PasswordName = "Password";
            string PasswordValue = Password;



            try
            {
                // Write the value to the Registry
                Registry.SetValue(keyPath, userName, UserNameValue, RegistryValueKind.String);
                //Registry.SetValue(keyPath, PasswordName, PasswordValue, RegistryValueKind.String);
                Registry.SetValue(keyPath, PasswordName, PasswordValue, RegistryValueKind.String);

                return true;

            }
            catch (Exception ex)
            {
                string sourceName = "DVLD_Project";


                if (!EventLog.SourceExists(sourceName))
                {
                    EventLog.CreateEventSource(sourceName, "Application");
                }

                EventLog.WriteEntry(sourceName, $"An error occurred: {ex.Message}", EventLogEntryType.Error);
                return false;
            }


        }

        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD_Project";
            string userName = "UserName";
            string password = "Password";


            try
            {
                // Read the value from the Registry
                string userNameValue = Registry.GetValue(keyPath, userName, null) as string;
                string passwordValue = Registry.GetValue(keyPath, password, null) as string;

                if (userNameValue != null)
                {
                    Username = userNameValue;
                }
                else
                {
                    MessageBox.Show($"Value {userNameValue} not found in the Registry.", "Errore.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (passwordValue != null)
                {
                    Password = passwordValue;
                }
                else
                {
                    MessageBox.Show($"Value {passwordValue} not found in the Registry.", "Errore.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                string sourceName = "DVLD_Project";


                if (!EventLog.SourceExists(sourceName))
                {
                    EventLog.CreateEventSource(sourceName, "Application");
                }

                EventLog.WriteEntry(sourceName, $"An error occurred: {ex.Message}", EventLogEntryType.Error);

                return false;

            }
            return true;



        }

        public static string ComputeHash(string input)
        {
            //SHA is Secutred Hash Algorithm.
            // Create an instance of the SHA-256 algorithm
            using (SHA256 sha256 = SHA256.Create())
            {
                // Compute the hash value from the UTF-8 encoded input string
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));


                // Convert the byte array to a lowercase hexadecimal string
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

    }
}
