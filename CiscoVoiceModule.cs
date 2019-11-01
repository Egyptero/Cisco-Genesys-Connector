using Genesyslab.Desktop.Infrastructure.Commands;
using Genesyslab.Desktop.Infrastructure.DependencyInjection;
using Genesyslab.Desktop.Infrastructure.ViewManager;
using Genesyslab.Desktop.Infrastructure;
using Genesyslab.Desktop.Modules.CiscoVoice.CiscoAgent;
using System.Collections.Generic;
using Genesyslab.Desktop.Modules.CiscoVoice.CustomCommand;
//using Microsoft.Practices.Composite.Modularity;

namespace Genesyslab.Desktop.Modules.CiscoVoice
{
	/// <summary>
	/// This class is a sample module which shows several ways of customization
	/// </summary>
	public class CiscoVoiceModule : IModule
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
		public CiscoVoiceModule(IObjectContainer container, IViewManager viewManager, ICommandManager commandManager)
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
            container.RegisterType<ICiscoVoiceModel, CiscoVoiceModel>();
            container.RegisterType<ICiscoVoiceView, CiscoVoiceView>();

            // Put the MySample view in the region "InteractionWorksheetRegion" //InteractionDetailsRegion
            //viewManager.ViewsByRegionName["InteractionsBundleRegion"].Clear();
            viewManager.ViewsByRegionName["InteractionsBundleRegion"].Insert(0,
				new ViewActivator() { ViewType = typeof(ICiscoVoiceView), ViewName = "CiscoVoice", ActivateView = true, Condition = CiscoVoiceModel.CiscoVoiceViewModelCondition }
			);
            //Register The commands
            commandManager.AddCommandToChainOfCommand("CiscoVoiceStopProcessingCommand",
                        new List<CommandActivator>() { new CommandActivator() {
                CommandType = typeof(CiscoVoiceStopProcessingCommand) ,
                Name="StopProcessing" } });
        }
	}
}
