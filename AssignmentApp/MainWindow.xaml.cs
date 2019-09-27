using AssignmentApp.DialogService;
using AssignmentApp.FileOperations;
using AssignmentApp.Models;
using AssignmentApp.Views;
using System.Windows;


namespace AssignmentApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ViewModels.AssignmentAppViewModel viewModel;
        private readonly IReadWriteJsonFile<Record> readWriteJsonFile;
        public MainWindow()
        {
            InitializeComponent();
            readWriteJsonFile = new ReadWriteJsonFile<Record>();
            viewModel = new ViewModels.AssignmentAppViewModel(readWriteJsonFile);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AssignmentAppView view = new Views.AssignmentAppView(viewModel);
            mainGrid.Children.Add(view);
        }
    }
}
