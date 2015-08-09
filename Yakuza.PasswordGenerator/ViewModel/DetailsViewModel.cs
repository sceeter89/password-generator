using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Windows.UI.Popups;
using Yakuza.PasswordGenerator.Messages.Actions;
using Yakuza.PasswordGenerator.Model;
using Yakuza.PasswordGenerator.Services;

namespace Yakuza.PasswordGenerator.ViewModel
{
   public class DetailsViewModel : ViewModelBase
   {
      public DetailsViewModel(IMessenger messenger, INavigationService navigation)
      {
         _messenger = messenger;
         _navigation = navigation;
         if (IsInDesignMode)
         {
            Entry = new PasswordEntry
            {
               Domain = "company.com",
               PasswordLength = 15,
               Tag = "Some tag",
               TagAsCurrentMonth = false,
               UseCapitalLetters = true,
               UseDigits = true,
               UseSpecialCharacters = false,
               Username = "username"
            };
         }

         GenerateCommand = new RelayCommand(Generate);
         EditCommand = new RelayCommand(() => _messenger.Send(new EditEntryMessage(Entry)));
         DeleteCommand = new RelayCommand(Delete);
      }
      
      private void Generate()
      {
         var password = Services.PasswordGenerator.Generate(Entry);

         Password = password;
      }

      private async void Delete()
      {
         var dialog = new MessageDialog("Please confirm that you want to remove this entry.", "Confirmation")
         {
            Options = MessageDialogOptions.AcceptUserInputAfterDelay
         };
         dialog.Commands.Add(new UICommand("confirm", a => PasswordStorage.Entries.Remove(Entry)));
         dialog.Commands.Add(new UICommand("cancel"));
         var result = await dialog.ShowAsync();

         _navigation.GoBack();
      }

      private PasswordEntry _entry;
      private readonly IMessenger _messenger;
      private string _password;
      private readonly INavigationService _navigation;

      public string Password
      {
         get
         {
            return _password;
         }
         set
         {
            _password = value;
            RaisePropertyChanged(() => Password);
         }
      }

      public PasswordEntry Entry
      {
         get
         {
            return _entry;
         }
         set
         {
            _entry = value;
            Password = string.Empty;
            RaisePropertyChanged(() => Entry);
         }
      }

      public RelayCommand GenerateCommand { get; private set; }
      public RelayCommand EditCommand { get; private set; }
      public RelayCommand DeleteCommand { get; private set; }
   }
}