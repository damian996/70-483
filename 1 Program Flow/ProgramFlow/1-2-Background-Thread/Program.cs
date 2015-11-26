using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _1_2_Background_Thread
{
    public static class Program
    {
        public static void ThreadMethod()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Thread proc: {0}", i);
                Thread.Sleep(1000); 
            }

        }
        static void Main()
        {
            //application will exit immediately because of t.IsBackground = true;
            //All background threads are immediately terminated when when all foreground threads end.
            //in this case there is no foreground thread so the t thread is terminated immediately
            //Foreground threads can be used to keep the application alive.
            
            Thread t = new Thread(new ThreadStart(ThreadMethod));
            t.IsBackground = true;
            t.Start();            
        }
    }
}
