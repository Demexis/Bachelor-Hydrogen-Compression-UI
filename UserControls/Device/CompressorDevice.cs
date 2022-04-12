using Bachelor_Project_Hydrogen_Compression_WinForms.Processing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bachelor_Project_Hydrogen_Compression_WinForms.UserControls.Device
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

        public Size TilemapSize;

        private bool _initialized;

        public bool StretchImageLayout = false;

        public CompressorComponent[] GetComponents 
        {
            get
            {
                List<CompressorComponent> components = new List<CompressorComponent>();

                for (int i = 0; i < _components.GetLength(0); i++)
                {
                    for (int j = 0; j < _components.GetLength(1); j++)
                    {
                        if (_components[i, j] != null) components.Add(_components[i, j]);
                    }
                }

                return components.ToArray();
            }
        }

        private CompressorComponent[,] _components;

        private CompressorPipe[,] _gasPipes;
        private CompressorPipe[,] _oilPipes;

        private Task _drawingTask;


        public void SetComponentStatus(string name, CompressorComponent.ComponentStatus status)
        {
            foreach(CompressorComponent component in _components)
            {
                if (component == null) continue;

                if(component.Name.Equals(name))
                {
                    component.Status = status;

                    //Console.WriteLine($"{component.Name} {component.Status}");

                    this.RepaintComponents();

                    break;
                }
            }
        }

        public CompressorDevice()
        {
            InitializeComponent();
        }

        public void InitializeRoadmap(Size tilemapSize)
        {
            this.TilemapSize = tilemapSize;
        }

        public void InitializePipes(string roadTilemapData, CompressorPipe.PipeType pipeType)
        {
            char[,] roadSymbols = new char[TilemapSize.Width, TilemapSize.Height];


            if(pipeType == CompressorPipe.PipeType.Gas)
                _gasPipes = new CompressorPipe[TilemapSize.Width, TilemapSize.Height];
            else if(pipeType == CompressorPipe.PipeType.Oil)
                _oilPipes = new CompressorPipe[TilemapSize.Width, TilemapSize.Height];

            CompressorPipe[,] arrayRef;

            if (pipeType == CompressorPipe.PipeType.Gas)
                arrayRef = _gasPipes;
            else if (pipeType == CompressorPipe.PipeType.Oil)
                arrayRef = _oilPipes;
            else
                return;


            int localWidth = 0;
            int localHeight = 0;
            foreach (char c in roadTilemapData)
            {
                if (c.Equals('\n'))
                {
                    localWidth = 0;
                    localHeight++;
                }
                else
                {
                    roadSymbols[localWidth, localHeight] = c;

                    localWidth++;
                }

            }

            char[,] temp = new char[TilemapSize.Width, TilemapSize.Height];

            for (int i = 0; i < TilemapSize.Width; i++)
            {
                for (int j = 0; j < TilemapSize.Height; j++)
                {
                    if(roadSymbols[i, j].Equals('1'))
                    {
                        bool left = i > 0 && roadSymbols[i - 1, j].Equals('1');
                        bool right = i + 1 < TilemapSize.Width && roadSymbols[i + 1, j].Equals('1');

                        bool up = j > 0 && roadSymbols[i, j - 1].Equals('1');
                        bool down = j + 1 < TilemapSize.Height && roadSymbols[i, j + 1].Equals('1');

                        if(left && right && up && down)
                        {
                            arrayRef[i, j] = new CompressorPipe(pipeType, CompressorPipe.PipeOrientation.Cross);
                        }

                        else if(left && right && up)
                        {
                            arrayRef[i, j] = new CompressorPipe(pipeType, CompressorPipe.PipeOrientation.T_Up);
                        }
                        else if (left && right && down)
                        {
                            arrayRef[i, j] = new CompressorPipe(pipeType, CompressorPipe.PipeOrientation.T_Down);
                        }
                        else if (left && down && up)
                        {
                            arrayRef[i, j] = new CompressorPipe(pipeType, CompressorPipe.PipeOrientation.T_Left);
                        }
                        else if (right && down && up)
                        {
                            arrayRef[i, j] = new CompressorPipe(pipeType, CompressorPipe.PipeOrientation.T_Right);
                        }

                        else if (right && down)
                        {
                            arrayRef[i, j] = new CompressorPipe(pipeType, CompressorPipe.PipeOrientation.LU_Corner);
                        }
                        else if (left && down)
                        {
                            arrayRef[i, j] = new CompressorPipe(pipeType, CompressorPipe.PipeOrientation.RU_Corner);
                        }
                        else if (left && up)
                        {
                            arrayRef[i, j] = new CompressorPipe(pipeType, CompressorPipe.PipeOrientation.RD_Corner);
                        }
                        else if (right && up)
                        {
                            arrayRef[i, j] = new CompressorPipe(pipeType, CompressorPipe.PipeOrientation.LD_Corner);
                        }

                        else if (right || left)
                        {
                            arrayRef[i, j] = new CompressorPipe(pipeType, CompressorPipe.PipeOrientation.StraightHorizontal);
                        }
                        else if (up || down)
                        {
                            arrayRef[i, j] = new CompressorPipe(pipeType, CompressorPipe.PipeOrientation.StraightVertical);
                        }

                        else
                        {
                            arrayRef[i, j] = null;
                        }
                    }
                }
            }

            roadSymbols = temp;

            //Console.WriteLine(TilemapSize);

            //Console.WriteLine(_roadSymbols);

            //for (int j = 0; j < TilemapSize.Height; j++)
            //{
            //    for (int i = 0; i < TilemapSize.Width; i++)
            //    {
            //        Console.Write(_roadSymbols[i, j]);
            //    }

            //    Console.WriteLine();
            //}

            _initialized = true;

            DrawRoadmap();
        }

        public void InitializeDeviceComponents(string componentsTilemapData, Dictionary<char, CompressorComponent> components)
        {
            _components = new CompressorComponent[TilemapSize.Width, TilemapSize.Height];

            int xi = 0, xj = 0;

            for(int i = 0; i < componentsTilemapData.Length; i++)
            {
                if(componentsTilemapData[i] != '\n')
                {
                    if(components.ContainsKey(componentsTilemapData[i]))
                    {
                        _components[xi, xj] = components[componentsTilemapData[i]];
                    }

                    xi++;
                }
                else
                {
                    xj++;
                    xi = 0;
                }
            }

            //for (int j = 0; j < TilemapSize.Height; j++)
            //{
            //    for (int i = 0; i < TilemapSize.Width; i++)
            //    {

            //        Console.Write(_componentSymbols[i, j]);
            //    }

            //    Console.WriteLine();
            //}
        }

        public void DrawRoadmap()
        {
            Draw(_gasPipes);
            Draw(_oilPipes);
        }

        private void Draw<T>(T[,] charData)
        {
            if (_initialized == false || charData == null || charData.GetLength(0) != TilemapSize.Width || charData.GetLength(1) != TilemapSize.Height) return;

            int x, y, width, height, tileWidth, tileHeight;

            (x, y, width, height, tileWidth, tileHeight) = GetTilemapData();

            for (int i = 0; i < TilemapSize.Width; i++)
            {
                if (StretchImageLayout) tileWidth = (width - x) / (TilemapSize.Width - i);
                y = 0;

                for (int j = 0; j < TilemapSize.Height; j++)
                {
                    if (StretchImageLayout) tileHeight = (height - y) / (TilemapSize.Height - j);

                    Graphics g = this.CreateGraphics();

                    //g.FillRectangle(new SolidBrush(Color.FromArgb(i * (255 / TilemapSize.Width), j * (255 / TilemapSize.Height), 0)), new Rectangle(x,
                    //        y,
                    //        tileWidth,
                    //        tileHeight));

                    if (charData[i, j] != null)
                    {
                        Image unscaledComponentImg = (charData[i, j] as IContainImage).GetImage();
                        Image interpolatedComponentImg = BitmapProcessing.GetInterpolatedBitmap((Bitmap)unscaledComponentImg, new Size(tileWidth + 1, tileHeight + 1));

                        g.DrawImage(interpolatedComponentImg,
                            x,
                            y,
                            tileWidth + 1,
                            tileHeight + 1
                        );
                    }

                    g.Dispose();

                    y += tileHeight;
                }

                x += tileWidth;
            }
        }

        public void DrawComponents()
        {
            Draw(_components);
        }

        private void CompressorDevice_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void CompressorDevice_Paint(object sender, PaintEventArgs e)
        {
            Repaint();
        }

        private void Repaint()
        {
            Action action = () => { DrawRoadmap(); DrawComponents(); };

            if(_drawingTask == null || _drawingTask.IsCompleted)
            {
                _drawingTask = new Task(action);
                _drawingTask.Start();
            }

            //DrawRoadmap();
            //DrawComponents();
        }

        private void RepaintComponents()
        {
            Action action = () => { DrawComponents(); };

            if (_drawingTask == null || _drawingTask.IsCompleted)
            {
                _drawingTask = new Task(action);
                _drawingTask.Start();
            }

            //DrawComponents();
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
    }
}
