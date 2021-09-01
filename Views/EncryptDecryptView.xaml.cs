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

namespace EncryptionDecryptionHashGeneration.Views
{
    /// <summary>
    /// Logique d'interaction pour EncryptDecryptView.xaml
    /// </summary>
    public partial class EncryptDecryptView : UserControl
    {
        public EncryptDecryptView()
        {
            InitializeComponent();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            bool? response = openFileDialog.ShowDialog();
            if(response==true)
            {
                MessageBox.Show(openFileDialog.FileName);
            }
        }
    }
}
