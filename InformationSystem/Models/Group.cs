using System;
using System.Collections.ObjectModel;
using System.Linq;
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

        /// <summary>
        /// Constructor for initialize static class members.
        /// </summary>
        static Group()
        {
            salary = 0;
            rand = new Random();
        }

        public string Name { get; set; }

        public ObservableCollection<Employee> Employees { get; set; }

        public ObservableCollection<Group> Groups { get; set; }

        private static uint salary;

        private static Random rand;

        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Метод автоматического создания организации.
        /// </summary>
        public static Group GenerateOrganization()
        {
            Group organization = new Group("Organization");

            organization = AddGroupsWithWorkers(organization);
            organization = AddManagers(organization);
            organization = AddAdministrators(organization);

            // Находим процент от всех зарплат в департаментах.
            salary = Convert.ToUInt32(GetSalaryAllEmployeesInDepart(organization) * 0.15);

            // Зарплата не должна быть меньше 1300.
            if (salary < 1300) salary = 1300;

            organization.Employees.Add(new CEO(1, "CEO", Convert.ToByte(rand.Next(18, 68)), 0, salary));

            return organization;
        }

        #region Private methods

        /// <summary>
        /// Возвращает организацию с департаментами и рабочими.
        /// </summary>
        /// <param name="organization">Организация.</param>
        private static Group AddGroupsWithWorkers(Group organization)
        {
            int countGroups = rand.Next(5, 11);

            // Добавление рандомного количества групп с рабочими.
            for (int i = 0; i < countGroups; i++)
            {
                Group group = new Group("Department_" + (i + 1));
                group = AddWorkers(group);
                organization.Groups.Add(group);
            }

            // Добавление в департаменты еще департаментов.
            for (int i = 0; i < organization.Groups.Count; i++)
            {
                countGroups = rand.Next(5, 11);
                for (int j = 0; j < countGroups; j++)
                {
                    Group gp = new Group("Department_" + (j + 1));
                    gp = AddWorkers(gp);
                    organization.Groups[i].Groups.Add(gp);
                }
            }

            return organization;
        }

        /// <summary>
        /// Возвращает организацию с обычными рабочими.
        /// </summary>
        /// <param name="organization">Организация.</param>
        private static Group AddWorkers(Group organization)
        {
            int countEmployees = rand.Next(10, 101); 
            
            // Добавление случайного количества случайных сотрудников.
            for (int i = 0; i < countEmployees; i++)
            {
                switch (rand.Next(2))
                {
                    case 0:
                        organization.Employees.Add(new Intern(Convert.ToUInt16(rand.Next(100, 1000)),
                                                "Intern_" + rand.Next(10, 100),
                                                Convert.ToByte(rand.Next(18, 68)),
                                                Convert.ToByte(rand.Next(1, 11)),
                                                Convert.ToUInt32(rand.Next(1, 11) * 1000)));
                        break;
                    case 1:
                        organization.Employees.Add(new Staff(Convert.ToUInt16(rand.Next(100, 1000)),
                                                "Staff_" + rand.Next(10, 100),
                                                Convert.ToByte(rand.Next(18, 68)),
                                                Convert.ToByte(rand.Next(1, 11)),
                                                Convert.ToUInt32(rand.Next(1, 11) * 1000)));
                        break;
                }
            }

            return organization;
        }

        /// <summary>
        /// Возвращает организацию с менеджерами.
        /// </summary>
        /// <param name="organization">Организация.</param>
        private static Group AddManagers(Group organization)
        {
            if (organization.Groups.Count != 0)
                for (int i = 0; i < organization.Groups.Count; i++)
                    organization.Groups[i] = AddManagers(organization.Groups[i]);
            else
            {
                // Находим процент от всех зарплат в департаментах.
                salary = Convert.ToUInt32(GetSalaryAllEmployeesInDepart(organization) * 0.15);

                // Зарплата не должна быть меньше 1300.
                if (salary < 1300) salary = 1300;

                organization.Employees.Add(new Manager(Convert.ToUInt16(rand.Next(10, 100)),
                                        "Manager_" + organization.Name.Replace("Department_", ""),
                                        Convert.ToByte(rand.Next(18, 68)),
                                        Convert.ToByte(rand.Next(1, 11)),
                                        salary));
            }

            return organization;
        }

        /// <summary>
        /// Возвращает организацию с администраторами.
        /// </summary>
        /// <param name="organization">Организация.</param>
        private static Group AddAdministrators(Group organization)
        {
            for (int i = 0; i < organization.Groups.Count; i++)
            {
                // Находим процент от всех зарплат в департаментах.
                salary = Convert.ToUInt32(GetSalaryAllEmployeesInDepart(organization) * 0.15);

                // Зарплата не должна быть меньше 1300.
                if (salary < 1300) salary = 1300;

                organization.Groups[i].Employees.Add(new Administrator(Convert.ToUInt16(rand.Next(10, 100)),
                                                    "Administrator_" + organization.Groups[i].Name.Replace("Department_", ""),
                                                    Convert.ToByte(rand.Next(18, 68)),
                                                    Convert.ToByte(rand.Next(1, 11)),
                                                    salary));
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
                salary += employee.Salary;

            // Заходит во все департаменты этого департамента.
            foreach (var dep in depart.Groups)
                GetSalaryAllEmployeesInDepart(dep);

            return salary;
        }

        #endregion
    }
}
