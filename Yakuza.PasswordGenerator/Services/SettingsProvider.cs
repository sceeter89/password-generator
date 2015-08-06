using System.Runtime.CompilerServices;
using Windows.Storage;

namespace Yakuza.PasswordGenerator.Services
{
   public static class SettingsProvider
   {
      public static string SecretSequence
      {
         get { return Get<string>(); }
         set { Set(value); }
      }

      public static int DefaultPasswordLength
      {
         get { return Get(fallback: 10); }
         set { Set(value); }
      }

      public static bool IncludeDigitsByDefault
      {
         get { return Get(fallback: true); }
         set { Set(value); }
      }

      public static bool IncludeSpecialsByDefault
      {
         get { return Get(fallback: true); }
         set { Set(value); }
      }

      public static bool IncludeCapitalsByDefault
      {
         get { return Get(fallback: true); }
         set { Set(value); }
      }

      public static string LockScreenBackground
      {
         get { return Get(fallback: "tree.jpg"); }
         set { Set(value); }
      }



      private static T Get<T>([CallerMemberName] string key = null, T fallback = default(T))
      {
         if (ApplicationData.Current.LocalSettings.Values.ContainsKey(key))
            return (T)ApplicationData.Current.LocalSettings.Values[key];

         return fallback;
      }

      private static void Set<T>(T value, [CallerMemberName] string key = null)
      {
         ApplicationData.Current.LocalSettings.Values[key] = value;
      }
   }
}
