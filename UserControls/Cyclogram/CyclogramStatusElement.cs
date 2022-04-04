using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor_Project_Hydrogen_Compression_WinForms
{
    public class CyclogramStatusElement
    {
        public string TitleID { get; set; }
        public string Text { get; set; }

        public CyclogramComponentElement Category { get; set; }

        public CyclogramStatusElement(string id, string text, CyclogramComponentElement category)
        {
            this.TitleID = id;
            this.Text = text;
            this.Category = category;
        }
    }
}
