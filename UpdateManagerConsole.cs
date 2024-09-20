using System;
using System.ComponentModel;
using System.Net;
using System.Windows.Forms;
using UpdateManager.Languages;

namespace UpdateManager
{
    public class UpdateManagerConsole
    {
        private readonly string productName = Application.ProductName;
        private readonly string productVersion = Application.ProductVersion;
        private readonly bool is64BitProcess = Environment.Is64BitProcess;

        private string UrlGitHubReleases;
        private string urlDownload;
        private string UrlVersion;
        private string UrlSHA256;
        private static string path;
        private string AppTitle;
        private double FileSize;
        private double Percentage;

        public UpdateManagerConsole(string appName, string urlVersion, string urlGitHubReleases, string urlSHA256)
        {
            Console.Clear();
            AppTitle = Console.Title;
            Console.Title = Localization.UpdateManager;
            UrlVersion = urlVersion;
            UrlGitHubReleases = urlGitHubReleases;
            UrlSHA256 = urlSHA256;

            if (appName != null && appName != "")
            {
                productName = appName;
            }

            CheckUpdate();
        }

        private void CheckUpdate()
        {
            try
            {
                bool ass = UpdateManager.Update.Check(productVersion, is64BitProcess, UrlVersion, UrlGitHubReleases + "download/", productName + ".Setup", Environment.GetEnvironmentVariable("USERPROFILE") + @"\Downloads", out string newVersion, out urlDownload, out path);

                if (ass)
                {
                    Console.WriteLine(Localization.LastVersion + " " + newVersion);
                    Console.WriteLine(Localization.lblANewVersionAva + "\n" + Localization.lblANewVersionAva2);

                    Console.WriteLine("1: " + Localization.Yes);
                    Console.WriteLine("2: " + Localization.Exit);
                    var ad = Console.ReadLine();
                    if (ad == "1")
                    {
                        Download.DownloadUpdates(urlDownload, path, WebDownloadFileCompleted, WebDownloadProgressChanged);
                        var f = Console.ReadLine();
                    }
                    else if (ad == "2")
                    {
                        Console.Clear();
                        Console.Title = AppTitle;
                    }
                    else
                    {
                        Console.Clear();
                        CheckUpdate();
                    }
                }
                else
                {
                    Console.WriteLine(Localization.LastVersion + " " + productVersion);
                    Console.WriteLine(Localization.NoNewVersion);

                    Console.WriteLine("1: " + Localization.CheckUpdates_CA);
                    Console.WriteLine("2: " + Localization.Exit);
                    var ad = Console.ReadLine();
                    if (ad == "1")
                    {
                        Console.Clear();
                        CheckUpdate();
                    }
                    else if (ad == "2")
                    {
                        Console.Clear();
                        Console.Title = AppTitle;
                    }
                    else
                    {
                        Console.Clear();
                        CheckUpdate();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(Localization.ERROR_2 + " " + ex.Message);
                Console.WriteLine(Localization.LastVersion + " " + Localization.ERROR_2 + " " + Localization.LatestVersionNotDetected);
                Console.WriteLine(Localization.ERROR_2 + " " + Localization.LatestVersionNotDetected);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine("1: " + Localization.CheckUpdates_CA);
                Console.WriteLine("2: " + Localization.Exit);

                var ad = Console.ReadLine();
                if (ad == "1")
                {
                    Console.Clear();
                    CheckUpdate();
                }
                else if (ad == "2")
                {
                    Console.Clear();
                    Console.Title = AppTitle;
                }
                else
                {
                    Console.Clear();
                    CheckUpdate();
                }
            }
        }

        private void WebDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double receive = double.Parse(e.BytesReceived.ToString());
            FileSize = double.Parse(e.TotalBytesToReceive.ToString());
            Percentage = receive / FileSize * 100;
            Console.WriteLine(Localization.Downloading + $" { string.Format("{0:0.##}%", Percentage)}");
        }

        private void WebDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {
                    Console.WriteLine(Localization.ERROR_2 + " " + e.Error.Message);
                    //var ad = Console.ReadLine();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(Localization.Downloaded + " " + "100%" + " " + Localization.TotalSize + $" {string.Format("{0:0.##} KB", FileSize / Percentage)}");
                    if (Hash.CheckHash(UrlSHA256, path, is64BitProcess))
                    {
                        Console.WriteLine(path);

                        InstallUpdates_CA();
                    }
                    else
                    {
                        Console.WriteLine(path);
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("\n" + Localization.ERROR_2 + " " + Localization.DownloadedFileDontTrue);
                        Console.BackgroundColor = ConsoleColor.Black;

                        InstallUpdates_CA();
                    }
                }
            }
            catch (Exception ex)
            {
                //Console.Clear();
                Console.WriteLine(path);
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\n\n" + Localization.ERROR_2 + " " + Localization.VerificationFileNo + "\n" + Localization.ERROR_2 + " " + ex.Message);
                Console.BackgroundColor = ConsoleColor.Black;

                InstallUpdates_CA();
            }
        }

        private void InstallUpdates_CA()
        {
            #region Install Updates
            Console.WriteLine("1: " + Localization.InstallUpdates_CA);
            //if (read == "1")
            //{
            try
            {
                Install.InstallUpdates_CA(path, productName);
                Application.Restart();
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(Localization.ERROR_2 + " " + ex.Message);
                Console.BackgroundColor = ConsoleColor.Black;
                var ad2 = Console.ReadLine();
            }
            //}
            //else if (read == "2")
            //{
            //    var ad2 = Console.ReadLine();
            //}
            //else
            //{
            //    var ad2 = Console.ReadLine();
            //}
            #endregion
        }
    }
}