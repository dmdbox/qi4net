using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThreadPerformerTest
{
    class Program
    {
        static Int64 result = 0;

        static object lockItem = "lockItem";

        static void Main(string[] args)
        {
            var max = 840000;
            var ilist = new List<int>();
            var counter = 0;
            var random = new Random();
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
         
            Qi.Threads.ThreadQuery.AvgExecute<int>(4, ilist, OnExecuteFunction);
            Console.WriteLine("4 thread result:" + result);
            Console.WriteLine((DateTime.Now - dateTime).TotalMilliseconds);


            Console.Read();


        }

        private static void OnExecuteFunction(IList<int> s)
        {
            Int64 partyResult = s.Aggregate<int, long>(0, (current, item) => current + item);
            lock (lockItem)
            {
                result += partyResult;
            }
        }
    }
}
