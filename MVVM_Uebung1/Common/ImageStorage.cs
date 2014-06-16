using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace MVVM_Uebung.Common
{
    class ImageStorage : IImageStorage
    {
        private IStorageFolder folder;
        private string folderName;
        public string FolderName
        {
            set { folderName = value; }
        }

        public ImageStorage()
        {
        }

        public async Task<BitmapImage> RestoreImageAsync(string fileName)
        {
            await EnsureFolderExistsAsync(folderName);
            try
            {
                var file = await folder.GetFileAsync(fileName);
                var image = new BitmapImage();
                await image.SetSourceAsync(await file.OpenReadAsync());
                return image;
            }
            catch
            {
                return null;
            }
        }

        private async Task EnsureFolderExistsAsync(string folderName)
        {
            if (folder == null)
                folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);
        }


        public async Task<IStorageFile> SaveImageAsync(IStorageFile file, string fileName, int desiredWidth)
        {
            await EnsureFolderExistsAsync(folderName);
            if (file != null)
            {
                using (StorageItemThumbnail thumbnail = await (file as StorageFile).GetThumbnailAsync(ThumbnailMode.PicturesView, 100))
                {
                    if (thumbnail != null && thumbnail.Type == ThumbnailType.Image)
                    {
                        var destinationFile = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                        Windows.Storage.Streams.Buffer buffer = new Windows.Storage.Streams.Buffer(Convert.ToUInt32(thumbnail.Size));

                        IBuffer iBuf = await thumbnail.ReadAsync(buffer, buffer.Capacity, InputStreamOptions.None);
                        using (var strm = await destinationFile.OpenAsync(FileAccessMode.ReadWrite))
                        {
                            await strm.WriteAsync(iBuf);
                        }
                        return destinationFile;
                    }
                }
            }
            return null;
        }

    }
}
