using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Uebung.Common
{
    public interface IUnique
    {
        string ID { get; }
        bool Equals(object obj);
    }
}
