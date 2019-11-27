namespace MyUnityApp.Refit
{
    public class UserModel
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public bool completed { get; set; }

        public override string ToString()
        {
            return " Object => UserId: "+userId+" & Id: "+id+" & Title: "+title+" & Completed: "+completed;
        }
    }
}
