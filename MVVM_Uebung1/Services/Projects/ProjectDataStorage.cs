using MVVM_Uebung.Common;
using MVVM_Uebung.Services.Assignments;
using MVVM_Uebung.Services.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Uebung.Services.Projects
{
    class ProjectDataStorage : BaseStorage<Project>, IProjectDataStorage
    {
        IAssignmentStorage assignmentStorage;

        public ProjectDataStorage(IDataStorage dataStorage, IImageStorage imageStorage, IAssignmentStorage assignmentStorage)
            : base(dataStorage, imageStorage, "Projects")
        {
            this.assignmentStorage = assignmentStorage;
        }


        public async Task<Windows.Storage.IStorageFile> SavePictureAsync(Windows.Storage.IStorageFile file, Project project, int desiredWidth)
        {
            return await base.SaveImageAsync(file, project.ID, desiredWidth);
        }

        public override async Task<IEnumerable<Project>> RestoreAsync()
        {
            var projectsTask = base.RestoreAsync();
            var assignmentsTask = assignmentStorage.RestoreAsync();

            var assignments = await assignmentsTask;
            var projects = await projectsTask;

            foreach (var project in projects)
            {
                project.Image = await this.GetImageAsync(project.ID);
            }
            return projects;
        }
        protected override IEnumerable<string> GetRelevantPropertiesNames(Type type)
        {
            var properties = base.GetRelevantProperties(type);

            if (type == typeof(Person))
                properties = properties.Where(p => p.Name == "ID");
            return PropertiesToString(properties);
        }
    }
}
