using System;
using System.Diagnostics;
using System.Text;
using static System.Console;

namespace DupeCheck
{
    class Program
    {
        static void Main()
        {
            var watch = new Stopwatch();

            watch.Start();

            for (var i = 0; i < 1000; i++)
            {
                var one = RandomString(50, false);
                var two = RandomString(50, false);

                var when = Distance(one, two);
                var what = NewDistance(one, two);

                if (what != when)
                {
                    WriteLine($"{ one } and { two } did not return same result.");
                }
            }

            watch.Stop();

            WriteLine(watch.Elapsed);
            WriteLine("Done");

            ReadKey();
        }

        static string RandomString(int size, bool lowerCase)
        {
            var builder = new StringBuilder();

            var random = new Random();

            for (var i = 0; i < size; i++)
            {
                var ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));

                builder.Append(ch);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }


        static int Distance(string s, string t)
        {
            var n = s.Length;
            var m = t.Length;
            var d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
                return m;

            if (m == 0)
                return n;

            // Step 2
            for (var i = 0; i <= n; d[i, 0] = i++) { }
            for (var j = 0; j <= m; d[0, j] = j++) { }

            // Step 3
            for (var i = 1; i <= n; i++)
            {
                //Step 4
                for (var j = 1; j <= m; j++)
                {
                    // Step 5
                    var cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m];
        }

        public static int NewDistance(string s1, string s2)
        {
            if (s2.Length == 0)
            {
                return s1.Length;
            }

            int[] costs = new int[s2.Length];

            for (int i = 0; i < costs.Length;)
            {
                costs[i] = ++i;
            }

            for (int i = 0; i < s1.Length; i++)
            {
                int insertionCost1 = i;

                int insertionCost2 = i;

                char c = s1[i];

                for (int j = 0; j < s2.Length; j++)
                {
                    int insertionCost = insertionCost1;

                    insertionCost1 = insertionCost2;

                    insertionCost2 = costs[j];

                    if (c != s2[j])
                    {
                        if (insertionCost < insertionCost1)
                        {
                            insertionCost1 = insertionCost;
                        }

                        if (insertionCost2 < insertionCost1)
                        {
                            insertionCost1 = insertionCost2;
                        }

                        ++insertionCost1;
                    }

                    costs[j] = insertionCost1;
                }
            }

            return costs[costs.Length - 1];
        }
    }
}
