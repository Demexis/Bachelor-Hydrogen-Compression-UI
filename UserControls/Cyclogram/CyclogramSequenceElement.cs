using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor_Project
{
    [Serializable]
    public class CyclogramSequenceElement
    {
        public bool Active = false;

        public string ComponentName;
        public string StatusName;

        public CyclogramSequenceElement(string componentName, string statusName)
        {
            this.ComponentName = componentName;
            this.StatusName = statusName;
        }
    }
}
