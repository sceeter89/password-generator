using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Yakuza.PasswordGenerator.Messages.Actions;
using Yakuza.PasswordGenerator.Model;
using Yakuza.PasswordGenerator.Messages.Navigation;

namespace Yakuza.PasswordGenerator.ViewModel
{
   public class DetailsViewModel : ViewModelBase
   {
      public DetailsViewModel(IMessenger messenger)
      {
         _messenger = messenger;
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

      private void Delete()
      {
         _messenger.Send(new DeleteEntryMessage(Entry));
         _messenger.Send(new GoBack());
      }

      private PasswordEntry _entry;
      private readonly IMessenger _messenger;
      private string _password;

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