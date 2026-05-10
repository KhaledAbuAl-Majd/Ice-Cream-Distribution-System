using MoneyMindManager_Presentation.Global;
using MoneyMindManagerGlobal;

namespace MoneyMindManager_Presentation
{
    public static class clsPL_Global
    {
        //public static clsUser CurrentUser { get;private set; }
        //public static frmMain MainForm { get; private set; }
        public static Form ActiveForm { get; set; }


        public static void SubscribeToErrorOcrruedEvent()
        {
            clsGlobalEvents.OnErrorOccured += clsPL_MessageBoxs.ShowErrorMessage;
        }

        //private static class clsRegisteryConstants
        //{
        //    public static string SubKeyName = @"Software\MonyMindManager";

        //    public static string UserNameValueName = "UserName";

        //    public static string PasswordValueName = "Password";

        //}

        ///// <summary>
        ///// Store UserName & Password After Encrypt it at Windows Registery 
        ///// If UserName Or Password = null, it will remove the old value from registery
        ///// </summary>
        ///// <returns>Success Value</returns>
        //public static async Task<bool> RememberUsernameAndPassword(string Username, string Password)
        //{
        //    return await Task<bool>.Run(() =>
        //     {
        //         try
        //         {
        //             using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
        //             {
        //                 using (RegistryKey key = baseKey.CreateSubKey(clsRegisteryConstants.SubKeyName, true))
        //                 {
        //                     if (key != null)
        //                     {
        //                         if (Username == null || Password == null)
        //                         {
        //                             if (key.GetValue(clsRegisteryConstants.UserNameValueName) != null)
        //                                 key.DeleteValue(clsRegisteryConstants.UserNameValueName);

        //                             if (key.GetValue(clsRegisteryConstants.PasswordValueName) != null)
        //                                 key.DeleteValue(clsRegisteryConstants.PasswordValueName);
        //                         }
        //                         else
        //                         {
        //                             string EncryptedUserName = clsSymmetricEncryption.Encrypt(Username);
        //                             string EncryptedPassword = clsSymmetricEncryption.Encrypt(Password);

        //                             key.SetValue(clsRegisteryConstants.UserNameValueName, EncryptedUserName, RegistryValueKind.String);

        //                             key.SetValue(clsRegisteryConstants.PasswordValueName, EncryptedPassword, RegistryValueKind.String);
        //                         }
        //                     }
        //                     else
        //                         return false;
        //                 }
        //             }

        //             return true;
        //         }
        //         catch (Exception ex)
        //         {
        //             clsGlobalEvents.RaiseErrorEvent(ex.Message, true);
        //             return false;
        //         }

        //     });
        //}

        //public static async Task<(bool Result, string UserName, string Password)> GetStoredCredential()
        //{
        //    string userName = null, password = null;
        //    bool result = false;

        //    result = await Task<bool>.Run(() =>
        //    {
        //        try
        //        {
        //            using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
        //            {
        //                using (RegistryKey key = baseKey.OpenSubKey(clsRegisteryConstants.SubKeyName, true))
        //                {
        //                    if (key != null)
        //                    {
        //                        string EncryptedUsername = key.GetValue(clsRegisteryConstants.UserNameValueName) as string;

        //                        string EncryptedPassword = key.GetValue(clsRegisteryConstants.PasswordValueName) as string;

        //                        if (EncryptedUsername == null || EncryptedPassword == null)
        //                            return false;

        //                        userName = clsSymmetricEncryption.Decrypt(EncryptedUsername);
        //                        password = clsSymmetricEncryption.Decrypt(EncryptedPassword);
        //                    }
        //                }

        //                return true;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            userName = null;
        //            password = null;
        //            clsGlobalEvents.RaiseErrorEvent(ex.Message, true);
        //            return false;
        //        }
        //    });

        //    return (result, userName, password);
        //}
    }
}
