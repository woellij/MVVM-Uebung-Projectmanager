using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_Uebung.Common;
using MVVM_Uebung.Services.Persons;

namespace MVVM_Uebung.Services.Assignments
{
    class AssignmentStorage : BaseStorage<Assignment>, IAssignmentStorage
    {
        IPersonDataStorage personStorage;
        public AssignmentStorage(IDataStorage dataStorage, IImageStorage imagestorage, IPersonDataStorage personStorage)
            : base(dataStorage, imagestorage, "Assignments")
        {
            this.personStorage = personStorage;
        }

        public override async Task<IEnumerable<Assignment>> RestoreAsync()
        {
            var assignmentsTask = base.RestoreAsync();
            var personsTask = personStorage.RestoreAsync();

            var assignments = await assignmentsTask;
            var persons = await personsTask;
            if (assignments != null)
                foreach (var assignment in assignments)
                {
                    var assignedPersonIds = assignment.AssignedPersons.Select(p => p.ID).ToList();
                    assignment.AssignedPersons.Clear();
                    foreach (var personId in assignedPersonIds)
                    {
                        var person = persons.FirstOrDefault(p => p.ID == personId);
                        if (person != null)
                            assignment.AssignedPersons.Add(person);
                    }
                }
            return assignments;
        }
    }
}
