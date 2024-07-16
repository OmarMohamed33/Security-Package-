using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Columnar : ICryptographicTechnique<string, List<int>>
    {
        public List<int> Analyse(string plainText, string cipherText)
        {
            cipherText = cipherText.ToLower();
            List<int> key_result = new List<int>();


            //get the # of letters rows
            int n_letters = plainText.Length;

            //get the # of keys columns how?
            // let'say the # of the key is 2 
            int key_size = 2;
            bool flag = false;
            while (!flag)
            {
                key_size++;
                int n_col = key_size;
                int n_rows = (int)Math.Ceiling((decimal)n_letters / (decimal)n_col);

                // construct 2 matrices with key =2 
                char[,] pt_matrix = new char[n_rows, n_col];
                char[,] ciphered_matrix = new char[n_rows, n_col];

                int k = 0;

                for (int i = 0; i < n_rows; i++)
                {
                    for (int j = 0; j < n_col; j++)
                    {
                        if (k > plainText.Length - 1)
                        {
                            pt_matrix[i, j] = 'x';


                        }
                        else
                        {
                            pt_matrix[i, j] = plainText[k++];
                        }

                    }
                }

                // constructing matrix for cipher text
                int f = 0;
                for (int i = 0; i < n_col; i++)
                {
                    for (int j = 0; j < n_rows; j++)
                    {
                        if (f < cipherText.Length)
                        {
                            ciphered_matrix[j, i] = cipherText[f++];
                        }

                    }
                }



                // split the cipher text to array first col only
                string x = cipherText.Substring(0, n_rows);


                for (int m = 0; m < n_col; m++)
                {
                    string ss = null;
                    for (int n = 0; n < n_rows; n++)
                    {
                        ss += pt_matrix[n, m];

                    }
                    if (x.Equals(ss))
                    {
                        flag = true;


                        // loop through the first column of the matrix of the plain text
                        // loop through the columns of the encrptyd matrix
                        // if you find that 2 columns are the same 
                        // add to the list the index of the row +1 
                        // loop through the second column of the matrix of the plain text
                        // repeat for all columns or for the # of key

                        List<string> ct_columns = new List<string>();
                        for (int i = 0; i < n_col; i++)
                        {
                            string col_string = null;
                            for (int j = 0; j < n_rows; j++)
                            {
                                col_string += ciphered_matrix[j, i];

                            }
                            ct_columns.Add(col_string);
                        }


                        List<string> pt_columns = new List<string>();
                        for (int i = 0; i < n_col; i++)
                        {
                            string col_string = null;
                            for (int j = 0; j < n_rows; j++)
                            {
                                col_string += pt_matrix[j, i];


                            }
                            pt_columns.Add(col_string);

                        }

                        /*                        for (int w = 0; w < key_size; w++)

                                                {
                                                    pt_columns[w]=pt_columns[w].Trim('x');
                                                    ct_columns[w]=ct_columns[w].Trim('x');
                                                }*/

                        for (int y = 0; y < key_size; y++)
                        {
                            for (int t = 0; t < key_size; t++)
                            {
                                if (pt_columns[y].Equals(ct_columns[t]))
                                {
                                    key_result.Add(t + 1);
                                    break;
                                }
                            }

                        }





                    }
                }
            }
            return key_result;

        }

        public string Decrypt(string cipherText, List<int> key)
        {
            //  throw new NotImplementedException();
            //string plainText = "ComputerScience";

            List<char> cipherlist = new List<char>();

            int columnss, rawss;
            //convert ciphertext to char array
            char[] characters = cipherText.ToCharArray();
            int[] keyArr = key.ToArray();
            char[] cT = new char[cipherText.Length];
            //array to comparison with key list
            int[] new_keyArr = new int[keyArr.Length];
            //no.columns
            columnss = keyArr.Length;
            //no.rows
            rawss = cipherText.Length / columnss;
            //ceiling
            if ((cipherText.Length % columnss) != 0)
                rawss += 1;

            //create a matrix
            char[,] xxx = new char[rawss, columnss];

            int counter = 0;
            //copy ciphertext to matrix
            for (int c = 0; c < columnss; c++)
            {
                for (int r = 0; r < rawss; r++)
                {
                    if (counter < cipherText.Length)
                    {
                        xxx[r, c] = characters[counter];
                    }
                    else
                    {
                        xxx[r, c] = 'z';
                    }
                    counter++;

                }
                //Console.WriteLine(xxx[r, c]);
            }
            //set a new array by numbers from 1 to key length
            int num = 1;
            int len = keyArr.Length;
            for (int g = 0; g < len; g++)
            {
                new_keyArr[g] = num;

                num++;

            }
            //return plaintext in matrix
            for (int too = 0; too < rawss; too++)
            {
                for (int coo = 0; coo < columnss; coo++)
                {
                    for (int koo = 0; koo < columnss; koo++)
                    {

                        if (keyArr[coo] == new_keyArr[koo])
                        {


                            if (xxx[too, koo] != 'z')
                            {
                                cipherlist.Add(xxx[too, koo]);
                                // Console.WriteLine(xxx[t, k]);
                            }

                        }
                    }


                }
            }
            //convert list to char array
            char[] a = cipherlist.ToArray();
            //convert char array to string
            string finalResult = new string(a).ToLower();
            return finalResult;


        }

        public string Encrypt(string plainText, List<int> key)
        {
            string encryption = null;

            // get the length of the key (number of columns)
            int columns = key.Count;

            // number of rows=  ceiling of length of plaintext / length of the key
            int rows = (int)Math.Ceiling((decimal)plainText.Length / (decimal)columns);

            // construct the matrix array (nested loop)
            char[,] array = new char[rows, columns];

            int index = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {

                    // (check if all of the char are filled and still a place in the array)
                    // add an 'x' char
                    if (index > plainText.Length - 1)
                    {
                        array[i, j] = 'x';
                    }
                    else array[i, j] = plainText[index++];
                }
            }

            // create a dictionary that holds key:order of col,value:list_of_char_in_column
            Dictionary<int, List<char>> dic = new Dictionary<int, List<char>>();

            // k index of the key
            int k = 0;

            // loop through the array to extract columns one by one.
            for (int c = 0; c < columns; c++)
            {
                // create a new list for each column
                List<char> list = new List<char>();

                for (int r = 0; r < rows; r++)
                {

                    list.Add(array[r, c]);
                }

                // add the order of the col and the list of char to dictionary
                dic.Add(key[k++], list);
            }


            // lambda function to order dictionary ascendingly based on key;
            var ordered_list = dic.OrderBy(x => x.Key).ToList();

            // extracting the cipher from the ordered dictionary
            foreach (KeyValuePair<int, List<char>> x in ordered_list)
            {
                foreach (char c in x.Value)
                {
                    encryption += c;
                }

            }


            return encryption;

        }
    }
}
