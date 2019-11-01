using System;
using System.Windows.Controls;
using Genesyslab.Desktop.Modules.Windows.Common.DimSize;
using System.ComponentModel;
using System.Windows;
using Genesyslab.Desktop.Infrastructure.DependencyInjection;
using System.Windows.Threading;
using System.Threading;
using Genesyslab.Desktop.Modules.Core.Model.Interactions;
using Genesyslab.Enterprise.Commons.Collections;
using Genesyslab.Platform.Commons.Protocols;
using Genesyslab.Enterprise.Model.Interaction;
using System.Windows.Input;
using Genesyslab.Desktop.Modules.CiscoVoice.FinesseClient;
using FinesseClient.Model;
using FinesseClient.View;
using FinesseClient.Common;
using System.Collections.Generic;
using Genesyslab.Desktop.Infrastructure.Commands;
using Genesyslab.Desktop.WPFCommon;
using static FinesseClient.Model.Dialog.MediaPropertiesClass;
using Genesyslab.Desktop.Modules.CiscoVoice.USDIntegration;

namespace Genesyslab.Desktop.Modules.CiscoVoice.CiscoAgent
{
	/// <summary>
	/// Interaction logic for MySampleView.xaml
	/// </summary>
	public partial class CiscoVoiceView : UserControl, ICiscoVoiceView, FinView
	{

        readonly IObjectContainer container;
        string _callId;
        string _interactionId;
        string _caseId;

		public CiscoVoiceView(ICiscoVoiceModel ciscoVoiceModel, IObjectContainer container )
		{
            Model = ciscoVoiceModel;
            
            Model.FinAgent = FinesseEventListener.Instance.FinAgent;
			this.container = container;
            var interactionManager = container.Resolve<IInteractionManager>();

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
          
            if (interaction != null)
            {
                

                if (interaction.State == InteractionStateType.Connected)
                {
                    KeyValueCollection userData = interaction.ExtractAttachedData();

                    if (userData != null && Model != null && userData.Contains("CiscoCallID"))
                    {
                        string callId = userData["CiscoCallID"].ToString();
                      //  MessageBox.Show(userData["CiscoCallID"].ToString());
                        if (callId != null)
                        {
                            _callId = callId;
                            _interactionId = interaction.InteractionId;
                            _caseId = interaction.CaseId;
                            if (FinesseEventListener.Instance.CiscoVoices == null)
                                FinesseEventListener.Instance.CiscoVoices = new System.Collections.Generic.Dictionary<string, FinView>();
                            if(!FinesseEventListener.Instance.CiscoVoices.ContainsKey(_callId))
                                FinesseEventListener.Instance.CiscoVoices.Add(_callId, this);
                            Model.LoadInteraction(callId);
                            Model.FinAgent.AnswerCall(callId); // Auto Answer for the call. Mamdouh Temp
                        }
                    }

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
		#region CiscoVoiceView Members

		/// <summary>
		/// Gets or sets the model.
		/// </summary>
		/// <value>The model.</value>
		public ICiscoVoiceModel Model
		{
			get { return this.DataContext as ICiscoVoiceModel; }
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
        private void CallButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            bool outcome = true;
            if (button.Name.Equals("Answer"))
            {
                Model.ShowCallWaiting = true;
                outcome = Model.FinAgent.AnswerCall(button.Tag as String);
            }
            else if (button.Name.Equals("Transfer"))
            {
                if (NumberToDial.Text == null || NumberToDial.Text == "")
                    FireErrorMessage("Dial Number can not be empty, please set the dial numbers");
                Model.ShowCallWaiting = true;
                outcome = Model.FinAgent.SSTransferCall(button.Tag as String, NumberToDial.Text);
            }
            else if (button.Name.Equals("Hold"))
            {
                Model.ShowCallWaiting = true;
                outcome = Model.FinAgent.HoldCall(button.Tag as String);
            }
            else if (button.Name.Equals("Resume"))
            {
                Model.ShowCallWaiting = true;
                outcome = Model.FinAgent.ResumeCall(button.Tag as String);
            }
            else if (button.Name.Equals("Release"))
            {
                Model.ShowCallWaiting = true;
                outcome = Model.FinAgent.ReleaseCall(button.Tag as String);
                if (!outcome)
                {
                    Dialog dialog = Model.FinAgent.FindDialog(button.Tag as String);
                    if (dialog != null)
                        Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.DataBind, (Action)(() => Model.FinAgent.AgentInformation.Dialogs.Remove(dialog)));
                }
            }

            if (!outcome)
                Model.ShowCallWaiting = false;
        }

        public void FireLoadingMessage(string msg)
        {
        }

        public void FireLoadLoginScreen()
        {
        }

        public void FireErrorMessage(string msg)
        {
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
            try
            {
                if (Model.FinAgent.AgentInformation.MessageEvent != null)
                {
                    if (Model.FinAgent.AgentInformation.MessageEvent.MessageType != null)
                    {
                        if (Model.FinAgent.AgentInformation.MessageEvent.MessageType.Equals("error"))
                        {
                            //FireErrorMessage("Code:" + _finAgent.AgentInformation.MessageEvent.ErrorCode + "," + _finAgent.AgentInformation.MessageEvent.ErrorMsg + " ,Type: " + _finAgent.AgentInformation.MessageEvent.ErrorType);
                            Model.ShowCallWaiting = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Model.FinAgent.FireLogMessage("Application Error during fire new event, ex:" + ex.Message + "\nStack trace\n" + ex.StackTrace);
                Model.FinAgent.FireErrorMessage("Application Error during fire new event");
            }
        }

        public void FireReLoginEvent()
        {
        }

        public void FireDisconnectEvent()
        {
        }

        public void FireCallEvent(Dialog dialog)
        {

            try
            {
                if (Model.Dialog == null)
                    Model.Dialog = Model.FinAgent.AgentInformation.SelectedDialog;
                Dialog locatedDialog = null;
                if (Model.Dialog != null)
                    locatedDialog = Model.FinAgent.FindDialog(Model.Dialog.ID);
                if (locatedDialog == null)
                {
                    FinesseEventListener.Instance.CiscoVoices.Remove(_callId);
                    var interactionManager = container.Resolve<IInteractionManager>();
                    if (interactionManager != null)
                    {
                        //Call Terminate Event is fired and interaction will be closed. View will be unregistered now
                        var interaction = interactionManager.GetInteractionById(_interactionId);
                        if (interaction != null)
                        {
                            IChainOfCommand Command = container.Resolve<ICommandManager>().
                                         GetChainOfCommandByName("CiscoVoiceStopProcessingCommand");
                            Utils.ExecuteAsynchronousCommand(Command,
                               new Dictionary<string, object>() { { "CommandParameter", interaction } });
                            interactionManager.CloseInteraction(_interactionId);
                            interactionManager.CloseCase(_caseId);
                        }
                    }
                }
                else
                {
                    Model.ShowCallWaiting = false;
                }

                bool isCallStart = false;
                bool isCallEnd = false;

                if (dialog.DialogEvent != null && dialog.State != null)
                {
                    if (dialog.DialogEvent.Equals("POST") && dialog.State.Equals("ALERTING")) // Call Ringing Event
                    {
                        Model.FinAgent.FireLogMessage("New Call started : Call From: " + dialog.FromAddress + " and Call To: " + dialog.ToAddress + " and dialog status is : " + dialog.State);
                        isCallStart = true;
                    }
                    else if (dialog.DialogEvent.Equals("DELETE") && dialog.State.Equals("ACTIVE")) // Call Terminated and call will be Transferred
                    {
                        Model.FinAgent.FireLogMessage("Call Termination event as you released the call : Call From: " + dialog.FromAddress + " and Call To: " + dialog.ToAddress);
                        isCallEnd = true;
                    }
                    else if (dialog.DialogEvent.Equals("DELETE") && dialog.State.Equals("DROPPED")) // Call Terminated and caller hangup
                    {
                        if (!dialog.MediaProperties.DNIS.Equals("8999") && !dialog.MediaProperties.DNIS.Equals("8999"))
                        {
                            Model.FinAgent.FireLogMessage("Call Termination event and caller dropped the call : Call From: " + dialog.FromAddress + " and Call To: " + dialog.ToAddress);
                            isCallEnd = true;
                        }
                    }
                    else if (dialog.DialogEvent.Equals("DELETE")) // Call Terminated and caller hangup for any other reason
                    {
                        if (!dialog.MediaProperties.DNIS.Equals("8999") && !dialog.MediaProperties.DNIS.Equals("8999"))
                        {
                            Model.FinAgent.FireLogMessage("Call Termination event  : Call From: " + dialog.FromAddress + " and Call To: " + dialog.ToAddress + ", Call State is: " + dialog.State);
                            isCallEnd = true;
                        }
                    }
                    else if (dialog.DialogEvent.Equals("RunningCall") && !dialog.State.Equals("DROPPED") && !dialog.State.Equals("FAILED")) // We found running call
                    {
                        if (!dialog.MediaProperties.DNIS.Equals("8999") && !dialog.MediaProperties.DNIS.Equals("8999"))
                        {
                            Model.FinAgent.FireLogMessage("Call running event and call still active : Call From: " + dialog.FromAddress + " and Call To: " + dialog.ToAddress);
                            isCallStart = true;
                        }
                    }
                }
                else
                {
                    foreach (Dialog.Participant participant in dialog.Participants)
                    {
                        if (participant.MediaAddress.Equals(Model.FinAgent.AgentInformation.Extension)) //Checking my status
                        {
                            if (participant.State.Equals("DROPPED")) // call is not active , and this is call terminate event
                            {
                                Model.FinAgent.FireLogMessage("We received message event without event discription. Seems system just started while call was active, we will terminate the call without firing end event");
                            }
                            else if (participant.State.Equals("INITIATING"))
                            {
                                Model.FinAgent.FireLogMessage("We received message event without event discription. Seems system just started while call is active, your status is: " + participant.State);
                            }
                        }
                    }
                }

                if (isCallStart)
                {
                    try
                    {
                    }
                    catch (Exception e)
                    {
                        Model.FinAgent.FireErrorMessage("Application: Error in call initiation in fire call event" + e.Message);
                        Model.FinAgent.FireDebugLogMessage("Application: Error in call initiation in fire call event\n" + e.StackTrace);
                        return;
                    }
                }

                if (isCallEnd)
                {
                    try
                    {
                        FinesseEventListener.Instance.CiscoVoices.Remove(_callId);
                        var interactionManager = container.Resolve<IInteractionManager>();
                        if (interactionManager != null)
                        {
                            //Call Terminate Event is fired and interaction will be closed. View will be unregistered now
                            var interaction = interactionManager.GetInteractionById(_interactionId);
                            if (interaction != null)
                            {
                                IChainOfCommand Command = container.Resolve<ICommandManager>().
                                             GetChainOfCommandByName("CiscoVoiceStopProcessingCommand");
                                Utils.ExecuteAsynchronousCommand(Command,
                                   new Dictionary<string, object>() { { "CommandParameter", interaction } });
                                interactionManager.CloseInteraction(_interactionId);
                                interactionManager.CloseCase(_caseId);
                            }
                        }


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


                        Dictionary<string, string> data = new Dictionary<string, string>();
                        data.Add("CallVar4", CICFormatted);
                        data.Add("InetractionID", dialog.ID);
                        data.Add("CaseID", "NA");
                        data.Add("AgentID", FinesseEventListener.Instance.FinAgent.AgentInformation.AgentID);
                        data.Add("AgentExt", FinesseEventListener.Instance.FinAgent.AgentInformation.Extension);
                        data.Add("FromAddress", dialog.FromAddress);
                        data.Add("ToAddress", dialog.ToAddress);
                        // data.Add("DialNumber", dialog.MediaProperties.DialedNumber);
                        data.Add("DialNumber", "8016");

                        //We should popup interaction ID now
                        WSEventListener.Instance.FireWSEvent(data, EventType.CALL, EventDirection.END);
                    }
                    catch (Exception e)
                    {
                        Model.FinAgent.FireErrorMessage("Application: Error in call termination during fire call event" + e.Message);
                        Model.FinAgent.FireDebugLogMessage("Application: Error in call termination in fire call event\n" + e.StackTrace);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Model.FinAgent.FireLogMessage("Application Error during fire call event, ex:" + ex.Message + "\nStack trace\n" + ex.StackTrace + "\nMore\n" + ex.ToString());
                Model.FinAgent.FireErrorMessage("Application Error during fire call event");
            }

        }

        public void FireQueueEvent(Queue queue)
        {
            //throw new NotImplementedException();
        }
    }
}
