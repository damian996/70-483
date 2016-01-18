using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_8_Task
{
    class Program
    {
        static void Main(string[] args)
        {
            //Threads don't return any value and there is no way to know when the operation has finished.
            //Task represents some work that needs to be done.
            //Task can tell when the operation has finished and what the result was.
            //Tasks are managed by task acheduler.

            Console.WriteLine("  ");
            Console.WriteLine("1-8");
            Console.WriteLine("  ");

            Task t = Task.Run(() =>
            {
                for(int x=0; x<100; x++)
                {
                    Console.Write("*");
                }
            });

            t.Wait();

            //1-9 1-10
            //Task<T> can be used if Task should return value
            //Task has a object oriented natureand it allows addind a continuation task.
            //The continuation will be executed straight after first task has finished.

            Console.WriteLine("  ");
            Console.WriteLine("1-9 1-10");
            Console.WriteLine("  ");

            Task<int> task2 = Task.Run(() =>
            {
                return 5;
            }).ContinueWith((i) => //The continuation methods has a few overloads what means we can add a few continuation pmethods depending on what happened i.e. exception.
            {
                return i.Result * 2;
            });

            Console.WriteLine(task2.Result);


            //1-11
            //Different conntinuation methods

            Console.WriteLine("  ");
            Console.WriteLine("1-11");
            Console.WriteLine("  ");

            Task<int> task3 = Task.Run(() =>
            {
                return 5;
            });
            
            task3.ContinueWith((i) =>
            {
                Console.WriteLine("Cancelled");
            }, TaskContinuationOptions.OnlyOnCanceled);

            task3.ContinueWith((i) =>
            {
                Console.WriteLine("Faulted");
            }, TaskContinuationOptions.OnlyOnFaulted);

            task3.ContinueWith((i) =>
            {
                Console.WriteLine("Completed");
            }, TaskContinuationOptions.OnlyOnRanToCompletion);

            //1-12
            //Attaching child tasks to a parent task
            //Task can have several child tasks.
            //Parent task finishes when all child tasks are ready.

            Console.WriteLine("  ");
            Console.WriteLine("1-12");
            Console.WriteLine("  ");

            Task<Int32[]> parent = Task.Run(() =>
            {
                var results = new Int32[3];

                new Task(() => results[0] = 0, TaskCreationOptions.AttachedToParent).Start();
                new Task(() => results[1] = 1, TaskCreationOptions.AttachedToParent).Start();
                new Task(() => results[2] = 2, TaskCreationOptions.AttachedToParent).Start();

                return results;

            });

            var finalTask = parent.ContinueWith(parentTask =>
            {
                foreach (var i in parentTask.Result)
                    Console.WriteLine(i);
            });

            finalTask.Wait();

            //1-13
            //Task factory - TaskFactory is created with a certain configuration and
            //it can be used to create tasks with this configuration

            Console.WriteLine("  ");
            Console.WriteLine("1-13");
            Console.WriteLine("  ");
            
            Task<Int32[]> parent1 = Task.Run(() =>
            {
                var results = new Int32[3];

                TaskFactory tf = new TaskFactory(TaskCreationOptions.AttachedToParent, TaskContinuationOptions.ExecuteSynchronously);

                tf.StartNew(() => results[0] = 0);
                tf.StartNew(() => results[1] = 1);
                tf.StartNew(() => results[2] = 2);
                
                return results;

            });

            var finalTask1 = parent.ContinueWith(parentTask =>
            {
                foreach (var i in parentTask.Result)
                    Console.WriteLine(i);
            });

            finalTask.Wait();


            Console.ReadKey();
        }  
    }
}
