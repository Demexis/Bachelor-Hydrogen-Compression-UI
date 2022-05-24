using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor_Project.UserControls.Device
{
    public class CompressorLayer
    {
        public enum LayerTypeEnum { Editor, GasPipes, OilPipes, Components }

        public LayerTypeEnum LayerType;
        private CompressorElement[,] _elements;
        public CompressorElement GetElement(int x, int y) => _elements[x, y];

        public CompressorElement[,] GetElements => _elements;

        public Action ElementChanged { get; set; }

        public void SetElement(CompressorElement element, int x, int y)
        {
            _elements[x, y] = element;
            ElementChanged?.Invoke();
        }

        public void SetElements(CompressorElement[,] elements)
        {
            _elements = elements;
            ElementChanged?.Invoke();
        }

        public CompressorLayer(Size size, LayerTypeEnum layerType)
        {
            _elements = new CompressorElement[size.Width, size.Height];
            LayerType = layerType;

            if (LayerType == LayerTypeEnum.Editor)
                RearrangeEditorTiles();
        }

        public void RearrangeElements(Size size)
        {
            CompressorElement[,] temp = (CompressorElement[,])_elements.Clone();

            _elements = new CompressorElement[size.Width, size.Height];

            for (int i = 0; i < temp.GetLength(0) && i < _elements.GetLength(0); i++)
            {
                for (int j = 0; j < temp.GetLength(1) && j < _elements.GetLength(1); j++)
                {
                    _elements[i, j] = temp[i, j];
                }
            }

            if(LayerType == LayerTypeEnum.Editor)
                RearrangeEditorTiles();

            Console.WriteLine($"Rearranged: {_elements.GetLength(0)}, {_elements.GetLength(1)}");
        }

        private void RearrangeEditorTiles()
        {
            for (int i = 0; i < _elements.GetLength(0); i++)
            {
                for (int j = 0; j < _elements.GetLength(1); j++)
                {
                    _elements[i, j] = new CompressorEditorElement();
                }
            }
        }

        public (string, Image)[] GetImagesOgLayerElements()
        {
            List<(string, Image)> namesAndImgs = new List<(string, Image)>();

            switch (LayerType)
            {
                case LayerTypeEnum.Components:

                    foreach (string componentType in Enum.GetNames(typeof(CompressorComponent.ComponentType)))
                    {
                        namesAndImgs.Add(($"Horizontal {componentType}", CompressorDeviceRules.GetComponentImages((CompressorComponent.ComponentType)Enum.Parse(typeof(CompressorComponent.ComponentType), componentType))["HorizontalDisabled"]));
                        namesAndImgs.Add(($"Vertical {componentType}", CompressorDeviceRules.GetComponentImages((CompressorComponent.ComponentType)Enum.Parse(typeof(CompressorComponent.ComponentType), componentType))["VerticalDisabled"]));
                    }
                    break;

                case LayerTypeEnum.GasPipes:

                    namesAndImgs.Add(("", CompressorDeviceRules.GasPipeImages["CrossEmpty"]));
                    break;

                case LayerTypeEnum.OilPipes:

                    namesAndImgs.Add(("", CompressorDeviceRules.GasPipeImages["CrossEmpty"]));
                    break;

                default:
                    break;
            }

            return namesAndImgs.ToArray();
        }

        public void ApplyRulesToTheLayer()
        {
            Size tilemapSize = new Size(_elements.GetLength(1), _elements.GetLength(0));

            switch (LayerType)
            {
                case LayerTypeEnum.Components:
                    break;
                case LayerTypeEnum.GasPipes:

                    CompressorPipe.PipeType pipeType = CompressorPipe.PipeType.Gas;

                    bool[,] temp = new bool[tilemapSize.Height, tilemapSize.Width];

                    for (int i = 0; i < tilemapSize.Height; i++)
                    {
                        for (int j = 0; j < tilemapSize.Width; j++)
                        {
                            temp[i, j] = (_elements[i, j] != null);
                        }
                    }

                    for (int i = 0; i < tilemapSize.Height; i++)
                    {
                        for (int j = 0; j < tilemapSize.Width; j++)
                        {
                            if (temp[i, j])
                            {
                                bool left = i > 0 && temp[i - 1, j];
                                bool right = i + 1 < tilemapSize.Height && temp[i + 1, j];

                                bool up = j > 0 && temp[i, j - 1];
                                bool down = j + 1 < tilemapSize.Width && temp[i, j + 1];

                                if (left && right && up && down)
                                {
                                    _elements[i, j] = new CompressorPipe(pipeType, CompressorPipe.PipeOrientation.Cross);
                                }

                                else if (left && right && up)
                                {
                                    _elements[i, j] = new CompressorPipe(pipeType, CompressorPipe.PipeOrientation.T_Up);
                                }
                                else if (left && right && down)
                                {
                                    _elements[i, j] = new CompressorPipe(pipeType, CompressorPipe.PipeOrientation.T_Down);
                                }
                                else if (left && down && up)
                                {
                                    _elements[i, j] = new CompressorPipe(pipeType, CompressorPipe.PipeOrientation.T_Left);
                                }
                                else if (right && down && up)
                                {
                                    _elements[i, j] = new CompressorPipe(pipeType, CompressorPipe.PipeOrientation.T_Right);
                                }

                                else if (right && down)
                                {
                                    _elements[i, j] = new CompressorPipe(pipeType, CompressorPipe.PipeOrientation.LU_Corner);
                                }
                                else if (left && down)
                                {
                                    _elements[i, j] = new CompressorPipe(pipeType, CompressorPipe.PipeOrientation.RU_Corner);
                                }
                                else if (left && up)
                                {
                                    _elements[i, j] = new CompressorPipe(pipeType, CompressorPipe.PipeOrientation.RD_Corner);
                                }
                                else if (right && up)
                                {
                                    _elements[i, j] = new CompressorPipe(pipeType, CompressorPipe.PipeOrientation.LD_Corner);
                                }

                                else if (right || left)
                                {
                                    _elements[i, j] = new CompressorPipe(pipeType, CompressorPipe.PipeOrientation.StraightHorizontal);
                                }
                                else if (up || down)
                                {
                                    _elements[i, j] = new CompressorPipe(pipeType, CompressorPipe.PipeOrientation.StraightVertical);
                                }

                                else
                                {
                                    _elements[i, j] = _elements[i, j];
                                }
                            }
                        }


                    }

                    break;
            }

            ElementChanged?.Invoke();

        }
        
    }
}
