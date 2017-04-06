using DnDScreenCapture.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDScreenCapture.ViewModel
{
    class OAuthWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Uri oauthUri;

        public Uri OAuthUri
        {
            get { return this.oauthUri; }
            set
            {
                this.oauthUri = value;
                PropertyChanged.Notice(this);
            }
        }

        public OAuthWindowViewModel()
        {
            
        }

        public void Initialized(Uri oauthUri)
        {
            this.oauthUri = oauthUri;
        }
    }
}
