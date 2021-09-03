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
        private string ImageDirectory= null;
        private string ImageName = null;
        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            bool? response = openFileDialog.ShowDialog();
            
            if(System.IO.Path.GetExtension(openFileDialog.FileName)==".png")
            {
                //MessageBox.Show(System.IO.Path.GetExtension(openFileDialog.FileName));
                FileAddress.Text = openFileDialog.FileName;
                //MD5 md5 = MD5.Create();
                Stream MyImage = File.OpenRead(openFileDialog.FileName);


                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(openFileDialog.FileName))
                    {
                        HashedText.Text = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "");
                    }
                }

                ImageDirectory = System.IO.Path.GetDirectoryName(openFileDialog.FileName);
                ImageName = System.IO.Path.GetFileNameWithoutExtension(openFileDialog.FileName);

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
            if (isHexadecimal==false)
            { MessageBox.Show("The key has to be in hexadecimal representation "); }
            //MessageBox.Show(XOR("a6","bf"));
            else { 


            if (!Directory.Exists(ImageDirectory))
            {
                Directory.CreateDirectory(ImageDirectory);
            }
            //File.Create(ImageDirectory + "/msgggh.txt").Close();
            //File.Create("C:/Users/Nidhal/Documents/msgggh.txt").Close();

            if (!File.Exists(ImageDirectory + "/" + ImageName + ".encrypt"))
            {
                File.Create(ImageDirectory + "/" + ImageName + ".encrypt").Close();
                
                using (StreamWriter sw = File.CreateText(ImageDirectory +"/"+ImageName+ ".encrypt"))
                {
                    sw.WriteLine(XOR(HashedText.Text, KeyText.Text));
                }
                

            }
            }
        }

        //This method converts a hexadecimal character to the binary form
        private int HexadecimalToBinary(char HexadecimalChar)
        {
            int Binary = 2;
            int NewBinary=0;
            
            if ((HexadecimalChar == 'a') || (HexadecimalChar == 'A')) Binary = 10;
            if ((HexadecimalChar == 'b') || (HexadecimalChar == 'B')) Binary = 11;
            if ((HexadecimalChar == 'c') || (HexadecimalChar == 'C')) Binary = 12;
            if ((HexadecimalChar == 'd') || (HexadecimalChar == 'D')) Binary = 13;
            if ((HexadecimalChar == 'e') || (HexadecimalChar == 'E')) Binary = 14;
            if ((HexadecimalChar == 'f') || (HexadecimalChar == 'F')) Binary = 15;
            if (Binary == 2) Binary = (int)HexadecimalChar;
            int Newnumber = Binary;
            for (int i = 3; i >= 0; i--)
            {
                NewBinary += ((Newnumber / (int)Math.Pow(2, i)) * (int)Math.Pow(10, i));
                Newnumber = Newnumber % (int)Math.Pow(2, i);
            }


            return (NewBinary);
        }

        //This method converts a binary character to the hexadecimal form
        private char BinaryToHexadecimal(string Binary)
        {
            int Hexadecimal = 0;
            char Hexadecimalchar='K';
            for (int i = 3; i >= 0; i--)
            {
                
                if (Binary[i]=='1') Hexadecimal+=(int)Math.Pow(2, 3-i);
                //MessageBox.Show(Binary[i].ToString());
            }
            if (Hexadecimal < 10) Hexadecimalchar = Convert.ToChar(Hexadecimal.ToString());

            if (Hexadecimal == 10) Hexadecimalchar = 'A';
            if (Hexadecimal == 11) Hexadecimalchar = 'B';
            if (Hexadecimal == 12) Hexadecimalchar = 'C';
            if (Hexadecimal == 13) Hexadecimalchar = 'D';
            if (Hexadecimal == 14) Hexadecimalchar = 'E';
            if (Hexadecimal == 15) Hexadecimalchar = 'F';
            return Hexadecimalchar;
        }

        //This method gives the result of the XOR operation applied on two hexadecimal characters
        private string XORTwoChars(char MD5HashChar, char KeyChar)
        {
            string MD5HashBinary= HexadecimalToBinary(MD5HashChar).ToString();
            string KeyBinary = HexadecimalToBinary(KeyChar).ToString();
            string XORResult = null;
            for (int i = 0; i < 4; i++)
            {
                XORResult+=((((int)MD5HashBinary[i]) + ((int)KeyBinary[i])) % 2).ToString();
                //bufferString = XORTwoChars(MD5Hash[i], Key[i]);
            }
                return (XORResult);
        }


        //This method gives the result of the XOR operation applied on two hexadecimal strings
        private string XOR(string MD5Hash, string Key)
        {
            string XORResult="";
            //In case the given key is shorter than 32 characters we repeat the key until it becomes longer than 32 character
            string Key32 = Key;
            while (Key32.Length < 32) Key32 += Key;
            for (int i = 0; i < 32; i++)
            {
                XORResult+= BinaryToHexadecimal(XORTwoChars(MD5Hash[i], Key32[i]));
            }
            return (XORResult);
        }

    }
    }
