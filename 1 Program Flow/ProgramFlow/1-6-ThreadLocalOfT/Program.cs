using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _1_6_ThreadLocalOfT
{
    class Program
    {
        //If we want to use local data in a thread and initialize it for each thread we can use ThreadLocal<T>.
        //Every thread will get it's own _field variable
        public static ThreadLocal<int> _field = new ThreadLocal<int>(() => { return Thread.CurrentThread.ManagedThreadId; });

        static void Main(string[] args)
        {
            new Thread(() =>
            {
                for (int i = 0; i < _field.Value; i++)
                {
                    Console.WriteLine("Thread A: {0}", i);
                }
            }
            ).Start();

            new Thread(() =>
            {
                for (int i = 0; i < _field.Value; i++)
                {
                    Console.WriteLine("Thread B: {0}", i);
                }
            }
            ).Start();

            Console.ReadKey();
        }
    }
}
