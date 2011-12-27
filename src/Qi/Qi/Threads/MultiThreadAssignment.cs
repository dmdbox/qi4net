using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Qi.Threads
{
    public delegate void ExecutionHandler<T>(IList<T> data);


    public sealed class MultiThreadAssignment<T>
    {
        private readonly ExecutionHandler<T> _executeHandler;
        private readonly int _threadCount;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="threadCount">启用多少个Thread去执行,最大9999,最少1</param>
        /// <param name="threadState"></param>
        /// <exception cref="ArgumentOutOfRangeException">当<see cref="threadCount"/>不在1～9999范围之内</exception>
        public MultiThreadAssignment(int threadCount, ExecutionHandler<T> threadState)
        {
            if (threadCount < 0 || threadCount > 9999)
                throw new ArgumentOutOfRangeException("threadCount", "Should be between 1~9999");
            if (threadState == null)
                throw new ArgumentNullException("threadState");
            _threadCount = threadCount;
            _executeHandler = threadState;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="datas"></param>
        public void Execute(IList<T> datas)
        {
            if (datas == null)
                throw new ArgumentNullException("datas");
            if (datas.Count == 0)
                return;
            var globalCheck = new ManualResetEvent(false);
            Thread[] threads;
            foreach (var assignedData in AssignThread(datas, out threads))
            {
                ActualExecute(assignedData, globalCheck);
            }

            while (true)
            {
                globalCheck.WaitOne();
                if (AllDone(threads))
                {
                    break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="eachCallback"></param>
        public void ExceuteEachThread(IList<T> datas, Func<int> eachCallback)
        {
            if (datas == null)
                throw new ArgumentNullException("datas");
            if (eachCallback == null) throw new ArgumentNullException("eachCallback");
            if (datas.Count == 0)
                return;
            var globalCheck = new ManualResetEvent(false);
            Thread[] threads;
            foreach (var assignedData in AssignThread(datas, out threads))
            {
                ActualExecute(assignedData, globalCheck);
            }

            while (true)
            {
                globalCheck.WaitOne();
                eachCallback();
                if (AllDone(threads))
                {
                    break;
                }
            }
        }


        public void AsyncExecute(IList<T> datas, Func<int> callback)
        {
            if (datas == null)
                throw new ArgumentNullException("datas");
            if (callback == null) throw new ArgumentNullException("callback");
            if (datas.Count == 0)
                return;
            var globalCheck = new ManualResetEvent(false);
            Thread[] threads;
            foreach (var assignedData in AssignThread(datas, out threads))
            {
                ActualExecute(assignedData, globalCheck);
            }

            while (true)
            {
                globalCheck.WaitOne();
                if (AllDone(threads))
                {
                    callback();
                    break;
                }
            }
        }


        private IEnumerable<IList<T>> AssignThread(IList<T> datas, out Thread[] threads)
        {
            DateTime dateTime = DateTime.Now;
            T[][] result = datas.ToArray().DivEqual(_threadCount);
            threads = new Thread[result.Length];
            for (int i = 0; i < result.Length; i++)
            {
                threads[i] = new Thread(s => _executeHandler((IList<T>) s)) {IsBackground = true};
            }
            Console.WriteLine("assignment time:" + (DateTime.Now - dateTime).TotalMilliseconds);
            return result;
        }


        private void ActualExecute(IList<T> data, ManualResetEvent globalCheckerEvent)
        {
            _executeHandler(data);
            globalCheckerEvent.Set();
        }

        private static bool AllDone(IEnumerable<Thread> threads)
        {
            bool allfinish = false;
            foreach (Thread t in threads)
            {
                if (t.IsAlive)
                {
                    break;
                }
                allfinish = true;
            }
            return allfinish;
        }
    }
}