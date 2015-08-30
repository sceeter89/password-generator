using QKit.JumpList;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Data;
using Yakuza.PasswordGenerator.Model;

namespace Yakuza.PasswordGenerator.Converters
{
   class AlphaGroupsByLabelsConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, string language)
      {
         var input = value as ObservableCollection<PasswordEntry>;
         var labels = input.SelectMany(x => x.Labels.Split(' ').Select(l => l.Trim()));
         if (input != null)
            return labels.ToAlphaGroups(x => x);

         return null;
      }

      public object ConvertBack(object value, Type targetType, object parameter, string language)
      {
         throw new NotImplementedException();
      }
   }
}
