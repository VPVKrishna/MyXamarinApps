using MyUnityApp.UnityApp;
using NUnit.Framework;
using Moq;
using CommonServiceLocator;
using MyUnityApp;
using MyUnityApp.Refit;
using System.Threading.Tasks;
using System;

namespace MyUnityUnitTestApp
{
    [TestFixture]
    public class CarKeyTest
    {
        #region Private Variables
        private CarKey carKey;
        private RefitTest RefitTestObject;
        private Mock<ApiInterface> apiInterfaceMock;
        #endregion

        #region OneTimeSetUp
        [OneTimeSetUp]
        public void CarKeyOneTimeSetUp()
        {
            //Mock<IServiceLocator> serviceLocatorMock = new Mock<IServiceLocator>();
            //ServiceLocator.SetLocatorProvider(()=> { return serviceLocatorMock.Object; });

            Mock<ILogger> loggerMock = new Mock<ILogger>();
            apiInterfaceMock = new Mock<ApiInterface>();
                        
            RefitTestObject = new RefitTest(apiInterfaceMock.Object);
            //serviceLocatorMock.Setup(x => x.GetInstance<ILogger>()).Returns(loggerMock.Object);
            carKey = new CarKey(loggerMock.Object);
        }
        #endregion

        #region OneTimeTearDown
        [OneTimeTearDown]
        public void CarKeyOneTimeTearDown()
        {
            carKey = null;
        }
        #endregion

        #region SetUp
        [SetUp]
        public void CarKeySetUp()
        {
            carKey.IsLocked = false;
        }
        #endregion

        #region TearDown
        [TearDown]
        public void CarKeyTearDown()
        {
            //carKey.IsLocked = false;
        }
        #endregion

        #region CarKey TestCases
        [Test]
        public void CarKeyLockTest()
        {
            //Arrange & Act
            carKey.Lock();

            //Assert
            Assert.AreEqual(true, carKey.IsLocked);
        }

        [Test]
        public void CarKeyUnlockTest()
        {
            //Arrange & Act
            carKey.UnLock();

            //Assert
            Assert.AreEqual(false, carKey.IsLocked);
        }
        #endregion

        #region Refit Api TestCases

        #region Refit UserData Success Cases
        [Test, Description("This approach may leads random failures")]
        public void RefitApiUserDataSuccessTest_Approach_1()
        {
            apiInterfaceMock.Setup(x => x.GetUserData(It.IsAny<int>())).ReturnsAsync(new UserModel() { id = 111, userId = 222, title = "KrishnaV", completed = true });

            RefitTestObject.GetDataAsync().ContinueWith(task => 
            {
                var userModel = task.Result;
                Assert.IsNotNull(userModel);
            });
        }

        [Test, Description("This approach will get user data and better approach to fetch the user data but blocks the UI until async task completed")]
        public void RefitApiUserDataSuccessTest_Approach_2()
        {
            apiInterfaceMock.Setup(x => x.GetUserData(It.IsAny<int>())).ReturnsAsync(new UserModel() { id = 111, userId = 222, title = "KrishnaV", completed = true });

            Task.Run(async () =>
            {
                var userData = await RefitTestObject.GetDataAsync();
                Assert.IsNotNull(userData);
            }).GetAwaiter().GetResult();
        }
        [Test, Description("This approach may leads random failures")]
        public async Task RefitApiUserDataSuccessTest_Approach_3Async()
        {
            apiInterfaceMock.Setup(x => x.GetUserData(It.IsAny<int>())).ReturnsAsync(new UserModel() { id = 111, userId = 222, title = "KrishnaV", completed = true });

            UserModel userModel = await RefitTestObject.GetDataAsync();
            Assert.IsNotNull(userModel);
        }
        #endregion

        #region Regit UserData Failure or Cancel Cases
        [Test]
        public void RefitApiUserDataErrorTest()
        {
            apiInterfaceMock.Setup(x => x.GetUserData(It.IsAny<int>())).Returns(() =>
            {
                throw new Exception("Test Exception for Mock object");
            });

            Task.Run(() =>
            {
                var task = RefitTestObject.GetDataAsync();
                Assert.IsTrue(task.IsFaulted);
            }).GetAwaiter().GetResult();
        }

        [Test]
        public void RefitApiUserDataCancelTest()
        {
            apiInterfaceMock.Setup(x => x.GetUserData(It.IsAny<int>())).Returns(valueFunction: AsyncTestHelper.SetCancelledOnThread);

            Task.Run(() =>
            {
                var task = RefitTestObject.GetDataAsync();
                Assert.IsTrue(task.IsCanceled);
            }).GetAwaiter().GetResult();
        }
        #endregion Regit UserData Failure or Cancel Cases

        #endregion Refit Api TestCases

    }

    #region Async Helper Class
    public static class AsyncTestHelper
    {
        #region Async Helper method for Cancellation
        public static Task<UserModel> SetCancelledOnThread()
        {
            var tcs = new TaskCompletionSource<UserModel>();
            tcs.SetCanceled();
            return tcs.Task;
        }
        #endregion
    }
    #endregion
}
