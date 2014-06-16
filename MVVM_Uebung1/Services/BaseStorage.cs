using MVVM_Uebung.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace MVVM_Uebung.Services
{
    public abstract class BaseStorage<T> : MVVM_Uebung.Services.ITypedStorage<T> where T : class, IUnique
    {
        private IDataStorage dataStorage;
        private IImageStorage imageStorage;
        private string filename;

        public BaseStorage(IDataStorage dataStorage, IImageStorage imageStorage, string fileName)
        {
            this.filename = fileName;
            this.dataStorage = dataStorage;
            this.imageStorage = imageStorage;
            this.imageStorage.FolderName = filename + "Pictures";
        }

        public virtual async Task<IEnumerable<T>> RestoreAsync()
        {
            var restored = await dataStorage.RestoreAsync<IEnumerable<T>>(filename);
            return restored;
        }
        public async virtual Task SaveAsync(IEnumerable<T> items)
        {
            await dataStorage.SaveAsync(items, filename, GetRelevantPropertiesNames);
        }

        protected async Task<BitmapImage> GetImageAsync(string name)
        {
            return await imageStorage.RestoreImageAsync(name);
        }
        protected async Task<IStorageFile> SaveImageAsync(IStorageFile file, string name, int desiredWidth)
        {
            return await imageStorage.SaveImageAsync(file, name, desiredWidth);
        }

        public async Task UpdateAsync(T updated)
        {
            var restored = new List<T>(await dataStorage.RestoreAsync<IEnumerable<T>>(filename));
            for (int i = 0; i < restored.Count(); i++)
            {
                var rest = restored[i];
                if (rest.ID == updated.ID)
                {
                    restored[i] = updated;
                    break;
                }
            }
            await dataStorage.SaveAsync(restored, filename, GetRelevantPropertiesNames);
        }

        #region Serialization properties
        protected virtual IEnumerable<string> GetRelevantPropertiesNames(Type type)
        {
            return PropertiesToString(GetRelevantProperties(type));
        }

        protected IEnumerable<string> PropertiesToString(IEnumerable<PropertyInfo> properties)
        {
            return properties.Select(prop => prop.Name);
        }

        protected virtual IEnumerable<PropertyInfo> GetRelevantProperties(Type type)
        {
            var properties = type.GetRuntimeProperties();
            return properties;
        }
        #endregion

    }
}
