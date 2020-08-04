using System;
using System.Collections.Generic;
using System.Linq;

namespace CryptoCalc
{
    static class BoolFuncs
    {
        public static string GenerateVector(int n)
        {
            string vect = "";
            Random r = new Random();
            for (int i = 0; i < Math.Pow(2.0, n); i++)
            {
                vect += (r.Next(0, 100) % 2).ToString();
            }
            return vect;
        }

        public static void GetCharacteristics(string vector, out string anf, out int deg, out int weight, 
            out List<int> fourier, out List<int> ua, out double nonLin)
        {
            if (!CheckVector(vector)) throw new Exception("Веткор значений задан неверно");

            List<int> vect = new List<int>();
            foreach (char c in vector) vect.Add(int.Parse(c.ToString()));

            int[,] truth_table = GetTruthTable(vect);

            List<string> vars = new List<string>();
            for (int i = 0; i < (int)Math.Log(vect.Count, 2.0); i++)
                vars.Add("x" + (i + 1).ToString());

            anf = Zhegalkin(vars, truth_table, out List<int> degs);
            deg = degs.Max();
            weight = vector.Count(x => x == '1');
            fourier = Fourier(truth_table);
            ua = UA(truth_table);
            nonLin = GetNonLin(truth_table, (int)Math.Log(vect.Count, 2.0));
        }

        public static List<int> GetFuncWithChars(int k, int nonLin)
        {
            Random r = new Random();
            List<int> bent = Bent_Generate(k);
            int n = 2 * k;
            while (true)
            {
                List<int> bent_tmp = new List<int>(bent);
                int wt = bent_tmp.FindAll(i => i == 1).Count;
                List<int> t0 = new List<int>(), t1 = new List<int>();
                if (wt == (int)Math.Pow(2.0, n - 1)) break;

                for (int i = 0; i < bent_tmp.Count; i++)
                {
                    if (bent_tmp[i] == 1) t1.Add(i);
                    else t0.Add(i);
                }

                if (wt < Math.Pow(2.0, n - 1))
                {
                    int rand = r.Next() % (t0.Count);
                    bent_tmp[t0[rand]] = 1;
                    t1.Add(t0[rand]);
                    t0.RemoveAt(rand);
                }
                else if (wt > Math.Pow(2.0, n - 1))
                {
                    int rand = r.Next() % (t1.Count);
                    bent_tmp[t1[rand]] = 0;
                    t0.Add(t1[rand]);
                    t1.RemoveAt(rand);
                }

                if (GetNonLin(GetTruthTable(bent), n) > nonLin) bent = bent_tmp;
            }

            return bent;
        }

        private static double GetNonLin(int[,] truth_table, int n)
        {
            List<int> u_a = UA(truth_table);
            int max = Math.Abs(u_a[0]);
            for (int i = 1; i < u_a.Count; i++)
                if (Math.Abs(u_a[i]) > max) max = Math.Abs(u_a[i]);

            return Math.Pow(2.0, n - 1) - (max / 2);
        }

        private static List<int> Bent_Generate(int k)
        {
            List<int> vect_g = new List<int>();
            Random r = new Random();
            for (int i = 0; i < Math.Pow(2.0, k); i++)
                vect_g.Add(r.Next(0, 100) % 2);
            List<int> scalar = Scalar(GetEmptyTruthTable(k), GetEmptyTruthTable(k));

            List<int> bent = new List<int>();
            for (int i = 0; i < Math.Pow(2.0, k); i++)
                for (int j = 0; j < Math.Pow(2.0, k); j++)
                    bent.Add((vect_g[i] + scalar[i + j]) % 2);

            return bent;
        }

        private static List<int> Scalar(int[,] t1, int[,] t2)
        {
            List<int> res = new List<int>();
            for (int i = 0; i < t1.GetLength(0); i++)
            {
                for (int j = 0; j < t2.GetLength(0); j++)
                {
                    int scalar = 0;
                    for (int k = 0; k < t1.GetLength(1); k++)
                        scalar += t1[i, k] * t2[j, k];
                    res.Add(scalar % 2);
                }
            }
            return res;
        }

        private static bool CheckVector(string vector)
        {
            int n = (int)Math.Log(vector.Length, 2.0);
            return vector.Length == Math.Pow(2.0, n);
        }

        private static int[,] GetTruthTable(List<int> vector)
        {
            int n = (int)Math.Log(vector.Count, 2.0); //Количество переменных
            int[,] truth_table = new int[vector.Count, n + 1];

            for (int i = 0; i < vector.Count; i++)
            {
                //Подставляем наборы переменных
                string s = Convert.ToString(i, 2);
                while (s.Length != n) s = "0" + s;
                for (int j = 0; j < n; j++)
                    truth_table[i, j] = int.Parse(s[j].ToString());
                truth_table[i, n] = vector[i];
            }

            return truth_table;
        }

        private static int[,] GetEmptyTruthTable(int k)
        {
            int[,] truth_table = new int[(int)Math.Pow(2.0, k), k];
            for (int i = 0; i < (int)Math.Pow(2.0, k); i++)
            {
                //Подставляем наборы переменных
                string s = Convert.ToString(i, 2);
                while (s.Length != k) s = "0" + s;
                for (int j = 0; j < k; j++)
                    truth_table[i, j] = int.Parse(s[j].ToString());
            }
            return truth_table;
        }

        private static List<List<int>> GetUiCoefs(int[,] truth_table, int n, bool isFourier)
        {
            List<List<int>> u_i = new List<List<int>>();
            for (int i = 0; i < Math.Pow(2.0, n); i++)
            {
                if (truth_table[i, n] == 0 && isFourier) continue;

                List<int> t = new List<int>();
                for (int j = 0; j < n; j++)
                    if (truth_table[i, j] == 1) t.Add(j + 1);
                if (t.Count == 0) t.Add(0);
                u_i.Add(t);
            }
            return u_i;
        }

        private static List<int> Fourier(int[,] truth_table)
        {
            int n = truth_table.GetLength(1) - 1; //Число переменных
            List<List<int>> u_i = GetUiCoefs(truth_table, n, true);

            List<int> fourier = new List<int>();
            for (int i = 0; i < Math.Pow(2.0, n); i++)
            {
                int wf_Fur = 0;
                foreach (List<int> k in u_i)
                {
                    int u = 0;
                    foreach (int j in k)
                    {
                        if (j == 0) continue;
                        u += truth_table[i, j - 1];
                    }
                    wf_Fur += (int)Math.Pow(-1.0, u % 2);
                }
                fourier.Add(wf_Fur);
            }

            return fourier;
        }

        private static List<int> UA(int[,] truth_table)
        {
            int n = truth_table.GetLength(1) - 1; //Число переменных
            List<List<int>> u_i = GetUiCoefs(truth_table, n, false);

            List<int> ua = new List<int>();
            for (int i = 0; i < Math.Pow(2.0, n); i++)
            {
                int wf_UA = 0;
                for (int l = 0; l < u_i.Count; l++)
                {
                    int u = 0;
                    foreach (int j in u_i[l])
                    {
                        if (j == 0) continue;
                        u += truth_table[i, j - 1];
                    }
                    if (truth_table[l, n] == 1) u += 1;
                    wf_UA += (int)Math.Pow(-1.0, u % 2);
                }
                ua.Add(wf_UA);
            }

            return ua;
        }

        //Получение полинома Жегалкина методом треугольника Паскаля (для АНФ)
        private static string Zhegalkin(List<string> vars, int[,] truth_table, out List<int> degs)
        {
            string result = "";
            int rows = truth_table.GetLength(0); //Количество строк ТИ
            int[] tmp = new int[rows]; //Треугольник Паскаля
            for (int i = 0; i < rows; i++)
                tmp[i] = truth_table[i, vars.Count];
            int[] mn = new int[rows]; //Указывает, какие наборы брать

            for (int i = 0; i < rows; i++) //Формирование т. Паскаля
            {
                mn[i] = tmp[0];
                for (int j = 0; j < rows - 1; j++)
                    tmp[j] = (tmp[j] + tmp[j + 1]) % 2;
            }

            degs = new List<int>();
            //Формирование полинома Жегалкина
            for (int i = 0; i < rows; i++)
            {
                if (mn[i] != 1) continue; //Пропускаем ненужные наборы
                int deg = 0;
                string part = "";
                for (int j = 0; j < vars.Count; j++)
                {
                    if (truth_table[i, j] == 0) continue;
                    else
                    {
                        if (part.Length == 0) part += vars[j];
                        else part += "&" + vars[j];
                        deg++;
                    }
                }
                if (part.Length == 0)
                {
                    part = "1";
                    deg = 1;
                }
                degs.Add(deg);
                if (result.Length == 0) result += part;
                else result += " ⊕ " + part;
            }
            return result;
        }
    }
}
