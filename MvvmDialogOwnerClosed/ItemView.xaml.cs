using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using MvvmDialogs;

namespace MvvmDialogOwnerClosed
{
    public partial class ItemView : UserControl
    {
        public ItemView()
        {
            InitializeComponent();

            Unloaded += ItemView_Unloaded;
        }

        private void ItemView_Unloaded(object sender, RoutedEventArgs e)
        {
            Debug.Assert(DialogServiceViews.GetIsRegistered(this));

            // Un-register view when an instance is unloaded
            DialogServiceViews.SetIsRegistered(this, false);

            Debug.Assert(!DialogServiceViews.GetIsRegistered(this));
        }
    }
}