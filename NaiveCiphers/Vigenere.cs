using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;

namespace CryptoCalc
{
    static class Vigenere
    {
        public static string DefaultTextForDec { get; set; }
        public static int KeyLength { get; set; }

        public static Dictionary<char, double> GetFreqDct(bool isEng)
        {
            string alphabet = isEng ? "abcdefghijklmnopqrstuvwxyz" : "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            List<double> freq = isEng ? Freqs.FreqEng : Freqs.FreqRus;

            var dct = new Dictionary<char, double>();
            for (int i = 0; i < freq.Count; i++)
                dct.Add(alphabet[i], freq[i]);
            return dct;
        }

        //Вычисление возможных длин ключа
        public static List<int> GetPossibleLengths(ref string text, bool isEng)
        {
            text = new Regex(@"[^0-9a-zA-Zа-яёА-ЯЁ\n \.,\?!]").Replace(text, string.Empty).ToLower();
            string tmp_text = text;
            int Max = 0;
            string trigram = "";

            tmp_text = isEng ? new Regex(@"[^a-z]").Replace(tmp_text, string.Empty) : 
                new Regex(@"[^а-яё]").Replace(tmp_text, string.Empty);

            Regex alphabetRegex = isEng ? new Regex("[a-z]{3}") : new Regex("[а-яё]{3}");

            Regex ngram;
            for (int i = 0; i < tmp_text.Length - 2; i++)
            {
                ngram = new Regex(tmp_text.Substring(i, 3));
                if (ngram.Matches(tmp_text).Count > Max)
                {
                    Max = ngram.Matches(tmp_text).Count;
                    trigram = tmp_text.Substring(i, 3);
                }
            }

            List<int> Lengths = new List<int>();
            MatchCollection m = new Regex(trigram).Matches(tmp_text);
            for (int i = 1; i < m.Count; i++)
                Lengths.Add(m[i].Index - m[i - 1].Index);

            List<int> nods = new List<int>();
            for (int i = 0; i < Lengths.Count; i++)
            {
                for (int j = i + 1; j < Lengths.Count; j++)
                {
                    int nod = (int)Algorithms.Euclid(Lengths[i], Lengths[j]);
                    if (!nods.Contains(nod) && nod > 1) nods.Add(nod);
                }
            }

            return nods;
        }

        //Шифрование / Дешифрование
        public static string Encrypt(string text, string key, bool isEnc, bool isEng)
        {
            key = CheckKey(key, isEng);
            if (key == null)
            {
                MessageBox.Show("Неверный ключ");
                return text;
            }

            string alphabet = isEng ? "abcdefghijklmnopqrstuvwxyz" : "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            text = new Regex(@"[^0-9a-zA-Zа-яёА-ЯЁ\n]").Replace(text, string.Empty).ToLower();
            int count = 0;
            string s = "";

            foreach (char letter in text)
            {
                if (count == key.Length) count = 0;
                int ind = alphabet.IndexOf(letter);
                if (ind == -1)
                {
                    s += letter;
                    continue;
                }
                int shift = alphabet.IndexOf(key[count]);

                if (isEnc) s += alphabet[(ind + shift) % alphabet.Length];
                else if (ind >= shift) s += alphabet[ind - shift];
                else s += alphabet[ind + alphabet.Length - shift];
                count++;
            }
            return s;
        }

        private static string CheckKey(string key, bool isEng)
        {
            Regex regex = isEng ? new Regex(@"[^a-z]") : new Regex(@"[^а-яё]");
            key = regex.Replace(key.ToLower(), string.Empty);
            return key.Length != 0 ? key : null;
        }

        public static Dictionary<char, double> GetFreqText(bool isEng, int shift)
        {
            string alphabet = isEng ? "abcdefghijklmnopqrstuvwxyz" : "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            Dictionary<char, double> freq = new Dictionary<char, double>();
            string tmp_text = isEng ? new Regex(@"[^a-z]").Replace(DefaultTextForDec, string.Empty) :
                new Regex(@"[^а-яё]").Replace(DefaultTextForDec, string.Empty);

            string text_pos = "";
            for (int i = shift; i < tmp_text.Length; i += KeyLength)
                text_pos += tmp_text[i];

            foreach (char letter in alphabet)
                freq.Add(letter, (double)new Regex(letter.ToString()).Matches(text_pos).Count / (double)text_pos.Length * 100.0);

            return freq;
        }

        public static string ChangeLetters(string text, int shift, int position, bool isEng, out char keyChar)
        {
            string alphabet = isEng ? "abcdefghijklmnopqrstuvwxyz" : "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            keyChar = alphabet[shift];
            int count = 0;
            char[] result = text.ToCharArray();
            for (int i = position; i < result.Length; i++)
            {
                while (!alphabet.Contains(result[i].ToString()))
                {
                    i++;
                    if (i >= result.Length) break;
                    count++;
                }
                if (i >= result.Length) break;
                if ((i - count) % KeyLength != position) continue;
                int ind = alphabet.IndexOf(Char.ToLower(text[i]));

                if (ind >= shift) result[i] = alphabet[(ind - shift) % alphabet.Length];
                else result[i] = alphabet[(alphabet.Length + ind - shift) % alphabet.Length];
            }

            return new string(result);
        }
    }
}
