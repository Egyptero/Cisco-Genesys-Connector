using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Genesyslab.Desktop.Modules.CiscoVoice.USDIntegration
{
    public sealed class WSEventListener
    {
        private static readonly WSEventListener instance = new WSEventListener();

        static WSEventListener()
        {

        }
        private WSEventListener()
        {

        }
        public static WSEventListener Instance { get { return instance; } }
        public void FireWSEvent(Dictionary<string, string> data, EventType eventType, EventDirection eventDirection)
        {
            try
            {
               
                string fileName = string.Empty;
                USDInteraction usdInteraction = new USDInteraction();
                if (data.ContainsKey("CallVar4"))
                    usdInteraction.CustomerCIC = data["CallVar4"];
                if (data.ContainsKey("InetractionID"))
                {
                    usdInteraction.InteractionID = data["InetractionID"];
                    fileName = usdInteraction.InteractionID;
                }
                if (data.ContainsKey("CaseID"))
                    usdInteraction.CaseID = data["CaseID"];
                if (data.ContainsKey("AgentID"))
                    usdInteraction.AgentID = data["AgentID"];
                if (data.ContainsKey("AgentExt"))
                    usdInteraction.AgentExt = data["AgentExt"];
                if (data.ContainsKey("FromAddress"))
                    usdInteraction.FromAddress = data["FromAddress"];
                if (data.ContainsKey("ToAddress"))
                    usdInteraction.ToAddress = data["ToAddress"];
                if (data.ContainsKey("DialNumber"))
                    usdInteraction.DialNumber = data["DialNumber"];
                if (data.ContainsKey("CallStart"))
                    usdInteraction.DialNumber = data["CallStart"];

                switch (eventType)
                {
                    case EventType.CALL:
                        usdInteraction.EventType = "CALL";
                        fileName += "_CALL";
                        break;
                    case EventType.CHAT:
                        usdInteraction.EventType = "CHAT";
                        fileName += "_CHAT";
                   
                        break;
                    case EventType.EMAIL:
                        usdInteraction.EventType = "EMAIL";
                        fileName += "_EMAIL";
                        break;
                }

                switch (eventDirection)
                {
                    case EventDirection.START:
                        usdInteraction.EventDirection = "START";
                        fileName += "_START.json";
                
                        break;
                    case EventDirection.END:
                        usdInteraction.EventDirection = "END";
                        fileName += "_END.json";
                        break;
                }
                // We should Write here
                string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\";

                string json = JsonConvert.SerializeObject(usdInteraction);
      
                //write string to file
                System.IO.File.WriteAllText(folderPath + fileName, json);
         
            }
            catch (Exception)
            {

            }
        }

    }
}
