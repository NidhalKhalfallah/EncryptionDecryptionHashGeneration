using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using EncryptionDecryptionHashGeneration.ViewModels;

namespace EncryptionDecryptionHashGeneration.Commands
{
    class DecryptButtonCommand : ICommand
    {
        EncryptionDecryptionViewModel _encryptionDecryptionViewModel;
        public DecryptButtonCommand(EncryptionDecryptionViewModel viewModel)
        {
            _encryptionDecryptionViewModel = viewModel;

        }
        public bool CanExecute(object parameter)
        {
            return (_encryptionDecryptionViewModel.DecryptCanExecute());
        }
        public event EventHandler CanExecuteChanged;


        public void Execute(object parameter)
        {
            _encryptionDecryptionViewModel.DecryptOnExecute();
        }
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

    }
}
