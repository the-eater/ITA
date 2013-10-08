using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace ITA.Objects
{
    public class InstallingApp : INotifyPropertyChanged
    {
        public App Origin { get; set; }
        public double DownloadPercentage { get; set; }
        public string Status { get; set; }
        private string TempName;
        public void Start()
        {
            WebClient wc = new WebClient();
            TempName = System.IO.Path.GetTempPath() + '\\' + Origin.Title + '.' + Origin.Extension;
            wc.DownloadFileCompleted += wc_DownloadFileCompleted;
            wc.DownloadProgressChanged += wc_DownloadProgressChanged;
            wc.DownloadFileAsync(Origin.DownloadUriForThisSystem, TempName);
            Status = "Downloading";
            NotifyPropertyChanged("Status");
        }

        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            DownloadPercentage = e.ProgressPercentage;
            NotifyPropertyChanged("DownloadPercentage");
        }

        void wc_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            Status = "Queued";
            NotifyPropertyChanged("Status");
            DownloadPercentage = 100;
            NotifyPropertyChanged("DownloadPercentage");
            if(Downloaded!=null)
                Downloaded();
        }

        public event Action Downloaded;
        public event Action Installed;
        public void Install()
        {
            Status = "Installing";
            NotifyPropertyChanged("Status");
            ProcessStartInfo psi = new ProcessStartInfo();
            if (Origin.InstallCommand[0] == '{')
            {
                psi.FileName = TempName;
                psi.Arguments = String.Format(Origin.InstallCommand, "").Trim();
            }
            else
            {
                psi.UseShellExecute = true;
                string fullCommand = String.Format(Origin.InstallCommand, '"' + TempName + '"',"\\\"" + TempName + "\\\"");
                int firstSplit = fullCommand.IndexOf(' ');
                psi.FileName = fullCommand.Substring(0, firstSplit).TrimEnd();
                psi.Arguments = fullCommand.Substring(firstSplit).TrimStart();
            }
            Process p = new Process()
            {
                StartInfo = psi,
                EnableRaisingEvents = true
            };
            p.Exited += (sender, e) =>
            {
                Status = (p.ExitCode == 0 || p.ExitCode == Origin.ExpectedExitCode) ? "Completed" : "Error?";
                NotifyPropertyChanged("Status");
                if (p.ExitCode != 0 && p.ExitCode != Origin.ExpectedExitCode && System.Windows.MessageBox.Show("The installation of '" + Origin.Title + "' may have failed :(\r\nDo you want to try to install it yourself?", "Oops (Code "+p.ExitCode+")", System.Windows.MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.Yes)
                {
                    try
                    {
                        Process.Start(TempName);
                    }
                    catch
                    {

                    }
                }
                if (Installed != null)
                    Installed();

            };
            try
            {
                p.Start();
            }
            catch (Exception e)
            {
                Status = "Broken";
                NotifyPropertyChanged("Status");
                if (System.Windows.MessageBox.Show("The installation of '" + Origin.Title + "' is broken :(\r\nDo you want to try downloading it yourself? (it may be that the download link is pointing to a page)", "Oops", System.Windows.MessageBoxButton.YesNo) == System.Windows.MessageBoxResult.Yes)
                {
                    Process.Start(Origin.DownloadUriForThisSystem.AbsoluteUri);
                }
                if (Installed != null)
                    Installed();
            }
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
