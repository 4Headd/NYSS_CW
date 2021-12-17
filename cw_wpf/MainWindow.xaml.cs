using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using Xceed.Words.NET;

namespace cw_wpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private VigenereCipher vigenereCipher = new VigenereCipher(LanguageToEncrypt.RuAlphabet, CipherMode.ROT0);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_GetResult_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (RadioButton_Encrypt.IsChecked == true)
                {
                    TextBox_Result.Text = vigenereCipher.Encrypt(TextBox_Request.Text, TextBox_Key.Text);
                }
                else if (RadioButton_Decrypt.IsChecked == true)
                {
                    TextBox_Result.Text = vigenereCipher.Decrypt(TextBox_Request.Text, TextBox_Key.Text);
                }

                if (!string.IsNullOrEmpty(TextBox_Result.Text))
                {
                    Button_Swap.IsEnabled = true;
                }
            }
            catch (WrongKeyException exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void RadioButton_ROTZero_Checked(object sender, RoutedEventArgs e)
        {
            vigenereCipher.cipherMode = CipherMode.ROT0;
        }

        private void RadionButton_ROTOne_Checked(object sender, RoutedEventArgs e)
        {
            vigenereCipher.cipherMode = CipherMode.ROT1;
        }

        private void ComboBox_Language_Selected(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;

            switch (selectedItem.Content?.ToString())
            {
                case "Русский алфавит":
                    vigenereCipher.Alphabet = LanguageToEncrypt.RuAlphabet;
                    break;
                case "Русский алфавит без Ё":
                    vigenereCipher.Alphabet = LanguageToEncrypt.RuAlphabetShort;
                    break;
                case "Английский алфавит":
                    vigenereCipher.Alphabet = LanguageToEncrypt.EngAlphabet;
                    break;
            }
        }

        private void ChooseFile_Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog;

            if (RadioButton_TxtExtension_Copy.IsChecked == true)
            {
                openFileDialog = new OpenFileDialog
                {
                    Filter = "txt files(*.txt)|*.txt"
                };

                openFileDialog.ShowDialog();

                try
                {
                    if (!string.IsNullOrEmpty(openFileDialog.FileName))
                    {
                        TextBox_Request.Text = File.ReadAllText(openFileDialog.FileName);
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show($"Что-то пошло не так во время открытия файла: {exception.Message}");
                }

            }
            else
            {
                openFileDialog = new OpenFileDialog
                {
                    Filter = "docx files(*.docx)|*.docx"
                };

                openFileDialog.ShowDialog();

                try
                {
                    if (!string.IsNullOrEmpty(openFileDialog.FileName))
                    {
                        DocX doc = DocX.Load(openFileDialog.FileName);

                        Xceed.Document.NET.Document document = doc.Copy();
                        string res = "";
                        bool isFirst = true;
                        for (int i = 0; i < document.Paragraphs.Count; i++)
                        {
                            if (isFirst)
                            {
                                isFirst = false;
                                res += document.Paragraphs[i].Text.ToString();
                            }
                            else
                            {
                                res += "\r\n" + document.Paragraphs[i].Text.ToString();
                            }
                        }

                        TextBox_Request.Text = res;
                        document.Dispose();
                        doc.Dispose();
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show($"Что-то пошло не так во время открытия файла: {exception.Message}");
                }

            }
        }


        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog;

            if (string.IsNullOrEmpty(TextBox_Result.Text))
            {
                MessageBox.Show("Поле с результатом пусто!");
                return;
            }

            if (RadioButton_TxtExtension.IsChecked == true)
            {
                saveFileDialog = new SaveFileDialog
                {
                    Filter = "txt files(*.txt)|*.txt"
                };

                saveFileDialog.ShowDialog();

                if (!string.IsNullOrEmpty(saveFileDialog.FileName))
                {
                    using (StreamWriter sw = File.CreateText(saveFileDialog.FileName))
                    {
                        sw.Write(TextBox_Result.Text);
                    }
                }
            }
            else
            {
                saveFileDialog = new SaveFileDialog
                {
                    Filter = "docx files(*.docx)|*.docx"
                };

                saveFileDialog.ShowDialog();

                string[] paragraphs = TextBox_Result.Text.Split('\n');
                string temp = string.Join("", paragraphs);
                paragraphs = temp.Split('\r');


                if (!string.IsNullOrEmpty(saveFileDialog.FileName))
                {
                    DocX doc = DocX.Create(saveFileDialog.FileName);
                    for (int i = 0; i < paragraphs.Length; i++)
                    {
                        doc.InsertParagraph(paragraphs[i]);
                    }
                    doc.Save();
                    doc.Dispose();
                }
            }
        }


        private void Button_Swap_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBox_Result.Text))
            {
                TextBox_Request.Text = TextBox_Result.Text;
                TextBox_Result.Text = string.Empty;
                Button_Swap.IsEnabled = false;
            }
        }
    }
}
