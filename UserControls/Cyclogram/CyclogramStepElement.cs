using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor_Project_Hydrogen_Compression_WinForms
{
    public class CyclogramStepElement
    {
        public string Name { get; set; }

        public List<CyclogramSequenceElement> Sequences = new List<CyclogramSequenceElement>();

        public int LengthMilliseconds = 0;
    }
}
