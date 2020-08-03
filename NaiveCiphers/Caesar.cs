using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace CryptoCalc
{
    static class Caesar
    {
        public static string CaesarCipher(string text, bool isEncrypt, int key, bool isEng)
        {
            string alphabet = isEng ? "abcdefghijklmnopqrstuvwxyz" : "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            key %= alphabet.Length;
            Regex regex = new Regex(@"[^0-9a-zA-Zа-яёА-ЯЁ\n ]");
            text = regex.Replace(text, string.Empty).ToLower();

            string s = "";
            foreach (char letter in text)
            {
                int ind = alphabet.IndexOf(Char.ToLower(letter));
                if (ind == -1)
                {
                    s += letter;
                    continue;
                }
                if (isEncrypt) s += alphabet[(ind + key) % alphabet.Length];
                else if (ind >= key) s += alphabet[ind - key];
                else s += alphabet[alphabet.Length + ind - key];
            }
            return s;
        }

        public static List<RadioButton> GetVariants(string text, bool isEng, TextBox tbForResult)
        {
            List<RadioButton> rbs = new List<RadioButton>();
            string alphabet = isEng ? "abcdefghijklmnopqrstuvwxyz" : "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            Regex regex = new Regex(@"[^0-9a-zA-Zа-яёА-ЯЁ\n ]");
            text = regex.Replace(text, string.Empty).ToLower();

            for (int i = 0; i < alphabet.Length; i++)
            {
                RadioButton rb = new RadioButton()
                {
                    Tag = i,
                    GroupName = "variants",
                    Content = i.ToString() + " " +  
                        CaesarCipher(text.Substring(0, text.Length > 80 ? 80 : text.Length), false, i, isEng)
                };
                int j = i;
                rb.Checked += delegate 
                {
                    tbForResult.Text = CaesarCipher(text, false, j, isEng);
                };

                rbs.Add(rb);
            }

            return rbs;
        }
    }
}
