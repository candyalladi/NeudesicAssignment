using System;
using System.Collections.Generic;
using System.Windows;

namespace AssignmentApp.DialogService
{
    public class DialogService : IDialogService
    {
        private readonly Window owner;

        public IDictionary<Type, Type> Mappings { get; private set; }

        public DialogService(Window onwer)
        {
            this.owner = onwer;
            Mappings = new Dictionary<Type, Type>();
        }
        public void Register<TViewModel, TView>() where TViewModel : IDialogCloseRequest
                                                  where TView : IDialog
        {
            if (Mappings.ContainsKey(typeof(TViewModel)))
            {
                throw new ArgumentException($"Type {typeof(TViewModel)} is already mapped to type {typeof(TView)}");
            }
            Mappings.Add(typeof(TViewModel), typeof(TView));
        }

        public bool? ShowDialog<TViewModel>(TViewModel viewModel) where TViewModel : IDialogCloseRequest
        {
            Type viewType = Mappings[typeof(TViewModel)];
            IDialog dialog = (IDialog)Activator.CreateInstance(viewType);

            EventHandler<DialogCloseRequestedEventArgs> handler = null;
            handler = (sender, e) =>
            {
                viewModel.CloseRequested -= handler;
                if (e.DialogResult.HasValue)
                {
                    dialog.DialogResult = e.DialogResult;
                }
                else
                {
                    dialog.Close();
                }
            };
            viewModel.CloseRequested += handler;
            dialog.DataContext = viewModel;
            dialog.Owner = owner;

            return dialog.ShowDialog();
        }
    }
}
