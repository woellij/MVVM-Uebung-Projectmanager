using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage.Pickers;
using Windows.Storage.FileProperties;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.Storage;
using MVVM_Uebung.Common;
using Windows.Media.Capture;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace MVVM_Uebung
{
    class Person : BindableBase
    {

        public Person()
        {

        }

        public Person(string vorname, string nachname, DateTime birthdate)
        {
            this.BirthDate = birthdate;
            this.Vorname = vorname;
            this.Nachname = nachname;
        }

        private ImageSource image;

        public ImageSource Image
        {
            get { return image; }
            set { this.SetProperty(ref image, value); }
        }


        private DateTimeOffset birthDate;
        public DateTimeOffset BirthDate
        {
            get { return birthDate; }
            set
            {
                this.SetProperty(ref birthDate, value);
                RaisePropertyChanged("Age");
            }
        }

        private string vorname;
        public string Vorname
        {
            get { return vorname; }
            set
            {
                this.SetProperty(ref vorname, value);
                this.RaisePropertyChanged("Name");
            }
        }

        private string nachname;
        public string Nachname
        {
            get { return nachname; }
            set
            {
                this.SetProperty(ref nachname, value);
                this.RaisePropertyChanged("Name");
            }
        }

        public string Name
        {
            get { return Vorname + " " + Nachname; }
        }

        private string gender;
        public string Gender
        {
            get { return gender; }
            set { this.SetProperty(ref gender, value); }
        }

        private void Reset()
        {
            this.Vorname = "";
            this.Nachname = "";
            this.BirthDate = DateTimeOffset.MinValue;
        }

        public ICommand ResetCommand
        {
            get { return new RelayCommand(Reset); }
        }
    }
}
