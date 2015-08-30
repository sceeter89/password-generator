using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Yakuza.PasswordGenerator.Model
{
   public class ModelBase : INotifyPropertyChanged
   {
      public event PropertyChangedEventHandler PropertyChanged;

      protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
      {
         var handler = PropertyChanged;
         if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
      }
   }
}
