namespace Yakuza.PasswordGenerator.Messages.Actions
{
   public class SearchForEntryMessage : IMessage
   {
      public SearchForEntryMessage(string label = null)
      {
         Label = label;
      }

      public string Label { get; private set; }
   }
}
