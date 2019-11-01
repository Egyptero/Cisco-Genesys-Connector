using FinesseClient.Common;
using FinesseClient.Model;
using FinesseClient.View;
using Genesyslab.Desktop.Infrastructure.Commands;
using Genesyslab.Desktop.Infrastructure.DependencyInjection;
using Genesyslab.Desktop.Modules.CiscoVoice.FinesseClient;
using Genesyslab.Desktop.Modules.CiscoVoice.USDIntegration;
using Genesyslab.Desktop.Modules.Core.Model.Interactions;
using Genesyslab.Desktop.Modules.OpenMedia.Model.Interactions.Chat;
using Genesyslab.Desktop.Modules.OpenMedia.Model.Interactions.Email;
using Genesyslab.Desktop.Modules.Voice.Model.Interactions;
using Genesyslab.Desktop.Modules.Windows.Common.DimSize;
using Genesyslab.Desktop.WPFCommon;
using Genesyslab.Enterprise.Commons.Collections;
using Genesyslab.Enterprise.Model.Interaction;
using Genesyslab.Platform.Commons.Protocols;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Genesyslab.Desktop.Modules.CiscoVoice.CiscoAgent
{
    public partial class USDView : UserControl, IUSDView
    {

        readonly IObjectContainer container;
        string _customerCIC;
        string _interactionId;
        string _caseId;
        string interactionID="";

        public USDView(IUSDModel uSDModel, IObjectContainer container)
        {
            Model = uSDModel;

            Model.FinAgent = FinesseEventListener.Instance.FinAgent;
            this.container = container;
            var interactionManager = container.Resolve<IInteractionManager>();

            FinesseEventListener.Instance.FinAgent.FireLogMessage("USD Module , We are initializing the USD View");

            if (interactionManager != null)
                interactionManager.InteractionEvent += InteractionManager_InteractionEvent;
       

            InitializeComponent();
            Width = Double.NaN;
            Height = Double.NaN;
            MinSize = new MSize() { Width = 600, Height = 400.0 };
        }

        private void InteractionManager_InteractionEvent(object sender, Infrastructure.EventArgs<Core.Model.Interactions.IInteraction> e)
        {
            var interaction = e.Value;
        
            string state = interaction.EntrepriseInteractionCurrent.State.ToString().ToLower();
            if (!(interactionID == e.Value.InteractionId))
            {
                interactionID = e.Value.InteractionId;
           

                FinesseEventListener.Instance.FinAgent.FireLogMessage("USD Module , A new interaction Event");
                try
                {
                    if (interaction != null)
                    {

                       
                        FinesseEventListener.Instance.FinAgent.FireLogMessage("USD Module , We have new interaction event" + interaction.State);
                        if (interaction is IInteractionEmail || interaction is IInteractionChat) // We have email interaction received
                        {
                          
                            _interactionId = interaction.InteractionId;
                            _caseId = interaction.CaseId;
                            FinesseEventListener.Instance.FinAgent.FireLogMessage("USD Module  , we have a new interaction of type Email or Chat");

                            if (interaction.State == InteractionStateType.Connected) // Interaction is connected , now we should popup
                            {
                            
                                FinesseEventListener.Instance.FinAgent.FireLogMessage("USD Module  , Interaction of email or chat is connected");

                                KeyValueCollection userData = interaction.ExtractAttachedData();

                                userData.Add("CustomerCIC", "123492");
                                userData.Add("FromAddress", "maref@istnetworks.com");
                                userData.Add("ToAddress", "amostafa@istnetworks.com");
                                //TODO Mamdouh Add Customer CIC and From Address and To Address
                                if (userData != null && Model != null && userData.Contains("CustomerCIC"))
                                {
                                 
                                    string customerCIC = userData["CustomerCIC"].ToString();
                                    string fromAddress = userData["FromAddress"].ToString();
                                    string toAddress = userData["ToAddress"].ToString();

                                    FinesseEventListener.Instance.FinAgent.FireLogMessage("USD Module CIC is:" + customerCIC);


                                    if (customerCIC != null)
                                    {
                                      
                                        //check the CIC length is 16 digits
                                        string CICFormatted = customerCIC;
                                        while (CICFormatted.Length < 16)
                                            CICFormatted = "0" + CICFormatted;

                                        _customerCIC = CICFormatted;

                                        Dictionary<string, string> data = new Dictionary<string, string>();
                                        data.Add("CallVar4", _customerCIC);
                                        data.Add("InetractionID", _interactionId);
                                        data.Add("CaseID", _caseId);
                                        data.Add("AgentID", interaction.Agent.UserName);
                                        data.Add("AgentExt", "NA");
                                        data.Add("FromAddress", fromAddress);
                                        data.Add("ToAddress", toAddress);
                                        data.Add("DialNumber", "8016");
                               

                                        //We should popup interaction ID now
                                        if (interaction is IInteractionEmail)
                                        {
                                            FinesseEventListener.Instance.FinAgent.FireLogMessage("USD Module Fire WS Event Start Email from address" + fromAddress);
                                            WSEventListener.Instance.FireWSEvent(data, EventType.EMAIL, EventDirection.START);
                                        }
                                        if (interaction is IInteractionChat)
                                        {
                                         
                                            FinesseEventListener.Instance.FinAgent.FireLogMessage("USD Module Fire WS Event Start Chat");
                                            WSEventListener.Instance.FireWSEvent(data, EventType.CHAT, EventDirection.START);
                                        }
                                    }
                                }

                            }
                            else if (interaction.State == InteractionStateType.Ended || interaction.State == InteractionStateType.Dropped)
                            {
                                KeyValueCollection userData = interaction.ExtractAttachedData();

                                if (userData != null && Model != null && userData.Contains("CustomerCIC"))
                                {
                                    string customerCIC = userData["CustomerCIC"].ToString();
                                    if (customerCIC != null)
                                    {
                                        _customerCIC = customerCIC;

                                        Dictionary<string, string> data = new Dictionary<string, string>();
                                        data.Add("CallVar4", _customerCIC);
                                        data.Add("InetractionID", _interactionId);
                                        data.Add("CaseID", _caseId);
                                        //We should popup interaction ID now
                                        if (interaction is IInteractionEmail)
                                        {
                                            WSEventListener.Instance.FireWSEvent(data, EventType.EMAIL, EventDirection.END);
                                        }
                                        if (interaction is IInteractionChat)
                                        {
                                            WSEventListener.Instance.FireWSEvent(data, EventType.CHAT, EventDirection.END);
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    FinesseEventListener.Instance.FinAgent.FireLogMessage("USD Module , Exception during the event:" + ex.StackTrace + "\n and the message is" + ex.Message);
                }
            }
        }

        MSize _MinSize;
        public MSize MinSize
        {
            get { return _MinSize; }  // (MSize)base.GetValue(MinSizeProperty); }
            set
            {
                _MinSize = value; // base.SetValue(MinSizeProperty, value);
                OnPropertyChanged("MinSize");
            }
        }


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion
        #region USDView Members

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>The model.</value>
        public IUSDModel Model
        {
            get { return this.DataContext as IUSDModel; }
            set { this.DataContext = value; }
        }

        #endregion

        #region IView Members

        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        public object Context { get; set; }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        public void Create()
        {
        }

        /// <summary>
        /// Destroys this instance.
        /// </summary>
        public void Destroy()
        {
        }

        #endregion

    }
}
