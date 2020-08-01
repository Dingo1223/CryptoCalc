using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCalc
{
    static class Algorithms
    {
        //Алгоритм Евклида (НОД)
        public static BigInteger Euclid(BigInteger a, BigInteger b) => b == 0 ? BigInteger.Abs(a) : Euclid(b, a % b);


    }
}
