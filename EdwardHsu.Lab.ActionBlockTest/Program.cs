using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace EdwardHsu.Lab.ActionBlockTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ActionBlock<int> actionBlock = null;
            actionBlock = new ActionBlock<int>(async time =>
            {
                await Task.Delay(time);
                Console.WriteLine($"OK, 剩餘工作量:{actionBlock.InputCount}");
            }, new ExecutionDataflowBlockOptions()
            {
                MaxDegreeOfParallelism = 2
            });

            actionBlock.Post(1000);
            actionBlock.Post(1000);
            actionBlock.Post(1000);
            actionBlock.Post(1000);

            actionBlock.Complete();


            Stopwatch sw = new Stopwatch();
            sw.Start();
            actionBlock.Completion.Wait();
            sw.Stop();

            Console.WriteLine("TOTLE TIME=" + sw.Elapsed.TotalSeconds);
        }
    }
}
