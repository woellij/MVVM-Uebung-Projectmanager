using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Uebung.Common.State
{
    interface IStateSaving
    {
        Task SaveStateAsync();
        Task RestoreStateAsync();
    }
}
