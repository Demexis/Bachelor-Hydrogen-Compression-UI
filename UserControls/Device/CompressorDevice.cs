using Bachelor_Project.Forms.Options_Forms;
using Bachelor_Project.Miscellaneous;
using Bachelor_Project.Processing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bachelor_Project.UserControls.Device
{
    public partial class CompressorDevice : UserControl
    {
        public enum DeviceAlignment { Left, Middle, Right }

        [Category("Appearance"), Description("Sets or gets alignment for device scheme.")]
        public DeviceAlignment Alignment
        {
            get => _alignment;
            set => _alignment = value;
        }

        private DeviceAlignment _alignment;


        private Size _tilemapSize = new Size(10, 10);
        public Size TilemapSize { get { return _tilemapSize; } set { _tilemapSize = value; OnTilemapSizeChange?.Invoke(value); Refresh(); } }

        public bool CanBeResized(Size size) { return true; } // TO DO

        public Action<Size> OnTilemapSizeChange;

        //private bool _initialized;

        public bool StretchImageLayout { get; set; } = false;

        public bool EditorMode { get; set; } = false;


        public Dictionary<CompressorLayer.LayerTypeEnum, CompressorLayer> Layers; 


        public void SetComponentStatus(string name, CompressorComponent.ComponentStatus status)
        {
            //foreach(CompressorComponent component in _components)
            //{
            //    if (component == null) continue;

            //    if(component.Name.Equals(name))
            //    {
            //        component.Status = status;

            //        //Console.WriteLine($"{component.Name} {component.Status}");

            //        //this.RepaintComponents();

            //        break;
            //    }
            //}
        }

        public CompressorDevice()
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            Layers = new Dictionary<CompressorLayer.LayerTypeEnum, CompressorLayer>()
            {
                [CompressorLayer.LayerTypeEnum.Editor] = new CompressorLayer(TilemapSize, CompressorLayer.LayerTypeEnum.Editor),
                [CompressorLayer.LayerTypeEnum.GasPipes] = new CompressorLayer(TilemapSize, CompressorLayer.LayerTypeEnum.GasPipes),
                [CompressorLayer.LayerTypeEnum.OilPipes] = new CompressorLayer(TilemapSize, CompressorLayer.LayerTypeEnum.OilPipes),
                [CompressorLayer.LayerTypeEnum.Components] = new CompressorLayer(TilemapSize, CompressorLayer.LayerTypeEnum.Components)
            };

            foreach(CompressorLayer layer in Layers.Values)
            {
                OnTilemapSizeChange += layer.RearrangeElements;
                layer.ElementChanged += this.Refresh;
            }

        }


        public CompressorComponent[] GetComponentsArray()
        {
            List<CompressorComponent> components = new List<CompressorComponent>();

            int rows = Layers[CompressorLayer.LayerTypeEnum.Components].GetElements.GetLength(0);
            int cols = Layers[CompressorLayer.LayerTypeEnum.Components].GetElements.GetLength(1);

            for (int y = 0; y < cols; y++)
            {
                for (int x = 0; x < rows; x++)
                {
                    CompressorComponent component = (CompressorComponent)Layers[CompressorLayer.LayerTypeEnum.Components].GetElements[x, y];

                    if (component != null) components.Add(component);
                }
            }

            return components.ToArray();
        }



        public void InitializeRoadmap(Size tilemapSize)
        {
            this.TilemapSize = tilemapSize;
        }

        


        private void Draw(CompressorLayer layer, Graphics g)
        {
            if (layer == null) return;

            int x, y, width, height, tileWidth, tileHeight;

            (x, y, width, height, tileWidth, tileHeight) = GetTilemapData();

            int initY = y;

            //Console.WriteLine(layer.GetElement.GetLength(0) + " " + layer.GetElement.GetLength(1));

            for (int i = 0; i < TilemapSize.Width && i < layer.GetElements.GetLength(0); i++)
            {
                if (StretchImageLayout) tileWidth = (width - x) / (TilemapSize.Width - i);
                y = initY;

                for (int j = 0; j < TilemapSize.Height && j < layer.GetElements.GetLength(1); j++)
                {
                    if (StretchImageLayout) tileHeight = (height - y) / (TilemapSize.Height - j);

                    //Graphics g = this.CreateGraphics();

                    //g.FillRectangle(new SolidBrush(Color.FromArgb(i * (255 / TilemapSize.Width), j * (255 / TilemapSize.Height), 0)), new Rectangle(x,
                    //        y,
                    //        tileWidth,
                    //        tileHeight));

                    if (layer.GetElement(i, j) != null)
                    {
                        Image unscaledComponentImg = (layer.GetElement(i, j) as IContainImage).GetImage();
                        Image interpolatedComponentImg = BitmapProcessing.GetInterpolatedBitmap((Bitmap)unscaledComponentImg, new Size(tileWidth + 1, tileHeight + 1));

                        if(layer.LayerType == CompressorLayer.LayerTypeEnum.Editor)
                        {
                            if(AppearanceOptionsForm.SelectedColorPalette != null)
                            {
                                // Set the image attribute's color mappings
                                ColorMap[] colorMap = new ColorMap[2];
                                colorMap[0] = new ColorMap();
                                colorMap[0].OldColor = Color.White;
                                colorMap[0].NewColor = AppearanceOptionsForm.SelectedColorPalette[FormColorVariant.BrightSecond];

                                colorMap[1] = new ColorMap();
                                colorMap[1].OldColor = Color.Black;
                                colorMap[1].NewColor = AppearanceOptionsForm.SelectedColorPalette[FormColorVariant.NormalFirst];

                                ImageAttributes attr = new ImageAttributes();
                                attr.SetRemapTable(colorMap);

                                // Draw using the color map
                                Rectangle rect = new Rectangle(x, y, tileWidth + 1, tileHeight + 1);
                                g.DrawImage(interpolatedComponentImg, rect, 0, 0, rect.Width, rect.Height, GraphicsUnit.Pixel, attr);
                            }

                        }
                        else
                            g.DrawImage(interpolatedComponentImg,
                                x,
                                y,
                                tileWidth + 1,
                                tileHeight + 1
                            );

                        if(layer.LayerType == CompressorLayer.LayerTypeEnum.Components)
                        {
                            g.DrawString(((CompressorComponent)layer.GetElement(i, j)).Name,
                                this.Font,
                                new SolidBrush(Color.White),
                                new Point(x, y));
                        }
                    }

                    //g.Dispose();

                    y += tileHeight;
                }

                x += tileWidth;
            }
        }


        private void DrawClear(Graphics g)
        {
            g.FillRectangle(new SolidBrush(BackColor),
                new Rectangle(0, 0, Width, Height));
        }

        private void CompressorDevice_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void CompressorDevice_Paint(object sender, PaintEventArgs e)
        {
            Repaint(e);
        }

        private void Repaint(PaintEventArgs e)
        {
            DrawClear(e.Graphics); 
            /*DrawRoadmap(); DrawComponents();*/ 
            if(EditorMode) Draw(Layers[CompressorLayer.LayerTypeEnum.Editor], e.Graphics);
            Draw(Layers[CompressorLayer.LayerTypeEnum.GasPipes], e.Graphics);
            Draw(Layers[CompressorLayer.LayerTypeEnum.OilPipes], e.Graphics);
            Draw(Layers[CompressorLayer.LayerTypeEnum.Components], e.Graphics);
        }


        private Tuple<int, int, int, int, int, int> GetTilemapData()
        {
            int x = 0, y = 0, width, height;

            int tileWidth, tileHeight;

            tileWidth = this.Width / TilemapSize.Width;
            tileHeight = this.Height / TilemapSize.Height;

            if (StretchImageLayout)
            {
                width = this.Width;
                height = this.Height;
            }
            else
            {
                width = tileWidth > tileHeight ? tileHeight * TilemapSize.Width : tileWidth * TilemapSize.Width;
                height = tileWidth > tileHeight ? tileHeight * TilemapSize.Height : tileWidth * TilemapSize.Height;

                tileWidth = tileWidth > tileHeight ? tileHeight : tileWidth;
                tileHeight = tileWidth > tileHeight ? tileHeight : tileWidth;

                switch(_alignment)
                {
                    case DeviceAlignment.Left:
                        break;
                    case DeviceAlignment.Middle:
                        x = this.Width / 2 - width / 2;
                        y = this.Height / 2 - height / 2;
                        break;
                    case DeviceAlignment.Right:
                        x = this.Width - width;
                        y = this.Height - height;
                        break;
                }
            }

            return new Tuple<int, int, int, int, int, int>(x, y, width, height, tileWidth, tileHeight);
        }

        private void CompressorDevice_Load(object sender, EventArgs e)
        {
            //Repaint();
        }

        public bool MousePosInsideTheGrid(Point mousePos)
        {
            int x, y, width, height, tileWidth, tileHeight;

            (x, y, width, height, tileWidth, tileHeight) = GetTilemapData();

            return (Mathf.Between(mousePos.X, x, x + width) && Mathf.Between(mousePos.Y, y, y + height));
        }

        public Point MousePosToGridPos(Point mousePos)
        {
            int x, y, width, height, tileWidth, tileHeight;

            (x, y, width, height, tileWidth, tileHeight) = GetTilemapData();

            Console.WriteLine($"{x} {y} {width} {height} {tileWidth} {tileHeight}");
            Console.WriteLine($"{this.Width} {this.Height}");

            return new Point((mousePos.X - x) / tileWidth, (mousePos.Y - y) / tileHeight);
        }
        
    }
    
}
