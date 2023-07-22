using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;

namespace UpdateManager
{
    public class Download
    {
        public static void DownloadUpdates(string urlDownload, string path, AsyncCompletedEventHandler WebDownloadFileCompleted, DownloadProgressChangedEventHandler WebDownloadProgressChanged)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WebClient webclient = new WebClient();
            webclient.DownloadFileCompleted += WebDownloadFileCompleted;
            webclient.DownloadProgressChanged += WebDownloadProgressChanged;
            webclient.DownloadFileAsync(new Uri(urlDownload), path);
        }

        public static void DirectDownload(string urlDefultDownload, string urlDownload)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(urlDownload))
                {
                    urlDownload = urlDefultDownload;
                }

                Process pr = new Process();
                pr.StartInfo.FileName = urlDownload;
                pr.Start();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}