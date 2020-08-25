using System.Windows;

namespace MvvmDialogOwnerClosed
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new ContainerVm();
        }
    }
}