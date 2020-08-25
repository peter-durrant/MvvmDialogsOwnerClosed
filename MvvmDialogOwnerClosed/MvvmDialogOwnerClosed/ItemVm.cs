using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using MvvmDialogs;
using MvvmDialogs.FrameworkDialogs.MessageBox;

namespace MvvmDialogOwnerClosed
{
    public class ItemVm : INotifyPropertyChanged

    {
        private readonly IDialogService _dialogService;

        public ItemVm(IDialogService dialogService)
        {
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));

            OpenDialogCommand = new RelayCommand(OpenDialog);
        }

        public ICommand OpenDialogCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OpenDialog()
        {
            var messageBoxSettings = new MessageBoxSettings
            {
                Caption = "Dialog",
                MessageBoxText = "Press OK",
                Button = MessageBoxButton.OK
            };

            _dialogService.ShowMessageBox(this, messageBoxSettings);
        }
    }
}