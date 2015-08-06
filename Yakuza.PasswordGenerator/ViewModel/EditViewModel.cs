using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Yakuza.PasswordGenerator.Model;
using Yakuza.PasswordGenerator.Services;
using Windows.UI.Popups;

namespace Yakuza.PasswordGenerator.ViewModel
{
   public class EditViewModel : ViewModelBase
   {
      public EditViewModel(IMessenger bus, INavigationService navigation)
      {
         _navigation = navigation;
         _bus = bus;
         if (IsInDesignMode)
         {
            EditedEntry = new PasswordEntry
            {
               Domain = "company.com",
               IsFavorite = true,
               PasswordLength = 15,
               Tag = "Some tag",
               TagAsCurrentMonth = false,
               UseCapitalLetters = true,
               UseDigits = true,
               UseSpecialCharacters = false,
               Username = "username"
            };
         }

         SaveChangesCommand = new RelayCommand(SaveChanges);
         CancelCommand = new RelayCommand(Cancel);
         DeleteCommand = new RelayCommand(Delete);
      }

      private async void Delete()
      {
         var dialog = new MessageDialog("Please confirm that you want to remove this entry.", "Confirmation")
         {
            Options = MessageDialogOptions.AcceptUserInputAfterDelay
         };
         dialog.Commands.Add(new UICommand("confirm", a => PasswordStorage.Entries.Remove(EditedEntry)));
         dialog.Commands.Add(new UICommand("cancel"));
         var result = await dialog.ShowAsync();

         _navigation.GoBack();
      }

      private void Cancel()
      {
         _navigation.GoBack();
      }

      private void SaveChanges()
      {
         PasswordStorage.UpdateItem(EditedEntry);
         _navigation.GoBack();
      }

      private PasswordEntry _editedEntry;
      private readonly IMessenger _bus;
      private readonly INavigationService _navigation;

      public PasswordEntry EditedEntry
      {
         get
         {
            return _editedEntry;
         }
         set
         {
            _editedEntry = value;
            RaisePropertyChanged(() => EditedEntry);
         }
      }

      public RelayCommand SaveChangesCommand { get; private set; }
      public RelayCommand CancelCommand { get; private set; }
      public RelayCommand DeleteCommand { get; private set; }
   }
}