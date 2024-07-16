using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Ceaser : ICryptographicTechnique<string, int>
    {

        public string Encrypt(string plainText, int key)
        {
            char[] alphabet = {'a','b','c','d','e','f','g','h','i','j','k',
           'l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'};

            char[] cT = new char[plainText.Length];

            char[] PT = new char[plainText.Length];

            for (int i = 0; i < plainText.Length; i++)
            {
                PT[i] = plainText[i];
            }

            for (int k = 0; k < plainText.Length; k++)
            {
                for (int c = 0; c < alphabet.Length; c++)
                {
                    if (PT[k].CompareTo(alphabet[c]) == 0)
                    {
                        cT[k] = alphabet[(c + key) % 26];
                    }
                }
            }
            string finalResult = new string(cT);
            return finalResult;
        }

        public string Decrypt(string cipherText, int key)
        {
            char[] alphabet = {'A','B','C','D','E','F','G','H','I','J','K',
            'L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'};

            char[] cT = new char[cipherText.Length];
            char[] PT = new char[cipherText.Length];

            for (int i = 0; i < cipherText.Length; i++)
            {
                cT[i] = cipherText[i];
            }

            for (int k = 0; k < cipherText.Length; k++)
            {
                for (int c = 0; c < alphabet.Length; c++)
                {
                    if (cT[k].CompareTo(alphabet[c]) == 0)
                    {
                        if ((c - key) > 0)
                        {
                            PT[k] = alphabet[(c - key) % 26];

                        }
                        else
                        {
                            int test = (26 + (c - key)) % 26;
                            PT[k] = alphabet[test];
                        }
                    }
                }
            }
            string finalResult = new string(PT).ToLower();
            return finalResult;

        }

        public int Analyse(string plainText, string cipherText)
        {
            char[] alphabet = {'A','B','C','D','E','F','G','H','I','J','K',
           'L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'};

            int pT_Index, cT_Index;
            int result;
            char[] cT = new char[plainText.Length];
            char[] PT = new char[plainText.Length];
            string str = plainText.ToUpper();
            for (int i = 0; i < cipherText.Length; i++)
            {
                PT[i] = str[i];
                cT[i] = cipherText[i];
            }

            char cipherT = cT[10];
            cT_Index = Array.IndexOf(alphabet, cipherT);

            char plainT = PT[10];
            pT_Index = Array.IndexOf(alphabet, plainT);

            result = (cT_Index - pT_Index) % 26;

            return result;
        }
    }
}