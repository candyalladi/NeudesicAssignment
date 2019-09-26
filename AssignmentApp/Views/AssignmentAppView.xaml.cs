using AssignmentApp.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace AssignmentApp.Views
{
    /// <summary>
    /// Interaction logic for AssignmentAppView.xaml
    /// </summary>
    public partial class AssignmentAppView : UserControl
    {
        private AssignmentAppViewModel viewModel;
        public AssignmentAppView(AssignmentAppViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            this.DataContext = viewModel;
        }
        private void OpenExistingFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json";
            if (openFileDialog.ShowDialog() == true)
            {
                //Process selected XML file or Json file
                viewModel.OpenExistingFile(openFileDialog.FileName);
            }
        }
    }
}
