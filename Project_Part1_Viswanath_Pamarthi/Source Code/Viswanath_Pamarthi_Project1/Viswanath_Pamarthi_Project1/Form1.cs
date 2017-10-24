using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Viswanath_Pamarthi_Project1
{
    public partial class Form1 : Form
    {
        OpenFileDialog inputFilePath = new OpenFileDialog();


        int ASCII_LowerCase_start = 97;
        int ASCII_LowerCase_end = 122;//+25
        int ASCII_UpperCase_start = 65;
        int ASCII_UpperCase_end = 90;//+25

        Dictionary<char, char> M1 = new Dictionary<char, char>();// { };
        Dictionary<char, char> M3 = new Dictionary<char, char>();// { };
        Dictionary<char, char> M2 = new Dictionary<char, char>
        {{'a','D'},{'b','K'},{'c','V'},{'d','Q'},{'e','F'},{'f','I'},{'g','B'},{'h','J'},{'i','W'},{'j','P'},{'k','E'},{'l','S'},{'m','C'},{'n','X'},{'o','H'},{'p','T'},{'q','M'},{'r','Y'},{'s','A'},{'t','U'},{'u','O'},{'v','L'},{'w','R'},{'x','G'},{'y','Z'},{'z','N'}};

        public Form1()
        {
            InitializeComponent();
            inputFilePath.Title = "Browse the input file";
            inputFilePath.CheckFileExists = true;
            inputFilePath.CheckPathExists = true;
            inputFilePath.DefaultExt = "txt";
            inputFilePath.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

            GenerateCipher(0, ref M1, ASCII_LowerCase_start, 3);
            GenerateCipher(1, ref M3, ASCII_LowerCase_start, 5);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="direction">0 for left, 1 for right</param>
        /// <param name="objectToStore"></param>
        /// <param name="startAsciiValue"></param>
        private void GenerateCipher(int direction,ref Dictionary<char,char> objectToStore, int startAsciiValue,int shiftBy)
        {
            int EndAsciiValue = startAsciiValue + 25;
            int temp = 0;

            if(direction==0)
            {
                for (int i = startAsciiValue; i <= EndAsciiValue; i++)
                {
                    temp = i - shiftBy;
                    if (temp >= startAsciiValue)
                    {
                        objectToStore.Add((char)(i), (char)(temp));
                    }
                    else
                    {
                        objectToStore.Add((char)(i), (char)((EndAsciiValue + 1) - (startAsciiValue%temp)));
                    }
                }
            }
            else
            {
                for (int i = startAsciiValue; i <= EndAsciiValue; i++)
                {
                    temp = i + shiftBy;
                    if (temp <= EndAsciiValue)
                    {
                        objectToStore.Add((char)(i), (char)(temp));
                    }
                    else
                    {
                        objectToStore.Add((char)(i), (char)((startAsciiValue-1)+(temp%EndAsciiValue)));
                    }
                }
            }
            
            

            
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lblFilePath.Text))
            {
                if (RadioBtnDecrypt.Checked)
                {
                    Decrypt();
                }

                if (RadioBtnEncrypt.Checked)
                {
                    Encrypt();
                }
            }
        }

        /// <summary>
        /// Encrypt the Data from file and write to output file in the same folder as inputFolder
        /// </summary>
        private void Encrypt()
        {
            var outputFilePath = Path.Combine(inputFilePath.FileName.Substring(0, inputFilePath.FileName.Length - 4) + "_Ciphertext.txt");
            using (StreamReader fileStream = new StreamReader(lblFilePath.Text))
            {
                using (StreamWriter outFileStream = new StreamWriter(outputFilePath))
                {
                    char[] temp = new char[1024];
                    int countData = 0;
                    int cycle = 0;
                    while ((countData = fileStream.ReadBlock(temp, 0, temp.Length)) > 0)
                    {
                        foreach (char c in temp)
                        {
                            if ((c >= ASCII_LowerCase_start && c <= ASCII_LowerCase_end))
                            {
                                if (cycle == 0)
                                {
                                    outFileStream.Write(M1[c]);
                                    cycle++;
                                }
                                else if (cycle == 1)
                                {
                                    outFileStream.Write(M2[c]);
                                    cycle++;
                                }
                                else if (cycle == 2)
                                {
                                    outFileStream.Write(M3[c]);
                                    cycle = 0;

                                }

                            }
                            else if ((c >= ASCII_UpperCase_start && c <= ASCII_UpperCase_end))
                            {
                                if (cycle == 0)
                                {
                                    outFileStream.Write(char.ToUpper(M1[char.ToLower(c)]));
                                    cycle++;
                                }
                                else if (cycle == 1)
                                {
                                    outFileStream.Write(M2[(char)(ASCII_LowerCase_start + (c - ASCII_UpperCase_start))]);
                                    cycle++;
                                }
                                else if (cycle == 2)
                                {
                                    outFileStream.Write(char.ToUpper(M3[char.ToLower(c)]));
                                    cycle = 0;
                                }
                            }
                            else if(c>=33 && c<=64)//Special characters considered only ASCII from 33 to 64. Encryption -1 of ASCII or Decryption +1 to ASCII
                                //Edge case: encrption if -1 value is lessa chan 33 then do the consecutive from 64 backwards is considered
                            {
                                if (c == 33)
                                    outFileStream.Write((char)64);
                                else
                                    outFileStream.Write((char)(c - 1));
                            }
                            else
                            {
                                outFileStream.Write(c);
                            }
                        }
                    }
                }
            }

            MessageBox.Show(outputFilePath, "Encrypted file is saved at:");
        }

        /// <summary>
        /// Decrypt the Data from file and write to output file in the same folder as inputFolder
        /// </summary>
        private void Decrypt()
        {
            var outputFilePath = inputFilePath.FileName.Substring(0, inputFilePath.FileName.Length - 4) + "_PlainText.txt";


            using (StreamReader fileStream = new StreamReader(lblFilePath.Text))
            {
                var s = Path.GetFileName(inputFilePath.FileName).Length;
                using (StreamWriter outFileStream = new StreamWriter(outputFilePath))  
                {
                    char[] temp = new char[1024];
                    int countData = 0;
                    int cycle = 0;
                    while ((countData = fileStream.ReadBlock(temp, 0, temp.Length)) > 0)
                    {
                        foreach (char c in temp)
                        {
                            if ((c >= ASCII_LowerCase_start && c <= ASCII_LowerCase_end))
                            {
                                if (cycle == 0)
                                {
                                    outFileStream.Write(M1.FirstOrDefault(x => x.Value == c).Key);
                                    cycle++;
                                }
                                else if (cycle == 1)
                                {
                                    //outFileStream.Write(M2.FirstOrDefault(x => x.Value == c).Key); There is no way while decrypting we have a lower case letter
                                    cycle++;
                                }
                                else if (cycle == 2)
                                {
                                    outFileStream.Write(M3.FirstOrDefault(x => x.Value == c).Key);
                                    cycle = 0;
                                }

                            }
                            else if ((c >= ASCII_UpperCase_start && c <= ASCII_UpperCase_end))
                            {
                                if (cycle == 0)
                                {
                                    outFileStream.Write(char.ToUpper(M1.FirstOrDefault(x => x.Value == char.ToLower(c)).Key));
                                    cycle++;
                                }
                                else if (cycle == 1)
                                {
                                    outFileStream.Write(M2.FirstOrDefault(x => x.Value == c).Key);
                                    cycle++;
                                }
                                else if (cycle == 2)
                                {
                                    outFileStream.Write(char.ToUpper(M3.FirstOrDefault(x => x.Value == char.ToLower(c)).Key));
                                    cycle = 0;

                                }
                            }
                            else if (c >= 33 && c <= 64)//Special characters considered only ASCII from 33 to 64. Encryption -1 of ASCII or Decryption +1 to ASCII
                                                        //Edge case: encrption if -1 value is lessa chan 33 then do the consecutive from 64 backwards is considered
                            {
                                if (c == 64)
                                    outFileStream.Write((char)33);
                                else
                                    outFileStream.Write((char)(c + 1));
                            }
                            else
                                outFileStream.Write(c);
                        }
                    }
                }
            }

            MessageBox.Show(outputFilePath, "Decrypted file is saved at:");
        }

        private void btnInputFile_Click(object sender, EventArgs e)
        {
       
            if(inputFilePath.ShowDialog()==DialogResult.OK)
            {
                if (inputFilePath.CheckFileExists)
                {
                    lblFilePath.Text = inputFilePath.FileName;
                    //MessageBox.Show("Exists");
                }
                else
                {
                   // MessageBox.Show("Doesn't Exist");
                }
            }
  
        }
       
    }
}
