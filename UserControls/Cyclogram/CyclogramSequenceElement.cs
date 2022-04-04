using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor_Project_Hydrogen_Compression_WinForms
{
    public class CyclogramSequenceElement
    {
        public bool Active = false;

        //public int Pos;
        public string SequenceID;
        //public string AfterID;
        public string TitleID;
        //public int Length;

        public CyclogramSequenceElement(string sequenceID, string textID)
        {
            //this.Pos = pos;
            this.SequenceID = sequenceID;
            //this.AfterID = afterID;
            this.TitleID = textID;
            //this.Length = length;
        }
    }
}
