using System;
using GalaSoft.MvvmLight.Messaging;
using Yakuza.PasswordGenerator.Messages.Actions;
using Windows.UI.Popups;

namespace Yakuza.PasswordGenerator.Services.ActionsHandlers
{
   class DeleteEntryHandler
   {
      private readonly IMessenger _messenger;

      public DeleteEntryHandler(IMessenger messenger)
      {
         _messenger = messenger;
         messenger.Register<DeleteEntryMessage>(this, Handle);
      }

      private async void Handle(DeleteEntryMessage message)
      {
         var dialog = new MessageDialog("Please confirm that you want to remove this entry.", "Confirmation")
         {
            Options = MessageDialogOptions.AcceptUserInputAfterDelay
         };
         dialog.Commands.Add(new UICommand("confirm", a => PasswordStorage.Entries.Remove(message.Entry)));
         dialog.Commands.Add(new UICommand("cancel"));
         var result = await dialog.ShowAsync();
      }
   }
}
