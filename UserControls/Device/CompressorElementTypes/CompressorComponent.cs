using Bachelor_Project.Processing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor_Project.UserControls.Device
{
    public class CompressorComponent : CompressorElement
    {
        public string Name;

        public bool Selected = false;

        public enum ComponentType { Valve, CounterTrigger, OpticalSensor, Reservoir, Pump }
        public enum ComponentOrientation { Horizontal, Vertical }
        public enum ComponentStatus { Disabled, Active, Inactive }

        public ComponentOrientation Orientation;
        public ComponentType Type;
        public ComponentStatus Status;

        public string SensorName;

        public Dictionary<string, Image> ComponentImages => CompressorDeviceRules.GetComponentImages(Type);

        private float _fillAmount;
        public float FillAmount
        {
            get
            {
                return _fillAmount;
            }
            set
            {
                _fillAmount = value > 1f ? 1f : (value < 0f ? 0f : value);
            }
        }

        public CompressorComponent(string name, ComponentType type, ComponentOrientation orientation,
             ComponentStatus status = ComponentStatus.Disabled, float fillAmount = 1)
        {
            this.Name = name;
            this.Orientation = orientation;
            this.Type = type;
            this.Status = status;
            //this.ComponentImages = CompressorDeviceRules.GetComponentImages(type);
            this._fillAmount = fillAmount;

        }

        public override Image GetImage()
        {
            Image img = (Image)ComponentImages[Orientation.ToString() + ComponentStatus.Disabled.ToString()].Clone();

            string key = Orientation.ToString() + Status.ToString();
            Image fillOverlay = ComponentImages[key];

            Graphics g = Graphics.FromImage(img);

            int x, y;
            Rectangle srcActiveRect;
            Rectangle destRect = default(Rectangle), sourceRect = default(Rectangle);

            if (Orientation == ComponentOrientation.Horizontal)
            {
                x = 0;
                y = 0;

                srcActiveRect = new Rectangle(x, y, (int)(fillOverlay.Width * FillAmount), fillOverlay.Height);

                if (srcActiveRect.Width == 0) srcActiveRect.Width = 1;

                fillOverlay = ((Bitmap)(fillOverlay)).Clone(srcActiveRect, fillOverlay.PixelFormat);

                destRect = new Rectangle(x, y, (int)(img.Width * FillAmount), img.Height);
                sourceRect = new Rectangle(0, 0, fillOverlay.Width, fillOverlay.Height);
            }
            else if (Orientation == ComponentOrientation.Vertical)
            {
                x = 0;
                y = (int)(img.Height * (1f - FillAmount));

                srcActiveRect = new Rectangle(x, y, fillOverlay.Width, (int)(fillOverlay.Height * FillAmount));

                if (srcActiveRect.Height == 0) srcActiveRect.Height = 1;

                fillOverlay = ((Bitmap)(fillOverlay)).Clone(srcActiveRect, fillOverlay.PixelFormat);

                destRect = new Rectangle(x, y, img.Width, img.Height - y);
                sourceRect = new Rectangle(0, 0, fillOverlay.Width, fillOverlay.Height);

                //Console.WriteLine(destRect);
                //Console.WriteLine(sourceRect);
            }

            if (!(destRect.Width == 0 || destRect.Height == 0 || sourceRect.Width == 0 || sourceRect.Height == 0))
            {
                g.DrawImage(fillOverlay, destRect, sourceRect, GraphicsUnit.Pixel);
            }

            g.Dispose();
            fillOverlay.Dispose();

            if(Selected)
            {
                Image selectionImg = new Bitmap(img.Width, img.Height);
                Graphics gS = Graphics.FromImage(selectionImg);
                gS.FillRectangle(new SolidBrush(Color.Green), new Rectangle(new Point(), selectionImg.Size));

                img = BitmapProcessing.GetMergedBitmaps((Bitmap)selectionImg, (Bitmap)img);
            }


            return img;
        }

        public override string ToString()
        {
            return this.Name;
        }

        public override object Clone()
        {
            return new CompressorComponent(this.Name, this.Type, this.Orientation, this.Status, this.FillAmount);
        }
    }
}
