using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor_Project_Hydrogen_Compression_WinForms.UserControls.Device
{
    public class CompressorPipe : IContainImage
    {
        public enum PipeType { Gas, Oil }
        public enum PipeOrientation { StraightHorizontal, StraightVertical, Cross, T_Left, T_Up, T_Right, T_Down, LU_Corner, RU_Corner, LD_Corner, RD_Corner }
        public enum PipeStatus { Empty, Filled }

        public PipeType Type;
        public PipeOrientation Orientation;
        public PipeStatus Status;

        public Dictionary<string, Image> PipeImages;

        public CompressorPipe(PipeType type, PipeOrientation orientation, PipeStatus status = PipeStatus.Empty)
        {
            this.Type = type;
            this.Orientation = orientation;
            this.Status = status;
            this.PipeImages = CompressorDeviceRules.GetPipeImages(type);
        }

        public Image GetImage()
        {
            Image img = (Image)PipeImages[Orientation.ToString() + Status.ToString()].Clone();

            return img;
        }
    }
}
