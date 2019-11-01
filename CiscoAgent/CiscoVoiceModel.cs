using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using FinesseClient;
using FinesseClient.Model;
using Genesyslab.Desktop.Modules.CiscoVoice.FinesseClient;
using Genesyslab.Desktop.Modules.Core.Model.Interactions;
using Genesyslab.Enterprise.Commons.Collections;
using Newtonsoft.Json.Linq;

namespace Genesyslab.Desktop.Modules.CiscoVoice.CiscoAgent
{
    /// <summary>
    /// The behabior of the MySampleView class
    /// </summary>
    public class CiscoVoiceModel : ICiscoVoiceModel, INotifyPropertyChanged
    {
        // Field variables
        string header = "Cisco Inbound Call";
        public string dialogId;
        FinAgent finAgent;
        ICase @case;
        bool callWaiting = false;
        #region Members
        Dialog _dialog;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="CiscoVoiceModel"/> class.
        /// </summary>
        /// <param name="view">The view.</param>
        public CiscoVoiceModel()
        {
           // Dialog = new Dialog(FinAgent);
        }

        #region CiscoVoiceModel Members
        public Dialog Dialog
        {
            get { return _dialog; }
            set
            {
                _dialog = value;
                OnPropertyChanged("Dialog");
            }
        }
        public FinAgent FinAgent { get { return finAgent; } set { finAgent = value; OnPropertyChanged("FinAgent"); } }
        internal static bool CiscoVoiceViewModelCondition(ref object context)
        {
            IDictionary<string, object> contextDictionary = context as IDictionary<string, object>;
            object caseView;
            contextDictionary.TryGetValue("CaseView", out caseView);
            object caseObject;
            contextDictionary.TryGetValue("Case", out caseObject);
            ICase @case = caseObject as ICase;
            if (@case != null)
            {
                if (@case.MainInteraction != null)
                {
                    IInteraction interaction = @case.MainInteraction;
                    KeyValueCollection userData = interaction.ExtractAttachedData();

                    if (userData != null && userData.Contains("CiscoCallID"))
                    {
                        string callId = userData["CiscoCallID"].ToString();

                        if (callId != null)
                            return true;
                    }

                }
            }
            return false;
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

        public bool? ShowCallWaiting { get { return callWaiting; } set { callWaiting = (bool) value; OnPropertyChanged("ShowCallWaiting"); OnPropertyChanged("ShowCallButtons"); } }
        public bool? ShowCallButtons { get { return !callWaiting; } set { callWaiting = !(bool)value; OnPropertyChanged("ShowCallWaiting"); OnPropertyChanged("ShowCallButtons"); } }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
        public void LoadInteraction(string dialogId)
        {
            if (FinAgent == null)
                return;
            if (FinAgent.AgentInformation == null)
                return;

            
            Dialog = FinAgent.FindDialog(dialogId);
            if (Dialog == null)
                Dialog = new Dialog(FinAgent);
            //new Dialog(FinAgent);
            //foreach (Dialog searchDialog in FinAgent.AgentInformation.Dialogs)
            //{
            //    if (searchDialog.ID.Equals(dialogId))
            //        Dialog = searchDialog;
            //}
        }
    }
}