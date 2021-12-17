using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cw_wpf
{
    public class VigenereCipher
    {
        public CipherMode cipherMode;
        public string Alphabet { get; set; }
        public VigenereCipher(string alphabet, CipherMode cipherMode)
        {
            Alphabet = alphabet;
            this.cipherMode = cipherMode;
        }

        private string Cipher(string word, string key, bool isToCipher)
        {
            string result = "";
            int notLetterShift = 0;
            key = key.ToLower();

            HashSet<char> wrongChars = new HashSet<char>();
            foreach (char letter in key)
            {
                if (!Alphabet.Contains(letter))
                {
                    wrongChars.Add(letter);
                }
            }

            if (wrongChars.Count != 0)
            {
                string exceptionMessage = "";
                foreach (char letter in wrongChars)
                {
                    exceptionMessage += letter;
                }
                throw new WrongKeyException($"Значение ключа содержит символы, не входящие в алфавит:\n{exceptionMessage}");
            }


            for (int i = 0; i < word.Length; i++)
            {
                if (Alphabet.Contains(char.ToLower(word[i])))
                {
                    bool isUpper = char.IsUpper(word[i]);

                    int currentLetterIndex = Alphabet.IndexOf(char.ToLower(word[i]));
                    int keyLetterIndex = (i - notLetterShift) % key.Length;

                    int keyLetterShift = Alphabet.IndexOf(key[keyLetterIndex]);
                    keyLetterShift = isToCipher ? keyLetterShift : keyLetterShift * (-1);

                    char resultChar;
                    if (cipherMode == CipherMode.ROT1 && isToCipher)
                    {
                        resultChar = Alphabet[((currentLetterIndex + keyLetterShift + 1) % Alphabet.Length + Alphabet.Length) % Alphabet.Length];
                        resultChar = isUpper ? char.ToUpper(resultChar) : resultChar;
                    }
                    else if(cipherMode == CipherMode.ROT1 && !isToCipher)
                    {
                        resultChar = Alphabet[((currentLetterIndex + keyLetterShift - 1) % Alphabet.Length + Alphabet.Length) % Alphabet.Length];
                        resultChar = isUpper ? char.ToUpper(resultChar) : resultChar;
                    }
                    else
                    {
                        resultChar = Alphabet[((currentLetterIndex + keyLetterShift) % Alphabet.Length + Alphabet.Length) % Alphabet.Length];
                        resultChar = isUpper ? char.ToUpper(resultChar) : resultChar;
                    }
                    

                    result += resultChar;
                }
                else
                {
                    result += word[i];
                    ++notLetterShift;
                }

            }

            return result;
        }

        public string Encrypt(string input, string key)
        {
            return Cipher(input, key, true);
        }

        public string Decrypt(string input, string key)
        {
            return Cipher(input, key, false);
        }
    }
}
