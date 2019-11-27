using CommonServiceLocator;
using MyUnityApp.Refit;
using MyUnityApp.UnityApp;
using Refit;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.ServiceLocation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MyUnityApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            SetUpContainer();
            FetchingContainer();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public void SetUpContainer()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<ILogger, FileLogger>(new TransientLifetimeManager());
            container.RegisterType<ILogger, ExternalFileLogger>(new TransientLifetimeManager());
            container.RegisterType<ILogger, ConsoleLogger>(new TransientLifetimeManager());
            container.RegisterType<ICar, Car>(new TransientLifetimeManager());// It mains the existing instance across the app life cycle.
            container.RegisterType<ICarKey, CarKey>(new TransientLifetimeManager());//It gives new instance for every call of Container.Resolve
            ApiInterface apiInterface = RestService.For<ApiInterface>("https://jsonplaceholder.typicode.com");
            container.RegisterInstance(apiInterface);// for register object
            container.RegisterType<RefitTest>();// for register class/interface type

            //container.RegisterType<ILogger, FileLogger>("File");
            //container.RegisterType<ILogger, ExternalFileLogger>("ExternalFile");
            //container.RegisterType<ILogger, ConsoleLogger.>("My");
            //container.RegisterType<ICar, Car>(new InjectionConstructor(container.Resolve<ILogger>("ExternalFile")));// It mains the existing instance across the app life cycle.
            //container.RegisterType<ICarKey, CarKey>(new InjectionConstructor(container.Resolve<ILogger>("My")));//It gives new instance for every call of Container.Resolve

            UnityServiceLocator unityServiceLocator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => unityServiceLocator);
        }

        public void FetchingContainer()
        {
            IUnityContainer Container = ServiceLocator.Current.GetInstance<IUnityContainer>();

            var car = Container.Resolve<ICar>();
            car.Run();

            car = Container.Resolve<ICar>();
            car.Run();
            car.Run();

            var carKey = Container.Resolve<ICarKey>();
            carKey.Lock();
            carKey.DisplayStatus();
        }
    }
}
