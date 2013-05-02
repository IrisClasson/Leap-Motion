using System.Linq;
using System.Windows;
using Leap;

namespace Leap_WPF.Util
{
    public static class CalculationsHelper
    {
        public static Point GetNormalizedXAndY(FingerList fingers, Screen screen)
        {
            var xNormalized = screen.Intersect(fingers[0], true, 1.0F).x;
            var yNormalized = screen.Intersect(fingers[0], true, 1.0F).y;

            var x = (xNormalized * screen.WidthPixels);
            var y = screen.HeightPixels - (yNormalized * screen.HeightPixels);

            return new Point(){X = x, Y = y };
        }

        public static bool IsWithinRangeOfCloud(int x, int y)
        {
            return (Enumerable.Range(150, 300).Contains(x)
                    && Enumerable.Range(140, 400).Contains(y));
        }
    }
}
