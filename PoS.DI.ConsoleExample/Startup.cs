using PoS.Di;
using System;

public class Startup
{
    static void Main(string[] args)
    {
        using (IContainer container = new Container())
        {
            container.AddTransient<IMyProgram, MyProgram>();
            var myProgram = container.GetService<IMyProgram>();   
            myProgram.Run();
        }   
    }
}


public class MyProgram : IMyProgram
{
    public void Run()
    {


        Console.ReadLine();
    }
}