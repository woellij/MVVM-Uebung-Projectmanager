using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Uebung.Common
{
    class ViewModelBase : BindableBase
    {
        private INavigation navigation;

        public INavigation Navigation
        {
            get { return navigation; }
        }

        public ViewModelBase(INavigation navigation)
        {
            this.navigation = navigation;
        }

        internal virtual void Init(object parameter)
        {

        }
    }
}
