using System.Net;

namespace TestNinja.Mocking
{
    public class InstallerHelper
    {
        private string _setupDestinationFile;
        private readonly IDowloader _dowloader;

        public InstallerHelper(IDowloader dowloader)
        {
            _dowloader = dowloader;
        }

        public bool DownloadInstaller(string customerName, string installerName)
        {
            try
            {
                _dowloader.Download(
                    string.Format("http://example.com/{0}/{1}",
                    customerName,
                    installerName), 
                    _setupDestinationFile);

                return true;
            }
            catch (WebException)
            {
                return false;
            }
        }
    }

    public interface IDowloader
    {
        bool Download(string url, string path);
    }

    public class Downloader : IDowloader
    {
        public bool Download(string url, string path)
        {
            var client = new WebClient();
            try
            {
                client.DownloadFile(url, path);

                return true;
            }
            catch (WebException)
            {
                return false;
            }
        }
    }
}