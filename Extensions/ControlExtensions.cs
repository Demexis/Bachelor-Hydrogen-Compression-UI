using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bachelor_Project.Extensions
{
    public static class ControlExtensions
    {
        public static IList<T> GetAllControlsRecusrvive<T>(this Control control) where T : Control
        {
            List<T> rtn = new List<T>();
            foreach (Control item in control.Controls)
            {
                T ctr = item as T;
                if (ctr != null)
                {
                    rtn.Add(ctr);
                }
                else
                {
                    rtn.AddRange(GetAllControlsRecusrvive<T>(item));
                }

            }
            return rtn;
        }

    }
}
