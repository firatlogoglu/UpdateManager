using System;
using System.Net;

namespace UpdateManager
{
    public class Update
    {
        public static bool Check(string ProductVersion, bool Is64BitProcess, string urlVersion, string urlgithub, string SetupFileName, string UserDownloadFolder, out string NewVersion, out string urlDownload, out string path)
        {
            string fileName;
            string bit;

            if (!Is64BitProcess)
            {
                bit = "x86";
            }
            else
            {
                bit = "x64";
            }

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                WebClient webclient = new WebClient();
                WebRequest.DefaultWebProxy = null;
                webclient.Proxy = null;

                var newVersion = webclient.DownloadString(urlVersion);
                if (newVersion.Contains("\n"))
                {
                    newVersion = newVersion.Replace("\n", "");
                }

                string nwVrsn = newVersion.Replace(".", "");
                string crntVrsn = ProductVersion.Replace(".", "");
                if (Convert.ToInt32(nwVrsn) > Convert.ToInt32(crntVrsn))
                {
                    urlDownload = string.Format("{0}v{1}/{2}_v{1}_{3}.msi", urlgithub, newVersion, SetupFileName, bit);
                    fileName = string.Format("{0}_v{1}_{2}.msi", SetupFileName, newVersion, bit);
                    path = UserDownloadFolder + "\\" + fileName;
                    NewVersion = newVersion;
                    return true;
                }
                else
                {
                    NewVersion = ProductVersion;
                    urlDownload = null;
                    path = null;
                    return false;
                }
            }
            catch (Exception)
            {
                NewVersion = "HATA";
                urlDownload = null;
                path = null;
                throw;
            }
        }
    }
}