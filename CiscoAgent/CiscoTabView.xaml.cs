using FinesseClient;
using FinesseClient.Common;
using FinesseClient.Model;
using FinesseClient.View;
using Genesyslab.Desktop.Infrastructure.DependencyInjection;
using Genesyslab.Desktop.Modules.CiscoVoice.FinesseClient;
using Genesyslab.Desktop.Modules.CiscoVoice.USDIntegration;
using Genesyslab.Desktop.Modules.CiscoVoice.ServiceReference5;
using Genesyslab.Desktop.Modules.Core.Model.Agents;
using Genesyslab.Desktop.Modules.Windows.Common.DimSize;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
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
using static FinesseClient.Model.Dialog.MediaPropertiesClass;

namespace Genesyslab.Desktop.Modules.CiscoVoice.CiscoAgent
{
    /// <summary>
    /// Interaction logic for CiscoTabView.xaml
    /// </summary>
    public partial class CiscoTabView : UserControl , ICiscoTabView, FinView
    {
        readonly IObjectContainer container;
        CiscoVoiceConfig CiscoVoiceConfig = new CiscoVoiceConfig();
        bool AutoLoginFunction;
        string _CapturePointUrl;
        string _MediaType;
        string _QueueName;
        string _TenantID;
        public CiscoTabView(ICiscoTabModel ciscoTabModel, IObjectContainer container)
        {
            Model = ciscoTabModel;
            //Initiate the finesse agent object
            if (Model.FinAgent == null)
            {
                FinesseEventListener.Instance.FinAgent = new FinAgent();
                FinesseEventListener.Instance.FinAgent.TraceStatus = true;
                FinesseEventListener.Instance.FinAgent.SaveLog = true;
                FinesseEventListener.Instance.FinAgent.FireLogMessage("Finesse Object Created");
                Model.FinAgent = FinesseEventListener.Instance.FinAgent;

            }
            Model.FinAgent.FireLogMessage("Finesse Agent Object is loaded into the Finesse Event Listener now.");
            FinesseEventListener.Instance.CiscoTab = this;
            Model.FinAgent.FireLogMessage("Cisco Tab is listening to Finesse Events now.");

            this.container = container;

            InitializeComponent();
            LoadConfiguration();
            Width = Double.NaN;
            Height = Double.NaN;
            MinSize = new MSize() { Width = 600, Height = 400.0 };
        }

        private void IAgent_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (Model.FinAgent != null)
            {
                Model.FinAgent.FireLogMessage("Property Change for property : " + e.PropertyName);
            }
        }

        public ICiscoTabModel Model
        {
            get { return this.DataContext as ICiscoTabModel; }
            set { this.DataContext = value; }
        }
        public object Context { get; set; }
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

        public void Create()
        {
            
            SwitchView("Login");
            if (AutoLoginFunction && Model.FinAgent.AgentInformation.Password != null)
                Model.FinAgent.SignIn();
        }

        public void Destroy()
        {
            if (Model.FinAgent.AgentInformation.Status == null)
                return;
            if (Model.FinAgent.AgentInformation.Status.Equals("LOGOUT"))
                return;
            else
            {
                if (Model.FinAgent.SignOut("LOGOUT", null))
                    return;
                else
                {
                    MessageBox.Show("We can not sign you out automatically.. Please try to relogin", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void SwitchView(string view)
        {
            if (view.Equals("Login"))
            {
                Login.Visibility = Visibility.Visible;
                Loading.Visibility = Visibility.Hidden;
                Main.Visibility = Visibility.Hidden;
            }
            else if (view.Equals("Loading"))
            {
                Login.Visibility = Visibility.Hidden;
                Loading.Visibility = Visibility.Visible;
                Main.Visibility = Visibility.Hidden;
            }
            else if(view.Equals("Main"))
            {
                Login.Visibility = Visibility.Hidden;
                Loading.Visibility = Visibility.Hidden;
                Main.Visibility = Visibility.Visible;
            }
        }
        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            //Finesse Login
            Model.FinAgent.AgentInformation.Password = Password.Password;
            LoadProgress.Value = 0;
            SaveConfiguration();
            SwitchView("Loading");
            Model.FinAgent.SignIn();
            
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        public void FireLoadingMessage(string msg)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, (System.Action)(() =>
            {
                if (LoadProgress.Value + 10 <= 100)
                    LoadProgress.Value += 10;
                else
                    LoadProgress.Value = 100;
            }));
        }

        public void FireLoadLoginScreen()
        {
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, (System.Action)(() =>
            {
                SwitchView("Login");
            }));
        }

        public void FireErrorMessage(string msg)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, (System.Action)(() =>
            {
                LoadError.Content = msg;
                CurrentStatus.Text = msg;
            }));

        }

        public void FireLogMessage(string msg)
        {
        }

        public Screen GetLocation()
        {
            return Screen.MainGrid;
        }

        public void SetContext(IModel model, FinView finView)
        {
        }

        public void FireNewEvent()
        {
        }

        public void FireReLoginEvent()
        {
            //Model.FinAgent.AgentInformation.Name = Model.FinAgent.AgentInformation.FirstName+" "+ Model.FinAgent.AgentInformation.LastName;
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, (System.Action)(() =>
            {
                SwitchView("Main");
            }));

        }

        public void FireDisconnectEvent()
        {
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, (System.Action)(() =>
            {
                SwitchView("Loading");
            }));

        }

        public void FireCallEvent(Dialog dialog)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, (System.Action)(() =>
            {
                try
                {
    
                    if (dialog.DialogEvent.Equals("POST") && dialog.State.Equals("ALERTING")) //Call Start Event
                    {
                        var remoteAddress = new System.ServiceModel.EndpointAddress(_CapturePointUrl);
                        //MessageBox.Show("1");
                        ////  ServiceReference1.iWebServiceCapturePointClient iWebServiceCapturePoint = new ServiceReference1.iWebServiceCapturePointClient( );
                        //WebReference.KVPair dialogID = new WebReference.KVPair();
                        //dialogID.key = "CiscoCallID";
                        //WebReference.KVPairValue dialogIDValue = new WebReference.KVPairValue();
                        //dialogIDValue.ValueString = dialog.ID;
                        //dialogID.value = dialogIDValue;
                        //WebReference.KVPair[] kvList = new WebReference.KVPair[]{ dialogID };
                        //Boolean F = true;
                        //WebReference.KVPair[] kvPairPlaceHolder = new WebReference.KVPair[] { dialogID };

                        //WebReference.KVPair[] userData = new WebReference.KVPair[] { dialogID };


                        //MessageBox.Show("2");

                        // MessageBox.Show(_MediaType);
                        //iWebServiceCapturePoint.Submit("", "CiscoCall", _MediaType, "", "", int.Parse(_TenantID),
                        //        _QueueName, "", "", // Virtual Call
                        //        "", "", "", "", F, "", kvList, kvList, userData, F, ref kvPairPlaceHolder);

                        //   iWebServiceCapturePoint.Submit("", "CiscoCall", _MediaType, "", "", int.Parse(_TenantID),
                        //_QueueName, "", "", // Virtual Call
                        //"", "", "", "", F, "", kvList, kvList, userData, F, ref kvPairPlaceHolder);
                        // MessageBox.Show("Virtual Call is created for the call id :" + dialog.ID);


                        //   MessageBox.Show("2");
                        //   WebReference.WebServiceCapturePoint ws = new WebReference.WebServiceCapturePoint();
                        //   ws.Submit("", "CiscoCall", _MediaType, "", "", int.Parse(_TenantID),true,
                        //_QueueName, "", "", // Virtual Call
                        //"", "", "","", F, "", kvList, kvList, userData, ref kvPairPlaceHolder);
                        //   MessageBox.Show("3");

                       

                        ServiceReference5.iWebServiceCapturePointClient iWebServiceCapturePoint = new ServiceReference5.iWebServiceCapturePointClient(new System.ServiceModel.BasicHttpBinding(), remoteAddress);

                        KVList kvList = new KVList();
                        Boolean F = false;
                        KVList kvPairPlaceHolder = new KVList();

                        KVList userData = new KVList();
                        KVPair dialogID = new KVPair();
                        dialogID.key = "CiscoCallID";
                        KVPairValue dialogIDValue = new KVPairValue();
                        dialogIDValue.ValueString = dialog.ID;
                        dialogID.value = dialogIDValue;
                        userData.Add(dialogID);
                        Nullable<bool> hold = null;

                        iWebServiceCapturePoint.Submit("", "New Call", _MediaType, "", "", int.Parse(_TenantID),
                                _QueueName, "", "", // Virtual Call
                                "", "", "", "", F, "", kvList, kvList, userData, hold, ref kvPairPlaceHolder);

                        Model.FinAgent.FireLogMessage("Virtual Call is created for the call id :" + dialog.ID);
                        Model.FinAgent.FireLogMessage("Virtual Call from : " + dialog.FromAddress + " To : " + dialog.ToAddress);
                        if (kvPairPlaceHolder != null)
                            foreach (ServiceReference5.KVPair kvPair in kvPairPlaceHolder)
                            {
                                Model.FinAgent.FireLogMessage("Virtual Call Received response Key : " + kvPair.key + " and Value : " + kvPair.value.ValueString + " for dialog id: " + dialog.ID);
                            }

                        //Start the popup activities

                        string CICFormatted = null;

                        //CIC Exist
                        if (((CallVariableClass)dialog.MediaProperties.CallVariables[3]).Value != null)
                        {
                            //check the CIC length is 16 digits
                            CICFormatted = ((CallVariableClass)dialog.MediaProperties.CallVariables[3]).Value;
                            string customerCIC = "123492";
                            CICFormatted = customerCIC;

                            while (CICFormatted.Length < 16)
                                CICFormatted = "0" + CICFormatted;
                        }
                        if (CICFormatted == null)
                            CICFormatted = "NA";
                       

                        string fromAddress = "maref@istnetworks.com";
                        string toAddress = "amostafa@istnetworks.com";
                        Dictionary<string, string> data = new Dictionary<string, string>();
                        //data.Add("CallVar4", CICFormatted);
                        data.Add("CallVar4", CICFormatted);
                        data.Add("InetractionID", dialog.ID);
                        data.Add("CaseID", "NA");
                        data.Add("AgentID", FinesseEventListener.Instance.FinAgent.AgentInformation.AgentID);
                        data.Add("AgentExt", FinesseEventListener.Instance.FinAgent.AgentInformation.Extension);
                        //data.Add("FromAddress", dialog.FromAddress);
                        //data.Add("ToAddress", dialog.ToAddress);
                        data.Add("FromAddress", fromAddress);
                        data.Add("ToAddress", toAddress);
                       // data.Add("DialNumber", dialog.MediaProperties.DialedNumber);
                        data.Add("DialNumber", "8016");
                      


                        //We should popup interaction ID now
                        WSEventListener.Instance.FireWSEvent(data, EventType.CALL, EventDirection.START);


                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                    Model.FinAgent.FireLogMessage(e.ToString());
                }
            }));
        }

        private void StatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (StatusComboBox.SelectedItem == null)
                    return;
                CurrentStatus.Text = "Change status to:" + (StatusComboBox.SelectedItem as VoiceStatus).StatusLabel;
                Model.FinAgent.ChangeAgentVoiceStatus(StatusComboBox.SelectedItem as VoiceStatus);
            }
            catch (Exception)
            {

            }
        }
        private void LoadConfiguration()
        {
            if (CiscoVoiceConfig.get("AgentID") != null)
                Model.FinAgent.AgentInformation.AgentID = CiscoVoiceConfig.get("AgentID");

            if (CiscoVoiceConfig.get("Ext") != null)
                Model.FinAgent.AgentInformation.Extension = CiscoVoiceConfig.get("Ext");

            if (CiscoVoiceConfig.get("XMPPPort") != null)
                Model.FinAgent.AgentInformation.XMPPPort = CiscoVoiceConfig.get("XMPPPort");

            if (CiscoVoiceConfig.get("XMPPURL") != null)
                Model.FinAgent.AgentInformation.XMPPURL = CiscoVoiceConfig.get("XMPPURL");

            if (CiscoVoiceConfig.get("HTTPPort") != null)
                Model.FinAgent.AgentInformation.HTTPPort = CiscoVoiceConfig.get("HTTPPort");

            if (CiscoVoiceConfig.get("HTTPURL") != null)
                Model.FinAgent.AgentInformation.HTTPURL = CiscoVoiceConfig.get("HTTPURL");

            if (CiscoVoiceConfig.get("SSL") != null)
                Model.FinAgent.AgentInformation.SSL = Convert.ToBoolean(CiscoVoiceConfig.get("SSL"));

            if (CiscoVoiceConfig.get("XmppConnectionType") != null)
                Model.FinAgent.AgentInformation.XMPPConnectionType = CiscoVoiceConfig.get("XmppConnectionType");

            if (CiscoVoiceConfig.get("HttpConnectionType") != null)
                Model.FinAgent.AgentInformation.HTTPConnectionType = CiscoVoiceConfig.get("HttpConnectionType");

            if (CiscoVoiceConfig.get("Pass") != null)
            {
                Model.FinAgent.AgentInformation.Password = CiscoVoiceConfig.get("Pass");
                if (Model.FinAgent.AgentInformation.Password != "")
                {
                    Password.Password = Model.FinAgent.AgentInformation.Password;
                    SavePasswordCheckBox.IsChecked = true;
                }
            }

            if (CiscoVoiceConfig.get("AutoLogin") != null)
            {
                AutoLoginFunction = bool.Parse(CiscoVoiceConfig.get("AutoLogin"));
                AutoLoginCheckBox.IsChecked = AutoLoginFunction;
            }

            //Check Finesse Side A Parameter
            if (CiscoVoiceConfig.get("FinesseA") != null)
                 Model.FinAgent.AgentInformation.DomainA = CiscoVoiceConfig.get("FinesseA");

            //Check Finesse Side B Parameter
            if (CiscoVoiceConfig.get("FinesseB") != null)
                Model.FinAgent.AgentInformation.DomainB = CiscoVoiceConfig.get("FinesseB");

            CapturePoint.Text = CiscoVoiceConfig.get("CapturePoint", "http://ec2-34-205-57-32.compute-1.amazonaws.com:7011/Genesys/Interaction/CiscoVoice_CP/WebServiceCapturePoint?wsdl");
            _CapturePointUrl = CapturePoint.Text;
            TenantID.Text = CiscoVoiceConfig.get("TenantID", "101");
            _TenantID = TenantID.Text;
            MediaType.Text = CiscoVoiceConfig.get("MediaType", "cisco voice");
            _MediaType = MediaType.Text;
            QueueName.Text = CiscoVoiceConfig.get("QueueName", "Cisco Voice Inbound Queue");
            _QueueName = QueueName.Text;

        }
        private void SaveConfiguration()
        {
            if(Model.FinAgent.AgentInformation.DomainA != null)
                CiscoVoiceConfig.set("FinesseA", Model.FinAgent.AgentInformation.DomainA);
            if (Model.FinAgent.AgentInformation.DomainB != null)
                CiscoVoiceConfig.set("FinesseB", Model.FinAgent.AgentInformation.DomainB);
            if (Model.FinAgent.AgentInformation.XMPPPort != null)
                CiscoVoiceConfig.set("XMPPPort", Model.FinAgent.AgentInformation.XMPPPort);
            if (Model.FinAgent.AgentInformation.XMPPURL != null)
                CiscoVoiceConfig.set("XMPPURL", Model.FinAgent.AgentInformation.XMPPURL);
            if (Model.FinAgent.AgentInformation.HTTPPort != null)
                CiscoVoiceConfig.set("HTTPPort", Model.FinAgent.AgentInformation.HTTPPort);
            if (Model.FinAgent.AgentInformation.HTTPURL != null)
                CiscoVoiceConfig.set("HTTPURL", Model.FinAgent.AgentInformation.HTTPURL);
            if (Model.FinAgent.AgentInformation.SSL)
                CiscoVoiceConfig.set("SSL", Model.FinAgent.AgentInformation.SSL);
            if (Model.FinAgent.AgentInformation.AgentID != null)
                CiscoVoiceConfig.set("AgentID", Model.FinAgent.AgentInformation.AgentID);
            if (Model.FinAgent.AgentInformation.Extension != null)
                CiscoVoiceConfig.set("Ext", Model.FinAgent.AgentInformation.Extension);
            if (Model.FinAgent.AgentInformation.XMPPConnectionType != null)
                CiscoVoiceConfig.set("XmppConnectionType", Model.FinAgent.AgentInformation.XMPPConnectionType);
            if (Model.FinAgent.AgentInformation.HTTPConnectionType != null)
                CiscoVoiceConfig.set("HttpConnectionType", Model.FinAgent.AgentInformation.HTTPConnectionType);

            if (SavePasswordCheckBox.IsChecked == true)
                CiscoVoiceConfig.set("Pass", Model.FinAgent.AgentInformation.Password);

            if (AutoLoginCheckBox.IsChecked == true)
                CiscoVoiceConfig.set("AutoLogin", "true");

            CiscoVoiceConfig.set("CapturePoint", CapturePoint.Text);
            _CapturePointUrl = CapturePoint.Text;
            CiscoVoiceConfig.set("TenantID", TenantID.Text);
            _TenantID = TenantID.Text;
            CiscoVoiceConfig.set("MediaType", MediaType.Text);
            _MediaType = MediaType.Text;
            CiscoVoiceConfig.set("QueueName", QueueName.Text);
            _QueueName = QueueName.Text;
            CiscoVoiceConfig.Save();
        }
        private void CallButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button.Name.Equals("Answer"))
            {
                CurrentStatus.Text = "Answer Call";
                Model.FinAgent.AnswerCall(button.Tag as String);
            }
            else if (button.Name.Equals("Transfer"))
            {
                CurrentStatus.Text = "Direct Transfer";
                //OpenDialPad(button.Tag as String, DialPadType.SSTransfer);
            }
            else if (button.Name.Equals("CTransfer"))
            {
                CurrentStatus.Text = "Transfer Call";
                Model.FinAgent.TransferCall(button.Tag as String);
            }
            else if (button.Name.Equals("Conference"))
            {
                CurrentStatus.Text = "Conference Call";
                //OpenDialPad(button.Tag as String, DialPadType.ConferenceCall);
            }
            else if (button.Name.Equals("Consult"))
            {
                CurrentStatus.Text = "Consult Call";
                //OpenDialPad(button.Tag as String, DialPadType.ConsultTransfer);
            }
            else if (button.Name.Equals("Keypad"))
            {
                CurrentStatus.Text = "Keypad Clicked";
                //OpenDialPad(button.Tag as String, DialPadType.Keypad);
            }
            else if (button.Name.Equals("Hold"))
            {
                CurrentStatus.Text = "Hold Call";
                Model.FinAgent.HoldCall(button.Tag as String);
            }
            else if (button.Name.Equals("Resume"))
            {
                CurrentStatus.Text = "Resume Call";
                Model.FinAgent.ResumeCall(button.Tag as String);
            }
            else if (button.Name.Equals("Release"))
            {
                CurrentStatus.Text = "Release Call";
                if (!Model.FinAgent.ReleaseCall(button.Tag as String))
                {
                    Dialog dialog = Model.FinAgent.FindDialog(button.Tag as String);
                    if (dialog != null)
                        System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.DataBind, (Action)(() => Model.FinAgent.AgentInformation.Dialogs.Remove(dialog)));
                }
            }
        }

        private void AutoLoginCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (AutoLoginCheckBox.IsChecked == true)
            {
                SavePasswordCheckBox.IsChecked = true;
                AutoLoginFunction = true;
            }
            else
                AutoLoginFunction = false;
        }

        public void FireQueueEvent(Queue queue)
        {
            //throw new NotImplementedException();
        }
    }
   
}
