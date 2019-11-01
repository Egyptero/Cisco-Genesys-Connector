using FinesseClient;
using FinesseClient.Common;
using FinesseClient.Model;
using FinesseClient.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Genesyslab.Desktop.Modules.CiscoVoice.FinesseClient
{
    public sealed class FinesseEventListener : FinView
    {
        private static readonly FinesseEventListener instance = new FinesseEventListener();
        private FinView ciscoTab;
        private Dictionary<String, FinView> ciscoVoices;
        private FinAgent finAgent;

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static FinesseEventListener()
        {
        }

        private FinesseEventListener()
        {
        }

        public static FinesseEventListener Instance
        {
            get
            {
                return instance;
            }
        }

        public FinAgent FinAgent { get { return finAgent; } set { finAgent = value; finAgent.FinView = Instance; } }
        public FinView CiscoTab { get { return ciscoTab; } set { ciscoTab = value; } }
        public Dictionary<String, FinView> CiscoVoices { get {return ciscoVoices; } set { ciscoVoices = value ; } }
        public void FireCallEvent(Dialog dialog)
        {
            if (ciscoTab != null)
                ciscoTab.FireCallEvent(dialog);
            if(ciscoVoices != null)
            {
                foreach (FinView ciscoVoice in ciscoVoices.Values)
                    ciscoVoice.FireCallEvent(dialog);
            }
        }

        public void FireDisconnectEvent()
        {
            if (ciscoTab != null)
                ciscoTab.FireDisconnectEvent();
            if (ciscoVoices != null)
            {
                foreach (FinView ciscoVoice in ciscoVoices.Values)
                    ciscoVoice.FireDisconnectEvent();
            }
        }

        public void FireErrorMessage(string msg)
        {
            if (ciscoTab != null)
                ciscoTab.FireErrorMessage(msg);
            if (ciscoVoices != null)
            {
                foreach (FinView ciscoVoice in ciscoVoices.Values)
                    ciscoVoice.FireErrorMessage(msg);
            }
        }

        public void FireLoadingMessage(string msg)
        {
            if (ciscoTab != null)
                ciscoTab.FireLoadingMessage(msg);
            if (ciscoVoices != null)
            {
                foreach (FinView ciscoVoice in ciscoVoices.Values)
                    ciscoVoice.FireLoadingMessage(msg);
            }
        }

        public void FireLoadLoginScreen()
        {
            if (ciscoTab != null)
                ciscoTab.FireLoadLoginScreen();
            if (ciscoVoices != null)
            {
                foreach (FinView ciscoVoice in ciscoVoices.Values)
                    ciscoVoice.FireLoadLoginScreen();
            }
        }

        public void FireLogMessage(string msg)
        {
            if (ciscoTab != null)
                ciscoTab.FireLogMessage(msg);
            if (ciscoVoices != null)
            {
                foreach (FinView ciscoVoice in ciscoVoices.Values)
                    ciscoVoice.FireLogMessage(msg);
            }
        }

        public void FireNewEvent()
        {
            if (ciscoTab != null)
                ciscoTab.FireNewEvent();
            if (ciscoVoices != null)
            {
                foreach (FinView ciscoVoice in ciscoVoices.Values)
                    ciscoVoice.FireNewEvent();
            }
        }

        public void FireQueueEvent(Queue queue)
        {
            //throw new NotImplementedException();
        }

        public void FireReLoginEvent()
        {
            if (ciscoTab != null)
                ciscoTab.FireReLoginEvent();
            if (ciscoVoices != null)
            {
                foreach (FinView ciscoVoice in ciscoVoices.Values)
                    ciscoVoice.FireReLoginEvent();
            }
        }

        public Screen GetLocation()
        {
            return Screen.MainGrid;
        }

        public void SetContext(IModel model, FinView finView)
        {
        }
    }
}
