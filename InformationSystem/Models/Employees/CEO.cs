namespace InformationSystem.Models.Employees
{
    /// <summary>
    /// Класс описывающий директора.
    /// </summary>
    internal class CEO : Employee
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">Employee ID.</param>
        /// <param name="name">Employee name.</param>
        /// <param name="age">Employee age.</param>
        /// <param name="projects">The number of projects an employee has.</param>
        /// <param name="salary">Employee salary.</param>
        public CEO(ushort id, string name, byte age, byte projects, uint salary)
            : base(id, name, age, Position.CEO, projects, salary)
        { }
    }
}
