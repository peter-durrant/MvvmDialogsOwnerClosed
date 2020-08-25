using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MvvmDialogs;

namespace MvvmDialogOwnerClosed
{
    public class ContainerVm : INotifyPropertyChanged
    {
        private readonly DialogService _dialogService;

        public ContainerVm()
        {
            _dialogService = new DialogService();
            Items = new ObservableCollection<ItemVm>
            {
                new ItemVm(_dialogService),
                new ItemVm(_dialogService),
                new ItemVm(_dialogService)
            };

            AddItemCommand = new RelayCommand(AddItem);
        }

        public ICommand AddItemCommand { get; }
        public ObservableCollection<ItemVm> Items { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        private void AddItem()
        {
            Items.Add(new ItemVm(_dialogService));
            if (Items.Count > 5) Items.RemoveAt(0);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}