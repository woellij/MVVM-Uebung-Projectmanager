using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace MVVM_Uebung.Services.Projects
{
    interface IProjectDataStorage : ITypedStorage<Project>, IImageHandler<Project>
    {

    }
}
