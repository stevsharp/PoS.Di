using PoS.Di;
using System;

public class Startup
{
    static void Main(string[] args)
    {
        using (IContainer container = new Container())
        {
            container.AddSingelton<IMyProgram, MyProgram>();

            container.AddTransient<IHelloWorldService, HelloWorldService>();

            var myProgram = container.GetService<IMyProgram>();   

            myProgram.Run();

            var helloWorldService = container.GetService<IHelloWorldService>();

            helloWorldService.Run();
        }

        Console.ReadLine();
    }
}


public class MyProgram : IMyProgram
{
    public void Run()
    {
        Console.WriteLine("Hello From My Program");
    }
}

public class HelloWorldService : IHelloWorldService
{
    public void Run()
    {
        Console.WriteLine("Hello From Hello World Service");
    }
}