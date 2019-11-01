using Genesyslab.Desktop.Infrastructure;
using Genesyslab.Desktop.Modules.Windows.Common.DimSize;

namespace Genesyslab.Desktop.Modules.CiscoVoice.CiscoAgent
{
	/// <summary>
	/// Interface matching the MySampleView view
	/// </summary>
	public interface ICiscoTabView : IView,IMin
	{
		/// <summary>
		/// Gets or sets the model.
		/// </summary>
		/// <value>The model.</value>
		ICiscoTabModel Model { get; set; }
	}
}
