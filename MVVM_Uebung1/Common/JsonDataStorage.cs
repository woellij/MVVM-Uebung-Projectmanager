using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Windows.Storage;
using Windows.Storage.Streams;
using System.Windows.Input;
using System.Reflection;
using Newtonsoft.Json.Serialization;
using System.IO;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Media;

namespace MVVM_Uebung.Common
{
    class JsonDataStorage : IDataStorage
    {
        private IStorageFolder Folder { get { return ApplicationData.Current.LocalFolder; } }

        private JsonSerializerSettings SerializationSettings { get { return new JsonSerializerSettings(); } }

        public async Task SaveAsync(object obj, string filename, Func<Type, IEnumerable<string>> getRelevantPropertiesCallback)
        {
            if (getRelevantPropertiesCallback != null)
            {
                var resolver = new CContractResolver(getRelevantPropertiesCallback);
                SerializationSettings.ContractResolver = resolver;
            }

            var json = await Task.Run(() => JsonConvert.SerializeObject(obj, this.SerializationSettings));
            var file = await Folder.CreateFileAsync(filename + ".json", CreationCollisionOption.ReplaceExisting);
            await Windows.Storage.FileIO.WriteTextAsync(file, json);
        }

        public async Task<T> RestoreAsync<T>(string filename)
        {
            try
            {
                var file = await Folder.GetFileAsync(filename + ".json");
                var json = await Windows.Storage.FileIO.ReadTextAsync(file);
                return await Task.Run(() => JsonConvert.DeserializeObject<T>(json, this.SerializationSettings));
            }
            catch
            {
                return default(T);
            }
        }
    }

    class CContractResolver : DefaultContractResolver
    {
        private Func<Type, IEnumerable<string>> getRelevantPropertiesCallback;

        public CContractResolver(Func<Type, IEnumerable<string>> getRelevantPropertiesCallback)
        {
            this.getRelevantPropertiesCallback = getRelevantPropertiesCallback;
        }
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> props = base.CreateProperties(type, memberSerialization);
            props = props.Where(p =>
                p.Writable && p.PropertyType != typeof(ICommand)
                && p.PropertyType != typeof(ImageSource)
                && getRelevantPropertiesCallback(type).Contains(p.PropertyName)).ToList();
            return props;
        }
    }
}
