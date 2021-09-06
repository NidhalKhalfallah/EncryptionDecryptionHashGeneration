using EncryptionDecriptionHashGeneration.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EncryptionDecriptionHashGeneration.Commands
{
    class BrowseButtonCommand : ICommand
    {
        EncryptionDecryptionViewModel _encryptionDecryptionViewModel;

        public BrowseButtonCommand(EncryptionDecryptionViewModel viewModel)
        {
            _encryptionDecryptionViewModel = viewModel;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _encryptionDecryptionViewModel.BrowseOnExecute();
        }

    }
}
