using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using Yakuza.PasswordGenerator.Model;
using Yakuza.PasswordGenerator.Services;
using System;
using System.Collections.Specialized;
using Yakuza.PasswordGenerator.Messages.Actions;
using System.Collections.Generic;
using System.Linq;

namespace Yakuza.PasswordGenerator.ViewModel
{
   public class BrowseDirectoriesViewModel : ViewModelBase
   {
      public class VirtualDirectory
      {
         public VirtualDirectory()
         {
            this.Subdirectories = new List<VirtualDirectory>();
            this.Entries = new List<PasswordEntry>();
         }

         public string Name { get; set; }
         public IList<VirtualDirectory> Subdirectories { get; set; }
         public IList<PasswordEntry> Entries { get; set; }

         public VirtualDirectory GetSubdirectory(string name)
         {
            VirtualDirectory subdir = Subdirectories.FirstOrDefault(x => x.Name == name);
            if (subdir != null)
            {
               return subdir;
            }

            subdir = new VirtualDirectory { Name = name };
            Subdirectories.Add(subdir);
            return subdir;
         }
      }

      private VirtualDirectory _current;
      private readonly ObservableCollection<PasswordEntry> _entries;
      private readonly VirtualDirectory _rootDir;

      public BrowseDirectoriesViewModel(IMessenger messenger)
      {
         this._entries = PasswordStorage.Entries;
         _entries.CollectionChanged += OnCollectionChanged;
         messenger.Register<MoveEntryToDirectoryMessage>(this, HandleEntryMoved);
         RebuildCatalog();

         if (ViewModelBase.IsInDesignModeStatic)
         {
            this.Current = new VirtualDirectory
            {
               Name = "/",
               Entries = new[] {
                  new PasswordEntry { Domain = "domain1.com", Tag= "Some tag", Username = "username1" },
                  new PasswordEntry { Domain = "domain2.com", Tag= "Some tag", Username = "username1" },
                  new PasswordEntry { Domain = "domain3.com", Tag= "Some tag", Username = "username1" },
                  new PasswordEntry { Domain = "domain4.com", Tag= "Some tag", Username = "username1" }
               },
               Subdirectories = new[] {
                  new VirtualDirectory { Name = "Subdir 1" },
                  new VirtualDirectory { Name = "Subdir 2" },
                  new VirtualDirectory { Name = "Subdir 3" }
               }
            };
         }
      }

      private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
      {
         RebuildCatalog();
      }

      private void HandleEntryMoved(MoveEntryToDirectoryMessage message)
      {
         RebuildCatalog();
      }

      private void RebuildCatalog()
      {
         var root = new VirtualDirectory { Name = "/" };

         foreach (var entry in _entries)
         {
            string[] paths = entry.EntryPath.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            VirtualDirectory current = root;
            for (int i = 0; i < paths.Length; i++)
            {
               current = root.GetSubdirectory(paths[i]);
            }
            current.Entries.Add(entry);
         }
      }

      public VirtualDirectory Current
      {
         get
         {
            return _current;
         }
         set
         {
            this._current = value;
            RaisePropertyChanged();
         }
      }
   }
}
