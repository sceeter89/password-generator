using System;
using GalaSoft.MvvmLight.Messaging;
using Windows.UI.Xaml.Controls;
using Yakuza.PasswordGenerator.Messages.Actions;
using Yakuza.PasswordGenerator.Pages;

namespace Yakuza.PasswordGenerator.Services.ActionsHandlers
{
   class AddPasswordEntryHandler
   {
      private readonly IMessenger _messenger;

      public AddPasswordEntryHandler(IMessenger messenger)
      {
         _messenger = messenger;
         messenger.Register<AddPasswordEntryMessage>(this, Handle);
      }

      private async void Handle(AddPasswordEntryMessage message)
      {
         var dialog = new AddNewPasswordDialog();
         var result = await dialog.ShowAsync();

         if (result == ContentDialogResult.Primary)
         {
            _messenger.Send(new EditEntryMessage(dialog.NewEntry));
         }
      }
   }
}
