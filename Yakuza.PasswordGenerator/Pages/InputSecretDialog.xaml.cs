using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers.Provider;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Yakuza.PasswordGenerator.Services;

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Yakuza.PasswordGenerator.Pages
{
   public sealed partial class InputSecretDialog : ContentDialog
   {
      public InputSecretDialog()
      {
         this.InitializeComponent();
      }

      private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
      {
         ErrorMessage.Text = "";

         if(Secret.Text.Length != 16)
         {
            ErrorMessage.Text = "Secret key must have length of 16.";
            args.Cancel = true;
            return;
         }

         var secret = Secret.Text.Substring(0, 15);
         var checksum = Secret.Text[15];

         if(SecretChecksum.Compute(secret) != checksum)
         {
            ErrorMessage.Text = "Provided secret key is invalid. Checksum is incorrect.";
            args.Cancel = true;
            return;
         }

         SettingsProvider.SecretSequence = secret;
      }

      private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
      {
      }
   }
}
