using QKit.JumpList;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Data;
using Yakuza.PasswordGenerator.Model;

namespace Yakuza.PasswordGenerator.Converters
{
   class AlphaGroupsByLabelsConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, string language)
      {
         var input = value as IEnumerable<PasswordEntry>;
         var labels = input.SelectMany(x => x.Labels.Split(' ').Select(l => l.Trim())).Distinct();
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
