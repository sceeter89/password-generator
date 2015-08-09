using Yakuza.PasswordGenerator.Model;

namespace Yakuza.PasswordGenerator.Messages.Actions
{
   public class EditEntryMessage : IMessage
   {
      public EditEntryMessage(PasswordEntry entry)
      {
         Entry = entry;
      }

      public PasswordEntry Entry { get; private set; }
   }
}
