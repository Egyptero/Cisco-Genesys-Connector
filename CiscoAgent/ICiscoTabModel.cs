using System;
using System.Windows.Input;
using FinesseClient;
using Genesyslab.Desktop.Modules.Core.Model.Interactions;

namespace Genesyslab.Desktop.Modules.CiscoVoice.CiscoAgent
{
	/// <summary>
	/// The behabior of the MySampleView class
	/// </summary>
	public interface ICiscoTabModel
	{
		/// <summary>
		/// Gets or sets the header to set in the parent view.
		/// </summary>
		/// <value>The header.</value>
		string Header { get; set; }
        FinAgent FinAgent { get; set; }

    }
}
