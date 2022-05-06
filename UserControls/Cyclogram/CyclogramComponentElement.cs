using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor_Project
{
    [Serializable]
    public class CyclogramComponentElement
    {
        public string Name { get; set; }

        public List<CyclogramStatusElement> Statuses = new List<CyclogramStatusElement>();
    }
}
