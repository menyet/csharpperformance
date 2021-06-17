using System;
using System.Diagnostics;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("(1) Event handlers");
                Console.WriteLine("(q) Quit");

                var cmd = Console.ReadKey();

                switch (cmd.KeyChar)
                {
                    case '1': EvendHandlers();
                        break;
                    case 'q': return;
                }
            }
        }

        private static void EvendHandlers()
        {
            new EventManager().Fill(1000);
            Console.ReadLine();
            new EventManager().Fill(10000);
            Console.ReadLine();
            new EventManager().Fill(100000);
            Console.ReadLine();
            new EventManager().Fill(1000000);
            Console.ReadLine();
        }
    }

    public class EventManager
    {
        public event EventHandler Event;

        public void Handler(object sender, EventArgs args)
        {

        }

        public void Fill(int num)
        {
            Console.WriteLine($"{num} handlers");

            var watch = new Stopwatch();

            for (int i = 0; i < num; i++)
            {
                if (i % 1000 == 0)
                {
                    Console.WriteLine($"Events subscribed: {i}, time passed: {watch.ElapsedMilliseconds} ms");
                    watch.Restart();
                }

                Event += Handler;
            }

            for (int i = 0; i < num; i++)
            {
                if (i % 1000 == 0)
                {
                    Console.WriteLine($"Events unsubscribed: {i}, time passed: {watch.ElapsedMilliseconds} ms");
                    watch.Restart();
                }

                Event -= Handler;
            }

        }

    }
}
