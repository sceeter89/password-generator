using Yakuza.PasswordGenerator.Model;

namespace Yakuza.PasswordGenerator.Messages.Actions
{
   public class ShowEntryDetailsMessage : IMessage
   {
      public ShowEntryDetailsMessage(PasswordEntry entry)
      {
         Entry = entry;
      }

      public PasswordEntry Entry { get; private set; }
   }
}
