using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Yakuza.PasswordGenerator.Pages;
using Windows.UI.Xaml.Controls;
using Yakuza.PasswordGenerator.Messages.Actions;
using System.Collections;
using Yakuza.PasswordGenerator.Model;
using Yakuza.PasswordGenerator.Services;

namespace Yakuza.PasswordGenerator.ViewModel
{
   public class SearchViewModel : ViewModelBase
   {
      private readonly IMessenger _bus;
      private bool _isBusy;

      public SearchViewModel(IMessenger bus)
      {
         _bus = bus;

         AddCommand = new RelayCommand(Add);
         DetailsCommand = new RelayCommand<PasswordEntry>(OpenDetails);
      }

      private void OpenDetails(PasswordEntry selectedEntry)
      {
         _bus.Send(new ShowEntryDetailsMessage(selectedEntry));
      }

      private async void Add()
      {
         //TODO: Extract this functionality to common place
         var dialog = new AddNewPasswordDialog();
         var result = await dialog.ShowAsync();

         if (result == ContentDialogResult.Primary)
         {
            _bus.Send(new EditEntryMessage(dialog.NewEntry));
         }
      }

      public IList Items
      {
         get { return PasswordStorage.Entries; }
      }

      public RelayCommand<PasswordEntry> DetailsCommand { get; private set; }
      public RelayCommand AddCommand { get; private set; }
   }
}