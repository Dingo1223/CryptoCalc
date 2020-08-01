using System;
using System.Numerics;

namespace CryptoCalc
{
    static class ProbTests
    {
        private static readonly Random r = new Random();

        //Возвращается false => составное
        //Возвращается true => неизвестно
        public static bool Test_Fermat(BigInteger number, int repeats)
        {
            if (number % 2 == 0) return false;
            int p = number >= int.MaxValue ? int.MaxValue - 1 : (int)number;

            for (int i = 0; i < repeats; i++)
            {
                int a = r.Next(2, p);
                if (Algorithms.Euclid(a, number) != 1) return false;
                if (Algorithms.ModExp(a, number - 1, number) != 1) return false;
            }
            return true;
        }

        public static bool Test_Solovay(BigInteger number, int repeats)
        {
            if (number % 2 == 0) return false;
            int p = number >= int.MaxValue ? int.MaxValue - 1 : (int)number;

            for (int i = 0; i < repeats; i++)
            {
                int a = r.Next(2, p);
                if (Algorithms.Euclid(a, number) != 1) return false;
                BigInteger tmp1 = Algorithms.ModExp(a, (number - 1) / 2, number);
                BigInteger tmp2 = Algorithms.Jacobi(a, number);
                if ((tmp1 != tmp2) && ((tmp1 - number) != tmp2)) return false;
            }
            return true;
        }

        public static bool Test_Rabin(BigInteger number, int repeats)
        {
            BigInteger t, s;
            if (number % 2 == 0) return false;
            else
            {
                t = number - 1;
                s = 0;
                while (t % 2 == 0)
                {
                    s++;
                    t /= 2;
                }
            }

            int p = number >= int.MaxValue ? int.MaxValue - 1 : (int)number;

            for (int i = 0; i < repeats; i++)
            {
                int a = r.Next(2, p - 1);
                BigInteger pow = Algorithms.ModExp(a, t, number);
                if (pow == 1 || pow == number - 1) continue;

                bool skip = false;
                for (int j = 1; j < s; j++)
                {
                    pow = Algorithms.ModExp(pow, 2, number);
                    if (pow == 1) return false;
                    if (pow == number - 1)
                    {
                        skip = true;
                        break;
                    }
                }
                if (!skip) return false;
            }
            return true;
        }
    }
}
