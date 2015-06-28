using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Navigation;
using QKit.JumpList;
using Yakuza.PasswordGenerator.Annotations;
using Yakuza.PasswordGenerator.Common;
using Yakuza.PasswordGenerator.Model;
using Yakuza.PasswordGenerator.Services;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Yakuza.PasswordGenerator.Pages
{
   /// <summary>
   /// An empty page that can be used on its own or navigated to within a Frame.
   /// </summary>
   public sealed partial class Browse : INotifyPropertyChanged
   {
      private readonly NavigationHelper _navigationHelper;
      private readonly ObservableDictionary _defaultViewModel = new ObservableDictionary();
      private IList _items;

      public Browse()
      {
         InitializeComponent();

         _navigationHelper = new NavigationHelper(this);
         _navigationHelper.LoadState += NavigationHelper_LoadState;
         _navigationHelper.SaveState += NavigationHelper_SaveState;

         DataContext = this;
      }

      /// <summary>
      /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
      /// </summary>
      public NavigationHelper NavigationHelper
      {
         get { return _navigationHelper; }
      }

      /// <summary>
      /// Gets the view model for this <see cref="Page"/>.
      /// This can be changed to a strongly typed view model.
      /// </summary>
      public ObservableDictionary DefaultViewModel
      {
         get { return _defaultViewModel; }
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

         LoadItems();
      }

      public IList Items
      {
         get { return _items; }
         set
         {
            _items = value;
            OnPropertyChanged();
         }
      }

      private void LoadItems()
      {
         BusyIndicator.IsActive = true;

         Items = PasswordStorage.Entries.ToAlphaGroups(x => x.Domain);

         BusyIndicator.IsActive = false;
      }

      protected override void OnNavigatedFrom(NavigationEventArgs e)
      {
         _navigationHelper.OnNavigatedFrom(e);
      }

      #endregion

      private void RefreshClicked(object sender, RoutedEventArgs e)
      {
         LoadItems();
      }

      private async void AddClicked(object sender, RoutedEventArgs e)
      {
         var dialog = new AddNewPasswordDialog();
         var result = await dialog.ShowAsync();

         if (result == ContentDialogResult.Primary)
            Frame.Navigate(typeof(Edit), dialog.NewEntry);
      }

      public event PropertyChangedEventHandler PropertyChanged;

      [NotifyPropertyChangedInvocator]
      private void OnPropertyChanged([CallerMemberName] string propertyName = null)
      {
         var handler = PropertyChanged;
         if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
      }

      private void ItemClicked(object sender, ItemClickEventArgs e)
      {
         var item = e.ClickedItem as PasswordEntry;
         Frame.Navigate(typeof(Details), item);
      }
   }
}
