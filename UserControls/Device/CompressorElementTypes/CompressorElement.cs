using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor_Project.UserControls.Device
{
    public abstract class CompressorElement : IContainImage, ICloneable
    {
        public abstract object Clone();
        public abstract Image GetImage();
    }
}
