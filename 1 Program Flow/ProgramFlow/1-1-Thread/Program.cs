using System;
using System.Threading;

namespace _1_1_Thread
{
    class Program
    {
        public static void ThreadMethod()
        {
            for(int i=0; i<10; i++)
            {
                Console.WriteLine("Thread proc: {0}", i);
                Thread.Sleep(0); //tell main thread that this thread has finished this iteration and main thread can continue.
            }

        }
        static void Main(string[] args)
        {
            Thread t = new Thread(new ThreadStart(ThreadMethod));
            t.Start();

            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("Main thread: {0}", i);
                Thread.Sleep(0); //tell the t thread that this thread has finished this iteration and t thread can continue.
            }

            t.Join(); //let the main thread wait until the t thread finishes

            Console.ReadKey();
        }
    }
}
