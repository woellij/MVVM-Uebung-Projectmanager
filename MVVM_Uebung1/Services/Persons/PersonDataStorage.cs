using MVVM_Uebung.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace MVVM_Uebung.Services.Persons
{
    class PersonDataStorage : BaseStorage<Person>, IPersonDataStorage
    {
        public PersonDataStorage(IDataStorage dataStorage, IImageStorage imageStorage)
            : base(dataStorage, imageStorage, "Persons")
        {
        }

        public override async Task<IEnumerable<Person>> RestoreAsync()
        {
            var profiles = await base.RestoreAsync();
            if (profiles != null)
                foreach (var p in profiles)
                {
                    var image = await this.GetImageAsync(p.ID);
                    p.Image = image;
                }
            return profiles;
        }

        public async Task<IStorageFile> SavePictureAsync(IStorageFile file, Person person, int desiredWidth)
        {
            return await base.SaveImageAsync(file, person.ID, desiredWidth);
        }
    }
}
