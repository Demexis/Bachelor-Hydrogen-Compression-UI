using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor_Project_Hydrogen_Compression_WinForms.Processing
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
    }
}
