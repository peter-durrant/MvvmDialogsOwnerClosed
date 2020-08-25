# MvvmDialogsOwnerClosed

This project demonstrates a memory leak in a particular usage scenario when using [MvvmDialogs](https://github.com/FantasticFiasco/mvvm-dialogs).

## Scenario

1. Items are added to a list and older items are removed to maintain the list size.
1. Each item has a view model [ItemViewVm.cs](MvvmDialogOwnerClosed/ItemVm.cs) that uses the `DialogService` to show a dialog.
1. Each item has a view [ItemView.xaml](MvvmDialogOwnerClosed/ItemView.xaml) that registers itself `DialogServiceViews.IsRegistered="True"`.
1. Each item view [ItemView.xaml.cs](MvvmDialogOwnerClosed/ItemView.xaml.cs) unregisters itself when unloaded using `DialogServiceViews.SetIsRegistered(this, false);`.

However, registrations are not completely unsubscribed. The items added to the owner windows event handler invocation list grows with each added item. When items are not removed there is no way unregister completely.

## Test

Build the demo WPF application contained in the solution [MvvmDialogOwnerClosed.sln](MvvmDialogOwnerClosed.sln) and run the application.

1. The application starts with 3 items in the list.
1. Pressing the "Add Item" button will add a new item to the list.
1. When the number of items exceeds 5, the oldest item in the list is removed.
1. Pressing the "Update Invocation List" will show the number of `OwnerClosed` invocations registered. There will be 3 intially plus 1 for each item added. No invocations are removed.
1. The invocations are identified calling `UpdateInvocationList` in [ContainerView.xaml.cs](MvvmDialogOwnerClosed/ContainerView.xaml.cs).
