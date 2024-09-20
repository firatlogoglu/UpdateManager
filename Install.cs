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
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Product WHERE Name='" + applicationName + "'");
                foreach (ManagementObject product in searcher.Get())
                {
                    //Console.WriteLine(Languages.Localization.Uninstalling + " " + product["Name"]);
                    product.InvokeMethod("Uninstall", null);
                    //Console.WriteLine("Uninstallation completed.");
                    break;
                }

                Process process2 = new Process();
                process2.StartInfo.FileName = "msiexec.exe";
                process2.StartInfo.Arguments = $" /i \"{path}\" /quiet";
                process2.StartInfo.Verb = "runas";
                process2.StartInfo.UseShellExecute = true;
                process2.StartInfo.CreateNoWindow = true;

                process2.Start();
                process2.WaitForExit();

                Console.WriteLine(Languages.Localization.InstallationCompleted);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void InstallUpdates_CA(string path, string applicationName)
        {
            try
            {
                Console.WriteLine(Languages.Localization.Uninstalling + " " + applicationName);
                Process process = new Process();
                process.StartInfo.FileName = "wmic.exe";
                process.StartInfo.Arguments = $" product where name=\"{applicationName}\" call uninstall";
                process.StartInfo.Verb = "runas";
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.WaitForExit();

                Process process2 = new Process();
                process2.StartInfo.FileName = "msiexec.exe";
                process2.StartInfo.Arguments = $" /i \"{path}\" /quiet";
                process2.StartInfo.Verb = "runas";
                process2.StartInfo.UseShellExecute = true;
                process2.StartInfo.CreateNoWindow = true;
                process2.Start();
                process2.WaitForExit();

                Console.WriteLine(Languages.Localization.InstallationCompleted);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}