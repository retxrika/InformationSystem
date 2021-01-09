namespace InformationSystem
{
    /// <summary>
    /// Класс описывающий интерна.
    /// </summary>
    internal class Intern : Employee
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">Employee ID.</param>
        /// <param name="name">Employee name.</param>
        /// <param name="age">Employee age.</param>
        /// <param name="projects">The number of projects an employee has.</param>
        /// <param name="salary">Employee salary.</param>
        public Intern(ushort id, string name, byte age, byte projects, uint salary)
            : base(id, name, age, Position.Intern, projects, salary)
        { }
    }
}
