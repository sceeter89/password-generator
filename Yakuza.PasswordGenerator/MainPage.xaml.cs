using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Yakuza.PasswordGenerator.Common;
using Yakuza.PasswordGenerator.Pages;
using Yakuza.PasswordGenerator.Services;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace Yakuza.PasswordGenerator
{
   /// <summary>
   /// An empty page that can be used on its own or navigated to within a Frame.
   /// </summary>
   public sealed partial class MainPage : Page
   {
      private NavigationHelper _navigationHelper;

      public MainPage()
      {
         InitializeComponent();

         NavigationCacheMode = NavigationCacheMode.Required;

         _navigationHelper = new NavigationHelper(this);
         _navigationHelper.LoadState += NavigationHelper_LoadState;
         _navigationHelper.SaveState += NavigationHelper_SaveState;
      }

      private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
      {

      }

      private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
      {

      }

      /// <summary>
      /// Invoked when this page is about to be displayed in a Frame.
      /// </summary>
      /// <param name="e">Event data that describes how this page was reached.
      /// This parameter is typically used to configure the page.</param>
      protected override async void OnNavigatedTo(NavigationEventArgs e)
      {
         // TODO: Prepare page for display here.

         // TODO: If your application contains multiple pages, ensure that you are
         // handling the hardware Back button by registering for the
         // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
         // If you are using the NavigationHelper provided by some templates,
         // this event is handled for you.
         _navigationHelper.OnNavigatedTo(e);

         if (string.IsNullOrWhiteSpace(SettingsProvider.SecretSequence))
         {
            var welcomeDialog = new FirstUseDialog();

            await welcomeDialog.ShowAsync();

            Frame.Navigate(typeof(GenerateSecret));
         }
      }

      protected override void OnNavigatedFrom(NavigationEventArgs e)
      {
         _navigationHelper.OnNavigatedFrom(e);
      }
   }
}
