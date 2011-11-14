using System;
using System.Collections.Generic;
using System.Threading;

namespace Qi.Threads
{
    public delegate void ExecutionHandler(object[] data);

    public sealed class MultiThreadAssignment
    {
        private readonly ExecutionHandler _executeHandler;
        private readonly int _threadCount;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="threadCount">启用多少个Thread去执行,最大9999,最少1</param>
        /// <param name="threadState"></param>
        /// <exception cref="ArgumentOutOfRangeException">当<see cref="threadCount"/>不在1～9999范围之内</exception>
        public MultiThreadAssignment(int threadCount, ExecutionHandler threadState)
        {
            if (threadCount < 0 || threadCount > 9999)
                throw new ArgumentOutOfRangeException("threadCount", "Should be between 1~20");
            if (threadState == null)
                throw new ArgumentNullException("threadState");
            _threadCount = threadCount;
            _executeHandler = threadState;
        }
        
        public void Execute(object[] datas)
        {
            if (datas.Length == 0)
                return;
            int threadCount = _threadCount < datas.Length ? _threadCount : datas.Length;
            var threads = new Thread[threadCount];
            var executeData = new List<List<object>>();
            int i = 0;
            foreach (object obj in datas)
            {
                //平均分配到每一个Thread和data
                if (i >= _threadCount)
                    i = 0;
                if (threads[i] == null)
                {
                    threads[i] = new Thread(state => _executeHandler((object[])state));
                }
                if (executeData.Count <= i)
                {
                    executeData.Add(new List<object>());
                }
                executeData[i].Add(obj);
                i++;
            }

            for (int j = 0; j < threads.Length; j++)
            {
                threads[j].Start(executeData[j].ToArray());
            }
         
            while (true)
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
                if (allfinish)
                {
                    break;
                }
                Thread.Sleep(500);
            }
        }
    }
}