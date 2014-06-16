using MVVM_Uebung.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace MVVM_Uebung
{
    class Project : BindableBase
    {
        private string id;
        public string ID
        {
            get { return id ?? (id = Guid.NewGuid().ToString()); }
            set { id = value; }
        }

        public string Title { get; set; }
        public string Description { get; set; }

        public IEnumerable<Person> AssignedPersons
        {
            get
            {
                if (assignments != null)
                {
                    return assignments.SelectMany(a => a.AssignedPersons);
                }
                return new List<Person>();
            }
        }

        private ObservableCollection<Assignment> assignments;
        public ObservableCollection<Assignment> Assignments
        {
            get { return assignments ?? (assignments = new ObservableCollection<Assignment>()); }
        }

        private ImageSource image;
        public ImageSource Image
        {
            get { return image; }
            set { this.SetProperty(ref image, value); }
        }

        public Project()
        {
        }
    }
}
