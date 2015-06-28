using Windows.UI.Popups;
using Windows.UI.Xaml;
using Yakuza.PasswordGenerator.Common;
using System;
using Windows.UI.Xaml.Navigation;
using Yakuza.PasswordGenerator.Model;
using Yakuza.PasswordGenerator.Services;

namespace Yakuza.PasswordGenerator.Pages
{
   /// <summary>
   /// An empty page that can be used on its own or navigated to within a Frame.
   /// </summary>
   public sealed partial class Edit
   {
      private readonly NavigationHelper _navigationHelper;

      public Edit()
      {
         InitializeComponent();

         _navigationHelper = new NavigationHelper(this);
         _navigationHelper.LoadState += NavigationHelper_LoadState;
         _navigationHelper.SaveState += NavigationHelper_SaveState;

      }

      /// <summary>
      /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
      /// </summary>
      public NavigationHelper NavigationHelper
      {
         get { return this._navigationHelper; }
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
         _navigationHelper.OnNavigatedTo(e);

         DataContext = e.Parameter;
         var item = DataContext as PasswordEntry;

         Domain.Text = item.Domain ?? "";
         Username.Text = item.Username ?? "";
         EntryTag.Text = item.Tag ?? "";
         TagAsCurrentMonth.IsChecked = item.TagAsCurrentMonth;
         UseSpecials.IsChecked = item.UseSpecialCharacters;
         UseCapitals.IsChecked = item.UseCapitalLetters;
         UseDigits.IsChecked = item.UseDigits;
         Length.Text = item.PasswordLength.ToString();
         IsFavorite.IsChecked = item.IsFavorite;
      }

      protected override void OnNavigatedFrom(NavigationEventArgs e)
      {
         _navigationHelper.OnNavigatedFrom(e);
      }

      #endregion

      private void SaveClicked(object sender, RoutedEventArgs e)
      {
         var item = DataContext as PasswordEntry;
         item.Domain = Domain.Text;
         item.Username = Username.Text;
         item.Tag = EntryTag.Text;
         item.TagAsCurrentMonth = TagAsCurrentMonth.IsChecked.HasValue && TagAsCurrentMonth.IsChecked.Value;
         item.UseSpecialCharacters = UseSpecials.IsChecked.HasValue && UseSpecials.IsChecked.Value;
         item.UseCapitalLetters = UseCapitals.IsChecked.HasValue && UseCapitals.IsChecked.Value;
         item.UseDigits = UseDigits.IsChecked.HasValue && UseDigits.IsChecked.Value;
         item.PasswordLength = int.Parse(Length.Text);
         item.IsFavorite = IsFavorite.IsChecked.HasValue && IsFavorite.IsChecked.Value;
         PasswordStorage.UpdateItem(item);

         Frame.GoBack();
      }

      private async void DeleteClicked(object sender, RoutedEventArgs e)
      {
         var dialog = new MessageDialog("Please confirm that you want to remove this entry.", "Confirmation")
         {
            Options = MessageDialogOptions.AcceptUserInputAfterDelay
         };
         dialog.Commands.Add(new UICommand("confirm", a => PasswordStorage.Entries.Remove(DataContext as PasswordEntry)));
         dialog.Commands.Add(new UICommand("cancel"));
         var result = await dialog.ShowAsync();

         Frame.GoBack();
      }

      private void CancelClicked(object sender, RoutedEventArgs e)
      {
         Frame.GoBack();
      }
   }
}
