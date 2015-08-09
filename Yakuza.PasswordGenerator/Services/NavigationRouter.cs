using GalaSoft.MvvmLight.Messaging;
using Yakuza.PasswordGenerator.Messages.Actions;
using Yakuza.PasswordGenerator.Pages;
using Yakuza.PasswordGenerator.ViewModel;

namespace Yakuza.PasswordGenerator.Services
{
   class NavigationRouter
   {
      private readonly DetailsViewModel _detailsViewModel;
      private readonly EditViewModel _editViewModel;
      private readonly INavigationService _navigation;

      public NavigationRouter(IMessenger messenger, INavigationService navigation,
         EditViewModel editViewModel, DetailsViewModel detailsViewModel)
      {
         _navigation = navigation;
         _editViewModel = editViewModel;
         _detailsViewModel = detailsViewModel;

         messenger.Register<EditEntryMessage>(this, HandleEdit);
         messenger.Register<ShowEntryDetailsMessage>(this, HandleDetails);
         messenger.Register<ShowSettingsMessage>(this, HandleSettings);
         messenger.Register<ShowHelpMessage>(this, HandleHelp);
         messenger.Register<SearchForEntryMessage>(this, HandleSearching);
      }

      private void HandleEdit(EditEntryMessage message)
      {
         _editViewModel.EditedEntry = message.Entry;
         _navigation.Navigate(typeof(Edit));
      }

      private void HandleDetails(ShowEntryDetailsMessage message)
      {
         _detailsViewModel.Entry = message.Entry;
         _navigation.Navigate(typeof(Details));
      }

      private void HandleSettings(ShowSettingsMessage message)
      {
         _navigation.Navigate(typeof(Settings));
      }

      private void HandleHelp(ShowHelpMessage message)
      {
         _navigation.Navigate(typeof(Help));
      }

      private void HandleSearching(SearchForEntryMessage message)
      {
         _navigation.Navigate(typeof(Search));
      }
   }
}
