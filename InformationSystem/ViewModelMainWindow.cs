using System.Windows.Controls;

namespace InformationSystem
{
    /// <summary>
    /// Логика взаимодействия моделей и представления.
    /// </summary>
    internal class ViewModelMainWindow
    {
        private MainWindow _w;
        public Group Organization;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="w">Main window of application.</param>
        /// <param name="organization">Organization.</param>
        public ViewModelMainWindow(MainWindow w, Group organization)
        {
            _w = w;
            Organization = organization;

            GenerateNewOrganization();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="w">Main window of application.</param>
        public ViewModelMainWindow(MainWindow w) : this(w, new Group("Organization"))
        { }

        /// <summary>
        /// Генерация новой организации.
        /// </summary>
        public void GenerateNewOrganization()
        {
            ClearData();
            // Генерируем организацию.
            Organization = Group.GenerateOrganization();
            // Добавляем её в TreeView.
            _w.tvGroups.Items.Add(CreateTreeItem(Organization));
        }

        /// <summary>
        /// Изменение организации.
        /// </summary>
        /// <param name="NewOrganization"></param>
        public void ChangeOrganization(Group NewOrganization)
        {
            if (NewOrganization != null)
            {
                ClearData();
                // Генерируем организацию.
                Organization = NewOrganization;
                // Добавляем её в TreeView.
                _w.tvGroups.Items.Add(CreateTreeItem(Organization));
            }
        }
        
        /// <summary>
        /// Удаляет организацию.
        /// </summary>
        public void ClearData()
        {
            Organization.Employees.Clear();
            Organization.Groups.Clear();

            _w.tvGroups.Items.Clear();
        }

        /// <summary>
        /// Рекурсивная функция создания TreeView из Group.
        /// </summary>
        /// <param name="group">Группа.</param>
        /// <returns>Готовый TreeView.</returns>
        private TreeViewItem CreateTreeItem(Group group)
        {
            TreeViewItem item = new TreeViewItem();
            item.Header = group.ToString();
            item.Tag = group;

            foreach (var gr in group.Groups)
                item.Items.Add(CreateTreeItem(gr));

            return item;
        }
    }
}
