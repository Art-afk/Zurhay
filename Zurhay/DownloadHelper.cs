using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;


namespace Zurhay
{
    public class DownloadHelper
    {
        public static readonly string ToDownload = "http://mipomnim.pp.ua/uploads/download.php?XMLName=LxHQHsZFaW.xml";
       // public static readonly string ImageToDownload = "http://cdn.mysql.com//Downloads/MySQLInstaller/mysql-installer-community-5.7.13.0.msi";
        public static readonly int BufferSize = 4096;

        public static async Task<int> CreateDownloadTask(string urlToDownload, IProgress<DownloadBytesProgress> progessReporter)
        {
            int receivedBytes = 0;
            int totalBytes = 0;
             WebClient client = new WebClient();

            
            using (var stream = await client.OpenReadTaskAsync(urlToDownload))
            {
                
                byte[] buffer = new byte[BufferSize];
                totalBytes = Int32.Parse(client.ResponseHeaders[HttpResponseHeader.ContentLength]);
            

                for (;;)
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        await Task.Yield();
                        break;
                    }

                    receivedBytes += bytesRead;
                    if (progessReporter != null)
                    {
                        DownloadBytesProgress args = new DownloadBytesProgress(urlToDownload, receivedBytes, totalBytes);
                        progessReporter.Report(args);
                    }
                }
                

            }

            client.DownloadFileCompleted += Client_DownloadFileCompleted;
            return receivedBytes;
          


        }

        private static void Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
         //   var bytes = e.Result; // get the downloaded data
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string localFilename = "downloaded.png";
            string localPath = Path.Combine(documentsPath, localFilename);
       //     File.WriteAllBytes(localPath, bytes); // writes to local storage
        }
    }
}