using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace DTW
{
    class Program
    {
        static void Main(string[] args)
        {
            HashSet<string> set = new HashSet<string>();
            set.Add("Cau");
            set.Add("ahoj");
            set.Add("dobry den");

            string result = FindMostSimilar("ajoj", set);
            Console.WriteLine(result);
            Console.ReadKey();
        }

        /// <summary>
        /// Method find the most similar word from vocabulary for input word using Levenshtein distance.
        /// </summary>
        /// <param name="word">input word</param>
        /// <param name="voc">vocabulary of words</param>
        /// <returns></returns>
        public static string FindMostSimilar(string word, HashSet<string> voc)
        {
            string result = "";
            double minDist = double.MaxValue;

            foreach (string refWord in voc)
            {
                double[,] A = new double[refWord.Length + 1, word.Length + 1];

                A[0, 0] = 0;
                for (int i = 1; i < refWord.Length+1; i++)
                {
                    A[i, 0] = double.MaxValue;
                }
                int j;
                for (j = 1; j < word.Length+1; j++)
                {
                    A[0, j] = double.MaxValue;
                }

                int minI = 1;
                j = 1;
                while (j < word.Length)
                {
                    bool nahoru = false;
                    int i = minI;
                    while (i < refWord.Length )
                    {
                        double newWord = (i == 0 && j == 0) ? 0 : double.MaxValue;
                        double over = 1*A[i - 1, j];
                        double right = 1*A[i, j - 1];
                        double rightOver = 1*A[i - 1, j - 1];

                        //smer posunu ve slovo
                        newWord = right;

                        //smer posunu v referenci
                        if (newWord > over) 
                        { 
                            newWord = over; 
                            if (i == minI) 
                                nahoru = true; 
                        }

                        //smer posunu v obou
                        if (newWord > rightOver) 
                            newWord = rightOver; 

                        A[i, j] = (refWord[i-1] == word[j-1] ? 0 : 1) + newWord;
                        i++;
                    }
                    if (nahoru) { minI++; }
                    j++;
                }
                if (A[refWord.Length - 1, word.Length - 1] < minDist)
                {
                    minDist = A[refWord.Length - 1, word.Length - 1];
                    result = refWord;
                }
            }
            return result;
        }
    }
}
