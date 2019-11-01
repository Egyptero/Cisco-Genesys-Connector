using Genesyslab.Desktop.Infrastructure;
using Genesyslab.Desktop.Infrastructure.Commands;
using Genesyslab.Desktop.Infrastructure.DependencyInjection;
using Genesyslab.Desktop.Modules.OpenMedia.Model.Interactions;
using Genesyslab.Enterprise.Commons.Collections;
using Genesyslab.Enterprise.Extensions;
using Genesyslab.Enterprise.Model.ServiceModel;
using Genesyslab.Enterprise.Services;
using Genesyslab.Platform.Commons.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Genesyslab.Desktop.Modules.CiscoVoice.CustomCommand
{
    // file CustomWorkitemCommand.cs
    public class CiscoVoiceCommand : IElementOfCommand
    {
        protected readonly IObjectContainer container;
        protected ILogger log;
        protected IOpenMediaService openMediaService;
        protected const int timeout = 10000;
        public CiscoVoiceCommand(IObjectContainer container)
        {
            this.container = container;
            this.log = container.Resolve<ILogger>();
            this.log = log.CreateChildLogger("CustomWorkitemCommand");
            IEnterpriseServiceProvider enterpriseServiceProvider = container.Resolve<IEnterpriseServiceProvider>();
            this.openMediaService = enterpriseServiceProvider.Resolve<IOpenMediaService>("openmediaService");
        }
        public virtual string Name { get; set; }
        #region IElementOfCommand Members
        public virtual bool Execute(IDictionary<string, object> parameters, IProgressUpdater progressUpdater)
        {
            return false;
        }
        #endregion
    }
    // file CustomWorkitemCommand.cs
    public class CiscoVoiceStopProcessingCommand : CiscoVoiceCommand
    {
        public CiscoVoiceStopProcessingCommand(IObjectContainer container) : base(container) { }
        public override bool Execute(IDictionary<string, object> parameters, IProgressUpdater progressUpdater)
        {
            log.Info("CustomWorkitemStopProcessingCommand");
            IInteractionOpenMedia interactionOpenMedia =
               parameters.TryGetValue("CommandParameter") as IInteractionOpenMedia;
            try
            {
                if ((interactionOpenMedia != null)
                && (interactionOpenMedia.EntrepriseOpenMediaInteractionCurrent != null))
                {
                    if (!interactionOpenMedia.EntrepriseOpenMediaInteractionCurrent.IsInWorkflow)
                    {
                        return false;
                    }
                    openMediaService.StopProcessing(interactionOpenMedia.EntrepriseOpenMediaInteractionCurrent,
                       parameters.TryGetValue("Reason") as KeyValueCollection,
                       parameters.TryGetValue("Extensions") as KeyValueCollection);

                }
                return false;
            }
            catch (Exception exp)
            {
                log.Error("CustomWorkitemStopProcessingCommand StopProcessing, Exception "
                                 + interactionOpenMedia, exp);
                return true;
            }
        }
    }
}
