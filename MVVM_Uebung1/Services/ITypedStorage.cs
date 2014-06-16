using MVVM_Uebung.Common;
using System;
using System.Threading.Tasks;
namespace MVVM_Uebung.Services
{
    interface ITypedStorage<T>
     where T : class, IUnique
    {
        Task<System.Collections.Generic.IEnumerable<T>> RestoreAsync();
        Task SaveAsync(System.Collections.Generic.IEnumerable<T> items);
        Task UpdateAsync(T project);
    }
}
