using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using Yakuza.PasswordGenerator.Model;

namespace Yakuza.PasswordGenerator.Services
{
   public static class PasswordStorage
   {
      static PasswordStorage()
      {
         Entries = new ObservableCollection<PasswordEntry>();
      }

      static void Entries_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
      {
         if (e.NewItems != null)
            foreach (var item in e.NewItems)
            {
               var passwordEntry = (PasswordEntry)item;
               if (passwordEntry.Id <= 0)
                  InsertItem(passwordEntry);
            }

         if (e.OldItems != null)
            foreach (var item in e.OldItems)
            {
               if (e.Action != NotifyCollectionChangedAction.Remove)
                  return;

               RemoveItem((PasswordEntry)item);
            }
      }

      private static void RemoveItem(PasswordEntry item)
      {
         using (var connection = NewConnection())
         {
            connection.CreateTable<PasswordEntry>();

            connection.Delete<PasswordEntry>(item.Id);
            connection.Commit();
         }
      }

      private static void InsertItem(PasswordEntry entry)
      {
         using (var connection = NewConnection())
         {
            connection.CreateTable<PasswordEntry>();

            var id = connection.Insert(entry);
            //entry.Id = id;

            connection.Commit();
         }
      }

      public static void UpdateItem(PasswordEntry entry)
      {
         using (var connection = NewConnection())
         {
            connection.CreateTable<PasswordEntry>();

            connection.Update(entry);
            connection.Commit();
         }
      }

      public static void Initialize()
      {
         Entries.Clear();

         var entries = Load();

         foreach (var entry in entries)
            Entries.Add(entry);

         Entries.CollectionChanged += Entries_CollectionChanged;
      }

      private static IEnumerable<PasswordEntry> Load()
      {
         using (var connection = NewConnection())
         {
            connection.CreateTable<PasswordEntry>();

            return connection.Table<PasswordEntry>().ToList();
         }
      }

      private static SQLiteConnection NewConnection()
      {
         var connection = new SQLiteConnection(new SQLitePlatformWinRT(), "passwords.db");

         return connection;
      }

      public static ObservableCollection<PasswordEntry> Entries { get; private set; }
   }
}