using System;
using Yakuza.PasswordGenerator.Pages;

namespace Yakuza.PasswordGenerator.Services
{
   internal static class SecretChecksum
   {
      private static byte[] _weights = { 1, 3, 5, 7, 13, 17, 1, 3, 5, 7, 11, 13, 17, 1, 3 };

      public static char Compute(string secret)
      {
         int sum = 0;
         for (int i = 0; i < 15; i++)
         {
            sum += Array.IndexOf(GenerateSecret.Characters, secret[i]) * _weights[i];
         }

         return GenerateSecret.Characters[sum % GenerateSecret.Characters.Length];
      }
   }
}
