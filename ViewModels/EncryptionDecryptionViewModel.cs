using EncryptionDecriptionHashGeneration.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncryptionDecriptionHashGeneration.ViewModels
{
    class EncryptionDecryptionViewModel: INotifyPropertyChanged
    {
        public BrowseButtonCommand BrowseButtonCommand { get; set; }
        public EncryptButtonCommand EncryptButtonCommand { get; set; }
        public DecryptButtonCommand DecryptButtonCommand { get; set; }

        //public string HashedText = "ddddddddd";
        public string ImageAddress { get; set; }

        bool encryptButtonEnabled;
        public bool EncryptButtonEnabled
        {
            get => encryptButtonEnabled;
            set
            {
                if (encryptButtonEnabled != value)
                {
                    encryptButtonEnabled = value;
                    this.RaisePropertyChanged();
                }
            }

        }


        bool decryptButtonEnabled;
        public bool DecryptButtonEnabled
        {
            get => decryptButtonEnabled;
            set
            {
                if (decryptButtonEnabled != value)
                {
                    decryptButtonEnabled = value;
                    this.RaisePropertyChanged();
                }
            }

        }

        string hashedtext;
        public string HashedText
        {
            get => hashedtext;
            set
            {
                if (hashedtext != value)
                {
                    hashedtext = value;
                    this.RaisePropertyChanged();
                }
            }

        }



        string fileaddress;
        public string FileAddress
        {
            get => fileaddress;
            set
            {
                if (fileaddress != value)
                {
                    fileaddress = value;
                    this.RaisePropertyChanged();
                }
            }

        }



        string imagedirectory;
        public string ImageDirectory
        {
            get => imagedirectory;
            set
            {
                if (imagedirectory != value)
                {
                    imagedirectory = value;
                    this.RaisePropertyChanged();
                }
            }

        }



        string imagename;
        public string ImageName
        {
            get => imagename;
            set
            {
                if (imagename != value)
                {
                    imagename = value;
                    this.RaisePropertyChanged();
                }
            }

        }



        string keytext;
        public string KeyText
        {
            get => keytext;
            set
            {
                if (keytext != value)
                {
                    keytext = value;
                    this.RaisePropertyChanged();
                }
            }

        }




        public event PropertyChangedEventHandler PropertyChanged;



        public EncryptionDecryptionViewModel()
        {
            BrowseButtonCommand = new BrowseButtonCommand(this);
            EncryptButtonCommand = new EncryptButtonCommand(this);
            DecryptButtonCommand = new DecryptButtonCommand(this);
            EncryptButtonEnabled = false;
            DecryptButtonEnabled = false;
            //FileAddress = "dddddd";
        }
        public bool EncryptCanExecute()
        {
            return EncryptButtonEnabled;
        }
        public bool DecryptCanExecute()
        {
            return DecryptButtonEnabled;
        }
        public void BrowseOnExecute()
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            bool? response = openFileDialog.ShowDialog();

            if ((System.IO.Path.GetExtension(openFileDialog.FileName) == ".png") || (System.IO.Path.GetExtension(openFileDialog.FileName) == ".encrypt"))
            {
                FileAddress = openFileDialog.FileName;
                ImageDirectory = System.IO.Path.GetDirectoryName(openFileDialog.FileName);
                ImageName = System.IO.Path.GetFileNameWithoutExtension(openFileDialog.FileName);

            }
            else if (response == true) MessageBox.Show("Only .png and .encrypt files are accepted");
            if (System.IO.Path.GetExtension(openFileDialog.FileName) == ".png")
            {
                Stream MyImage = File.OpenRead(openFileDialog.FileName);
                using (var md5 = MD5.Create())
                {
                    using (var stream = File.OpenRead(openFileDialog.FileName))
                    {
                        HashedText = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "");
                    }
                }
                EncryptButtonEnabled = true;
                DecryptButtonEnabled = false;
                EncryptButtonCommand.RaiseCanExecuteChanged();
                DecryptButtonCommand.RaiseCanExecuteChanged();

                //EncryptButton.IsEnabled = true;
                //DecryptButton.IsEnabled = false;
            }
            if (System.IO.Path.GetExtension(openFileDialog.FileName) == ".encrypt")
            {
                //DecryptButton.IsEnabled = true;
                //EncryptButton.IsEnabled = false;
                HashedText = "";
                EncryptButtonEnabled = false;
                DecryptButtonEnabled = true;
                EncryptButtonCommand.RaiseCanExecuteChanged();
                DecryptButtonCommand.RaiseCanExecuteChanged();
            }
        }

        private void RaisePropertyChanged([CallerMemberName] string propertyName="")
        {
            if (!string.IsNullOrEmpty(propertyName))
                { this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }

        }


        public void EncryptOnExecute()
        {
            
            if (LegitKey() == false) MessageBox.Show("The key has to be in hexadecimal representation ");
            

            else
            {


                if (!Directory.Exists(ImageDirectory))
                {
                    Directory.CreateDirectory(ImageDirectory);
                }

                if (!File.Exists(ImageDirectory + "/" + ImageName + ".encrypt"))
                {
                    File.Create(ImageDirectory + "/" + ImageName + ".encrypt").Close();

                    using (StreamWriter sw = File.CreateText(ImageDirectory + "/" + ImageName + ".encrypt"))
                    {
                        sw.WriteLine(XOR(HashedText, KeyText));
                    }
                    MessageBox.Show("The encrypted file has been successfully created");

                }
                else MessageBox.Show("The file " + ImageDirectory + "/" + ImageName + ".encrypt already exists");
            }

            

        }

        public void DecryptOnExecute()
        {

            if (File.Exists(ImageDirectory + "/" + ImageName + ".png"))
            {
                if (LegitKey() == false) MessageBox.Show("The key has to be in hexadecimal representation ");
                else
                {
                    string HashedString = File.ReadAllText(FileAddress);
                    HashedText = XOR(HashedString, KeyText);
                }
            }
            else { MessageBox.Show("The file " + ImageDirectory + "/" + ImageName + ".png doesn't exist anymore"); }
            
        }


        //This method converts a hexadecimal character to the binary form
        private int HexadecimalToBinary(char HexadecimalChar)
        {
            int Binary = 2;
            int NewBinary = 0;

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
            char Hexadecimalchar = 'K';
            for (int i = 3; i >= 0; i--)
            {

                if (Binary[i] == '1') Hexadecimal += (int)Math.Pow(2, 3 - i);
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
            string MD5HashBinary = HexadecimalToBinary(MD5HashChar).ToString();
            string KeyBinary = HexadecimalToBinary(KeyChar).ToString();
            string XORResult = null;
            for (int i = 0; i < 4; i++)
            {
                XORResult += ((((int)MD5HashBinary[i]) + ((int)KeyBinary[i])) % 2).ToString();
                //bufferString = XORTwoChars(MD5Hash[i], Key[i]);
            }
            return (XORResult);
        }

        //This method gives the result of the XOR operation applied on two hexadecimal strings
        private string XOR(string MD5Hash, string Key)
        {
            string XORResult = "";
            //In case the given key is shorter than 32 characters we repeat the key until it becomes longer than 32 character
            string Key32 = Key;
            while (Key32.Length < 32) Key32 += Key;
            for (int i = 0; i < 32; i++)
            {
                XORResult += BinaryToHexadecimal(XORTwoChars(MD5Hash[i], Key32[i]));
            }
            return (XORResult);
        }

        //This method checks if the given Key is hexadecimal
        private bool LegitKey()
        {
            char[] hex_allowed = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'A', 'b', 'B', 'c', 'C', 'd', 'D', 'e', 'E', 'f', 'F' };
            bool isLegit = true;
            bool isHexadecimal = true;
            if ((KeyText == null) || (KeyText == "")) isLegit = false;
            else { 


            foreach (char c in KeyText)
            {
                if (!(hex_allowed.Contains(c)))
                {
                    isHexadecimal = false;
                    break;
                }
            }
            
            if (isHexadecimal == false)  isLegit = false;
            }
            return (isLegit);

        }




    }
}
