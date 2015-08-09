using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Yakuza.PasswordGenerator.Messages.Actions;
using System.Collections;
using Yakuza.PasswordGenerator.Model;
using Yakuza.PasswordGenerator.Services;

namespace Yakuza.PasswordGenerator.ViewModel
{
   public class SearchViewModel : ViewModelBase
   {
      private readonly IMessenger _bus;

      public SearchViewModel(IMessenger bus)
      {
         _bus = bus;

         AddCommand = new RelayCommand(() => _bus.Send(new AddPasswordEntryMessage()));
         DetailsCommand = new RelayCommand<PasswordEntry>(entry => _bus.Send(new ShowEntryDetailsMessage(entry)));
      }

      public IList Items
      {
         get { return PasswordStorage.Entries; }
      }

      public RelayCommand<PasswordEntry> DetailsCommand { get; private set; }
      public RelayCommand AddCommand { get; private set; }
   }
}