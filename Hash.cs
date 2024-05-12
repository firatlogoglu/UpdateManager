using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;

namespace UpdateManager
{
    internal class Hash
    {
        internal static bool CheckHash(string UrlSHA256, string path)
        {
            string hash2;
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                WebClient webclient = new WebClient();
                WebRequest.DefaultWebProxy = null;
                webclient.Proxy = null;
                var hash1 = webclient.DownloadString(UrlSHA256);

                using (var sha256 = SHA256.Create())
                {
                    using (var stream = File.OpenRead(path))
                    {
                        byte[] hash = sha256.ComputeHash(stream);
                        hash2 = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                    }
                }

                return hash1.Equals(hash2, StringComparison.OrdinalIgnoreCase);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}