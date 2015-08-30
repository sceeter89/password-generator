using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Yakuza.PasswordGenerator.Messages.Actions;
using System.Collections;
using Yakuza.PasswordGenerator.Model;
using Yakuza.PasswordGenerator.Services;
using System.Linq;

namespace Yakuza.PasswordGenerator.ViewModel
{
   public class SearchViewModel : ViewModelBase
   {
      private readonly IMessenger _bus;
      private string _filterByLabel = null;

      public SearchViewModel(IMessenger bus)
      {
         _bus = bus;
         _bus.Register<SearchForEntryMessage>(this, m =>
         {
            _filterByLabel = m.Label;
            RaisePropertyChanged(() => Items);
         });

         AddCommand = new RelayCommand(() => _bus.Send(new AddPasswordEntryMessage()));
         DetailsCommand = new RelayCommand<PasswordEntry>(entry => _bus.Send(new ShowEntryDetailsMessage(entry)));
      }

      public IList Items
      {
         get
         {
            if (_filterByLabel == null)
               return PasswordStorage.Entries;
            return PasswordStorage.Entries.Where(x => x.Labels.Split(' ').Contains(_filterByLabel)).ToList();
         }
      }

      public RelayCommand<PasswordEntry> DetailsCommand { get; private set; }
      public RelayCommand AddCommand { get; private set; }
   }
}