namespace WebApi_Shwetha.Services_LifetimeDemo
{
    public class SingletonService
    {
        private readonly string _guid;

        public SingletonService()
        {
            _guid = Guid.NewGuid().ToString();
        }

        public string GetGuid()
        {
            return _guid;
        }
    }
}

