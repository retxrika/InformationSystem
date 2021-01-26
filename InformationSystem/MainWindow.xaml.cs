using System;
using System.Windows;
using System.Windows.Controls;

namespace InformationSystem
{
    #region ТЗ

    // Задание 1.
    // Спроектировать информационную систему позволяющей работать со следующей структурой:
    // Организация, в которой есть департаменты и сотрудники.
    // Наполнение деталями предлагается реализовать самостоятельно
    // Наполнение сотрудниками и департаментами происходит автоматически из файла *.txt, 
    //                                                           предпочтительнее *.xml или *.json 
    //
    // Сотрудники делятся на несколько групп, разделенных должностями и оплатой труда
    // Есть 
    //   сотрудники - управленцы (например: директор, Первый заместитель директора, начальник ведомства, 
    //                                      начальник департамента)
    // 
    //   ОАО "Лучшие кодеры"
    //       Департамент_1
    //          Департамент_11
    //          Департамент_12
    //       Департамент_2
    //          Департамент_21
    //          Департамент_22
    //          Департамент_23
    //          Департамент_24
    //       Департамент_3
    //          Департамент_31
    //       Департамент_4
    //          Департамент_41
    //          Департамент_42
    //          Департамент_43
    //          Департамент_44
    //          Департамент_45
    //          Департамент_46
    //          Департамент_47
    //          Департамент_48
    //       Департамент_5                Начальник_5
    //          Департамент_51            Начальник_51
    //              Департамент_511       Начальник_511
    //                  Департамент_5111  Начальник_5111
    //                        Департамент_51111      Начальник_51111
    //                              Сотрудник 1
    //                              Сотрудник 2
    //                              Сотрудник 3
    //                              Интерн 1
    //                              Интерн 2
    //                        Департамент_51112
    //                        Департамент_51113
    //                        Департамент_51114
    //                  Департамент_5112
    //                  Департамент_5113
    //              Департамент_512
    //          Департамент_52
    //              Департамент_521
    //              Департамент_522
    //              Департамент_523
    //          Департамент_53
    //              Департамент_531
    //          Департамент_54

    //   сотрудники - рабочие
    //   интерны
    // У интернов оплата труда фиксированная и определяется при приёме (например $500 в месяц)
    // У сотрудников - рабочих оплата труда почасовая и определяется при приёме (например $12 в час)
    // У сотрудников - управленцев оплата труда составляет 15% от общей выплаченной суммы всем сотрудникам 
    // числящихся в его отделе, но не менее $1300. 
    //
    // Структура организации следующая:
    // Организация, состоит из ведомств в которые включены департаменты
    // У каждого ведомства и департамента есть свой начальник.
    // Директор руководит Организацией
    // 
    // Реализовать и продемонстрировать работу информационной системы
    // В консоли или с использованием UI

    #endregion

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModelMainWindow _viewModel;
        private FileIOService service;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel = new ViewModelMainWindow(this, new Group("Organization"));
            service = new FileIOService(this);
        }

        /// <summary>
        /// При нажатии на элемент из TreeView метод будет заполнять DataGrid.
        /// </summary>
        private void tvGroups_Selected(object sender, RoutedEventArgs e) 
            => dgEmployees.ItemsSource = ((e.OriginalSource as TreeViewItem).Tag as Group).Employees;

        private void MenuItem_Click_About(object sender, RoutedEventArgs e) 
            => MessageBox.Show("\"Information System\" version 0.0.1", "About", MessageBoxButton.OK, MessageBoxImage.Information);
        
        private void MenuItem_Click_GenerateNew(object sender, RoutedEventArgs e) => _viewModel.GenerateNewOrganization();
        
        private void MenuItem_Click_ClearData(object sender, RoutedEventArgs e) => _viewModel.ClearData();
        
        private void MenuItem_Click_Exit(object sender, RoutedEventArgs e) => Close();

        private void MenuItem_Click_Save(object sender, RoutedEventArgs e) => service.SaveData(_viewModel.Organization);

        private void MenuItem_Click_Load(object sender, RoutedEventArgs e) => _viewModel.ChangeOrganization(service.UnloadData());
    }
}
