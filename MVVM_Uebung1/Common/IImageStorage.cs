using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;
namespace MVVM_Uebung.Common
{
    public interface IImageStorage
    {
        string FolderName { set; }
        Task<BitmapImage> RestoreImageAsync(string fileName);
        Task<IStorageFile> SaveImageAsync(IStorageFile file, string fileName, int desiredWidth);
    }
}
