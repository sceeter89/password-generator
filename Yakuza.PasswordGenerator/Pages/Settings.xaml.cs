using Yakuza.PasswordGenerator.Common;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Yakuza.PasswordGenerator.Model;
using Yakuza.PasswordGenerator.Services;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Media;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Input;
using Windows.UI.Input;

namespace Yakuza.PasswordGenerator.Pages
{
   public sealed partial class Settings : Page
   {
      private NavigationHelper navigationHelper;
      private ObservableDictionary defaultViewModel = new ObservableDictionary();

      public Settings()
      {
         InitializeComponent();

         navigationHelper = new NavigationHelper(this);
         navigationHelper.LoadState += NavigationHelper_LoadState;
         navigationHelper.SaveState += NavigationHelper_SaveState;
      }

      /// <summary>
      /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
      /// </summary>
      public NavigationHelper NavigationHelper
      {
         get { return this.navigationHelper; }
      }

      /// <summary>
      /// Gets the view model for this <see cref="Page"/>.
      /// This can be changed to a strongly typed view model.
      /// </summary>
      public ObservableDictionary DefaultViewModel
      {
         get { return this.defaultViewModel; }
      }

      /// <summary>
      /// Populates the page with content passed during navigation.  Any saved state is also
      /// provided when recreating a page from a prior session.
      /// </summary>
      /// <param name="sender">
      /// The source of the event; typically <see cref="NavigationHelper"/>
      /// </param>
      /// <param name="e">Event data that provides both the navigation parameter passed to
      /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
      /// a dictionary of state preserved by this page during an earlier
      /// session.  The state will be null the first time a page is visited.</param>
      private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
      {
      }

      /// <summary>
      /// Preserves state associated with this page in case the application is suspended or the
      /// page is discarded from the navigation cache.  Values must conform to the serialization
      /// requirements of <see cref="SuspensionManager.SessionState"/>.
      /// </summary>
      /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
      /// <param name="e">Event data that provides an empty dictionary to be populated with
      /// serializable state.</param>
      private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
      {
      }

      #region NavigationHelper registration

      /// <summary>
      /// The methods provided in this section are simply used to allow
      /// NavigationHelper to respond to the page's navigation methods.
      /// <para>
      /// Page specific logic should be placed in event handlers for the  
      /// <see cref="NavigationHelper.LoadState"/>
      /// and <see cref="NavigationHelper.SaveState"/>.
      /// The navigation parameter is available in the LoadState method 
      /// in addition to page state preserved during an earlier session.
      /// </para>
      /// </summary>
      /// <param name="e">Provides data for navigation methods and event
      /// handlers that cannot cancel the navigation request.</param>
      protected override void OnNavigatedTo(NavigationEventArgs e)
      {
         navigationHelper.OnNavigatedTo(e);
      }

      protected override void OnNavigatedFrom(NavigationEventArgs e)
      {
         navigationHelper.OnNavigatedFrom(e);
      }

      #endregion

      private void GenerateClicked(object sender, RoutedEventArgs e)
      {
         Frame.Navigate(typeof(GenerateSecret));
      }

      private async void InputClicked(object sender, RoutedEventArgs e)
      {
         var dialog = new InputSecretDialog();

         await dialog.ShowAsync();
      }

      private void UseDigitsChecked(object sender, RoutedEventArgs e)
      {
         SettingsProvider.IncludeDigitsByDefault = DefaultUseDigits.IsChecked ?? false;
      }

      private void UseCapitalsChecked(object sender, RoutedEventArgs e)
      {
         SettingsProvider.IncludeCapitalsByDefault = DefaultUseCapitals.IsChecked ?? false;
      }

      private void UseSpecialsChecked(object sender, RoutedEventArgs e)
      {
         SettingsProvider.IncludeSpecialsByDefault = DefaultUseSpecials.IsChecked ?? false;
      }

      private void EnableLockScreenChecked(object sender, RoutedEventArgs e)
      {
         SettingsProvider.IncludeSpecialsByDefault = DefaultUseSpecials.IsChecked ?? false;
      }

      private async void SetLockBackgroundClicked(object sender, RoutedEventArgs e)
      {
         var dialog = new InputSecretDialog();

         await dialog.ShowAsync();
      }

      private void HelpClicked(object sender, RoutedEventArgs e)
      {

      }

      private void OverlayPressed(object sender, PointerRoutedEventArgs e)
      {
         Pointer ptr = e.Pointer;
         var point = PointerPoint.GetCurrentPoint(ptr.PointerId);

         var position = point.Position;

         var x = (int)(position.X / 37) - 1;
         var y = (int)(position.Y / 44) - 6;

         Marker.Visibility = Visibility.Visible;
         Marker.Margin = new Thickness(x * 37, y * 44, 0, 0);
      }
   }
}
