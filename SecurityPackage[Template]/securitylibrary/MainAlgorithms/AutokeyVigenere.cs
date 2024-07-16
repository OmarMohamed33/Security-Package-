using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class AutokeyVigenere : ICryptographicTechnique<string, string>
    {
        public void Viginere_table(char[,] table) 
        {
            /* --------p.t--------
              -
              -
              k
              e-------row-------
              y
              -
              -
              --------------------*/
            int row = 0, shift = 0;
            string Alphapitic = "abcdefghijklmnopqrstuvwxyz";
            while (row < 26)
            {
                int column = 0;
                while (column < 26)
                {
                    table[row, column] = Alphapitic[(column + shift) % 26];
                    column++;
                }
                shift++;
                row++;
            }
        }
        public string Analyse(string plainText, string cipherText)
        {
            // throw new NotImplementedException();
            char[,] table = new char[26, 26];
            Viginere_table(table);
            cipherText = cipherText.ToLower();
            plainText = plainText.ToLower();
            string key = null;
            for (int i = 0; i < plainText.Length; i++)
            {
                for (int column = 0; column < 26; column++)
                {
                    if (plainText[i] == table[0, column])
                    {
                        for (int row = 0; row < 26; row++)
                        {
                            if (cipherText[i] == table[row, column])
                                key = string.Concat(key, table[row, 0]);

                        }
                    }
                }
               
            }
            string text = "";
            for (int j = 0; j < 4; j++)
            {
                text += plainText[j];

            }
            int index = key.IndexOf(text);
             key = key.Remove(index);
            return key;

        }

        public string Decrypt(string cipherText, string key)
        {
           // throw new NotImplementedException();
            char[,] table = new char[26, 26];
            Viginere_table(table);
            string plain_Text = null;
            cipherText = cipherText.ToLower();
            key = key.ToLower();

            for (int i = 0; i < cipherText.Length; i++)
            {
                if (i < key.Length)
                {
                    for (int row = 0; row < 26; row++)
                    {
                        if (key[i] == table[row, 0])
                        {
                            for (int column = 0; column < 26; column++)
                            {
                                if (cipherText[i] == table[column, row])
                                    plain_Text = string.Concat(plain_Text, table[0, column]);
                            }
                        }
                    }
                   
                }
                else
                {
                    for (int row = 0; row < 26; row++)
                    {
                        if (plain_Text[i - key.Length] == table[row, 0])
                        {
                            for (int column = 0; column < 26; column++)
                            {
                                if (cipherText[i] == table[column, row])
                                    plain_Text = string.Concat(plain_Text, table[0, column]);

                            }
                        }
                    }

                }

            }
   
            return plain_Text;
        }

        public string Encrypt(string plainText, string key)
        {
            //throw new NotImplementedException();
            char[,] table = new char[26, 26];
            Viginere_table(table);

            string cipher_text = null;
            plainText = plainText.ToLower();
            key = key.ToLower();

            if (key.Length < plainText.Length)
            {
                int N = plainText.Length - key.Length;
                for (int i = 0; i < N; i++)
                {
                    key = string.Concat(key, plainText[i]);
                }
            }
            for (int i = 0; i < plainText.Length; i++)
            {
                int r = 0, k = 0;
                for (int row = 0; row < 26; row++)
                {
                    if (table[row, 0] == key[i])
                        r = row;

                }
                for (int column = 0; column < 26; column++)
                {
                    if (table[0, column] == plainText[i])
                        k = column;

                }
                cipher_text = string.Concat(cipher_text, table[r, k]);
            }
            return cipher_text;
        }
    }
}
