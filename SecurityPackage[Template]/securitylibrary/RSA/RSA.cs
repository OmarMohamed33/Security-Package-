using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.RSA
{
    public class RSA
    {
        public int Encrypt(int p, int q, int M, int e)
        {
            int n = p * q;
            int phi_n = (p - 1) * (q - 1);
            /*int C = M ^ e % n;*/
            int C = 1;
            for (int i = 0; i < e; i++)
            {
                C = (C * M) % n;
            }
            return C;
        }

        public int Decrypt(int p, int q, int C, int e)
        {
            int n = p * q;
            int phi_n = (p - 1) * (q - 1);
            int private_key = 1;

            while ((private_key * e) % phi_n != 1)
            {
                private_key++;

            }


            int M = 1;
            for (int i = 0; i < private_key; i++)
            {
                M = (M * C) % n;
            }
            return M;
        }
    }
}
