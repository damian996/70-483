using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _1_4_Stopping_Thread
{
    class Program
    {
        static void Main(string[] args)
        {
            //To abort thread we can use Thread.Abort() method. However it is executed by other thread
            //and may leave the stopped thread in a corrupted state. It is better to use sharred variable
            //that both thread can access and modify.
            bool stopped = false;
            Thread t = new Thread(new ThreadStart(() =>
            {
                while (!stopped)
                {
                    Console.WriteLine("Running...");
                    Thread.Sleep(1000);
                }
            }));

            t.Start();
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            //thread runs until stopped becomes true.
            stopped = true;
            //t.Join() makes the main thread waiting until second thread finishes running.
            t.Join();
        }
    }
}
