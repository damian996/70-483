using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _1_7_ThreadPool
{
    class Program
    {
        //Threadpool stores threads that are not in use at the moment but those threads are available to pick up a task at anytime.
        //Normal thread will be destroyed after it finishes its work. Thread from a thread pool will return to the pool and wait for another task.
        //thread pool autoamtically manages the available nubmer of threads. When it is initialized the number of threads is 0 and threads are
        //automatically added to the pool when new requests arrive. Threadpool can kill threads that have not been used for a certain period of time.
        static void Main(string[] args)
        {
            ThreadPool.QueueUserWorkItem((s) =>
            {
                Console.WriteLine("Working on a thread from a thread pool");
            });

            Console.ReadKey();
        }
    }
}
