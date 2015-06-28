using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Devices.Sensors;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Yakuza.PasswordGenerator.Common;
using Yakuza.PasswordGenerator.Services;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Yakuza.PasswordGenerator.Pages
{
   /// <summary>
   /// An empty page that can be used on its own or navigated to within a Frame.
   /// </summary>
   public sealed partial class GenerateSecret
   {
      enum GenerationState { None, Gathering, }

      private GenerationState _state = GenerationState.None;
      private readonly IList<char> _generatedSecrets = new List<char>();

      private readonly NavigationHelper _navigationHelper;
      private readonly ObservableDictionary _defaultViewModel = new ObservableDictionary();
      private Accelerometer _accelerometer;
      private Gyrometer _gyrometer;

      public GenerateSecret()
      {
         InitializeComponent();

         _navigationHelper = new NavigationHelper(this);
         _navigationHelper.LoadState += NavigationHelper_LoadState;
         _navigationHelper.SaveState += NavigationHelper_SaveState;
         _dispatcher = new DispatcherTimer();
         _dispatcher.Interval = TimeSpan.FromSeconds(1);
         _dispatcher.Tick += _dispatcher_Tick;
      }

      async void _dispatcher_Tick(object sender, object e)
      {
         if (_state == GenerationState.None)
         {
            StartStopButton.Content = "Generate";
            _dispatcher.Stop();
            return;
         }

         var multiplier = _random.NextDouble() * 100;
         var vectorX = multiplier * (_gyroReading.AngularVelocityX + 1) * (_forceReading.AccelerationX + 1);
         var vectorY = multiplier * (_gyroReading.AngularVelocityY + 1) * (_forceReading.AccelerationY + 1);
         var vectorZ = multiplier * (_gyroReading.AngularVelocityZ + 1) * (_forceReading.AccelerationZ + 1);

         var value = Math.Sqrt(Math.Pow(vectorX, 2) + Math.Pow(vectorY, 2) + Math.Pow(vectorZ, 2));

         while (value > Characters.Length)
            value *= _random.NextDouble();

         _generatedSecrets.Add(Characters[(int)value]);
         ProgressBar.Value = _generatedSecrets.Count;
         _newSecretCode = string.Join(" ", _generatedSecrets);
         SecretDisplay.Text = _newSecretCode;
         if (_generatedSecrets.Count == 15)
         {
            _state = GenerationState.None;
            StartStopButton.Content = "Generate";
            _dispatcher.Stop();
            ApplyButton.IsEnabled = true;
            _newSecretCodeWithChecksum = _newSecretCode + SecretChecksum.Compute(_newSecretCode);
            SecretDisplay.Text = _newSecretCodeWithChecksum;
            return;
         }

         await Task.Delay(TimeSpan.FromSeconds(_random.NextDouble() * 2.5 + 0.5));
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

      internal readonly static char[] Characters =
      {
         'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
         'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
         '.', '?', '/', '\\', '[', ']', '{', '}', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')'
      };

      private AccelerometerReading _forceReading;
      private GyrometerReading _gyroReading;
      private readonly DispatcherTimer _dispatcher;
      private readonly Random _random = new Random();
      private string _newSecretCode;
      private string _newSecretCodeWithChecksum;

      private void GyrometerReadingUpdated(GyrometerReading reading)
      {
         _gyroReading = reading;
         if (_state == GenerationState.Gathering)
         {
            SpeedX.Value = reading.AngularVelocityX;
            SpeedY.Value = reading.AngularVelocityY;
            SpeedZ.Value = reading.AngularVelocityZ;
         }
      }

      private void ForceReadingUpdated(AccelerometerReading reading)
      {
         _forceReading = reading;
         if (_state == GenerationState.Gathering)
         {
            ForceX.Value = reading.AccelerationX;
            ForceY.Value = reading.AccelerationY;
            ForceZ.Value = reading.AccelerationZ;
         }
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
         this._navigationHelper.OnNavigatedTo(e);
         if (_accelerometer == null || _gyrometer == null)
         {
            _gyrometer = Gyrometer.GetDefault();
            _accelerometer = Accelerometer.GetDefault();
            _accelerometer.ReadingChanged += async (s, args) =>
            {
               await Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                {
                   ForceReadingUpdated(args.Reading);
                });
            };
            _gyrometer.ReadingChanged += async (s, args) =>
            {
               await Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                {
                   GyrometerReadingUpdated(args.Reading);
                });
            };
         }
      }

      protected override void OnNavigatedFrom(NavigationEventArgs e)
      {
         _navigationHelper.OnNavigatedFrom(e);
      }

      #endregion

      private void GenerateClicked(object sender, RoutedEventArgs e)
      {
         if (_state == GenerationState.None)
         {
            StartStopButton.Content = "Cancel";
            ApplyButton.IsEnabled = false;
            ProgressBar.Value = 0;
            _generatedSecrets.Clear();
            _state = GenerationState.Gathering;
            _dispatcher.Start();
            return;
         }

         _state = GenerationState.None;
         StartStopButton.Content = "Generate";
         _dispatcher.Stop();
      }

      private async void ApplyClicked(object sender, RoutedEventArgs e)
      {
         var dialog = new OverwriteSecretDialog(_newSecretCodeWithChecksum);
         var result = await dialog.ShowAsync();

         if (result == ContentDialogResult.Secondary || result == ContentDialogResult.None)
            return;

         SettingsProvider.SecretSequence = _newSecretCode;
         Frame.GoBack();
      }
   }
}
