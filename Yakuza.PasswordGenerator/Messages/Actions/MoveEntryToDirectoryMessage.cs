using Yakuza.PasswordGenerator.Model;

namespace Yakuza.PasswordGenerator.Messages.Actions
{
   public class MoveEntryToDirectoryMessage : IMessage
   {
      public MoveEntryToDirectoryMessage(PasswordEntry entry, string destination)
      {
         Entry = entry;
         Destination = destination;
      }

      public string Destination { get; private set; }
      public PasswordEntry Entry { get; private set; }
   }
}
