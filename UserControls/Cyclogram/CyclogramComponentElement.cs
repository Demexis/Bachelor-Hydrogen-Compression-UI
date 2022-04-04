using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor_Project_Hydrogen_Compression_WinForms
{
    public class CyclogramComponentElement
    {
        public string Name { get; set; }
        public List<CyclogramStatusElement> Titles = new List<CyclogramStatusElement>();
    }
}
