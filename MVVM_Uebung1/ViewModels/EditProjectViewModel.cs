using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_Uebung.Common;
using MVVM_Uebung.Services.Projects;
using System.Windows.Input;

namespace MVVM_Uebung.ViewModels
{
    class EditProjectViewModel : ViewModelBase
    {
        private Project project;

        public Project Project
        {
            get { return project; }
            set { this.SetProperty(ref project, value); }
        }

        IProjectDataStorage projectStorage;
        public EditProjectViewModel(INavigation navigation, IProjectDataStorage projectStorage)
            : base(navigation)
        {
            this.projectStorage = projectStorage;
        }


        internal override void Init(object parameter)
        {
            base.Init(parameter);
            var project = parameter as Project;
            if (project == null)
                Navigation.GoBackCommand.Execute(null);
            else Project = project;
        }

        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    await projectStorage.UpdateAsync(project);
                    Navigation.GoBackCommand.Execute(null);
                });
            }
        }
    }
}
