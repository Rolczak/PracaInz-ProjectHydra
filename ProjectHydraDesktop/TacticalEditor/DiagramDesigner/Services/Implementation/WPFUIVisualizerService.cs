using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Threading;
using ProjectHydraDesktop.TacticalEditor.DiagramDesigner.Models;

namespace ProjectHydraDesktop.TacticalEditor
{
    public class WPFUIVisualizerService : IUIVisualizerService
    {


        #region Public Methods
        /// <summary>
        /// This method displays a modal dialog associated with the given key.
        /// </summary>
        /// <param name="dataContextForPopup">Object state to associate with the dialog</param>
        /// <returns>True/False if UI is displayed.</returns>
        public bool? ShowUnitEditDialog(ref Dictionary<int, string> colorTypes, ref Integer selected)
        {
            if (!System.Windows.Interop.BrowserInteropHelper.IsBrowserHosted)
            {
                PopupFriendlyUnitWindow win = new PopupFriendlyUnitWindow(ref colorTypes, ref selected);
                win.Owner = Application.Current.MainWindow;
                if (win != null)
                {
                    return win.ShowDialog();
                }
            }
            else
            {
                return false;
            }
            return false;
        }
        public bool? ShowFriendlyUnitEditDialog(ref FriendlyUnitModel unitModel)
        {
            PopupFriendlyUnitEditWindow win = new PopupFriendlyUnitEditWindow(ref unitModel);
            win.Owner = Application.Current.MainWindow;
            if (win != null)
            {
                return win.ShowDialog();
            }
            return false;
        }

        #endregion


    }
}
