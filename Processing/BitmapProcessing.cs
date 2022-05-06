using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor_Project.Processing
{
    public static class BitmapProcessing
    {
        public static Bitmap GetInterpolatedBitmap(Bitmap original, Size desiredSize)
        {
            Bitmap result = new Bitmap(desiredSize.Width, desiredSize.Height);

            using (Graphics g = Graphics.FromImage(result))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

                Point[] dest =
                {
                    new Point(0, 0),
                    new Point(desiredSize.Width, 0),
                    new Point(0, desiredSize.Height),
                };
                Rectangle source = new Rectangle(0, 0, original.Width, original.Height);

                g.DrawImage(original, dest, source, GraphicsUnit.Pixel);
            }
            return result;
        }

        public static Bitmap GetMergedBitmaps(params Bitmap[] bitmaps)
        {
            Point maxSize = Point.Empty;
            foreach (Bitmap bitmap in bitmaps)
            {
                if (bitmap.Width > maxSize.X)
                {
                    maxSize.X = bitmap.Width;
                }

                if (bitmap.Height > maxSize.Y)
                {
                    maxSize.Y = bitmap.Height;
                }
            }

            Bitmap result = new Bitmap(maxSize.X, maxSize.Y);

            using (Graphics g = Graphics.FromImage(result))
            {
                foreach (Bitmap bitmap in bitmaps)
                {
                    Point[] dest =
                    {
                        new Point(0, 0),
                        new Point(bitmap.Width, 0),
                        new Point(0, bitmap.Height),
                    };
                    Rectangle source = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

                    g.DrawImage(bitmap, dest, source, GraphicsUnit.Pixel);
                }
            }

            return result;
        }
    }
}
