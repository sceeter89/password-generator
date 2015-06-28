using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Security.Cryptography.Core;
using Yakuza.PasswordGenerator.Model;

namespace Yakuza.PasswordGenerator.Services
{
   public static class PasswordGenerator
   {
      private static readonly char[] LowerLetters = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
      private static readonly char[] CapitalLetters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
      private static readonly char[] Digits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
      private static readonly char[] Specials = { '!', '@', '#', '$', '%', '*', '?', '=', '+', '-', '_' };

      public static string Generate(PasswordEntry entry)
      {
         var entryStringified = entry.Domain + entry.Username + (entry.TagAsCurrentMonth
            ? DateTime.Now.ToString("yyyy-MM-dd")
            : entry.Tag);
         var entryBytes = Encoding.UTF8.GetBytes(entryStringified);
         var secretBytes = Encoding.UTF8.GetBytes(SettingsProvider.SecretSequence);

         var resultBytes = new byte[entryBytes.Length + secretBytes.Length];

         for (int i = 0; i < entryBytes.Length; i++)
         {
            resultBytes[i] = (byte)((entryBytes[i % entryBytes.Length] * i * secretBytes[Math.Abs(secretBytes.Length - i) % secretBytes.Length]) % 256);
         }

         for (int i = entryBytes.Length; i < resultBytes.Length; i++)
         {
            resultBytes[i] = (byte)((entryBytes[i * secretBytes[i % secretBytes.Length] % entryBytes.Length] * secretBytes[entryBytes[i % entryBytes.Length] % secretBytes.Length]) % 256);
         }

         var hap = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Sha512);
         var ch = hap.CreateHash();

         ch.Append(resultBytes.AsBuffer());

         var hash = ch.GetValueAndReset().ToArray();
         var hashCharacters = new List<char>();
         var pointer = 0;
         if (entry.UseDigits)
            for (int i = 0; i < 12; i++)
            {
               hashCharacters.Add(Digits[hash[pointer] * i % Digits.Length]);
               pointer++;
            }
         if (entry.UseSpecialCharacters)
            for (int i = 0; i < 12; i++)
            {
               hashCharacters.Add(Specials[hash[pointer] * i % Specials.Length]);
               pointer++;
            }
         if (entry.UseCapitalLetters)
            for (int i = 0; i < 20; i++)
            {
               hashCharacters.Add(CapitalLetters[hash[pointer] * i % CapitalLetters.Length]);
               pointer++;
            }
         for (int i = 0; i < 20; i++)
         {
            hashCharacters.Add(LowerLetters[hash[pointer] * i % LowerLetters.Length]);
            pointer++;
         }
         var passwordChars = new List<char>(entry.PasswordLength);

         for (int i = 0; passwordChars.Count < entry.PasswordLength; i += 13)
         {
            passwordChars.Add(hashCharacters[i % hashCharacters.Count]);
         }
         
         var threshold = Math.Sqrt(Enumerable.Sum(hash, x => (double)x));

         return string.Join("", passwordChars.OrderBy(x => x > threshold));
      }
   }
}