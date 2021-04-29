using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace SortingMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            int max = 100000; // Number for max payroll
            List<int> payroll = new List<int>();
            List<int> unsortedList = new List<int>();
            List<string> resultMerge = new List<string>();
            List<string> resultBubble = new List<string>();
            List<string> resultDefalt = new List<string>();
            long totalTimeMerge;
            long totalTimeBubble;
            long totalTimeDefalt;
            Stopwatch timer = new Stopwatch();
            Stopwatch totalTimer = new Stopwatch();
            string fileName = "result" + max + ".csv";
            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
            

            Random rnd = new Random();

            for (int i = 0; i < max; i++)
            {
                payroll.Add(rnd.Next(10000, 100000));
            }
            /*for (int i = 0; i < payroll.Count; i++)
            {
                StringBuilder.AppendLine(payroll[i].ToString());
            }
            File.WriteAllText(fileName, StringBuilder.ToString());*/

            // Defalt sorting Method
            resultDefalt.Add("Defalt Sorting " + max);
            totalTimer.Start();
            for (int i = 0; i < 100; i++)
            {
                unsortedList = payroll;
                timer.Start();
                unsortedList.Sort();
                timer.Stop();
                resultDefalt.Add((timer.ElapsedMilliseconds).ToString());
                timer.Reset();
            }
            totalTimer.Stop();
            totalTimeDefalt = totalTimer.ElapsedMilliseconds;
            totalTimer.Reset();

            // Merge sorting Method
            resultMerge.Add("Merge Sorting " + max);
            totalTimer.Start();
            for (int i = 0; i < 100; i++)
            {
                unsortedList = payroll;
                timer.Start();
                mergeSort(unsortedList);
                timer.Stop();
                resultMerge.Add((timer.ElapsedMilliseconds).ToString());
                timer.Reset();
            }
            totalTimer.Stop();
            totalTimeMerge = totalTimer.ElapsedMilliseconds;
            totalTimer.Reset();

            // Bubble sorting Method
            resultBubble.Add("Bubble Sorting " + max);
            totalTimer.Start();
            for (int i = 0; i < 100; i++)
            {
                unsortedList = payroll;
                timer.Start();
                bubbleSort(unsortedList);
                timer.Stop();
                resultBubble.Add((timer.ElapsedMilliseconds).ToString());
                timer.Reset();
            }
            totalTimer.Stop();
            totalTimeBubble = totalTimer.ElapsedMilliseconds;
            totalTimer.Reset();

            using (StreamWriter sw = new StreamWriter(fs))
            {
                for (int i = 0; i < resultMerge.Count; i++)
                {
                    if (i == 0)
                    {
                        string temp = "Case";
                        sw.WriteLine("{0},{1},{2},{3}", temp, resultDefalt[i], resultBubble[i], resultMerge[i]);
                    }
                    else
                    {
                        sw.WriteLine("{0},{1},{2},{3}", i, resultDefalt[i], resultBubble[i], resultMerge[i]);
                    }
                }
            }
            fs.Close();
        }

        // Bubble Sorting
        private static List<int> bubbleSort(List<int> list)
        {
            int temp = 0;
            
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list.Count - i - 1; j++)
                {
                    if (list[j] > list[j + 1])
                    {
                        temp = list[j + 1];
                        list[j + 1] = list[j];
                        list[j] = temp;
                    }
                }
            }
            return list;
        }

        // Mergge Sorting
        private static List<int> mergeSort(List<int> list)
        {
            List<int> left = new List<int>();
            List<int> right = new List<int>();
            if (list.Count <= 1) return list;

            int mid = list.Count / 2;
            
            for (int i = 0; i < mid; i++)
            {
                left.Add(list[i]);
            }
            for (int j = mid; j < list.Count; j++)
            {
                right.Add(list[j]);
            }
            left = mergeSort(left);
            right = mergeSort(right);
            list = merge(left, right);
            return list;
        }

        public static List<int> merge(List<int> left, List<int> right)
        {
            List<int> result = new List<int>();

            while (left.Count > 0 || right.Count > 0)
            {
                if (left.Count > 0 && right.Count > 0)
                {
                    if (left.First() <= right.First())
                    {
                        result.Add(left.First());
                        left.Remove(left.First());
                    }
                    else
                    {
                        result.Add(right.First());
                        right.Remove(right.First());
                    }
                }
                else if (left.Count > 0)
                {
                    result.Add(left.First());
                    left.Remove(left.First());
                }
                else if (right.Count > 0)
                {
                    result.Add(right.First());
                    right.Remove(right.First());
                }
            }
            return result;
        }
    }
}
