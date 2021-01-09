namespace InformationSystem.Models.Employees
{
    /// <summary>
    /// Класс описывающий работника.
    /// </summary>
    internal class Staff : Employee
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">Employee ID.</param>
        /// <param name="name">Employee name.</param>
        /// <param name="age">Employee age.</param>
        /// <param name="projects">The number of projects an employee has.</param>
        /// <param name="salary">Employee salary.</param>
        public Staff(ushort id, string name, byte age, byte projects, uint salary)
            : base(id, name, age, Position.Staff, projects, salary)
        { }
    }
}
