using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Yakuza.PasswordGenerator.Pages
{
   public sealed partial class OverwriteSecretDialog
   {
      public OverwriteSecretDialog(string secret)
      {
         InitializeComponent();

         Secret.Text = secret;
      }

      private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
      {
      }

      private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
      {
      }
   }
}
