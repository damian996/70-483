using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParameterizedThreadStart1
{
    class Program
    {
        public static void ThreadMethod(object o)
        {
            for (int i = 0; i < (int)o; i++)
            {
                Console.WriteLine("Thread proc: {0}", i);
                Thread.Sleep(1000);
            }

        }
        static void Main(string[] args)
        {
            //ParameterizedThreadStart  paramter allows to pass a parameter to the thread method. 
            //Parameter is passed whent the t.Start() method is called.
            Thread t = new Thread(new ParameterizedThreadStart(ThreadMethod));
            t.Start(5);
            t.Join();           
        }
    }
}
