using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyUnityApp.UnityApp;

namespace MyUnityUnitTestApp
{
    [TestClass]
    public class CarTest
    {
        private Car car;
        public void CarOneTimeSetup()
        {
            ILogger logger = new ConsoleLogger();
            car = new Car(logger);
        }

        [TestMethod]
        public void CarOneTimeRunTest()
        {
            //Arrange
            CarOneTimeSetup();

            //Act
            car.Run();

            //Assert
            Assert.AreEqual(1, car.Value);
        }

        [TestMethod]
        public void CarTwoTimesRunTest()
        {
            //Arrange
            CarOneTimeSetup();

            //Act
            car.Run();
            car.Run();

            //Assert
            Assert.AreEqual(2, car.Value);
        }
    }
}
