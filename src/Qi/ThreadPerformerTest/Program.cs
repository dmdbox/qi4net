using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Qi.Threads;

namespace ThreadPerformerTest
{
    internal class Program
    {
        private static Int64 result;

        private static void Main(string[] args)
        {
            int max = 840001;
            var ilist = new List<int>();
            int counter = 0;
            var random = new Random();
            IList<int> a=new List<int>();
            
            while (counter < max)
            {
                ilist.Add(random.Next(max));
                counter++;
            }

            DateTime dateTime = DateTime.Now;

            for (int i = 0; i < ilist.Count; i++)
            {
                result += ilist[i];
            }
            Console.WriteLine("Single thread result:" + result);
            Console.WriteLine((DateTime.Now - dateTime).TotalMilliseconds);

            dateTime = DateTime.Now;
            result = 0;

            ThreadQuery.AvgExecute(4, ilist, OnExecuteFunction);
            Console.WriteLine("4 thread result:" + result);
            Console.WriteLine((DateTime.Now - dateTime).TotalMilliseconds);


            Console.Read();
        }

        private static void OnExecuteFunction(IList<int> s)
        {
            Int64 partyResult = s.Aggregate<int, long>(0, (current, item) => current + item);
            result += partyResult;
        }
    }
}