﻿using System;
using System.Collections.Generic;
using System.Numerics;
using System.Windows;

namespace CryptoCalc
{
    static class Algorithms
    {
        //Алгоритм Евклида (НОД)
        public static BigInteger Euclid(BigInteger a, BigInteger b) => b == 0 ? BigInteger.Abs(a) : Euclid(b, a % b);

        //Расширенный алгоритм Евклида
        public static BigInteger Euclid_Ext(BigInteger a, BigInteger b, ref BigInteger x, ref BigInteger y, BigInteger x1, BigInteger y1)
        {
            if (b == 0) return a;
            BigInteger x_tmp = x, y_tmp = y;
            x = x1;
            y = y1;
            return Euclid_Ext(b, a % b, ref x, ref y, x_tmp - x1 * (a / b), y_tmp - y1 * (a / b));
        }

        //Обобщенный алгоритм Евклида
        public static BigInteger Euclid_Gen(List<BigInteger> nums)
        {
            nums.Sort();
            for (int i = nums.Count - 1; i >= 0; i--)
                if (nums[i] == 0) nums.RemoveAt(i);
            if (nums.Count == 1) return nums[0];
            for (int i = 1; i < nums.Count; i++)
                nums[i] %= nums[0];
            return Euclid_Gen(nums);
        }

        //Решение диофантовых уравнений
        public static BigInteger Diophantine(BigInteger a, BigInteger b, BigInteger c, ref BigInteger x1, ref BigInteger y1)
        {
            BigInteger x = x1, y = y1;
            BigInteger d = Euclid_Ext(BigInteger.Abs(a), BigInteger.Abs(b), ref x, ref y, 0, 1);
            if (a < 0) x = -x;
            if (b < 0) y = -y;
            if (c % d != 0) return 0;
            x1 = x * (c / d);
            y1 = y * (c / d);
            return d;
        }

        //Решение сравнений РАЕ
        public static BigInteger Modular_RAE(BigInteger a, BigInteger b, BigInteger n, ref BigInteger x)
        {
            BigInteger x1 = 1, y = 0, d = 0;
            while (a > n) a -= n;
            while (b > n) b -= n;
            while (b < 0) b += n;

            if (a >= n) d = Euclid_Ext(BigInteger.Abs(a), BigInteger.Abs(n), ref x1, ref y, 0, 1);
            else d = Euclid_Ext(BigInteger.Abs(n), BigInteger.Abs(a), ref x1, ref y, 0, 1);

            if (d < 0) d *= -1;
            if (b % d != 0) return 0;
            while (y < 0) y += n / d;
            x = (y * (b / d)) % n;
            return d;
        }

        //Решение сравнений Обратный
        public static BigInteger Modular_Rev(BigInteger a, BigInteger b, BigInteger n, ref BigInteger x)
        {
            if (!Algorithms.IsPrime(n)) return 0;

            BigInteger d;
            while (a > n) a -= n;
            while (b > n) b -= n;
            while (b < 0) b += n;

            d = Euclid(a, n);
            if (b % d != 0) return 0;
            x = Reverse_RAE(a, n) * (b / d);

            while (x < 0) x += n;
            while (x > n) x -= n;
            return d;
        }

        //Возведение в степень по модулю
        public static BigInteger ModExp(BigInteger a, BigInteger n, BigInteger N)
        {
            if (n == 0) return 1;
            BigInteger z = ModExp(a, n / 2, N);
            return n % 2 == 0 ? z * z % N : a * z * z % N;
        }

        //Нахождение обратного по модулю элемента (РАЕ)
        public static BigInteger Reverse_RAE(BigInteger a, BigInteger n)
        {
            BigInteger x1 = 1, y = 0;
            _ = Diophantine(a, n, 1, ref x1, ref y);
            if (x1 < 0) x1 += n;
            return x1;
        }

        //Нахождение обратного по модулю элемента (возведение в степень)
        public static BigInteger Reverse_Pow(BigInteger a, BigInteger n) => ModExp(a, n - 2, n);

        //Проверка на простоту (для малых чисел)
        public static bool IsPrime(BigInteger n)
        {
            for (BigInteger i = 2; i <= n / 2; i++)
                if (n % i == 0) return false;
            return true;
        }

        //Решение системы сравнений (греко-китайская теорема)
        public static GKO_X GKO(List<PartGKO> gko)
        {
            List<BigInteger> s = new List<BigInteger>();
            foreach (PartGKO part in gko) s.Add(part.N);

            for (int i = 0; i < s.Count - 1; i++)
            {
                for (int j = i + 1; j < s.Count; j++)
                {
                    if (Algorithms.Euclid(s[i], s[j]) != 1)
                    {
                        MessageBox.Show("Модули должны быть взаимно простыми");
                        return new GKO_X();
                    }
                }
            }

            GKO_X x = new GKO_X(gko[0].B * Reverse_RAE(gko[0].A, gko[0].N), gko[0].N);
            for (int i = 1; i < gko.Count; i++)
            {
                BigInteger x1 = 0;
                Modular_RAE(x.N, gko[i].B * Reverse_RAE(gko[i].A, gko[i].N) - x.A, gko[i].N, ref x1);
                x.A += x.N * x1;
                x.N *= gko[i].N;
            }
            return x;
        }

        //Символ Лежандра/Якоби
        public static BigInteger Jacobi(BigInteger a, BigInteger b)
        {
            if (Euclid(a, b) != 1) return 0;
            BigInteger r = 1;
            if (a < 0)
            {
                a = -a;
                if (b % 4 == 3) r = -r;
            }

            do
            {
                BigInteger t = 0;
                while (a % 2 == 0)
                {
                    t++;
                    a /= 2;
                }
                if (t % 2 != 0)
                    if (b % 8 == 3 || b % 8 == 5) r = -r;

                if (a % 4 == 3 && b % 4 == 3) r = -r;
                BigInteger c = a;
                a = b % c;
                b = c;
            }
            while (a != 0);

            return r;
        }

        //Алгоритм Шэнкса
        private static BigInteger Shanks(BigInteger a, BigInteger p)
        {
            if (Jacobi(a, p) != 1) throw new Exception("Нет решений");
            BigInteger s = 0, q = p - 1;
            while (q % 2 == 0)
            {
                s++;
                q /= 2;
            }
            if (s == 1) return ModExp(a, (p + 1) / 4, p);
            if (p % 8 == 5)
            {
                if (ModExp(a, (p - 1) / 4, p) == 1)
                    return ModExp(a, (p + 3) / 8, p);
                if (ModExp(a, (p - 1) / 4, p) == -1)
                    return 2 * a * ModExp(4 * a, (p - 5) / 8, p);
            }
            BigInteger z = 1;
            while (Jacobi(z, p) != -1) z++;
            BigInteger c = ModExp(z, q, p);
            BigInteger m = s;
            BigInteger R = ModExp(a, (q + 1) / 2, p);
            BigInteger t = ModExp(a, q, p);
            while (t % p != 1)
            {
                int i = 0;
                while (ModExp(t, (int)Math.Pow(2, i), p) != 1) i++;
                BigInteger b = ModExp(c, (BigInteger)Math.Pow(2, (long)m - i - 1), p);
                R = (R * b) % p;
                t = (t * b * b) % p;
                c = (b * b) % p;
                m = i;
            }
            return R;
        }

        //Решение квадратичных сравнений с использованием алгоритма Шэнкса
        public static void Quadr_Solve(BigInteger a, BigInteger p, out BigInteger x1, out BigInteger x2)
        {
            if (!Algorithms.IsPrime(p))
            {
                MessageBox.Show("Модуль должен быть простым");
                x1 = x2 = 0;
            }
            else
            {
                x1 = Algorithms.Shanks(a, p);
                x2 = p - x1;
            }
        }

        public static BigInteger Discr_Solve(BigInteger g, BigInteger a, BigInteger m)
        {
            BigInteger n = EulerPhi(m);
            BigInteger a1 = 0, a2 = 0, b1 = 0, b2 = 0, x1 = 1, x2 = 1;
            if (a == g) return 1;
            bool start = true;
            while (x1 != x2 || start)
            {
                start = false;
                if (x1 < m / 3)
                {
                    x1 = (a * x1) % m;
                    b1 += 1 % n;
                }
                else if (x1 >= (m / 3) && x1 < (2 * m / 3))
                {
                    x1 = (x1 * x1) % m;
                    a1 = (2 * a1) % n;
                    b1 = (2 * b1) % n;
                }
                else
                {
                    x1 = (g * x1) % m;
                    a1 = (a1 + 1) % n;
                }
                for (int j = 0; j < 2; j++)
                {
                    if (x2 < (m / 3))
                    {
                        x2 = (a * x2) % m;
                        b2 = (b2 + 1) % n;
                    }
                    else if (x2 >= (m / 3) && x2 < (2 * m / 3))
                    {
                        x2 = (x2 * x2) % m;
                        a2 = (2 * a2) % n;
                        b2 = (2 * b2) % n;
                    }
                    else
                    {
                        x2 = (g * x2) % m;
                        a2 = (a2 + 1) % n;
                    }
                }
            }
            BigInteger u = (a1 - a2) % n, v = (b2 - b1) % n;
            if ((v % n) == 0) return 0;
            BigInteger nu = 1, mu = 0, x = 0, i = 0;
            BigInteger d = Euclid_Ext(v, n, ref nu, ref mu, 0, 1);
            while (i != (d + 1))
            {
                x = ((u * nu + i * n) / d) % n;
                if ((Math.Pow((long)g, (long)x) - (long)a) % (long)m == 0) return x;
                i++;
            }
            return x;
        }

        private static BigInteger EulerPhi(BigInteger input)
        {
            BigInteger res = 1, i = 2;
            while (i * i <= input)
            {
                BigInteger p = 1;
                while (input % i == 0)
                {
                    input /= i;
                    p *= i;
                }
                p /= i;
                if (p != 0) res *= p * (i - 1);
                i++;
            }
            BigInteger n = input - 1;
            return n == 0 ? res : n * res;
        }

        public static BigInteger EC_Ord(BigInteger a, BigInteger b, BigInteger p)
        {
            BigInteger result = p + 1;
            for (BigInteger i = 0; i < p; i++)
            {
                result += Jacobi(BigInteger.Pow(i, 3) + (a * i) + b, p);
            }
            return result;
        }

        //Генерация многочлена (задача разделения секрета)
        public static List<int> GenerateF(int n, int t, int k, int p, out Dictionary<int, int> subkeys)
        {
            Random r = new Random();
            List<int> a = new List<int>();
            for (int i = 0; i < t - 1; i++)
                a.Add(r.Next(1, p));
            a.Add(k);

            subkeys = new Dictionary<int, int>();
            for (int i = 0; i < n; i++)
                subkeys.Add(i + 1, Algorithms.Horner(a, i + 1, p));

            return a;
        }

        //Схема Горнера (задача разделения секрета)
        private static int Horner(List<int> a, int x, int mod)
        {
            int res = a[0];
            for (int i = 1; i < a.Count; i++)
                res = (res * x + a[i]) % mod;
            return res;
        }

        //Получение строки СЛАУ
        private static List<int> GetRow(int x, int y, int t, int p)
        {
            List<int> res = new List<int>();
            for (int i = t - 1; i > 0; i--)
                res.Add((int)ModExp(x, i, p));
            res.Add(1);
            res.Add(y);
            return res;
        }

        //Решение системы СЛАУ (нахождение k)
        public static int Gaussian(Dictionary<int, int> subkeyes, int t, int p)
        {
            List<List<int>> a = new List<List<int>>();
            foreach (KeyValuePair<int, int> pair in subkeyes)
                a.Add(Algorithms.GetRow(pair.Key, pair.Value, t, p));

            for (int i = 0; i < t; i++)
            {
                for (int j = i + 1; j < t; j++)
                {
                    List<int> tmp1 = new List<int>(a[i]), tmp2 = new List<int>(a[j]);
                    for (int m = 0; m < tmp1.Count; m++)
                    {
                        tmp1[m] = tmp1[m] * a[j][i];
                        tmp2[m] = (tmp1[m] - tmp2[m] * a[i][i]) % p;
                        while (tmp2[m] < 0) tmp2[m] += p;
                    }
                    a[j] = new List<int>(tmp2);
                }
            }
            int res = a[t - 1][t] * (int)Reverse_Pow(a[t - 1][t - 1], p);
            while (res < 0) res += p;
            while (res > p) res -= p;
            return res;
        }

        //Использование многочлена Лагранжа
        //p - модуль
        public static int Lagrange(Dictionary<int, int> subkeyes, int p)
        {
            int res = 0;
            foreach (KeyValuePair<int, int> pair in subkeyes)
            {
                int up = 1, down = 1;
                foreach (KeyValuePair<int, int> pair_sec in subkeyes)
                {
                    if (pair.Key == pair_sec.Key) continue;
                    up *= pair_sec.Key;
                    down *= pair_sec.Key - pair.Key;
                }
                res += pair.Value * up * (int)Reverse_Pow(down, p);
            }
            res %= p;
            while (res < 0) res += p;
            return res;
        }

        //Получение коэффициентов ЛРУ
        private static List<int> Get_Coefs(List<int> coefs, int p)
        {
            for (int i = 1; i < coefs.Count; i++) coefs[i] *= -1;

            int rev = (int)Reverse_RAE(coefs[0], p);
            for (int i = 0; i < coefs.Count; i++)
            {
                coefs[i] *= rev;
                while (coefs[i] < 0) coefs[i] += p;
                while (coefs[i] >= p) coefs[i] -= p;
            }

            return coefs;
        }

        //Получение ЛРП
        public static List<int> Get_LRP(List<int> LRP, List<int> coefs, int p, out int per)
        {
            coefs = Get_Coefs(coefs, p);

            for (int i = 0; i < coefs.Count - 1; i++)
            {
                while (LRP[i] < 0) LRP[i] += p;
                while (LRP[i] >= p) LRP[i] -= p;
            }

            bool flag = true;
            while (flag)
            {
                int next = 0;
                for (int i = 1; i < coefs.Count; i++)
                    next += coefs[i] * LRP[LRP.Count - i];

                while (next < 0) next += p;
                while (next >= p) next -= p;
                LRP.Add(next);

                flag = false;
                for (int i = 0; i < coefs.Count - 1; i++)
                {
                    if (LRP[LRP.Count - coefs.Count + 1 + i] != LRP[i])
                    {
                        flag = true;
                        break;
                    }
                }
            }

            per = LRP.Count - coefs.Count + 1;
            return LRP;
        }
    }
}
