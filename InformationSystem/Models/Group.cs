using System;
using System.Collections.ObjectModel;
using System.Threading;

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
            Groups = new ObservableCollection<Group>();
        }

        public string Name { get; set; }

        public ObservableCollection<Employee> Employees { get; set; }

        public ObservableCollection<Group> Groups { get; set; }

        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Метод автоматического создания организации.
        /// </summary>
        public static Group GenerateOrganization(Group organization)
        {
            Random rand = new Random();
            organization.Groups = GenerateCollectionGroups(rand);

            return organization;
        }

        /// <summary>
        /// Метод создания рандомной коллекции сотрудников.
        /// </summary>
        private static ObservableCollection<Employee> GenerateCollectionEmployees(Random rand)
        {
            ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
            int countEmployees = rand.Next(10, 101);
            
            // Добавление случайного количества случайных сотрудников.
            for (int i = 0; i < countEmployees; i++)
            {
                switch (rand.Next(2))
                {
                    case 0:
                        employees.Add(new Intern(Convert.ToUInt16(rand.Next(100, 1000)),
                                                "Intern_" + rand.Next(10, 100),
                                                Convert.ToByte(rand.Next(18, 68)),
                                                Convert.ToByte(rand.Next(1, 11)),
                                                Convert.ToUInt32(rand.Next(1, 11) * 1000)));
                        break;
                    case 1:
                        employees.Add(new Staff(Convert.ToUInt16(rand.Next(100, 1000)),
                                                "Staff_" + rand.Next(10, 100),
                                                Convert.ToByte(rand.Next(18, 68)),
                                                Convert.ToByte(rand.Next(1, 11)),
                                                Convert.ToUInt32(rand.Next(1, 11) * 1000)));
                        break;
                }
            }

            return employees;
        }

        /// <summary>
        /// Метод создания рандомной коллекции департаментов.
        /// </summary>
        private static ObservableCollection<Group> GenerateCollectionGroups(Random rand)
        {
            ObservableCollection<Group> groups = new ObservableCollection<Group>();
            int countGroups = rand.Next(5, 11);

            for (int i = 0; i < countGroups; i++)
            {
                Group group = new Group("Department_" + (i + 1));
                group.Employees = GenerateCollectionEmployees(rand);
                groups.Add(group);
            }

            return groups;
        }
    }
}
