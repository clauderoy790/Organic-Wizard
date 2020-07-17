using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Tesseract;

namespace Shared
{
    public static class ImgUtils
    {
        private static TesseractEngine _engine = null;

        static ImgUtils()
        {
            Init();
        }
        
        public static void Init()
        {
            if (_engine == null)
            {
                try
                {
                    _engine = new TesseractEngine("Data/tessdata", "eng", EngineMode.Default);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Tesseract engine failed to initialize: {ex.Message}");
                }
            }
        }

        public static ProcessImageInfo ProcessImage(int x, int y, int rightX, int rightY, float zoomFactor = 1)
        {
            string result = "";
            int width = rightX - x;
            int height = rightY - y;
            Bitmap bmp = new Bitmap(width, height);
            Rectangle bounds = new Rectangle(x, y, width, height);
            using (Graphics g = Graphics.FromImage(bmp))
                g.CopyFromScreen(bounds.Location, Point.Empty, bounds.Size);
            if (zoomFactor > 1)
            {
                bmp = ZoomImage(bmp, zoomFactor);
            }
            using (var page = _engine.Process(bmp))
            {
                result = page.GetText();
            }

            return new ProcessImageInfo()
            {
                Text = result,
                Bmp = bmp
            };
        }

        public static ProcessImageInfo ProcessImage(string imagePath)
        {
            string result = "";
            Bitmap bmp = (Bitmap)Image.FromFile(imagePath);
            using (var page = _engine.Process(bmp))
            {
                result = page.GetText();
            }

            return new ProcessImageInfo()
            {
                Text = result,
                Bmp = bmp
            };
        }

        private static Bitmap ZoomImage(Bitmap originalBitmap, float zoomFactor)
        {
            System.Drawing.Size newSize = new System.Drawing.Size((int)(originalBitmap.Width * zoomFactor), (int)(originalBitmap.Height * zoomFactor));
            Bitmap bmp = new Bitmap(originalBitmap, newSize);
            return bmp;
        }

        public class ProcessImageInfo
        {
            public Bitmap Bmp { get; set; }
            public string Text { get; set; }
        }
    }
}
