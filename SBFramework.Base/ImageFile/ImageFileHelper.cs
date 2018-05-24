using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using SBFramework.Base.Text;

namespace SBFramework.Base.ImageFile
{
    public static class ImageFileHelper
    {
        public const string DefaultImageDirectory = "C:\\ImageCrypto\\Files\\";
        public const string StartOfFile = ".SOF.";
        public const string EndOfFile = ".EOF.";

        public static void ToImage(this byte[] bytes, string directory = DefaultImageDirectory, string fileName = "CryptedFile.jpg")
        {
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
            var completePath = directory + fileName;

            using (var ms = new MemoryStream(bytes))
            {
                Image.FromStream(ms).Save(completePath, ImageFormat.Jpeg);
            }
        }

        /// <summary>
        /// Create a image with inverted colors.
        /// Base Font: https://stackoverflow.com/questions/190385/how-to-manipulate-images-at-the-pixel-level-in-c-sharp
        /// </summary>
        /// <param name="this"></param>
        /// <param name="destFileName"></param>
        public static void CreateNegative(this Image @this, string destFileName)
        {
            var bmp = new Bitmap(@this);

            var diffBm = new Bitmap(bmp.Width, bmp.Height, bmp.PixelFormat);

            for (var y = 0; y < bmp.Height; y++)
            {
                for (var x = 0; x < bmp.Width; x++)
                {
                    // Get Both Colours at the pixel point
                    var col2 = bmp.GetPixel(x, y);

                    // Get the difference RGB
                    var r = Math.Abs(255 - col2.R);
                    var g = Math.Abs(255 - col2.G);
                    var b = Math.Abs(255 - col2.B);

                    // Create new grayscale RGB colour
                    var newcol = Color.FromArgb(r, g, b);

                    diffBm.SetPixel(x, y, newcol);
                }
            }

            diffBm.Save(destFileName, bmp.RawFormat);
        }

        public static void Diff(this Image @thisImage, Image iSrc2, string destFileName)
        {
            var @this = new Bitmap(@thisImage);
            var src2 = new Bitmap(iSrc2);

            var diffBm = new Bitmap(@this.Width, @this.Height, @this.PixelFormat);

            for (var y = 0; y < @this.Height; y++)
            {
                for (var x = 0; x < @this.Width; x++)
                {
                    //Get Both Colours at the pixel point
                    var col1 = @this.GetPixel(x, y);
                    var col2 = src2.GetPixel(x, y);

                    // Get the difference RGB
                    var r = Math.Abs(col1.R - col2.R);
                    var g = Math.Abs(col1.G - col2.G);
                    var b = Math.Abs(col1.B - col2.B);

                    // Invert the difference average
                    var dif = 255 - ((r + g + b) / 3);

                    // Create new grayscale RGB colour
                    var newcol = Color.FromArgb(dif, dif, dif);

                    diffBm.SetPixel(x, y, newcol);
                }
            }

            diffBm.Save(destFileName, @this.RawFormat);
        }

        public static List<int> SetTextOnImage(this Image @this, string text, string destFileName)
        {
            var bmp = new Bitmap(@this);
            var bytes = (StartOfFile + text + EndOfFile).ToByte();
            var diffBm = new Bitmap(bmp.Width, bmp.Height, bmp.PixelFormat);
            var totalLenth = (bmp.Height * bmp.Width);
            var privateKey = GenerateKey(bytes.Length, totalLenth);

            if ((totalLenth < bytes.Length) || privateKey.Count != bytes.Length) throw new ArgumentException();

            var tbCounter = 0;
            var next = privateKey[tbCounter];
            var position = 0;

            for (var y = 0; y < bmp.Height; y++)
            {
                for (var x = 0; x < bmp.Width; x++)
                {
                    // Get Both Colours at the pixel point
                    var col2 = bmp.GetPixel(x, y);

                    int r;

                    if ((position < next) || (bytes.Length < (tbCounter + 1))) r = col2.R;
                    else
                    {
                        var valueByte = Convert.ToInt32(bytes[tbCounter]);
                        var sum = Convert.ToInt32(col2.R) + valueByte;
                        if (sum > 255) sum -= 255;
                        r = sum;
                        tbCounter++;

                        if (tbCounter < privateKey.Count)
                            next += privateKey[tbCounter];
                    }

                    var g = Math.Abs(col2.G);
                    var b = Math.Abs(col2.B);

                    var newcol = Color.FromArgb(r, g, b);

                    diffBm.SetPixel(x, y, newcol);

                    position++;
                }
            }

            diffBm.Save(destFileName, bmp.RawFormat);
            return privateKey;
        }

        public static string GetTextFromImage(this Image @this, Image originalFile, List<int> privateKey)
        {
            if (@this.Width != originalFile.Width || @this.Height != originalFile.Height) throw new ArgumentException();
            var bmp = new Bitmap(@this);
            var original = new Bitmap(originalFile);

            var tbCounter = 0;
            var next = privateKey[tbCounter];
            var position = 0;
            var bytesCrypted = new List<byte>();
            var bytesOriginal = new List<byte>();
            var retBytes = new List<byte>();

            for (var y = 0; y < bmp.Height; y++)
            {
                for (var x = 0; x < bmp.Width; x++)
                {
                    if (position == next)
                    {
                        bytesCrypted.Add(bmp.GetPixel(x, y).R);
                        bytesOriginal.Add(original.GetPixel(x, y).R);
                        tbCounter++;
                        if (privateKey.Count == tbCounter) break;
                        next += privateKey[tbCounter];
                    }
                    position++;
                }
                if (privateKey.Count == tbCounter) break;
            }

            for (var i = 0; i < bytesCrypted.Count; i++)
            {
                if (bytesCrypted[i] >= bytesOriginal[i])
                    retBytes.Add(Convert.ToByte(bytesCrypted[i] - bytesOriginal[i]));
                else
                {
                    var val = Convert.ToByte(bytesCrypted[i] + (255 - bytesOriginal[i]));
                    retBytes.Add(val);
                }
            }

            var retString = Encoding.UTF8.GetString(retBytes.ToArray());

            if (retString.StartsWith(StartOfFile) && retString.EndsWith(EndOfFile))
                return retString.Replace(StartOfFile, string.Empty).Replace(EndOfFile, string.Empty);

            throw new InvalidOperationException();
        }

        public static long GetArea(this Image @this)
        {
            return @this.Width * @this.Height;
        }

        private static List<int> GenerateKey(long lenthText, long lenthImage)
        {
            var maxDistancia = Convert.ToInt32(Math.Abs(lenthImage / lenthText));
            var retList = new List<int>();
            var random = new Random();

            for (var i = 0; i < lenthText; i++)
            {
                retList.Add(random.Next(1, maxDistancia));
            }

            return retList;
        }
    }
}
