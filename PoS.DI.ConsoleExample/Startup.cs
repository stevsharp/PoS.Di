using PoS.Di;
using System;

public class Startup
{
    static void Main(string[] args)
    {
        using (IContainer container = new Container())
        {
            container.AddTransient<IMyProgram, MyProgram>();

            container.AddSingelton<IHelloWorldService, HelloWorldService>();
            container.AddSingelton<IHelloWorldService1, HelloWorldService1>();

            var helloWorldService = container.GetService<IHelloWorldService>();

            helloWorldService.Run();

            var helloWorldService1 = container.GetService<IHelloWorldService1>();

            helloWorldService1.Run();

            var myProgram = container.GetService<IMyProgram>();

            myProgram.Run();

            //Type myProgram = typeof(MyProgram);
            //MyProgram nstance = (MyProgram)Activator.CreateInstance(myProgram, helloWorldService);
            //nstance.Run();

        }


      

        Console.ReadLine();
    }
}


public class MyProgram : IMyProgram
{
    public readonly IHelloWorldService _helloWorldService;
    public readonly IHelloWorldService1 _helloWorldService1;
    public MyProgram(IHelloWorldService helloWorldService, IHelloWorldService1 helloWorldService1)
    {
        _helloWorldService = helloWorldService;
        _helloWorldService1 = helloWorldService1;
    }
    public void Run()
    {
        Console.WriteLine("Hello From My Program");
    }
}


//public class MyProgram : IMyProgram
//{
//    public readonly IHelloWorldService _helloWorldService;
//    public MyProgram(IHelloWorldService helloWorldService)
//    {
//        _helloWorldService = helloWorldService;
//    }
//    public void Run()
//    {
//        Console.WriteLine("Hello From My Program");

//        _helloWorldService.Run();
//    }
//}


public class HelloWorldService : IHelloWorldService
{
    public void Run()
    {
        Console.WriteLine($"Hello From Hello World Service {typeof(HelloWorldService)}");
    }
}

public class HelloWorldService1 : IHelloWorldService1
{
    public void Run()
    {
        Console.WriteLine($"Hello From Hello World Service {typeof(HelloWorldService1)}");
    }
}