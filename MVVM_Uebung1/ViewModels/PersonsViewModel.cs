using MVVM_Uebung.Common;
using MVVM_Uebung.Common.State;
using MVVM_Uebung.Services.Persons;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Media.Imaging;

namespace MVVM_Uebung
{
    class PersonsViewModel : BindableBase, IStateSaving
    {
        public ObservableCollection<Person> Persons { get; set; }

        private Person selectedPerson;
        public Person SelectedPerson
        {
            get { return selectedPerson; }
            set { this.SetProperty(ref selectedPerson, value); }
        }

        IPersonDataStorage personStorage;

        public PersonsViewModel(IPersonDataStorage personStorage)
        {
            Persons = new ObservableCollection<Person>();
            this.personStorage = personStorage;
        }

        public ICommand AddPersonCommand
        {
            get { return new RelayCommand(AddPerson); }
        }

        private void AddPerson()
        {
            var newPerson = new Person("Vorname", "Nachname", DateTime.Now);
            RaisePropertyChanged();
            Persons.Add(newPerson);
            SelectedPerson = newPerson;
        }

        public async Task SaveStateAsync()
        {
            await personStorage.SaveAsync(Persons);
        }

        public async Task RestoreStateAsync()
        {
            var restored = await personStorage.RestoreAsync();
            if (restored != null)
                foreach (var restoredPerson in restored)
                    this.Persons.Add(restoredPerson);
        }

        public ICommand AddProfilePictureCommand
        {
            get { return new RelayCommand(AddProfilePicture); }
        }

        public ICommand TakeProfilePictureCommand
        {
            get { return new RelayCommand(TakeAndAddProfilePicture); }
        }

        private async void TakeAndAddProfilePicture()
        {
            CameraCaptureUI camera = new CameraCaptureUI();

            StorageFile file = await camera.CaptureFileAsync(CameraCaptureUIMode.Photo);
            if (file != null)
            {
                var thumb = await this.personStorage.SavePictureAsync(file, selectedPerson, 50);
                await SetImageSource(thumb);
            }
        }

        private async void AddProfilePicture()
        {
            var image = await LetUserPickImageAsync();
            var thumb = await this.personStorage.SavePictureAsync(image, selectedPerson, 50);
            await SetImageSource(thumb);
        }

        private async Task SetImageSource(IStorageFile image)
        {
            var bmp = new BitmapImage();
            await bmp.SetSourceAsync(await image.OpenReadAsync());
            this.SelectedPerson.Image = bmp;
        }

        private async Task<StorageFile> LetUserPickImageAsync()
        {
            FileOpenPicker picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;

            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            var img = await picker.PickSingleFileAsync();
            return img;
        }
    }
}
