using MVVM_Uebung.Common;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MVVM_Uebung
{
    public class BindableBase : INotifyPropertyChanged, IUnique
    {

        private string id;
        public string ID
        {
            get
            {
                if (id == null)
                    this.id = Guid.NewGuid().ToString();
                return id;
            }
            set { id = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Setzt die mit “ref” übergebene Backingvariable auf den neuen Wert value und informiert die XAML Elemente darüber
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <returns>Gibt zurück, ob sich der Wert überhaupt geändert hat.</returns>
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName]string propertyName = "")
        {
            if (object.Equals(field, value))
                return false;

            field = value;
            RaisePropertyChanged(propertyName);
            return true;
        }
        /// <summary>
        /// Informiert XAML Elemente darüber, dass sich die Eigenschaft mit dem übergebenen Namen geändert hat
        /// </summary>
        /// <param name="propertyName"></param>
        protected void RaisePropertyChanged(string propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                var args = new PropertyChangedEventArgs(propertyName);
                handler(this, args);
            }
        }
    }
}
