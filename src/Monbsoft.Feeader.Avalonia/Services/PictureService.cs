using Avalonia.Media.Imaging;
using Monbsoft.Feeader.Avalonia.Infrastructure;
using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Monbsoft.Feeader.Avalonia.Services
{
    public static class PictureService
    {
        private static string s_cachePictureFolderPath = Path.Combine(Constants.CachePath, Constants.Pictures);
        private static HttpClient s_httpClient = new();
        
        public static void InitializePictureCache()
        {
            if (!Directory.Exists(s_cachePictureFolderPath))
                Directory.CreateDirectory(s_cachePictureFolderPath);            
        }
        public static async Task<Bitmap?> LoadPictureBitmapAsync(Uri pictureUri, CancellationToken cancellationToken)
        {
            Bitmap? picture = null;
            picture = await LoadCacheAsync(pictureUri, cancellationToken);
            if (picture == null)
            {
                picture = await LoadOnlineAsync(pictureUri, cancellationToken);
                if (picture != null)
                {
                    _ = SaveAsync(pictureUri, picture, cancellationToken);
                }
            }
            return picture;
        }
        private static ulong CreateHash64(string str)
        {
            byte[] utf8 = System.Text.Encoding.UTF8.GetBytes(str);

            ulong value = (ulong)utf8.Length;
            for (int n = 0; n < utf8.Length; n++)
            {
                value += (ulong)utf8[n] << ((n * 5) % 56);
            }

            return value;
        }       
        private static string GetCacheFileName(Uri uri)
        {
            return $"{CreateHash64(uri.ToString()).ToString()}.bmp";
        } 
        private static Task<Bitmap?> LoadCacheAsync(Uri pictureUri, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var file = new FileInfo(Path.Combine(s_cachePictureFolderPath, GetCacheFileName(pictureUri)));
                if (file.Exists)
                {
                    using (var stream = file.OpenRead())
                    {
                        return ReadPicture(stream);
                    }
                }
                return null;
            }, cancellationToken);
        }

        private static Task<Bitmap?> LoadOnlineAsync(Uri pictureUri, CancellationToken cancellationToken)
        {
            return Task.Run(async () =>
            {
                try
                {
                    var data = await s_httpClient.GetByteArrayAsync(pictureUri!, cancellationToken);                    
                    using (var stream = new MemoryStream(data))
                    {
                        var bitmap = Bitmap.DecodeToWidth(stream, 400);
                        return bitmap;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return null;
            });
        }
        private static Task SaveAsync(Uri pictureUri, Bitmap picture, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {              
                using (var fs = File.OpenWrite(Path.Combine(s_cachePictureFolderPath, GetCacheFileName(pictureUri))))
                {
                    picture.Save(fs);
                };
            }, cancellationToken);
        }

        private static Bitmap ReadPicture(Stream stream)
        {
            return Bitmap.DecodeToWidth(stream, 400);
        }
    }
}