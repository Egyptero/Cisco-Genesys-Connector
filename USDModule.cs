using Genesyslab.Desktop.Infrastructure;
using Genesyslab.Desktop.Infrastructure.Commands;
using Genesyslab.Desktop.Infrastructure.DependencyInjection;
using Genesyslab.Desktop.Infrastructure.ViewManager;
using Genesyslab.Desktop.Modules.CiscoVoice.CiscoAgent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genesyslab.Desktop.Modules.CiscoVoice
{
    /// <summary>
    /// This class is a sample module which shows several ways of customization
    /// </summary>
    public class USDModule : IModule
    {
        readonly IObjectContainer container;
        readonly IViewManager viewManager;
        readonly ICommandManager commandManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CiscoVoiceModule"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="viewManager">The view manager.</param>
        /// <param name="commandManager">The command manager.</param>
        public USDModule(IObjectContainer container, IViewManager viewManager, ICommandManager commandManager)
        {
            this.container = container;
            this.viewManager = viewManager;
            this.commandManager = commandManager;
        }

        /// <summary>
        /// Initializes the module.
        /// </summary>
        public void Initialize()
        {
            // Add a view in the right panel in the interaction window

            // Here we register the view (GUI) "InstGenesysView" and its behavior counterpart "InstGenesysModel"
            container.RegisterType<IUSDModel, USDModel>();
            container.RegisterType<IUSDView, USDView>();

            // Put the MySample view in the region "InteractionWorksheetRegion" //InteractionDetailsRegion
            //viewManager.ViewsByRegionName["InteractionsBundleRegion"].Clear();
            viewManager.ViewsByRegionName["InteractionsBundleRegion"].Insert(0,
                new ViewActivator() { ViewType = typeof(IUSDView), ViewName = "USD", ActivateView = true, Condition = USDModel.USDViewModelCondition }
            );
        }
    }

}
