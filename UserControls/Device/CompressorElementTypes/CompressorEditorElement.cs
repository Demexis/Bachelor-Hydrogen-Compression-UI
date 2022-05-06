using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor_Project.UserControls.Device
{
    public class CompressorEditorElement : CompressorElement
    {
        public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override Image GetImage()
        {
            return Properties.Resources.editor_tile;
        }
    }
}
