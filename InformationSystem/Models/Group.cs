using System.Collections.ObjectModel;

namespace InformationSystem
{
    /// <summary>
    /// Класс описывающий группу сотрудников.
    /// </summary>
    internal class Group
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">Group name.</param>
        public Group(string name)
        {
            Name = name;
            Employees = new ObservableCollection<Employee>();
        }
            
        public string Name { get; set; }

        public ObservableCollection<Employee> Employees { get; set; }
    }
}
