using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace MVVM_Uebung.Services
{
    public interface IImageHandler<T>
    {
        Task<IStorageFile> SavePictureAsync(IStorageFile file, T person, int desiredWidth);
    }
}
