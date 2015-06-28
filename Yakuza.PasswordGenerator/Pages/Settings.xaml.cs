﻿using Yakuza.PasswordGenerator.Common;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Yakuza.PasswordGenerator.Model;
using Yakuza.PasswordGenerator.Services;

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
         Frame.Navigate(typeof (GenerateSecret));
      }

      private async void InputClicked(object sender, RoutedEventArgs e)
      {
         var dialog = new InputSecretDialog();

         await dialog.ShowAsync();
      }

      void UseDigitsChecked(object sender, RoutedEventArgs e)
      {
         SettingsProvider.IncludeDigitsByDefault = DefaultUseDigits.IsChecked ?? false;
      }

      void UseCapitalsChecked(object sender, RoutedEventArgs e)
      {
         SettingsProvider.IncludeCapitalsByDefault = DefaultUseCapitals.IsChecked ?? false;
      }

      void UseSpecialsChecked(object sender, RoutedEventArgs e)
      {
         SettingsProvider.IncludeSpecialsByDefault = DefaultUseSpecials.IsChecked ?? false;
      }
   }
}