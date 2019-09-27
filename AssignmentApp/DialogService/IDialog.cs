using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AssignmentApp.DialogService
{
    public interface IDialog
    {
        object DataContext { get; set; }
        bool? DialogResult { get; set; }
        Window Owner { get; set; }
        void Close();
        bool? ShowDialog();
    }

    public interface IDialogService
    {
        void Register<TViewModel, TView>() where TViewModel : IDialogCloseRequest
                                           where TView : IDialog;

        bool? ShowDialog<TViewModel>(TViewModel viewModel) where TViewModel : IDialogCloseRequest;
    }

    public interface IDialogCloseRequest
    {
        event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;
    }
}
