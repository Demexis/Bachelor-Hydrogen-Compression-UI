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
        public Size TilemapSize;

        private char[,] _roadSymbols;
        private char[,] _componentSymbols;

        private Dictionary<char, Image> _roadCharRules = CompressorDeviceRules.RoadImages;

        private bool _initialized;

        private Dictionary<char, CompressorComponent> _components;

        private Task _drawingTask;


        public void SetComponentStatus(string name, CompressorComponent.ComponentStatus status)
        {
            foreach(char key in _components.Keys)
            {
                if(_components[key].Name.Equals(name))
                {
                    _components[key].Status = status;

                    Console.WriteLine($"{_components[key].Name} {_components[key].Status}");

                    this.RepaintComponents();

                    break;
                }
            }
        }

        public CompressorDevice()
        {
            InitializeComponent();
        }

        public void InitializeRoadMap(string roadTilemapData)
        {
            int width = 0, height = 1;

            int localWidth = 0;
            foreach (char c in roadTilemapData)
            {
                if(c.Equals('\n'))
                {
                    if (localWidth > width) width = localWidth;

                    localWidth = 0;
                    height++;
                }
                else
                {
                    localWidth++;
                }

            }

            TilemapSize = new Size(width, height);
            _roadSymbols = new char[width, height];

            localWidth = 0;
            int localHeight = 0;
            foreach (char c in roadTilemapData)
            {
                if (c.Equals('\n'))
                {
                    if (localWidth > width) width = localWidth;

                    localWidth = 0;
                    localHeight++;
                }
                else
                {
                    _roadSymbols[localWidth, localHeight] = c;

                    localWidth++;
                }

            }

            //for (int j = 0; j < TilemapSize.Height; j++)
            //{
            //    for (int i = 0; i < TilemapSize.Width; i++)
            //    {
            //        Console.Write(_roadSymbols[i, j]);
            //    }

            //    Console.WriteLine();
            //}

            char[,] temp = new char[TilemapSize.Width, TilemapSize.Height];

            for (int i = 0; i < TilemapSize.Width; i++)
            {
                for (int j = 0; j < TilemapSize.Height; j++)
                {
                    if(_roadSymbols[i, j].Equals('1'))
                    {
                        bool left = i > 0 && _roadSymbols[i - 1, j].Equals('1');
                        bool right = i + 1 < TilemapSize.Width && _roadSymbols[i + 1, j].Equals('1');

                        bool up = j > 0 && _roadSymbols[i, j - 1].Equals('1');
                        bool down = j + 1 < TilemapSize.Height && _roadSymbols[i, j + 1].Equals('1');

                        if(left && right && up && down)
                        {
                            temp[i, j] = '+';
                        }

                        else if(left && right && up)
                        {
                            temp[i, j] = '9';
                        }
                        else if (left && right && down)
                        {
                            temp[i, j] = '7';
                        }
                        else if (left && down && up)
                        {
                            temp[i, j] = '8';
                        }
                        else if (right && down && up)
                        {
                            temp[i, j] = '0';
                        }

                        else if (right && down)
                        {
                            temp[i, j] = '3';
                        }
                        else if (left && down)
                        {
                            temp[i, j] = '4';
                        }
                        else if (left && up)
                        {
                            temp[i, j] = '5';
                        }
                        else if (right && up)
                        {
                            temp[i, j] = '6';
                        }

                        else if (right || left)
                        {
                            temp[i, j] = '2';
                        }
                        else if (up || down)
                        {
                            temp[i, j] = '1';
                        }

                        else
                        {
                            temp[i, j] = 'n';
                        }
                    }
                }
            }

            _roadSymbols = temp;

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
            _components = components;

            _componentSymbols = new char[TilemapSize.Width, TilemapSize.Height];

            int xi = 0, xj = 0;

            for(int i = 0; i < componentsTilemapData.Length; i++)
            {
                if(componentsTilemapData[i] != '\n')
                {
                    if(_components.ContainsKey(componentsTilemapData[i]))
                    {
                        _componentSymbols[xi, xj] = componentsTilemapData[i];
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
            if (_initialized == false || _roadCharRules == null) return;

            int x = 0, y = 0, width, height;

            for (int i = 0; i < TilemapSize.Width; i++)
            {
                width = (this.Width - x) / (TilemapSize.Width - i);
                y = 0;

                for (int j = 0; j < TilemapSize.Height; j++)
                {
                    height = (this.Height - y) / (TilemapSize.Height - j);

                    Graphics g = this.CreateGraphics();

                    g.FillRectangle(new SolidBrush(Color.FromArgb(i * (255 / TilemapSize.Width), j * (255 / TilemapSize.Height), 0)), new Rectangle(x,
                            y,
                            width,
                            height));

                    if (_roadCharRules.ContainsKey(_roadSymbols[i, j]))
                    {
                        Image unscaledComponentImg = _roadCharRules[_roadSymbols[i, j]];
                        Image interpolatedComponentImg = BitmapProcessing.GetInterpolatedBitmap((Bitmap)unscaledComponentImg, new Size(width + 1, height + 1));

                        g.DrawImage(interpolatedComponentImg,
                            x,
                            y,
                            width + 1,
                            height + 1
                            );

                        Console.WriteLine($"OOO   {x} {y} {width} {height}");
                    }

                    g.Dispose();

                    y += height;
                }

                x += width;
            }
        }

        public void DrawComponents()
        {
            if (_initialized == false || _components == null) return;

            int x = 0, y = 0, width, height;

            for (int i = 0; i < TilemapSize.Width; i++)
            {
                width = (this.Width - x) / (TilemapSize.Width - i);
                y = 0;

                for (int j = 0; j < TilemapSize.Height; j++)
                {
                    height = (this.Height - y) / (TilemapSize.Height - j);

                    Graphics g = this.CreateGraphics();

                    if (_components.ContainsKey(_componentSymbols[i, j]))
                    {
                        Image unscaledComponentImg = _components[_componentSymbols[i, j]].GetImage;
                        Image interpolatedComponentImg = BitmapProcessing.GetInterpolatedBitmap((Bitmap)unscaledComponentImg, new Size(width + 1, height + 1));

                        g.DrawImage(interpolatedComponentImg,
                            x,
                            y,
                            width + 1,
                            height + 1
                            );
                    }

                    g.Dispose();

                    y += height;
                }

                x += width;
            }
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
    }
}
