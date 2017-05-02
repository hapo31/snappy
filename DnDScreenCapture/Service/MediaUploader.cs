using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CoreTweet;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace DnDScreenCapture.Service
{
    public class MediaUploader
    {
        private Service.Twitter twitterService;

        public MediaUploader(Service.Twitter twitterService)
        {
            this.twitterService = twitterService;
        }

        /// <summary>
        /// BitmapをPng形式にしてMemoryStreamを返す
        /// </summary>
        /// <param name="bitmap">変換前のデータが入ったBitmap</param>
        /// <returns></returns>
        public MemoryStream ConvertToPNGBytes(Bitmap bitmap)
        {
            var mem = new MemoryStream();
            bitmap.Save(mem, ImageFormat.Png);
            mem.Seek(0, SeekOrigin.Begin);
            return mem;
        } 

        /// <summary>
        /// 指定したバイトストリームの内容をTwitterにアップロードする
        /// </summary>
        /// <param name="stream">アップロードする画像データのバイトストリーム</param>
        /// <returns></returns>
        public async Task<MediaUploadResult[]> MediaUpload(List<Stream> stream )
        {
            return await Task.WhenAll(stream.Select(async data =>
            {
                return await twitterService.Token.Media.UploadAsync(media: data);
            }));
        }

        /// <summary>
        /// 指定したバイトストリームの内容をTwitterにアップロードする
        /// </summary>
        /// <param name="stream">アップロードする画像データのバイトストリーム</param>
        /// <returns></returns>
        public async Task<MediaUploadResult> MediaUpload(Stream stream)
        {
            return await twitterService.Token.Media.UploadAsync(media: stream);
        }

    }
}
