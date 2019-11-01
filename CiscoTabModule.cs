using Genesyslab.Desktop.Infrastructure.Commands;
using Genesyslab.Desktop.Infrastructure.DependencyInjection;
using Genesyslab.Desktop.Infrastructure.ViewManager;
using Genesyslab.Desktop.Infrastructure;
using Genesyslab.Desktop.Modules.CiscoVoice.CiscoAgent;
using Genesyslab.Desktop.Modules.Windows.Event;
//using Microsoft.Practices.Composite.Modularity;

namespace Genesyslab.Desktop.Modules.CiscoVoice
{
	/// <summary>
	/// This class is a sample module which shows several ways of customization
	/// </summary>
	public class CiscoTabModule : IModule
	{
        readonly IObjectContainer container;
        readonly IViewManager viewManager;
        readonly ICommandManager commandManager;
        readonly IViewEventManager viewEventManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtensionSampleModule"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="viewManager">The view manager.</param>
        /// <param name="commandManager">The command manager.</param>
        public CiscoTabModule(IObjectContainer container, IViewManager viewManager, ICommandManager commandManager, IViewEventManager viewEventManager)
        {
            this.container = container;
            this.viewManager = viewManager;
            this.commandManager = commandManager;
            this.viewEventManager = viewEventManager;
        }

		/// <summary>
		/// Initializes the module.
		/// </summary>
		public void Initialize()
		{


            // Here we register the view (GUI) "InstGenesysView" and its behavior counterpart "InstGenesysModel"
            container.RegisterType<ICiscoTabModel, CiscoTabModel>();
            container.RegisterType<ICiscoTabView, CiscoTabView>();

            // Put the MySample view in the region "InteractionWorksheetRegion" //InteractionDetailsRegion
            viewManager.ViewsByRegionName["ToolbarWorkplaceRegion"].Add(
                new ViewActivator() { ViewType = typeof(ICiscoTabView), ViewName = "CiscoTab"}
           );
            //viewManager.ViewsByRegionName["ToolbarWorkplaceRegion"].Insert(0,
            //    new ViewActivator() { ViewType = typeof(ICiscoTabView), ViewName = "CiscoAgent" });

            // Here we register the view (GUI) "ICiscoMenuView"
            container.RegisterType<ICiscoMenuView, CiscoMenuView>();

            // Put the CiscoMenuView view in the region "WorkspaceMenuRegion" (The Workspace toggle button in the main toolbar)
            viewManager.ViewsByRegionName["WorkspaceMenuRegion"].Insert(0,
                new ViewActivator() { ViewType = typeof(ICiscoMenuView), ViewName = "CiscoMenu", ActivateView = true });
        }
    }
}
