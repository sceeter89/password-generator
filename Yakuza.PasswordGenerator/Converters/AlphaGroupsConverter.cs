using QKit.JumpList;
using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Data;
using Yakuza.PasswordGenerator.Model;

namespace Yakuza.PasswordGenerator.Converters
{
   class AlphaGroupsConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, string language)
      {
         var input = value as ObservableCollection<PasswordEntry>;
         if (input != null)
            return input.ToAlphaGroups(x => x.Domain);

         return null;
      }

      public object ConvertBack(object value, Type targetType, object parameter, string language)
      {
         throw new NotImplementedException();
      }
   }
}
