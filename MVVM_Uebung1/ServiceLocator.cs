using MVVM_Uebung.Common;
using MVVM_Uebung.Services.Assignments;
using MVVM_Uebung.Services.Persons;
using MVVM_Uebung.Services.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MVVM_Uebung
{
    class ServiceLocator
    {
        public static INavigation navigation;
        public static INavigation Navigation
        {
            get { return navigation ?? (navigation = new Navigation(Window.Current.Content as Frame)); }
        }

        public static IDataStorage dataStorage;
        public static IDataStorage DataStorage
        {
            get { return dataStorage ?? (dataStorage = new JsonDataStorage()); }
        }

        public static IImageStorage ImageStorage
        {
            get { return new ImageStorage(); }
        }

        private static IPersonDataStorage personStorage;
        public static IPersonDataStorage PersonStorage
        {
            get
            {
                if (personStorage == null)
                {
                    personStorage = new PersonDataStorage(DataStorage, ImageStorage);
                }
                return personStorage;
            }
        }

        public static IAssignmentStorage AssigmentStorage
        {
            get { return new Services.Assignments.AssignmentStorage(DataStorage, ImageStorage, PersonStorage); }
        }

        public static IProjectDataStorage ProjectStorage
        {
            get { return new Services.Projects.ProjectDataStorage(DataStorage, ImageStorage, AssigmentStorage); }
        }
    }
}
