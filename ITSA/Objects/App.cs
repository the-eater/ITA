using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ITSA.Objects
{
    public class App
    {

        public Uri Icon
        {
            get;
            set;
        }

        public String InstallCommand
        {
            get;
            set;
        }

        public bool IsMSI
        {
            get;
            set;
        }

        public string Extension
        {
            get
            {
                return IsMSI ? "msi" : "exe";
            }
        }

        public String Title
        {
            get;
            set;
        }

        public Uri DownloadUriForThisSystem
        {
            get { return this.DownloadUri64!=null && System.Environment.Is64BitOperatingSystem ? this.DownloadUri64 : this.DownloadUri; }
        }

        public Uri DownloadUri
        {
            get;
            set;
        }

        public Uri DownloadUri64
        {
            get;
            set;
        }
    }
}
