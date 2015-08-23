using SQLite.Net.Attributes;

namespace Yakuza.PasswordGenerator.Model
{
   [Table("PasswordEntries")]
   public class PasswordEntry : ModelBase
   {
      private string _domain;
      private string _username;
      private bool _useSpecialCharacters;
      private int _passwordLength;
      private int _id;
      private string _tag;
      private bool _tagAsCurrentMonth;
      private bool _useCapitalLetters;
      private bool _useDigits;

      [PrimaryKey]
      [AutoIncrement]
      public int Id
      {
         get { return _id; }
         set
         {
            _id = value;
            OnPropertyChanged();
         }
      }

      public string Domain
      {
         get { return _domain; }
         set
         {
            _domain = value;
            OnPropertyChanged();
         }
      }

      public string Username
      {
         get { return _username; }
         set
         {
            _username = value;
            OnPropertyChanged();
         }
      }

      public string Tag
      {
         get { return _tag; }
         set
         {
            _tag = value;
            OnPropertyChanged();
         }
      }

      public bool TagAsCurrentMonth
      {
         get { return _tagAsCurrentMonth; }
         set
         {
            _tagAsCurrentMonth = value;
            OnPropertyChanged();
         }
      }

      public bool UseSpecialCharacters
      {
         get { return _useSpecialCharacters; }
         set
         {
            _useSpecialCharacters = value;
            OnPropertyChanged();
         }
      }

      public bool UseCapitalLetters
      {
         get { return _useCapitalLetters; }
         set
         {
            _useCapitalLetters = value;
            OnPropertyChanged();
         }
      }

      public bool UseDigits
      {
         get { return _useDigits; }
         set
         {
            _useDigits = value;
            OnPropertyChanged();
         }
      }

      public int PasswordLength
      {
         get { return _passwordLength; }
         set
         {
            _passwordLength = value;
            OnPropertyChanged();
         }
      }
   }
}