using MVVM_Uebung.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Uebung
{
    class Assignment : BindableBase
    {
        private ObservableCollection<Person> assignedPersons;

        public ObservableCollection<Person> AssignedPersons
        {
            get { return assignedPersons; }
            set { assignedPersons = value; }
        }
    }
}
