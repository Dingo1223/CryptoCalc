using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace CryptoCalc
{
    static class Transpos
    {
        private static readonly List<double> FreqEng = new List<double>() { 8.167, 1.492, 2.782, 4.253, 12.702,
            2.228, 2.015, 6.094, 6.966, 0.153, 0.772, 4.025, 2.406, 6.749, 7.507, 1.929,
            0.095, 5.987, 6.327, 9.056, 2.758, 0.978, 2.360, 0.150, 1.974, 0.074 };

        private static readonly List<double> FreqRus = new List<double>() { 8.01, 1.59, 4.54, 1.70, 2.98, 8.45, 0.04,
            0.94, 1.65, 7.35, 1.21, 3.49, 4.40, 3.21, 6.70, 10.97, 2.81, 4.73, 5.47, 6.26,
            2.62, 0.26, 0.97, 0.48, 1.44, 0.73, 0.36, 1.74, 1.90, 0.04, 0.32, 0.64, 2.01 };

        private static List<string> Old_letters = new List<string>();
        private static List<string> New_letters = new List<string>();

        public static string GenerateKey(bool isEng)
        {
            Random r = new Random();
            string alphabet = isEng ? "abcdefghijklmnopqrstuvwxyz" : "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            List<int> used = new List<int>();
            string new_key = "";

            for (int i = 0; i < alphabet.Length; i++)
            {
                int j = r.Next(0, alphabet.Length);
                while (used.Contains(j))
                {
                    j++;
                    if (j == alphabet.Length)
                    {
                        j = 0;
                        continue;
                    }
                }
                used.Add(j);
                new_key += alphabet[j];
            }

            return new_key;
        }

        private static bool CheckKey(string key, bool isEng)
        {
            string alphabet = isEng ? "abcdefghijklmnopqrstuvwxyz" : "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            if (key.Length != alphabet.Length) return false;
            foreach (char c in alphabet)
            {
                if (!key.Contains(c)) return false;
            }
            return true;
        }

        public static string TransposCipher(string text, string key, bool isEnc, bool isEng)
        {
            if (!CheckKey(key, isEng))
            {
                MessageBox.Show("Неверный ключ");
                return text;
            }

            string alphabet = isEng ? "abcdefghijklmnopqrstuvwxyz" : "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            Regex regex = new Regex(@"[^0-9a-zA-Zа-яёА-ЯЁ\n]");
            text = regex.Replace(text, string.Empty).ToLower();
            string s = "";

            if (isEnc)
            {
                foreach (char letter in text)
                {
                    int ind = alphabet.IndexOf(letter);
                    if (ind == -1) s += letter;
                    else s += key[ind];
                }
            }
            else
            {
                foreach (char letter in text)
                {
                    int ind = key.IndexOf(letter);
                    if (ind == -1) s += letter;
                    else s += alphabet[ind];
                }
            }

            return s;
        }

        public static Dictionary<char, double> GetFreqDct(bool isEng)
        {
            string alphabet = isEng ? "abcdefghijklmnopqrstuvwxyz" : "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            List<double> freq = isEng ? FreqEng : FreqRus;

            var dct = new Dictionary<char, double>();
            for (int i = 0; i < freq.Count; i++)
                dct.Add(alphabet[i], freq[i]);
            dct = dct.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
            return dct;
        }

        public static Dictionary<char, double> GetTextFreq(ref string text, bool isEng)
        {
            string alphabet = isEng ? "abcdefghijklmnopqrstuvwxyz" : "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            string tmp_text = text;
            text = new Regex(@"[^0-9a-zA-Zа-яёА-ЯЁ\n ]").Replace(text, string.Empty).ToLower();

            tmp_text = isEng ? new Regex(@"[^a-z]").Replace(tmp_text, string.Empty) : 
                new Regex(@"[^а-яё]").Replace(tmp_text, string.Empty);

            Dictionary<char, double> freqText = new Dictionary<char, double>();
            foreach (char letter in alphabet)
                freqText.Add(letter, (double)new Regex(letter.ToString()).Matches(text).Count / (double)text.Length * 100.0);

            return freqText.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        public static string SwitchLetters(string text, string from, string to, bool isEng, bool isCancel, out bool success)
        {
            string alphabet = isEng ? "abcdefghijklmnopqrstuvwxyz" : "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            success = false;
            if (!CheckSymbol(from.ToLower(), alphabet) || !CheckSymbol(to.ToLower(), alphabet))
            {
                MessageBox.Show("Не выбраны символы");
                return text;
            }

            if ((Old_letters.Contains(from) || New_letters.Contains(to)) && !isCancel)
            {
                MessageBox.Show("Символы уже использовались");
                return text;
            }

            if (isCancel)
            {
                Old_letters.Remove(from);
                New_letters.Remove(to);
                text = text.Replace(to.ToUpper(), from);
            }
            else
            {
                Old_letters.Add(from);
                New_letters.Add(to);
                text = text.Replace(from, to.ToUpper());
            }

            success = true;
            return text;
        }

        private static bool CheckSymbol(string s, string alphabet)
        {
            if (s.Length == 1 && alphabet.Contains(s)) return true;
            else return false;
        }
    }
}
