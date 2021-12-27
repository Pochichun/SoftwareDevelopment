using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Di
{
    class S
    {
        public Type ServiceType { get; }

        public Type ImplementationType { get; }

        public object Implementation { get; set; }

        public ServiceLifeTime LifeTime { get; }


        public S(Type ServiceType, Type ImplementationType, ServiceLifeTime LifeTime)
        {
            this.ServiceType = ServiceType;
            this.LifeTime = LifeTime;
            this.ImplementationType = ImplementationType;
        }
    }
    public enum ServiceLifeTime
    {
        Transient,
        Singleton
    }
    public class MyClassSingleton
    {
        static int count = 0;
        public MyClassSingleton()
        {
            count++;
            Console.WriteLine("Класс {0}", count);
        }
    }


    public class MyClassTransient
    {
        static int count = 0;
        public MyClassTransient()
        {
            count++;
            Console.WriteLine("Класс {0}", count);
        }
    }


    public static class Di
    {
        static List<S> s = new List<S>();
        public static void AddTransient<TS, TImplementation>()
        {
            s.Add(new S(typeof(TS), typeof(TImplementation), ServiceLifeTime.Transient));
        }
        public static void AddSingleton<TS, TImplementation>()
        {
            s.Add(new S(typeof(TS), typeof(TImplementation), ServiceLifeTime.Singleton));
        }
        static object Get(Type T)
        {
            S sGet = null;
            bool sFound = false;
            foreach (S service in s)
            {
                if (service.ServiceType.Equals(T))
                {
                    sGet = service;
                    sFound = true;
                }
            }
            if (!sFound)
                throw new Exception("Не найдено");
            if (sGet.Implementation != null)
            {
                return sGet.Implementation;
            }
            var actualType = sGet.ImplementationType;
            var constructor = actualType.GetConstructors().First();
            foreach (var a in constructor.GetParameters())
            {
                if (Cycle(T, a.ParameterType))
                    throw new Exception("Циклическая зависимость");

            }
            List<object> parameters = new List<object>();
            foreach (var x in constructor.GetParameters())
            {
                parameters.Add(Get(x.ParameterType));
            }
            var parametersArray = parameters.ToArray();
            var implementation = Activator.CreateInstance(actualType, parametersArray);
            if (sGet.LifeTime == ServiceLifeTime.Singleton)
            {
                sGet.Implementation = implementation;
            }
            return implementation;
        }
        static bool Cycle(Type sType, Type pType)
        {
            S service_ = null;
            foreach (S service in s)
            {
                if (service.ServiceType.Equals(pType))
                {
                    service_ = service;
                }
            }
            Type actualParameter = service_.ImplementationType;
            var constructorParameter = actualParameter.GetConstructors().First();
            foreach (var x in constructorParameter.GetParameters())
            {
                if (sType == x.ParameterType)
                    return true;
            }
            return false;
        }
        public static T Get<T>()
        {
            return (T)Get(typeof(T));
        }
    }
   
}
