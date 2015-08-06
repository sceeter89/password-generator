using Windows.UI.Xaml.Controls;
using Yakuza.PasswordGenerator.Model;
using Yakuza.PasswordGenerator.Services;

namespace Yakuza.PasswordGenerator.Pages
{
   public sealed partial class AddNewPasswordDialog
   {
      public PasswordEntry NewEntry { get; set; }

      public AddNewPasswordDialog()
      {
         InitializeComponent();
      }

      public void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
      {
      }

      private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
      {
         var deferral = args.GetDeferral();
         args.Cancel = false;
         NewEntry = new PasswordEntry
         {
            Id = -1,
            Domain = Domain.Text,
            Username = Username.Text,
            PasswordLength = SettingsProvider.DefaultPasswordLength,
            UseCapitalLetters = SettingsProvider.IncludeCapitalsByDefault,
            UseDigits = SettingsProvider.IncludeDigitsByDefault,
            UseSpecialCharacters = SettingsProvider.IncludeSpecialsByDefault,
         };
         PasswordStorage.Entries.Add(NewEntry);

         deferral.Complete();
      }
   }
}
