using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary.ElGamal
{
    public class ElGamal
    {
        /// <summary>
        /// Encryption
        /// </summary>
        /// <param name="alpha"></param>
        /// <param name="q"></param>
        /// <param name="y"></param>
        /// <param name="k"></param>
        /// <returns>list[0] = C1, List[1] = C2</returns>
        /// 
        public int squareMultiply(int a, int b, int c)
        {
            int res = 1;
            for (int i = 0; i < b; i++)
            {
                res = (res * a) % c;
            }
            return res;
        }
        public int EcuildsInverse(int number, int baseN)
        {
            int b = number;
            int m = baseN;
            int a1 = 1, a2 = 0, a3 = m;
            int b1 = 0, b2 = 1, b3 = b;

            while (b3 != 0 && b3 != 1)
            {
                int q = a3 / b3;
                int t1 = a1 - (q * b1)
                , t2 = a2 - (q * b2)
                , t3 = a3 - (q * b3);

                a1 = b1;
                a2 = b2;
                a3 = b3;
                b1 = t1;
                b2 = t2;
                b3 = t3;
            }

            if (b3 == 0)
            {
                return -1;
            }
            else if (b3 == 1)
            {
                b2 = b2 < -1 ? b2 + baseN : b2;
                return b2;
            }

            return -1;
        }

        public List<long> Encrypt(int q, int alpha, int y, int k, int m)
        {
            //c1= alpha^k mod q (square multiply)
            long c1 = squareMultiply(alpha, k, q);

            //Key=y^k mod q
            long key = squareMultiply(y, k, q);

            //c2=key*M mod q
            long c2 = (key * m) % q;

            List<long> result = new List<long>();
            result.Add(c1);
            result.Add(c2);

            return result;
            //return list of c1 & c2
        }
        public int Decrypt(int c1, int c2, int x, int q)
        {
            //key=c1^x mod q (square multiply)
            int key = squareMultiply(c1, x, q);

            //m=c2*key inverse mod q =>> eculid's algo
            int m = (c2 * EcuildsInverse(key, q)) % q;
            return m;
        }
    }
}
