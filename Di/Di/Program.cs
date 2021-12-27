using System;

namespace Di
{
    class Program
    {
        static void Main(string[] args)
        {
            Di.AddTransient<ISnow, Snow>();
            Di.AddTransient<IWinter, Winter>();
            Di.Get<ISnow>();
            Console.WriteLine("Singleton:");
            Di.AddSingleton<MyClassSingleton, MyClassSingleton>();
            Di.Get<MyClassSingleton>();
            Di.Get<MyClassSingleton>();
            Console.WriteLine("Transient:");
            Di.AddTransient<MyClassTransient, MyClassTransient>();
            Di.Get<MyClassTransient>();
            Di.Get<MyClassTransient>();
        }
    }
}