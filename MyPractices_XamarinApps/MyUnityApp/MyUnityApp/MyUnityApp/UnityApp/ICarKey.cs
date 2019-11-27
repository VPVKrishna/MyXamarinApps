namespace MyUnityApp.UnityApp
{
    public interface ICarKey
    {
        void UnLock();
        void Lock();
        void DisplayStatus();
    }

    public class CarKey : ICarKey
    {
        private readonly ILogger logger;
        public bool IsLocked { set; get; }

        public CarKey(ILogger logger)
        {
            this.logger = logger;
        }

        public void UnLock()
        {
            IsLocked = false;
        }

        public void Lock()
        {
            IsLocked = true;
        }

        public void DisplayStatus()
        {
            logger.LogMessage("Lock Status :" + IsLocked);
        }
    }
}
