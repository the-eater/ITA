using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace ITSA.Objects
{
    public class InstallingApp : INotifyPropertyChanged
    {
        public App Origin { get; set; }
        public double DownloadPercentage { get; set; }
        public void Start()
        {
            WebClient wc = new WebClient();
            string TempName = System.IO.Path.GetTempPath() + '\\' + Origin.Title + '.' + Origin.Extension;
            wc.DownloadFileCompleted += wc_DownloadFileCompleted;
            wc.DownloadProgressChanged += wc_DownloadProgressChanged;
            wc.DownloadFileAsync(Origin.DownloadUriForThisSystem,TempName);
        }

        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            DownloadPercentage = e.ProgressPercentage;
            NotifyPropertyChanged("DownloadPercentage");
        }

        void wc_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
