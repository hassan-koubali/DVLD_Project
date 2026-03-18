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
            string KeyEncript = "1234567890123456";
            string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD_Project";
            string userName = "UserName";
            string UserNameValue = Username;
            string PasswordName = "Password";
            //string PasswordValue = Password;

            string PasswordEncripted = Encrypt(Password, KeyEncript);


            try
            {
                // Write the value to the Registry
                Registry.SetValue(keyPath, userName, UserNameValue, RegistryValueKind.String);
                //Registry.SetValue(keyPath, PasswordName, PasswordValue, RegistryValueKind.String);
                Registry.SetValue(keyPath, PasswordName, PasswordEncripted, RegistryValueKind.String);

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
            string KeyEncript = "1234567890123456";


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
                    Password = Decrypt(passwordValue, KeyEncript);
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

        public static string Encrypt(string plainText, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                // Set the key and IV for AES encryption
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = new byte[aesAlg.BlockSize / 8];


                // Create an encryptor
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);


                // Encrypt the data
                using (var msEncrypt = new System.IO.MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (var swEncrypt = new System.IO.StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }


                    // Return the encrypted data as a Base64-encoded string
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        static string Decrypt(string cipherText, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                // Set the key and IV for AES decryption
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = new byte[aesAlg.BlockSize / 8];


                // Create a decryptor
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);


                // Decrypt the data
                using (var msDecrypt = new System.IO.MemoryStream(Convert.FromBase64String(cipherText)))
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (var srDecrypt = new System.IO.StreamReader(csDecrypt))
                {
                    // Read the decrypted data from the StreamReader
                    return srDecrypt.ReadToEnd();
                }
            }
        }


    }
}
