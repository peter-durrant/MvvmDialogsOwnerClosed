using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace MvvmDialogOwnerClosed
{
    public partial class ContainerView : UserControl
    {
        public ContainerView()
        {
            InitializeComponent();
        }

        private void UpdateInvocationList()
        {
            var mainWindow = Application.Current.MainWindow;

            InvocationListReport.Text = "";
            const BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance;

            var propertyInfo = typeof(Window).GetProperty("Events", bindingFlags);
            var eventHandlerList = propertyInfo.GetValue(mainWindow, new object[] { }) as EventHandlerList;
            var listEntryType = typeof(EventHandlerList).GetNestedType("ListEntry", bindingFlags);

            var headFieldInfo = typeof(EventHandlerList).GetField("head", bindingFlags);

            var listEntry = headFieldInfo.GetValue(eventHandlerList);
            while (listEntry != null)
            {
                var nextFieldInfo = listEntryType.GetField("next", bindingFlags);
                var handlerFieldInfo = listEntryType.GetField("handler", bindingFlags);

                var next = nextFieldInfo.GetValue(listEntry);
                var handler = handlerFieldInfo.GetValue(listEntry);
                var handlerDelegate = handler as Delegate;

                InvocationListReport.Text += $"Method name: {handlerDelegate.Method.Name}\r\n";

                foreach (var d in handlerDelegate.GetInvocationList())
                    InvocationListReport.Text += $"  Method name: {d.Method.Name}\r\n";

                listEntry = next;
            }
        }

        private void UpdateInvocationList_OnClick(object sender, RoutedEventArgs e)
        {
            UpdateInvocationList();
        }
    }
}