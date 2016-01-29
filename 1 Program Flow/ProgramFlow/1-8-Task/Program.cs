using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

            finalTask.Wait(); //FINAL TASK WILL ONLY RUN ONCE THE PARENT TASK HAS FINISHED

            Console.WriteLine("  ");
            Console.WriteLine("1-14");
            Console.WriteLine("  ");

            //1-14 In this case all tasks are exeuted simultaneously 

            Task<int>[] tasks = new Task<int>[3];

            tasks[0] = Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Task 1");
                return 1;
            });

            tasks[1] = Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Task 2");
                return 2;
            });

            tasks[2] = Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Task 3");
                return 3;
            });

            Task.WaitAll(tasks); //will wait until all tasks have finished.

            //Task.WhenAll(tasks); use this to specify continuation method

            //Task.WaitAny(tasks); use this to wait until any of the tasks has finished - example below.

            Console.WriteLine("  ");
            Console.WriteLine("1-15");
            Console.WriteLine("  ");

            //1-15 In this example we process a completed Task as soon as it finished. 
            //Finished tasks are processed in the while loop and then removed from the array.
            //By keeping track of which tasks are finished we don't neet wait until all tasks have completed.

            tasks = new Task<int>[3];

            tasks[0] = Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Task 1");
                return 1;
            });

            tasks[1] = Task.Run(() =>
            {
                Thread.Sleep(3000);
                Console.WriteLine("Task 2");
                return 2;
            });

            tasks[2] = Task.Run(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("Task 3");
                return 3;
            });

            while(tasks.Length >0)
            {
                int i = Task.WaitAny(tasks);
                Task<int> completedTask = tasks[i];
                Console.WriteLine(completedTask.Result);

                var temp = tasks.ToList();
                temp.RemoveAt(i);
                tasks = temp.ToArray();
                
            }

            Console.ReadKey();
        }  
    }
}
