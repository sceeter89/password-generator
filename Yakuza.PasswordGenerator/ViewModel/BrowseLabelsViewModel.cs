using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Yakuza.PasswordGenerator.Messages.Actions;
using System.Collections;
using Yakuza.PasswordGenerator.Services;
using GalaSoft.MvvmLight.Command;
using System;

namespace Yakuza.PasswordGenerator.ViewModel
{
   public class BrowseLabelsViewModel : ViewModelBase
   {
      private readonly IMessenger _bus;

      public BrowseLabelsViewModel(IMessenger bus)
      {
         _bus = bus;
         _bus.Register<ShowLabelsBrowser>(this, e => RebuildLabelsList());
         ShowLabelsCommand = new RelayCommand<string>(Show);
      }

      private void Show(string label)
      {
         _bus.Send(new SearchForEntryMessage(label));
      }

      private void RebuildLabelsList()
      {
         RaisePropertyChanged(() => Items);
      }

      public IList Items
      {
         get { return PasswordStorage.Entries; }
      }

      public RelayCommand<string> ShowLabelsCommand { get; internal set; }
   }
}