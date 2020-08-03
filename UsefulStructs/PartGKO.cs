using System.Numerics;

namespace CryptoCalc
{
    public struct PartGKO
    {
        public BigInteger A, B, N;

        public PartGKO(BigInteger a, BigInteger b, BigInteger n)
        {
            A = a;
            B = b;
            N = n;
        }
    }
}
