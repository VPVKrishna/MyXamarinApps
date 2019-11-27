using MyUnityApp.UnityApp;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyUnityApp.UnityApp
{
    public interface ICar
    {
        void Run();
    }

    public class Car : ICar
    {
        private readonly ILogger logger;
        public int Value { set; get; }
        public Car(ILogger logger)
        {
            this.logger = logger;
        }
        public void Run()
        {
            logger.LogMessage($"Print message : {++Value}");
        }
    }
}
