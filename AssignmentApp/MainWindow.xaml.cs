using AssignmentApp.FileOperations;
using AssignmentApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            Views.AssignmentAppView view = new Views.AssignmentAppView(viewModel);
            mainGrid.Children.Add(view);
        }
    }
}
