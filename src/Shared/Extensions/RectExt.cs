using System.Drawing;
using System.Windows;

namespace Shared
{
    public static class RectExt
    {
        public static bool ToIntRect(this Rectangle rect, out Int32Rect intRect)
        {
            intRect = new Int32Rect();

            if (rect.X % 1 != 0 || rect.Y % 1 != 0 || rect.Width % 1 != 0 || rect.Height % 1 != 0)
                return false;

            intRect.X = (int)rect.X;
            intRect.Y = (int)rect.Y;
            intRect.Width = (int)rect.Width;
            intRect.Height = (int)rect.Height;

            return true;
        }
    }
}
