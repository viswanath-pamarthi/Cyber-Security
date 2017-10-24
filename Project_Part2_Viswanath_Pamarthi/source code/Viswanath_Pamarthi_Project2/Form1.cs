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

namespace Viswanath_Pamarthi_Project2
{
    public partial class Form1 : Form
    {
        OpenFileDialog inputFilePath = new OpenFileDialog();
        char pad = '*';
        int depth = 4;

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
        private void GenerateCipher(int direction, ref Dictionary<char, char> objectToStore, int startAsciiValue, int shiftBy)
        {
            int EndAsciiValue = startAsciiValue + 25;
            int temp = 0;

            if (direction == 0)
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
                        objectToStore.Add((char)(i), (char)((EndAsciiValue + 1) - (startAsciiValue % temp)));
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
                        objectToStore.Add((char)(i), (char)((startAsciiValue - 1) + (temp % EndAsciiValue)));
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
            List<string> rows = new List<string>();
            
            for(int i=0;i<depth;i++)
            {
                rows.Add("");
            }


            var outputFilePath = Path.Combine(inputFilePath.FileName.Substring(0, inputFilePath.FileName.Length - 4) + "_Ciphertext.txt");
            using (StreamReader fileStream = new StreamReader(lblFilePath.Text))
            {
                using (StreamWriter outFileStream = new StreamWriter(outputFilePath))
                {
                    char[] temp = new char[1024];
                    int countData = 0;
                    int cycle = 0;
                    int up = 0;

                    while ((countData = fileStream.ReadBlock(temp, 0, temp.Length)) > 0)
                    {
                        foreach (char c in temp)
                        {
                            if (c == '\0')
                                break;
                            if (c == 32)
                                continue;

                            if ((up == 0) && (cycle < 3))
                            {
                                //outFileStream.Write(M1[c]);
                                rows[cycle] += c;
                                cycle++;
                            }
                            else if ((up == 1) && (cycle ==1))
                            {
                                rows[cycle] += c;
                                cycle = 0;
                                up = 0;
                            }
                            else if ((up == 1) && (cycle ==2))
                            {
                                rows[cycle] += c;
                                cycle--;
                            }
                            else if ((up == 0)&&(cycle == 3))
                            {
                                rows[cycle] += c;
                                cycle = 2;
                                up = 1;

                            }
                        }

                        foreach(string str in rows)
                        {
                            outFileStream.Write(str.Trim(' '));
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
                List<int> rowsLengths = new List<int>();
                List<List<char>> rows = new List<List<char>>();
                for (int i = 0; i < depth; i++)
                {
                    rowsLengths.Add(0);
                    rows.Add(new List<char>());
                }

                var s = Path.GetFileName(inputFilePath.FileName).Length;
                using (StreamWriter outFileStream = new StreamWriter(outputFilePath))
                {
                    char[] temp = new char[1024];
                    int countData = 0;
                    int cycle = 0;
                    
                    int up = 0;

                    while ((countData = fileStream.ReadBlock(temp, 0, temp.Length)) > 0)
                    {
                        foreach (char c in temp)
                        {
                            if (c == '\0')
                                break;
                            if (c == 32)
                                continue;

                            if ((up == 0) && (cycle < 3))
                            {
                                //outFileStream.Write(M1[c]);
                                rowsLengths[cycle]++;
                                cycle++;
                            }
                            else if ((up == 1) && (cycle == 1))
                            {
                                rowsLengths[cycle]++;
                                cycle = 0;
                                up = 0;
                            }
                            else if ((up == 1) && (cycle == 2))
                            {
                                rowsLengths[cycle]++;
                                cycle--;
                            }
                            else if ((up == 0) && (cycle == 3))
                            {
                                rowsLengths[cycle]++;
                                cycle = 2;
                                up = 1;
                            }
                        }

                        
                    }


                    string allTextTodecrypt = File.ReadAllText(lblFilePath.Text);
                    int indexOfCharacter = 0;


                    for (int j = 0; j < depth; j++)
                    {
                        for (int i = 0; i < rowsLengths[j]; i++)
                        {
                            rows[j].Add(allTextTodecrypt[indexOfCharacter]);
                            indexOfCharacter++;
                        }
                    }

                    int[] trackNextCharacterOfeachrow = new int[4] { 0, 0, 0, 0 };
                    up = 0;
                    cycle = 0;
                    foreach(char c in allTextTodecrypt)
                    {
                        if (c == '\0')
                            break;
                        if (c == 32)
                            continue;

                        if ((up == 0) && (cycle < 3))
                        {
                            //outFileStream.Write(M1[c]);
                            outFileStream.Write(rows[cycle][trackNextCharacterOfeachrow[cycle]]);
                            trackNextCharacterOfeachrow[cycle] ++;
                            cycle++;
                        }
                        else if ((up == 1) && (cycle == 1))
                        {
                            outFileStream.Write(rows[cycle][trackNextCharacterOfeachrow[cycle]]);
                            trackNextCharacterOfeachrow[cycle]++;
                            cycle = 0;
                            up = 0;
                        }
                        else if ((up == 1) && (cycle == 2))
                        {
                            outFileStream.Write(rows[cycle][trackNextCharacterOfeachrow[cycle]]);
                            trackNextCharacterOfeachrow[cycle]++;
                            cycle--;
                        }
                        else if ((up == 0) && (cycle == 3))
                        {
                            outFileStream.Write(rows[cycle][trackNextCharacterOfeachrow[cycle]]);
                            trackNextCharacterOfeachrow[cycle]++;
                            cycle = 2;
                            up = 1;

                        }
                    }
                   

                }
            }
        

            MessageBox.Show(outputFilePath, "Decrypted file is saved at:");
        }

        private void btnInputFile_Click(object sender, EventArgs e)
        {

            if (inputFilePath.ShowDialog() == DialogResult.OK)
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

        private void btnRun_Click_1(object sender, EventArgs e)
        {

        }
    }
}
