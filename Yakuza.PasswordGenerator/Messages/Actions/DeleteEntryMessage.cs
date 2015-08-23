using Yakuza.PasswordGenerator.Model;

namespace Yakuza.PasswordGenerator.Messages.Actions
{
   public class DeleteEntryMessage : IMessage
   {
      public DeleteEntryMessage(PasswordEntry entry)
      {
         Entry = entry;
      }

      public PasswordEntry Entry { get; private set; }
   }
}
