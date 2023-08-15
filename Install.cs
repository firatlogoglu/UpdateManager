using System;
using System.Diagnostics;
using System.Management;

namespace UpdateManager
{
    public class Install
    {
        public static void InstallUpdates(string path, string applicationName)
        {
            try
            {
                UninstallOldVersion(applicationName);
                Process pr = new Process();
                pr.StartInfo.FileName = path;
                pr.StartInfo.Arguments = "";
                pr.StartInfo.UseShellExecute = true;
                pr.StartInfo.CreateNoWindow = false;

                pr.Start();
                pr.WaitForExit();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void UninstallOldVersion(string applicationName)
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Product WHERE Name='" + applicationName + "'");
                foreach (ManagementObject product in searcher.Get())
                {
                    //Console.WriteLine("Uninstalling: " + product["Name"]);
                    product.InvokeMethod("Uninstall", null);
                    //Console.WriteLine("Uninstallation completed.");
                    break;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}