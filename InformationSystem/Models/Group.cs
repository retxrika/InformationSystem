using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;

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

        private static uint wholeSalary = 0;
        
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
            organization.Groups = GenerateCollectionGroupsWithWorkers(rand);

            AddMainEmployees(organization, rand);

            return organization;
        }

        #region Private methods

        /// <summary>
        /// Возвращает рандомную коллекцию департаментов c рандомными рабочими.
        /// </summary>
        private static ObservableCollection<Group> GenerateCollectionGroupsWithWorkers(Random rand)
        {
            ObservableCollection<Group> groups = new ObservableCollection<Group>();
            int countGroups = rand.Next(5, 11);

            // Добавление рандомного количества групп с рабочими.
            for (int i = 0; i < countGroups; i++)
            {
                Group group = new Group("Department_" + (i + 1));
                group.Employees = GenerateCollectionWorkers(rand);
                groups.Add(group);
            }

            // Добавление в департаменты еще департаментов.
            foreach (var group in groups)
            {
                countGroups = rand.Next(5, 11);
                for (int i = 0; i < countGroups; i++)
                {
                    Group gp = new Group("Department_" + (i + 1));
                    gp.Employees = GenerateCollectionWorkers(rand);
                    group.Groups.Add(gp);
                }
            }

            return groups;
        }
        
        /// <summary>
        /// Возвращает рандомную коллекцию рабочих.
        /// </summary>
        private static ObservableCollection<Employee> GenerateCollectionWorkers(Random rand)
        {
            ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
            int countEmployees = rand.Next(5); /// TODO: (10, 101) изменить диапазон 
            
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
        /// Возвращает организацию с управленцами.
        /// </summary>
        /// <param name="organization">Организация.</param>
        private static Group AddMainEmployees(Group organization, Random rand)
        {
            // Если это организация — добавить директора.
            if (organization.Name == "Organization")
                organization.Employees.Add(new CEO(1, "CEO", Convert.ToByte(rand.Next(18, 68)), 0, 0));

            // Перебираем все департаменты в группы.
            for (int i = 0; i < organization.Groups.Count; i++)
            {
                // Если в департаменте есть департаменты — добавить администратора.
                if (organization.Groups[i].Groups.Count != 0)
                    organization.Groups[i].Employees.Add(new Administrator(Convert.ToUInt16(rand.Next(10, 100)),
                                            "Administrator_" + organization.Groups[i].Name.Replace("Department_", ""),
                                            Convert.ToByte(rand.Next(18, 68)),
                                            Convert.ToByte(rand.Next(1, 11)),
                                            0));
                // Иначе менеджера.
                else
                    organization.Groups[i].Employees.Add(new Manager(Convert.ToUInt16(rand.Next(10, 100)),
                                            "Manager_" + organization.Groups[i].Name.Replace("Department_", ""),
                                            Convert.ToByte(rand.Next(18, 68)),
                                            Convert.ToByte(rand.Next(1, 11)),
                                            0));
                // Добавляем управленцев в текущем департаменте.
                organization.Groups[i] = AddMainEmployees(organization.Groups[i], rand);
            }

            return organization;
        }

        /// <summary>
        /// Возвращает суммированную зарплату всех сотрудников в департаменте.
        /// </summary>
        /// <param name="depart">Департамент.</param>
        private static uint GetSalaryAllEmployeesInDepart(Group depart)
        {
            // Считает в данном департаменте зарплату всех сотрудников.
            foreach (var employee in depart.Employees)
                wholeSalary += employee.Salary;

            // Заходит во все департаменты этого департамента.
            foreach (var dep in depart.Groups)
                GetSalaryAllEmployeesInDepart(dep);

            return wholeSalary;
        }

        #endregion
    }
}
