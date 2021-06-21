using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using Tesseract;

namespace Shared
{
    public static class ImgUtils
    {
        private const string TESSDATA_FOLDER = "Data/tessdata";
        private static OcrEngineMgr _engineMgr = null;

        static ImgUtils()
        {
            Init();
        }

        public static void Init()
        {
            if (_engineMgr == null)
            {
                try
                {
                    string tessDir = Path.Combine(Directory.GetCurrentDirectory(), TESSDATA_FOLDER);
                    if (!Directory.Exists(tessDir))
                    {
                        tessDir = GetTessdataPath();
                        if (!Directory.Exists(tessDir))
                            throw new DirectoryNotFoundException("Failed to find tessdata folder, make sure it is somewhere in your solution");
                    }
                    _engineMgr = new OcrEngineMgr(tessDir, "eng", EngineMode.Default,5);
                }
                catch (Exception ex)
                {
                    Debug.Log($"Tesseract engine failed to initialize: {ex.Message}");
                }
            }
        }

        public static LockBitmap GetLockBitmap(Int32Rect invRec)
        {
            return GetLockBitmap(invRec.X, invRec.Y, invRec.Width, invRec.Height);
        }

        public static LockBitmap GetLockBitmap(Rectangle rect)
        {
            Int32Rect rectInt = new Int32Rect();

            if (!rect.ToIntRect(out rectInt))
                throw new ArgumentException("parameter rect must not contain decimals.");

            return GetLockBitmap(rectInt);
        }

        public static LockBitmap GetLockBitmap(int x, int y, int w, int h)
        {
            LockBitmap lockb = null;
            Rectangle bounds = new Rectangle(x, y, w, h);
            Bitmap bmp = new Bitmap(w, h);
            int maxTries = 3;
            int nbTries = 0;

            while (lockb == null && ++nbTries <= maxTries)
            {
                try
                {
                    using (Graphics g = Graphics.FromImage(bmp))
                        g.CopyFromScreen(bounds.Location, System.Drawing.Point.Empty, bounds.Size);
                    lockb = new LockBitmap(bmp);
                }
                catch (Exception ex)
                {
                    Debug.Log("Failed to take screenshot: " + ex.Message);
                }
            }

            if (lockb == null)
                throw new Exception("Failed to create bitmap");

            return lockb;
        }

        private static string GetTessdataPath()
        {
            string path = "";
            string solutionDir = GetSlnFolderPath();

            var directories = Directory.GetDirectories(solutionDir);
            foreach (var directory in directories)
            {
                string tessFolder = Path.Combine(solutionDir, directory, TESSDATA_FOLDER);
                if (Directory.Exists(tessFolder))
                {
                    path = tessFolder;
                    break;
                }
            }

            return path;
        }

        private static string GetSlnFolderPath()
        {
            DirectoryInfo curDir = new DirectoryInfo(Directory.GetCurrentDirectory());
            while (curDir.Parent != null && curDir.Parent.Exists)
            {
                var files = Directory.GetFiles(curDir.FullName).ToList();
                if (files.Any((f) => { return f.EndsWith(".sln"); }))
                {
                    return curDir.FullName;
                }
                curDir = curDir.Parent;
            }

            return "";
        }

        public static ProcessImageInfo ProcessImage(double x, double y, double rightX, double bottomY, float zoomFactor = 1)
        {
            ProcessImageInfo info = new ProcessImageInfo();
            string result = "";
            double width = rightX - x;
            double height = bottomY - y;
            Bitmap bmp = new Bitmap(width.ToInt(), height.ToInt());
            Rectangle bounds = new Rectangle(x.ToInt(), y.ToInt(), width.ToInt(), height.ToInt());
            try
            {
                using (Graphics g = Graphics.FromImage(bmp))
                    g.CopyFromScreen(bounds.Location, System.Drawing.Point.Empty, bounds.Size);
                if (zoomFactor > 1)
                {
                    bmp = ZoomImage(bmp, zoomFactor);
                }

                using (Page page = _engineMgr.Process(bmp))
                {
                    result = page.GetText();
                    if (!string.IsNullOrEmpty(result))
                        result = result.Trim().ToLower();
                }
                info.Text = result;
                info.Bmp = bmp;
                info.IsValidText = !string.IsNullOrEmpty(result);
            }
            catch (Exception ex)
            {
                Debug.Log("Error while processing image: " + ex.Message);
                info.Ex = ex;
                info.IsValidText = false;
            }
            return info;
        }

        public static ProcessImageInfo ProcessImage(string imagePath)
        {
            string result = "";
            Bitmap bmp = (Bitmap)Image.FromFile(imagePath);
            using (var page = _engineMgr.Process(bmp))
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
            public Exception Ex { get; set; }
            public bool IsValidText { get; set; }
        }
    }
}
