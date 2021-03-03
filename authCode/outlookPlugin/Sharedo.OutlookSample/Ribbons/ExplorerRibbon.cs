using Microsoft.Office.Tools.Ribbon;
using Sharedo.OutlookSample.Forms.Configuration;
using Sharedo.OutlookSample.Forms.Debug;
using Sharedo.OutlookSample.Services.Models;
using Sharedo.OutlookSample.Util;

namespace Sharedo.OutlookSample.Ribbons
{
    public partial class ExplorerRibbon
    {
        private void ExplorerRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            // Subscribe to state changes for sharedo integration
            App.TokenManager.SharedoLinkStateChanged += SharedoLinkStateChanged;

            // Setup the ribbon based on current config
            SharedoLinkStateChanged(App.TokenManager.State);
        }

        private void SharedoLinkStateChanged(TokenStatus state)
        {
            // ReSharper disable once ReplaceWithSingleAssignment.False
            var enabled = false;
            if (state == TokenStatus.Success) enabled = true;

            _btnDebug.Enabled = enabled;
        }

        private void _btnConfiguration_Click(object sender, RibbonControlEventArgs e)
        {
            var configWindow = new ConfigurationWindow(App.TokenManager);
            configWindow.OpenDialogAsChildOfActiveWindow();
        }

        private void _btnDebug_Click(object sender, RibbonControlEventArgs e)
        {
            var debugWindow = new DebugWindow(App.TokenManager);
            debugWindow.OpenDialogAsChildOfActiveWindow();
        }
    }
}
