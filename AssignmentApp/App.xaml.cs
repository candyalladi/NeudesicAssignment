using AssignmentApp.DialogService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AssignmentApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            IDialogService dialogService = new DialogService.DialogService(MainWindow);
            dialogService.Register<DialogViewModel, DialogWindow>();

        }
    }
}
