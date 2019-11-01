using FinesseClient;
using Genesyslab.Desktop.Modules.Core.Model.Interactions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesyslab.Desktop.Modules.CiscoVoice.CiscoAgent
{
    class CiscoTabModel : ICiscoTabModel, INotifyPropertyChanged
    {
        string header = "Cisco Agent";
        FinAgent finAgent;
        public string Header {
            get { return header; }
            set { if (value != header) header = value; OnPropertyChanged("Header"); }
        }

        public FinAgent FinAgent { get { return finAgent; } set { finAgent = value; OnPropertyChanged("FinAgent"); } }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
