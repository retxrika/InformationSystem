namespace InformationSystem
{
    /// <summary>
    /// Абстрактный класс описывающий сотрудника.
    /// </summary>
    internal abstract class Employee
    {
        /// <summary>
        /// Positions of employees.
        /// </summary>
        public enum Position : byte
        {
            CEO,
            Administrator,
            Manager,
            Staff,
            Intern
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">Employee ID.</param>
        /// <param name="name">Employee name.</param>
        /// <param name="age">Employee age.</param>
        /// <param name="pos">Employee position.</param>
        /// <param name="projects">The number of projects an employee has.</param>
        /// <param name="salary">Employee salary.</param>
        public Employee(ushort id, string name, byte age, Position pos, byte projects, uint salary)
        {
            ID = id;
            Name = name;
            Age = age;
            Pos = pos;
            Projects = projects;
            Salary = salary;
        }

        #region Auto properties

        public ushort ID { get; set; }

        public string Name { get; set; }

        public byte Age { get; set; }

        public Position Pos { get; set; }

        public byte Projects { get; set; }

        public uint Salary { get; set; }

        #endregion
    }
}
