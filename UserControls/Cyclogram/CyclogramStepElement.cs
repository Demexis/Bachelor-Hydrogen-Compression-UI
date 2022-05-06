using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachelor_Project
{
    [Serializable]
    public class CyclogramStepElement
    {
        public string Name { get; set; }

        public List<CyclogramSequenceElement> Sequences = new List<CyclogramSequenceElement>();

        public int LengthMilliseconds = 0;

        public CyclogramStepElement(string name, int lengthMilliseconds)
        {
            this.Name = name;
            this.LengthMilliseconds = lengthMilliseconds;
        }

        public CyclogramStepElement(string name, int lengthMilliseconds, List<CyclogramSequenceElement> sequences)
        {
            this.Name = name;
            this.LengthMilliseconds = lengthMilliseconds;
            this.Sequences = sequences;
        }

        public override string ToString()
        {
            return $"{this.Name} ({LengthMilliseconds} ms)";
        }
    }
}
