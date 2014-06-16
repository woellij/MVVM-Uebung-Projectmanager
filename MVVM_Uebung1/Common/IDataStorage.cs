using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace MVVM_Uebung.Common
{
    public interface IDataStorage
    {
        Task<T> RestoreAsync<T>(string filename);
        Task SaveAsync(object obj, string filename, Func<Type, IEnumerable<string>> getRelevantPropertiesCallback);
    }
}
