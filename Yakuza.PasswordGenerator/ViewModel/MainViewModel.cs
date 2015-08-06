using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Yakuza.PasswordGenerator.ViewModel
{
   /// <summary>
   /// This class contains properties that the main View can data bind to.
   /// <para>
   /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
   /// </para>
   /// <para>
   /// You can also use Blend to data bind with the tool's support.
   /// </para>
   /// <para>
   /// See http://www.galasoft.ch/mvvm
   /// </para>
   /// </summary>
   public class MainViewModel : ViewModelBase
   {
      private readonly IMessenger _bus;

      /// <summary>
      /// Initializes a new instance of the MainViewModel class.
      /// </summary>
      public MainViewModel(IMessenger bus)
      {
         _bus = bus;
         ////if (IsInDesignMode)
         ////{
         ////    // Code runs in Blend --> create design time data.
         ////}
         ////else
         ////{
         ////    // Code runs "for real"
         ////}
      }

      public RelayCommand AddCommand { get; private set; }
      public RelayCommand BrowseCommand { get; private set; }
      public RelayCommand SearchCommand { get; private set; }
      public RelayCommand SettingsCommand { get; private set; }
      public RelayCommand LockCommand { get; private set; }
      public RelayCommand HelpCommand { get; private set; }
      public RelayCommand RateCommand { get; private set; }
   }
}