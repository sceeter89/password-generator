using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Yakuza.PasswordGenerator.Messages.Navigation;
using Yakuza.PasswordGenerator.Model;
using Yakuza.PasswordGenerator.Services;

namespace Yakuza.PasswordGenerator.ViewModel
{
   public class EditViewModel : ViewModelBase
   {
      public EditViewModel(IMessenger messenger)
      {
         _messenger = messenger;
         if (IsInDesignMode)
         {
            EditedEntry = new PasswordEntry
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

         SaveChangesCommand = new RelayCommand(SaveChanges);
         CancelCommand = new RelayCommand(Cancel);
      }

      private void Cancel()
      {
         _messenger.Send(new GoBack());
      }

      private void SaveChanges()
      {
         PasswordStorage.UpdateItem(EditedEntry);
         _messenger.Send(new GoBack());
      }

      private PasswordEntry _editedEntry;
      private readonly IMessenger _messenger;

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
   }
}