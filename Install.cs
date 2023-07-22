using System;
using System.Diagnostics;

namespace UpdateManager
{
    public class Install
    {
        public static void InstallUpdates(string path)
        {
            try
            {
                Process pr = new Process();
                pr.StartInfo.FileName = path;
                pr.StartInfo.Arguments = "";
                pr.StartInfo.UseShellExecute = true;
                pr.StartInfo.CreateNoWindow = false;

                pr.Start();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}