using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Uebung
{
    class Genders : IEnumerable<string>
    {
        public IEnumerable<string> genders
        {
            get { return new string[] { "männlich", "weiblich" }; }
        }

        public IEnumerator<string> GetEnumerator()
        {
            return genders.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return genders.GetEnumerator();
        }
    }
}
