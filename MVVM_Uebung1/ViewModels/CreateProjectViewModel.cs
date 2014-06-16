
using System.Windows.Input;
using MVVM_Uebung.Common;
using MVVM_Uebung.Services.Projects;

namespace MVVM_Uebung.ViewModels
{
    class CreateProjectViewModel : Common.ViewModelBase
    {
        private readonly IProjectDataStorage projectStorage;
        public Project Project { get; set; }
        public CreateProjectViewModel(INavigation navigation, IProjectDataStorage projectStorage)
            : base(navigation)
        {
            this.projectStorage = projectStorage;
            Project = new Project();
        }

        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    await projectStorage.SaveAsync(Project);
                    Navigation.GoBackCommand.Execute(null);
                });
            }
        }
    }
}
