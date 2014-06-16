
using MVVM_Uebung.Common;
using MVVM_Uebung.Services.Projects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM_Uebung
{
    class ProjectsViewModel : ViewModelBase, Common.State.IStateSaving
    {
        private ObservableCollection<Project> projects;
        public ObservableCollection<Project> Projects { get { return projects ?? (projects = new ObservableCollection<Project>()); } }

        private Project selectedProject;
        public Project SelectedProject
        {
            get { return selectedProject; }
            set
            {
                this.SetProperty(ref selectedProject, value);
                RaisePropertyChanged("EditCommand");
            }
        }

        IProjectDataStorage storage;
        public ProjectsViewModel(IProjectDataStorage projectDataStorage, INavigation navigation)
            : base(navigation)
        {
            this.storage = projectDataStorage;
        }


        public ICommand AddCommand { get { return new RelayCommand(AddProject); } }
        public ICommand EditCommand { get { return new RelayCommand(EditProject, () => SelectedProject != null); } }


        private void AddProject()
        {
            var project = new Project();
            Projects.Add(project);
            this.SelectedProject = project;
        }
        private void EditProject()
        {
            Navigation.Show(typeof(ViewModels.EditProjectViewModel), this.SelectedProject);
        }



        #region State

        public async Task SaveStateAsync()
        {
            await storage.SaveAsync(this.Projects);
        }

        public async Task RestoreStateAsync()
        {
            var restored = await storage.RestoreAsync();
            if (restored != null)
                foreach (var rp in restored)
                    this.Projects.Add(rp);
        }
        #endregion // State
    }
}
