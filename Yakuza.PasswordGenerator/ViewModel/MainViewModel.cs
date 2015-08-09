using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Yakuza.PasswordGenerator.Pages;
using Windows.UI.Xaml.Controls;
using Yakuza.PasswordGenerator.Messages.Actions;

namespace Yakuza.PasswordGenerator.ViewModel
{
   public class MainViewModel : ViewModelBase
   {
      private readonly IMessenger _bus;

      public MainViewModel(IMessenger bus)
      {
         _bus = bus;

         AddCommand = new RelayCommand(Add);
         BrowseCommand = new RelayCommand(Browse);
         SearchCommand = new RelayCommand(() => _bus.Send(new SearchForEntryMessage()));
         SettingsCommand = new RelayCommand(() => _bus.Send(new ShowSettingsMessage()));
         LockCommand = new RelayCommand(Lock);
         HelpCommand = new RelayCommand(() => _bus.Send(new ShowHelpMessage()));
         RateCommand = new RelayCommand(Rate);
      }

      private async void Add()
      {
         var dialog = new AddNewPasswordDialog();
         var result = await dialog.ShowAsync();

         if (result == ContentDialogResult.Primary)
         {
            _bus.Send(new EditEntryMessage(dialog.NewEntry));
         }
      }

      private void Browse()
      {
         throw new NotImplementedException();
      }

      private void Lock()
      {
         throw new NotImplementedException();
      }

      private void Rate()
      {
         throw new NotImplementedException();
      }

      public RelayCommand AddCommand { get; private set; }
      public RelayCommand BrowseCommand { get; private set; }
      public RelayCommand SearchCommand { get; private set; }
      public RelayCommand SettingsCommand { get; private set; }
      public RelayCommand LockCommand { get; private set; }
      public RelayCommand HelpCommand { get; private set; }
      public RelayCommand RateCommand { get; private set; }
   }
}