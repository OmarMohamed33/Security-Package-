using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Monoalphabetic : ICryptographicTechnique<string, string>
    {
        public string Analyse(string plainText, string cipherText)
        {
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            plainText = plainText.ToLower();
            string key = null;
            cipherText = cipherText.ToLower();
            List<char> letters = new List<char>();


            // create a dictionary where key is the alphabet letter and value is the respective
            // ciphered letter for it initially null;
            Dictionary<char, char> dict = new Dictionary<char, char>();

            // loop through 26 letters to init dict
            for (int i = 0; i < 26; i++)
            {
                // add the alphabet letters to the list
                letters.Add(alphabet[i]);
                dict.Add(alphabet[i], '*');
            }

            // loop through the plaintext and the ciphered 
            for (int i = 0; i < plainText.Length; i++)
            {
                dict[plainText[i]] = cipherText[i];
                letters.Remove(cipherText[i]);
            }


            // loop through the dictionary values to concatenate a string
            foreach (char x in dict.Values)
            {
                if (x != '*')
                {
                    key += x;
                    letters.Remove(x);

                }
                else
                {
                    key += letters[0];
                    letters.Remove(letters[0]);
                }
            }



            return key;
        }

        public string Decrypt(string cipherText, string key)
        {
            string decrypted_msg = null;
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Dictionary<char, char> dict = new Dictionary<char, char>();

            for (int i = 0; i < 26; i++)
            {
                dict.Add(char.ToUpper(key[i]), alphabet[i]);
            }

            foreach (char x in cipherText)
            {

                decrypted_msg += dict[char.ToUpper(x)];

            }

            return decrypted_msg;

        }

        public string Encrypt(string plainText, string key)
        {
            string encrypted_msg = null;
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Dictionary<char, char> dict = new Dictionary<char, char>();

            for (int i = 0; i < 26; i++)
            {
                dict.Add(alphabet[i], key[i]);
            }


            foreach (char x in plainText)
            {
                encrypted_msg += dict[char.ToUpper(x)];

            }

            return encrypted_msg;

        }

        /// <summary>
        /// Frequency Information:
        /// E   12.51%
        /// T	9.25
        /// A	8.04
        /// O	7.60
        /// I	7.26
        /// N	7.09
        /// S	6.54
        /// R	6.12
        /// H	5.49
        /// L	4.14
        /// D	3.99
        /// C	3.06
        /// U	2.71
        /// M	2.53
        /// F	2.30
        /// P	2.00
        /// G	1.96
        /// W	1.92
        /// Y	1.73
        /// B	1.54
        /// V	0.99
        /// K	0.67
        /// X	0.19
        /// J	0.16
        /// Q	0.11
        /// Z	0.09
        /// </summary>
        /// <param name="cipher"></param>
        /// <returns>Plain text</returns>
        public string AnalyseUsingCharFrequency(string cipher)
        {
            cipher = cipher.ToLower();
            string k = null;
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            alphabet = alphabet.ToLower();
            string freq_let = "etaoinsrhldcumfpgwybvkxjqz";
            Dictionary<char, int> letter = new Dictionary<char, int>();
            //throw new NotImplementedException();
            foreach (char x in cipher)
            {
                if (letter.ContainsKey(x))
                {
                    letter[x]++;
                }
                else letter.Add(x, 1);
            }




            var sorted_keys = letter.OrderByDescending(x => x.Value).ToList();

            Dictionary<char, char> ordered_freq = new Dictionary<char, char>();


            Dictionary<char, char> ordered_key = new Dictionary<char, char>();
            for (int i = 0; i < 26; i++)
            {
                ordered_key.Add(alphabet[i], '*');
            }

            for (int i = 0; i < 26; i++)
            {
                ordered_freq.Add(freq_let[i], sorted_keys[i].Key);
            }

            foreach (KeyValuePair<char, char> kv in ordered_freq)
            {
                ordered_key[kv.Key] = kv.Value;
            }

            Dictionary<char, char> reverse_dict = new Dictionary<char, char>();
            foreach (KeyValuePair<char, char> kv in ordered_key)
            {
                reverse_dict.Add(kv.Value, kv.Key);
            }

            foreach (char c in cipher)
            {
                k += reverse_dict[c];
            }

            return k;
        }
    }
}
