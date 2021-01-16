using System.Windows.Controls;

namespace InformationSystem
{
    /// <summary>
    /// Логика взаимодействия моделей и представления.
    /// </summary>
    internal class ViewModelMainWindow
    {
        private MainWindow _w;
        private Group _organization;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="w">Main window of application.</param>
        /// <param name="organization">Organization.</param>
        public ViewModelMainWindow(MainWindow w, Group organization)
        {
            _w = w;
            _organization = organization;

            // Генерируем организацию.
            _organization = Group.GenerateOrganization(_organization);
            // Добавляем её в TreeView.
            w.tvGroups.Items.Add(CreateTreeItem(_organization));
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
