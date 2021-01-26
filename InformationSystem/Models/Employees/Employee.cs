namespace InformationSystem
{
    /// <summary>
    /// Абстрактный класс описывающий сотрудника.
    /// </summary>
    internal abstract class Employee
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">Employee ID.</param>
        /// <param name="name">Employee name.</param>
        /// <param name="age">Employee age.</param>
        /// <param name="position">Employee position.</param>
        /// <param name="projects">The number of projects an employee has.</param>
        /// <param name="salary">Employee salary.</param>
        public Employee(ushort id, string name, byte age, string position, byte projects, uint salary)
        {
            ID = id;
            Name = name;
            Age = age;
            Position = position;
            Projects = projects;
            Salary = salary;
        }

        public override string ToString()
        {
            return Name;
        }

        #region Auto properties

        public ushort ID { get; set; }

        public string Name { get; set; }

        public byte Age { get; set; }

        public string Position { get; set; }

        public byte Projects { get; set; }

        public uint Salary { get; set; }

        #endregion
    }
}
