using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight;
using Yakuza.PasswordGenerator.Services;
using Yakuza.PasswordGenerator.Services.ActionsHandlers;

namespace Yakuza.PasswordGenerator.ViewModel
{
   public class ViewModelLocator
   {
      public ViewModelLocator()
      {
         ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

         if (ViewModelBase.IsInDesignModeStatic)
         {
            // Create design time view services and models
         }
         else
         {
            // Create run time view services and models
            SimpleIoc.Default.Register(() => Messenger.Default);
         }
         SimpleIoc.Default.Register<INavigationService, NavigationService>();

         SimpleIoc.Default.Register<EditViewModel>();
         SimpleIoc.Default.Register<SearchViewModel>();
         SimpleIoc.Default.Register<DetailsViewModel>();
         SimpleIoc.Default.Register<BrowseLabelsViewModel>();
         SimpleIoc.Default.Register<MainViewModel>();

         SimpleIoc.Default.Register<NavigationRouter>(createInstanceImmediately: true);
         SimpleIoc.Default.Register<AddPasswordEntryHandler>(createInstanceImmediately: true);
      }

      public MainViewModel Main
      {
         get
         {
            return ServiceLocator.Current.GetInstance<MainViewModel>();
         }
      }

      public EditViewModel Edit
      {
         get
         {
            return ServiceLocator.Current.GetInstance<EditViewModel>();
         }
      }

      public DetailsViewModel Details
      {
         get
         {
            return ServiceLocator.Current.GetInstance<DetailsViewModel>();
         }
      }

      public SearchViewModel Search
      {
         get
         {
            return ServiceLocator.Current.GetInstance<SearchViewModel>();
         }
      }

      public BrowseLabelsViewModel BrowseDirectories
      {
         get
         {
            return ServiceLocator.Current.GetInstance<BrowseLabelsViewModel>();
         }
      }

      public static void Cleanup()
      {
         // TODO Clear the ViewModels
      }
   }
}