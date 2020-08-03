using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace CryptoCalc
{
    static class Transpos
    {
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
            List<double> freq = isEng ? Freqs.FreqEng : Freqs.FreqRus;

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
