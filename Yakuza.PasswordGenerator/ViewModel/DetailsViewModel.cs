using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Yakuza.PasswordGenerator.Messages.Actions;
using Yakuza.PasswordGenerator.Model;

namespace Yakuza.PasswordGenerator.ViewModel
{
   public class DetailsViewModel : ViewModelBase
   {
      public DetailsViewModel(IMessenger bus)
      {
         _bus = bus;
         if (IsInDesignMode)
         {
            Entry = new PasswordEntry
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

         GenerateCommand = new RelayCommand(Generate);
         EditCommand = new RelayCommand(() => _bus.Send(new EditEntryMessage(Entry)));
      }
      
      private void Generate()
      {
         var password = Services.PasswordGenerator.Generate(Entry);

         Password = password;
      }

      private PasswordEntry _editedEntry;
      private readonly IMessenger _bus;
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
            return _editedEntry;
         }
         set
         {
            _editedEntry = value;
            Password = string.Empty;
            RaisePropertyChanged(() => Entry);
         }
      }

      public RelayCommand GenerateCommand { get; private set; }
      public RelayCommand EditCommand { get; private set; }
   }
}