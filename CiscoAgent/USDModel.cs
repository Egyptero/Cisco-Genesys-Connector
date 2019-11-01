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
    public class USDModel : IUSDModel, INotifyPropertyChanged
    {
        // Field variables
        string header = "USD Integration Adaptor";
        public string dialogId;
        FinAgent finAgent;
        ICase @case;

        /// <summary>
        /// Initializes a new instance of the <see cref="CiscoVoiceModel"/> class.
        /// </summary>
        /// <param name="view">The view.</param>
        public USDModel()
        {
            // Dialog = new Dialog(FinAgent);
        }

        #region CiscoVoiceModel Members
        public FinAgent FinAgent { get { return finAgent; } set { finAgent = value; OnPropertyChanged("FinAgent"); } }
        internal static bool USDViewModelCondition(ref object context)
        {
            return true;
        }

        /// <summary>
        /// Gets or sets the header to set in the parent view.
        /// </summary>
        /// <value>The header.</value>
        public string Header
        {
            get { return header; }
            set { if (header != value) { header = value; OnPropertyChanged("Header"); } }
        }

        /// <summary>
        /// Gets or sets the case.
        /// </summary>
        /// <value>The case.</value>
        public ICase Case
        {
            get { return @case; }
            set { if (@case != value) { @case = value; OnPropertyChanged("Case"); } }
        }

        #endregion

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
