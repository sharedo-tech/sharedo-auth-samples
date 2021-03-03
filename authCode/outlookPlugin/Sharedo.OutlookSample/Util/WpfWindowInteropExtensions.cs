using System.Windows;
using System.Windows.Interop;

namespace Sharedo.OutlookSample.Util
{
    public static class WpfWindowInteropExtensions
    {
        public static bool? OpenDialogAsChildOfActiveWindow(this Window window)
        {
            var activeWindow = Globals.ThisAddIn.Application.ActiveWindow();
            var outlookHwnd = new OfficeWin32Window(activeWindow).Handle;

            var interop = new WindowInteropHelper(window);
            interop.Owner = outlookHwnd;

            return window.ShowDialog();
        }
    }
}
