using System;
using System.Windows.Input;
using FinesseClient;
using FinesseClient.Model;
using Genesyslab.Desktop.Modules.Core.Model.Interactions;

namespace Genesyslab.Desktop.Modules.CiscoVoice.CiscoAgent
{
	/// <summary>
	/// The behabior of the MySampleView class
	/// </summary>
	public interface ICiscoVoiceModel
	{
		/// <summary>
		/// Gets or sets the header to set in the parent view.
		/// </summary>
		/// <value>The header.</value>
		string Header { get; set; }

		/// <summary>
		/// Gets or sets the case.
		/// </summary>
		/// <value>The case.</value>
		ICase Case { get; set; }
        FinAgent FinAgent { get; set; }
        Dialog Dialog { get; set; }
        bool ? ShowCallWaiting { get; set; }
        bool? ShowCallButtons { get; set; }
        void LoadInteraction(string dialogId);
    }
}
