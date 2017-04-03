using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DnDScreenCapture.Utils
{
    public static class BitmapConverterExtension
    {
        /// <summary>
        /// BitmapオブジェクトのインスタンスからMemorySteamを取得します。
        /// </summary>
        /// <param name="bmp">Bitmapクラスのインスタンス</param>
        /// <returns>MemorySteamオブジェクトのインスタンス</returns>
        public static MemoryStream GetMemoryStream(this Bitmap bmp)
        {
            var ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Bmp);
            return ms;
        }

        /// <summary>
        /// BitmapオブジェクトのインスタンスからBitmapFrameを取得します。
        /// </summary>
        /// <param name="bmp">Bitmapクラスのインスタンス</param>
        /// <returns>BitmapFrameオブジェクトのインスタンス</returns>
        public static BitmapFrame GetBitmapFrame(this Bitmap bmp)
        {
            using (var ms = bmp.GetMemoryStream())
            {
                return BitmapFrame.Create(ms, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            }
        }
    }
}
