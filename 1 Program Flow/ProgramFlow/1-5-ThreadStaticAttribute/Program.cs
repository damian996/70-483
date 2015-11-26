using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _1_5_ThreadStaticAttribute
{
    class Program
    {
        //The ThreadStatic atttribute causes that every thread gets it's own copy of the _field varaible and threads don't share this variable.
        //When the attribute is removed, all threads share the same variable and update the same variable (final value of _field would be 20 in such case)
        [ThreadStatic]
        public static int _field;

        static void Main(string[] args)
        {
            new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    _field++;
                    Console.WriteLine("Thread A: {0}", _field);
                }
            }
            ).Start();

            new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    _field++;
                    Console.WriteLine("Thread B: {0}", _field);
                }
            }
            ).Start();

            Console.ReadKey();
        }
    }
}
