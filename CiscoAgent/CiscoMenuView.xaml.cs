using Genesyslab.Desktop.Infrastructure.DependencyInjection;
using Genesyslab.Desktop.Modules.Windows.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Genesyslab.Desktop.Modules.CiscoVoice.CiscoAgent
{
    /// <summary>
    /// Interaction logic for CiscoMenuView.xaml
    /// </summary>
    public partial class CiscoMenuView : MenuItem,ICiscoMenuView
    {
        readonly IObjectContainer container;
        readonly IViewEventManager viewEventManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="MySampleMenuView"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public CiscoMenuView(IObjectContainer container, IViewEventManager viewEventManager)
        {
            this.container = container;
            this.viewEventManager = viewEventManager;

            InitializeComponent();

            Width = Double.NaN;
            Height = Double.NaN;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Cisco Voice is Clicked");
            viewEventManager.Publish(new GenericEvent()
            {
                Target = GenericContainerView.ContainerView,
                Context = "ToolbarWorkplace",
                Action = new GenericAction[]
                    {
                        new GenericAction ()
                        {
                            Action = ActionGenericContainerView.ActivateThisPanel,
                            Parameters = new object[] { "CiscoTab" }
                        }
                    }
            });

            // Show and active the MyWorkplace view in the ToolbarWorksheet region
            viewEventManager.Publish(new GenericEvent()
            {
                Target = GenericContainerView.ContainerView,
                Context = "ToolbarWorksheet",
                Action = new GenericAction[]
                    {
                        new GenericAction ()
                        {
                            Action = ActionGenericContainerView.ShowHidePanelRight,
                            Parameters = new object[] { Visibility.Visible, "MyWorkplaceContainerView" }
                        },
                        new GenericAction ()
                        {
                            Action = ActionGenericContainerView.ActivateThisPanel,
                            Parameters = new object[] { "MyWorkplaceContainerView" }
                        }
                    }
            });

        }
    }
}
