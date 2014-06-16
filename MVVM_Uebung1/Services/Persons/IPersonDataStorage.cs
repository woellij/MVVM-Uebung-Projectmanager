using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;

namespace MVVM_Uebung.Services.Persons
{
    interface IPersonDataStorage : ITypedStorage<Person>, IImageHandler<Person>
    {
    }
}
