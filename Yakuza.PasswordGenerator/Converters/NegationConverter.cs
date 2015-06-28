using System;
using Windows.UI.Xaml.Data;

namespace Yakuza.PasswordGenerator.Converters
{
   public class NegationConverter:IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, string language)
      {
         if(value is bool == false)
            return null;

         var typedValue = (bool) value;

         return typedValue == false;
      }

      public object ConvertBack(object value, Type targetType, object parameter, string language)
      {
         throw new NotImplementedException();
      }
   }
}