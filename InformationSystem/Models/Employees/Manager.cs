﻿namespace InformationSystem
{
    /// <summary>
    /// Класс описывающий менеджера.
    /// </summary>
    internal class Manager : Employee
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">Employee ID.</param>
        /// <param name="name">Employee name.</param>
        /// <param name="age">Employee age.</param>
        /// <param name="projects">The number of projects an employee has.</param>
        /// <param name="salary">Employee salary.</param>
        public Manager(ushort id, string name, byte age, byte projects, uint salary)
            : base(id, name, age, "Manager", projects, salary)
        { }
    }
}
