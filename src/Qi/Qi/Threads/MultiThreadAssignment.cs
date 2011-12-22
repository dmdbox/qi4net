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
            IAsyncResult i = AsyncExecute(datas, null);
            WaitHandle t = i.AsyncWaitHandle;
            t.WaitOne();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public IAsyncResult AsyncExecute(IList<T> datas, AsyncCallback callback)
        {
            var waitCallBack = new WaitCallback(Waitting);
            if (datas.Count == 0)
            {
                return waitCallBack.BeginInvoke(new Thread[0], callback, null);
            }

            List<T>[] assignedData = AssignThread(datas);
            var threads = new Thread[assignedData.Length];
            int i = 0;
            foreach (var a in assignedData)
            {
                threads[i] = new Thread(s => _executeHandler((IList<T>)s)) { IsBackground = true };
                threads[i].Start(a);
                i++;
            }
            return waitCallBack.BeginInvoke(threads, callback, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        private static void Waitting(object state)
        {
            var threads = (Thread[])state;
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
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        private List<T>[] AssignThread(IList<T> datas)
        {
            DateTime date = DateTime.Now;
            //int threadCount = _threadCount < datas.Count ? _threadCount : datas.Count;
            var assignedDataSet = new List<T>[
                (datas.Count < _threadCount ? (datas.Count) : _threadCount)];
            for (int i = 0; i < assignedDataSet.Length; i++)
            {
                assignedDataSet[i] = new List<T>();
            }

            Assignment(assignedDataSet, datas.ToArray());
            Console.WriteLine((DateTime.Now - date).TotalMilliseconds);
            return assignedDataSet;
        }

        private void Assignment(IList<List<T>> assignedDataSet, T[] datas)
        {
            if (datas.Length < _threadCount)
            {
                //当data的数目少于_threadCount，那么就用foreach平均分配;
                int i = 0;
                foreach (T obj in datas)
                {
                    //平均分配到每一个Thread和data
                    if (i >= _threadCount)
                        i = 0;
                    assignedDataSet[i].Add(obj);
                    i++;
                }
            }
            else
            {
                long remainder;
                long d = Math.DivRem(datas.LongLength, Convert.ToInt64(_threadCount), out remainder);
                for (int i = 0; i < _threadCount; i++)
                {
                    var ary = new T[d];
                    Array.Copy(datas, d * i, ary, 0, ary.Length);
                    assignedDataSet[i].AddRange(ary);
                }
                if (remainder != 0)
                {
                    var ary = new T[remainder];
                    Array.Copy(datas, d * _threadCount, ary, 0, ary.Length);
                    Assignment(assignedDataSet, ary);
                }
            }
        }
    }
}