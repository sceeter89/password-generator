using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System.Collections.ObjectModel;

namespace Yakuza.PasswordGenerator.Model
{
   [Table("Directories")]
   public class Directory : ModelBase
   {
      private int _id;
      private string _name;
      private ObservableCollection<Directory> _subdirectories;
      private ObservableCollection<PasswordEntry> _entries;

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

      public string Name
      {
         get { return _name; }
         set
         {
            _name = value;
            OnPropertyChanged();
         }
      }

      [OneToMany]
      public ObservableCollection<Directory> Subdirectories
      {
         get { return _subdirectories; }
         set
         {
            _subdirectories = value;
            OnPropertyChanged();
         }
      }

      [OneToMany]
      public ObservableCollection<PasswordEntry> Entries
      {
         get { return _entries; }
         set
         {
            _entries = value;
            OnPropertyChanged();
         }
      }
   }
}
