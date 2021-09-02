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
using System.Security.Cryptography;
using System.IO;

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
        private static byte[] _emptyBuffer = new byte[0];
        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            bool? response = openFileDialog.ShowDialog();
            
            if(System.IO.Path.GetExtension(openFileDialog.FileName)==".png")
            {
                //MessageBox.Show(System.IO.Path.GetExtension(openFileDialog.FileName));
                FileAddress.Text = openFileDialog.FileName;
                MD5 md5 = MD5.Create();
                Stream MyImage = File.OpenRead(openFileDialog.FileName);

                long bufferSize = MyImage.Length;





                byte[] buffer = new byte[bufferSize];
                int bytesRead = MyImage.Read(buffer, 0, 10);

                int readBytes;
                while ((readBytes = MyImage.Read(buffer, 0, (int)bufferSize)) > 0)
                {
                    md5.TransformBlock(buffer, 0, readBytes, buffer, 0);
                }

                md5.TransformFinalBlock(_emptyBuffer, 0, 0);
                byte[] testresult = md5.Hash;
                BitConverter.ToString(testresult).Replace("-", "");

                var s = new StringBuilder();
                foreach (byte b in testresult)
                    s.Append(b.ToString("x2").ToLower());
                 //MessageBox.Show(s.ToString());

                //BitConverter.ToString(md5.Hash).Replace("-", "");
                //MessageBox.Show(buffer[6].ToString());
                //md5.TransformBlock(buffer, 0, (int)bufferSize, null, 0);

            }
            
        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            char[] hex_allowed = { '0','1', '2', '3','4', '5', '6', '7', '8', '9', 'a', 'A', 'b', 'B', 'c', 'C', 'd', 'D', 'e', 'E', 'f', 'F' };
            bool isHexadecimal = true;
            foreach (char c in KeyText.Text)
            {
                if (!(hex_allowed.Contains(c)))
                    {
                    isHexadecimal = false;
                    break;
                }
            }
            if ((KeyText.Text.Length!=32) || (isHexadecimal==false))
            { MessageBox.Show("The key has to contain 32 characters in hexadecimal representation "); }
        }
    }
}
