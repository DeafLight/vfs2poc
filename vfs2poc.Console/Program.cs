using System.Diagnostics;

namespace vfs2poc.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            for (var j = 0; j < 20; j++)
            {
                var x = new X();
                Stopwatch s = new Stopwatch();
                s.Start();
                for (var i = 0; i < 10000000; i++)
                {
                    DoStuff(x);
                }
                s.Stop();

                //System.Console.WriteLine(s.Elapsed.TotalMilliseconds);

                Stopwatch t = new Stopwatch();
                t.Start();
                for (var i = 0; i < 10000000; i++)
                {
                    DoStuff((dynamic)x);
                }
                t.Stop();

                //System.Console.WriteLine(t.Elapsed.TotalMilliseconds);
                System.Console.WriteLine(t.Elapsed.TotalMilliseconds / s.Elapsed.TotalMilliseconds);
            }

            System.Console.ReadLine();
        }

        static X DoStuff(X x)
        {
            return x;
        }
    }

    class X
    {
    }
}
